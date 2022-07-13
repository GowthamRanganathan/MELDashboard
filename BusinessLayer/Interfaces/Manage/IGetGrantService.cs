using RepositoryLayer.Models.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.Manage
{
    public interface IGetGrantService
    {
        public List<GrantDetails> GetGrants();
    }
}
