using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Unicorn.Core.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Linq;

namespace Unicorn.Core.Services
{
    public class AuthJWTService : IAuthService
    {
        private readonly IMembershipProvider membershipProvider;

        public AuthJWTService(IMembershipProvider membershipProvider)
        {
            this.membershipProvider = membershipProvider;
        }

        private static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Properties.Settings.Default.PrivateKey));
        }       

        public async Task<string> GenerateJwtTokenAsync(string provider, string uid)
        {
            long accountId = uid == Properties.Settings.Default.PrivateKey ? Properties.Settings.Default.AdminAccountId : await membershipProvider.VerifyUser(provider, uid);

            if (accountId == 0) // User not exists in DB
            {
                return null;
            }

            return await GenerateToken(accountId);
        }

        public async Task<string> GenerateToken(long accountId)
        {
            ClaimsIdentity identity = await membershipProvider.GetUserClaims(accountId); // Role, email etc.            

            var dateTimeNow = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: Properties.Settings.Default.Issuer,
                    audience: Properties.Settings.Default.Audience,
                    notBefore: dateTimeNow,
                    claims: identity.Claims,
                    expires: new DateTime(2017, 09, 29),//dateTimeNow.Add(TimeSpan.FromMinutes(Properties.Settings.Default.TokenLifeTime)), // + 5 minutes by default claim time
                    signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public Task<bool> ValidateTokenAsync(string tokenString)
        {
            bool result = false;

            try
            {
                SecurityToken securityToken = new JwtSecurityToken(tokenString);
                JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Properties.Settings.Default.Issuer,
                    ValidAudience = Properties.Settings.Default.Audience,
                    IssuerSigningKey = GetSymmetricSecurityKey(),
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };

                securityTokenHandler.ValidateToken(tokenString, validationParameters, out securityToken);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return Task.FromResult(result);
        }

        public IEnumerable<Claim> GetClaims(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            return new JwtSecurityToken(token).Claims;
        }

        public Claim GetClaimValue(string token, string key)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            return new JwtSecurityToken(token).Claims.FirstOrDefault(x => x.Type == key);
        }
    }
}
