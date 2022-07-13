using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Models.Login;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations.Authentication
{
    public class CreateClaims : ICreateClaims
    {
        private readonly IOptions<Jwt> _jwtConfig;

        public CreateClaims (IOptions<Jwt> jwtConfig)
        {
            this._jwtConfig = jwtConfig;
        }
        public List<Claim> claims (UserCredentials userCredentials)
        {
            var claims = new List<Claim>();
            string userName = userCredentials.firstname + " " + userCredentials.lastname;
            claims.Add(new Claim(ClaimTypes.Name, userName));
            claims.Add(new Claim("email_id", userCredentials.email_Id));
            claims.Add(new Claim("location", userCredentials.location));
            claims.Add(new Claim(ClaimTypes.Role, userCredentials.Organization));
            return claims;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken (string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtConfig.Value.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
