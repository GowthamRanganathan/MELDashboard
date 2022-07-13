using RepositoryLayer.Models.Login;

namespace BusinessLayer.Interfaces.Login
{
    public interface IValidateLoginAndCreateToken
    {
        void ValidateAndAuthorize (UserCredentials userCredentials);
    }
}
