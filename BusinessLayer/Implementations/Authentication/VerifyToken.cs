using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BusinessLayer.Implementations.Authentication
{
    public class VerifyToken : IVerifyToken
    {
        private readonly ICreateRefreshToken _createRefreshToken;
        public VerifyToken(ICreateRefreshToken createRefreshToken)
        {
            _createRefreshToken = createRefreshToken;
        }

       public void Verify (AuthorizationFilterContext context)
       {
            string accessToken = context.HttpContext.Session.GetString("AccessToken");
            string refreshToken = context.HttpContext.Session.GetString("RefreshToken");
            TokenModel newToken = new TokenModel();
            var services = context.HttpContext.RequestServices;
            if (!string.IsNullOrEmpty(accessToken))
            {
                var jwthandler = new JwtSecurityTokenHandler();
                var jwttoken = jwthandler.ReadToken(accessToken);
                var expDate = jwttoken.ValidTo;
                if (expDate < DateTime.UtcNow.AddMinutes(1))
                {
                    TokenModel tokenModel = new TokenModel();
                    tokenModel.AccessToken = accessToken;
                    tokenModel.RefreshToken = refreshToken;
                    TokenApiModel userDetails = new TokenApiModel
                    {
                        AccessToken = tokenModel.AccessToken,
                        RefreshToken = tokenModel.RefreshToken,
                    };
                    newToken =  _createRefreshToken.Refresh(userDetails);
                    if (newToken != null && !string.IsNullOrEmpty(newToken.AccessToken) && !string.IsNullOrEmpty(newToken.RefreshToken))
                    {
                        context.HttpContext.Session.SetString("AccessToken", newToken.AccessToken);
                        context.HttpContext.Session.SetString("RefreshToken", newToken.RefreshToken);
                    }
                    else
                    {
                        context.HttpContext.Session.Clear();
                        //context.Result = new RedirectResult("~/Login/LogUser");
                    }
                }
            }
        }


    }
}
