using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Unicorn.Core.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

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

        public async Task<string> GenerateJwtToken(string provider, string uid)
        {
            if (!await membershipProvider.VerifyUser(provider, uid))
                return "Wrong access"; // User not exists in DB

            ClaimsIdentity identity = await membershipProvider.GetUserClaims(provider, uid); // Role, email etc.            

            var dateTimeNow = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: Properties.Settings.Default.Issuer,
                    audience: Properties.Settings.Default.Audience,
                    notBefore: dateTimeNow,
                    claims: identity.Claims,
                    expires: dateTimeNow.Add(TimeSpan.FromMinutes(Properties.Settings.Default.TokenLifeTime)), // + 5 minutes by default claim time
                    signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }        

        public bool ValidateToken(string tokenString)
        {
            bool result = false;
            if (string.IsNullOrWhiteSpace(tokenString))
            {
                return result;
            }

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

                ClaimsPrincipal claimsPrincipal = securityTokenHandler.ValidateToken(tokenString, validationParameters, out securityToken);

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
