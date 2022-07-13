using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [CustomAuthorizeFilter]
    public class DashboardController : Controller
    {

        private ILogger<DashboardController> _logger;
        public DashboardController (ILogger<DashboardController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index ()
        {
            return View();
        }

        public IActionResult SattvaDashboard (string userName, string organization)
        {
            try
            {
                ViewBag.userName = userName;
                ViewBag.organization = organization;
                _logger.LogInformation("Dashboard", "SattvaDashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "SattvaDashboard");
            }
            return View();
        }

        public IActionResult BMGFDashboard (string userName, string organization)
        {
            try
            {
                ViewBag.userName = userName;
                ViewBag.organization = organization;
                _logger.LogInformation("Dashboard", "SattvaDashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "BMGFDashboard");
            }
            return View();
        }
    }
}
