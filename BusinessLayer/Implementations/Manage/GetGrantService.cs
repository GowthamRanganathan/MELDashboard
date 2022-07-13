using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Manage;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Implementations.Manage
{
    public class GetGrantService : IGetGrantService
    {
        private readonly IGetGrantDetailsRepository _grantRepository;

        public GetGrantService(IGetGrantDetailsRepository grantRepository)
        {
            _grantRepository = grantRepository;
        }

        public List<GrantDetails> GetGrants()
        {
            List<GrantDetails> obj = _grantRepository.GetGrantsDetails();
            return obj;
        }
    }
}
