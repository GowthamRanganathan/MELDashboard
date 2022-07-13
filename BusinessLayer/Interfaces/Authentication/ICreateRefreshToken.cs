using BusinessLayer.Models.Auth;
using System;

namespace BusinessLayer.Interfaces.Authentication
{
    public interface ICreateRefreshToken
    {
        //int SaveRefreshToken (string emailid, string RefreshToken, DateTime RefreshTokenExpiryTime);
        string GenerateRefreshToken ();
        TokenModel Refresh (TokenApiModel tokenApiModel);
    }
}
