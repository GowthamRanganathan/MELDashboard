using RepositoryLayer.Models.Login;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IGetUserLoginDetailsRepository
    {
        Task<UserCredentials> GetLoginDetails (LoginDetails userDetails);
    }
}
