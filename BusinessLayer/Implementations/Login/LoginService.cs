using BusinessLayer.Interfaces.Login;
using BusinessLayer.Models.Login;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models.Login;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations.Login
{
    public class LoginService : ILoginService
    {
        private readonly IGetUserLoginDetailsRepository _loginRepository;
        private readonly IValidateLoginAndCreateToken _validateLoginAndCreateToken;

        public LoginService (IGetUserLoginDetailsRepository loginRepository, IValidateLoginAndCreateToken validateLoginAndCreateToken)
        {
            _loginRepository = loginRepository;
            _validateLoginAndCreateToken = validateLoginAndCreateToken;
        }

        public async Task<UserCredentials> Login (UserLogin loginModel)
        {
            LoginDetails userDetails = new LoginDetails
            {
                UserName = loginModel.UserName,
                UserPassword = loginModel.UserPassword,
            };
            UserCredentials userCredentials = await _loginRepository.GetLoginDetails(userDetails);
            _validateLoginAndCreateToken.ValidateAndAuthorize(userCredentials);
            return userCredentials;
        }
    }
}
