using BusinessLayer.Models.Login;
using RepositoryLayer.Models.Login;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.Login
{
    public interface ILoginService
    {
        Task<UserCredentials> Login (UserLogin loginModel);
    }
}
