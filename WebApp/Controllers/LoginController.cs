using BusinessLayer.Interfaces.Login;
using BusinessLayer.Models.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Models.Login;
using System;
using System.Threading.Tasks;
using Utilities;
using WebApp.Filters;

namespace MEL_Dashboard.Controllers
{
    //[ServiceFilter(typeof(LogFilter))]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private ILogger<LoginController> _logger;
        public LoginController (ILoginService loginService, IHttpContextAccessor httpContext, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _logger = logger;
        }

        public IActionResult Login ()
        {

            _logger.LogInformation("Login" + Environment.NewLine + DateTime.Now);

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login (UserLogin userLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserCredentials userCredentials = await _loginService.Login(userLogin);

                    if (userCredentials.Organization == WebAppConstants.SattvaOrganization)
                    {
                        HttpContext.Session.SetString("UserName", userCredentials.firstname);
                        HttpContext.Session.SetString("UserOrganisation", userCredentials.Organization);
                        return RedirectToRoute(new { controller = WebAppConstants.DashboardController, action = WebAppConstants.SattvaActionResult, userName = userCredentials.firstname, organization = userCredentials.Organization });
                    }
                    else if (userCredentials.Organization == WebAppConstants.BMGFOrganization)
                    {
                        HttpContext.Session.SetString("UserName", userCredentials.firstname);
                        HttpContext.Session.SetString("UserOrganisation", userCredentials.Organization);
                        return RedirectToRoute(new { controller = WebAppConstants.BMGFDashboardController, action = WebAppConstants.BMGFActionResult, userName = userCredentials.firstname, organization = userCredentials.Organization });
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Invalid Credentials");
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "Login");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword (UserLogin userLogin)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout ()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "LogIn");
        }
    }
}
