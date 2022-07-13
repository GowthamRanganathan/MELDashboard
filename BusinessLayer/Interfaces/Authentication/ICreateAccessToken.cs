using RepositoryLayer.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.Authentication
{
    public interface ICreateAccessToken
    {
        string GenerateAccessToken (IEnumerable<Claim> claims);
    }
}
