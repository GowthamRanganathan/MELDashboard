using BusinessLayer.Interfaces.PowerBI;
using BusinessLayer.Models.PowerBiEmbed;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Implementations.PowerBI
{
    public class EmbedDashboard : IEmbedDashboard
    {
        private readonly IGetClient _getClient;

        public EmbedDashboard(IGetClient getClient)
        {
            _getClient = getClient;
        }

        public List<DashboardEmbedInfo> GetEmbedInfo(Guid workspaceId)
        {
            List<DashboardEmbedInfo> dashboardEmbedInfos = new();

            PowerBIClient pbiClient = _getClient.Get();

            var dashboards = pbiClient.Dashboards.GetDashboardsInGroupAsync(workspaceId).Result;

            //get first dashboard
            var dashboard = dashboards.Value.FirstOrDefault();

            if (dashboard == null)
            {
                throw new NullReferenceException("Workspace has no dashboards");
            }

            var generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "view");

            var tokenResponse = pbiClient.Dashboards.GenerateTokenInGroupAsync(workspaceId, dashboard.Id, generateTokenRequestParameters);

            if (tokenResponse == null)
            {
                throw new NullReferenceException("Failed to generate embed token");
            }

            // Generate Embed Configuration.
            DashboardEmbedInfo dashboardEmbedInfo = new DashboardEmbedInfo
            {
                EmbedToken = tokenResponse.Result,
                EmbedUrl = dashboard.EmbedUrl,
                DashboardId = dashboard.Id
            };

            dashboardEmbedInfos.Add(dashboardEmbedInfo);

            return dashboardEmbedInfos;
        }

    }
}
