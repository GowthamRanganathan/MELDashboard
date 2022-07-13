using RepositoryLayer.Models.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.Manage
{
    public interface ILoadDownloadDataService
    {
        public List<DownloadDetails> LoadDownloadData(string grantName);
    }
}
