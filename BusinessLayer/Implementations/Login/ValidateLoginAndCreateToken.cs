using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.Login;
using BusinessLayer.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RepositoryLayer.Models.Login;
using System;

namespace BusinessLayer.Implementations.Login
{
    public class ValidateLoginAndCreateToken : IValidateLoginAndCreateToken
    {
        private readonly ICreateAccessToken _createAccessToken;
        private readonly ICreateClaims _createClaims;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ICreateRefreshToken _createRefreshToken;
        private readonly IOptions<Jwt> _jwtOptions;
        private readonly ISaveRefreshToken _saveRefreshToken;
        public ValidateLoginAndCreateToken (ICreateAccessToken createAccessToken, ICreateClaims createClaims, IHttpContextAccessor httpContext, ICreateRefreshToken createRefreshToken, IOptions<Jwt> jwtOptions, ISaveRefreshToken saveRefreshToken)
        {
            _createAccessToken = createAccessToken;
            _createClaims = createClaims;
            _httpContext = httpContext;
            _createRefreshToken = createRefreshToken;
            _jwtOptions = jwtOptions;
            _saveRefreshToken = saveRefreshToken;
        }

        public void ValidateAndAuthorize (UserCredentials userCredentials)
        {
            if (userCredentials.firstname != null && userCredentials.Organization != null)
            {
                var claims = _createClaims.claims(userCredentials);
                var accessToken = _createAccessToken.GenerateAccessToken(claims);
                var refreshToken = _createRefreshToken.GenerateRefreshToken();
                _saveRefreshToken.SaveRefresh(userCredentials.email_Id, refreshToken, DateTime.Now.AddHours(_jwtOptions.Value.refershTimeOut));
                _httpContext.HttpContext.Session.SetString("AccessToken", accessToken);
                _httpContext.HttpContext.Session.SetString("RefreshToken", refreshToken);
            }
        }
    }
}
