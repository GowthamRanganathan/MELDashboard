using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Models.Auth;
using Microsoft.Extensions.Options;
using RepositoryLayer.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations.Authentication
{
    public class CreateRefreshToken : ICreateRefreshToken
    {
        private readonly IOptions<Jwt> _jwtConfig;
        private readonly ICreateClaims _createClaims;
        private readonly ICreateAccessToken _createAccessToken;
        private readonly IOptions<Jwt> _jwtOptions;
        private readonly ISaveRefreshToken _saveRefreshToken;
        public CreateRefreshToken (IOptions<Jwt> jwtConfig, ICreateClaims createClaims, ICreateAccessToken createAccessToken, IOptions<Jwt> jwtOptions, ISaveRefreshToken saveRefreshToken)
        {
            _jwtConfig = jwtConfig;
            _createClaims = createClaims;
            _createAccessToken = createAccessToken;
            _jwtOptions = jwtOptions;
            _saveRefreshToken = saveRefreshToken;
        }
        //create random number
        public string GenerateRefreshToken ()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public TokenModel Refresh (TokenApiModel tokenApiModel)
        {
            TokenModel tokenModel = new TokenModel();
            if (tokenApiModel.AccessToken == null && tokenApiModel.RefreshToken ==null)
            {
                return null;
            }
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var principal = _createClaims.GetPrincipalFromExpiredToken(accessToken);
            var email_id = principal.Claims.Where(x => x.Type == "email_id").Select(x => x.Value).FirstOrDefault();
            var newAccessToken = _createAccessToken.GenerateAccessToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();
            _saveRefreshToken.SaveRefresh(email_id, newRefreshToken, DateTime.Now.AddHours(_jwtOptions.Value.refershTimeOut));
            tokenModel.AccessToken = newAccessToken;
            tokenModel.RefreshToken = newRefreshToken;
            return tokenModel;
        }
    }
}
