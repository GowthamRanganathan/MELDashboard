using RepositoryLayer.Models.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IGetGrantDetailsRepository
    {
        List<GrantDetails> GetGrantsDetails();
    }
}
