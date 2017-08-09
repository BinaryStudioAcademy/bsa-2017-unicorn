using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Unicorn.Providers;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Services
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // Our server
        public const string AUDIENCE = "http://localhost:4200"; // Angular client
        const string KEY = "mysupersecret_secretkey!123";   // Secret Key
        public const int LIFETIME = 10; // Token lifetime
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }

    // TODO: Make IAuthService interface
    public class AuthService
    {
        private readonly MembershipProvider _membershipProvider;

        public AuthService(MembershipProvider membershipProvider)
        {
            _membershipProvider = membershipProvider;
        }

        public string GenerateJwtToken(string provider, string uid)
        {
            if (!_membershipProvider.VerifyUser(provider, uid))
                return "Wrong access"; // User not exists in DB

            ClaimsIdentity identity = _membershipProvider.GetUserClaims(provider, uid); // Role, email etc.            

            var dateTimeNow = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: dateTimeNow,
                    claims: identity.Claims,
                    expires: dateTimeNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public bool ValidateToken(string TokenString)
        {
            bool result = false;
            if (string.IsNullOrWhiteSpace(TokenString))
            {
                return result;
            }

            try
            {
                SecurityToken securityToken = new JwtSecurityToken(TokenString);
                JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
                
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {                    
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidAudience = AuthOptions.AUDIENCE,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };

                ClaimsPrincipal claimsPrincipal = securityTokenHandler.ValidateToken(TokenString, validationParameters, out securityToken);                

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