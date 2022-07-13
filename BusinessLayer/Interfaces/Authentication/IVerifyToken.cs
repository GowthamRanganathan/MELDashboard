using Microsoft.AspNetCore.Mvc.Filters;

namespace BusinessLayer.Interfaces.Authentication
{
    public interface IVerifyToken
    {
        void Verify (AuthorizationFilterContext context);
    }
}
