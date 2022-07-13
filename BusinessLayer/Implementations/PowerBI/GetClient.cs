using BusinessLayer.Interfaces.PowerBI;
using Microsoft.PowerBI.Api;
using Microsoft.Rest;
using System;
using Utilities;

namespace BusinessLayer.Implementations.PowerBI
{
    public class GetClient : IGetClient
    {
        private readonly IAzureAdService _azureAdService;

        public GetClient(IAzureAdService azureAdService)
        {
            _azureAdService = azureAdService;
        }

        public PowerBIClient Get()
        {
            TokenCredentials tokenCredentials = new(_azureAdService.GetAccessToken(), BusinessConstants.bearer);

            return new PowerBIClient(new Uri(BusinessConstants.urlPowerBiServiceApiRoot), tokenCredentials);
        }
    }
}
