using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Models.Login;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations.Authentication
{
    public class CreateAccessToken: ICreateAccessToken
    {
        private readonly IOptions<Jwt> _jwtConfig;

        public CreateAccessToken (IOptions<Jwt> jwtConfig)
        {
            this._jwtConfig = jwtConfig;
        }
        public string GenerateAccessToken (IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtConfig.Value.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(this._jwtConfig.Value.accessTimeOut),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
