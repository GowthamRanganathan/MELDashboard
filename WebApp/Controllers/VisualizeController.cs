using BusinessLayer.Interfaces.PowerBI;
using BusinessLayer.Models.PowerBiEmbed;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace MEL_Dashboard.Controllers
{
    public class VisualizeController : Controller
    {
        private readonly IOptions<AzurePowerBi> _azureAdConfig;
        private readonly IEmbedDashboard _embedDashboard;

        public VisualizeController(IOptions<AzurePowerBi> azureAdConfig, IEmbedDashboard embedDashboard)
        {
            _azureAdConfig = azureAdConfig;
            _embedDashboard = embedDashboard;
        }

        public IActionResult Index()
        {
            List<DashboardEmbedInfo> dashboardEmbedInfo = _embedDashboard.GetEmbedInfo(new Guid(_azureAdConfig.Value.WorkspaceId));
            return View(dashboardEmbedInfo);
        }
    }
}
