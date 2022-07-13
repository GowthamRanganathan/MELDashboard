using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class BMGFDashboardController : Controller
    {
        private ILogger<BMGFDashboardController> _logger;
        string userName;
        string organization;
        public IActionResult Index (ILogger<BMGFDashboardController> logger)
        {
            return View();

        }

        public IActionResult Dashboard (string userName, string organization)
        {
            try
            {
                TempData["userName"] = userName;
                TempData["organization"] = organization;
                ViewBag.userName = userName;
                ViewBag.organization = organization;

                //_logger.LogInformation("Dashboard", "SattvaDashboard");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "BMGFDashboard");
            }
            return View();
        }

        public IActionResult StratagicGoals ()
        {
            if (TempData.ContainsKey("userName") && TempData.ContainsKey("organization"))
                ViewBag.userName = TempData["userName"] as string;
            ViewBag.organization = TempData["organization"] as string;
            TempData.Keep("userName");
            TempData.Keep("organization");
            return View();
        }

        public IActionResult BodyofWork ()
        {
            if (TempData.ContainsKey("userName") && TempData.ContainsKey("organization"))
                ViewBag.userName = TempData["userName"] as string;
            ViewBag.organization = TempData["organization"] as string;
            TempData.Keep("userName");
            TempData.Keep("organization");
            return View();
        }
        public IActionResult Geographices ()
        {
            if (TempData.ContainsKey("userName") && TempData.ContainsKey("organization"))
                ViewBag.userName = TempData["userName"] as string;
            ViewBag.organization = TempData["organization"] as string;
            TempData.Keep("userName");
            TempData.Keep("organization");
            return View();
        }
        public IActionResult Grants ()
        {
            if (TempData.ContainsKey("userName") && TempData.ContainsKey("organization"))
                ViewBag.userName = TempData["userName"] as string;
            ViewBag.organization = TempData["organization"] as string;
            TempData.Keep("userName");
            TempData.Keep("organization");
            return View();
        }
        public IActionResult KeyInfoandDataSources ()
        {
            if (TempData.ContainsKey("userName") && TempData.ContainsKey("organization"))
                ViewBag.userName = TempData["userName"] as string;
            ViewBag.organization = TempData["organization"] as string;
            TempData.Keep("userName");
            TempData.Keep("organization");
            return View();
        }
    }
}
