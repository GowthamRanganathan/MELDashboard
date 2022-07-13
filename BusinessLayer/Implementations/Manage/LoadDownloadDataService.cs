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
    public class LoadDownloadDataService : ILoadDownloadDataService
    {
        private readonly ILoadDownloadDetailRespository _grantRepository;

        public LoadDownloadDataService(ILoadDownloadDetailRespository grantRepository)
        {
            _grantRepository = grantRepository;
        }

        public List<DownloadDetails> LoadDownloadData(string grantName)
        {
            List<DownloadDetails> obj = _grantRepository.LoadDownloadData(grantName);
            return obj;
        }
    }
}
