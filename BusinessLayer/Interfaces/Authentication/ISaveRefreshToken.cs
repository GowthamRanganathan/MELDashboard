using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.Authentication
{
    public interface ISaveRefreshToken
    {
        int SaveRefresh (string emailid, string RefreshToken, DateTime RefreshTokenExpiryTime);
    }
}
