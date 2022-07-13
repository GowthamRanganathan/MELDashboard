using BusinessLayer.Interfaces.PowerBI;
using BusinessLayer.Models.PowerBiEmbed;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Linq;
using System.Security;

namespace BusinessLayer.Implementations.PowerBI
{
    public class AzureAdService : IAzureAdService
    {
        private readonly IOptions<AzurePowerBi> _azureAdConfig;

        public AzureAdService(IOptions<AzurePowerBi> azureAdConfig)
        {
            _azureAdConfig = azureAdConfig;
        }

        public string GetAccessToken()
        {
            AuthenticationResult authenticationResult;

            IPublicClientApplication clientApp = PublicClientApplicationBuilder.Create(_azureAdConfig.Value.ClientId).WithAuthority(_azureAdConfig.Value.AuthorityUri).Build();

            SecureString password = new SecureString();

            foreach (var key in _azureAdConfig.Value.PbiPassword)
            {
                password.AppendChar(key);
            }

            authenticationResult = clientApp.AcquireTokenByUsernamePassword(_azureAdConfig.Value.Scope, _azureAdConfig.Value.PbiUsername, password).ExecuteAsync().Result;

            return authenticationResult.AccessToken;
        }
    }
}
