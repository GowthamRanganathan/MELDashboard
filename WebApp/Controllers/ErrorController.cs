using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController (ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [Route("Error")]
        public IActionResult Error ()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;
            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler (int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    ViewBag.ErrorMessage = "Bad Request.The request was invalid.";
                    break;
                case 401:
                    return RedirectToAction("LogIn", "LogIn");
                case 403:
                    ViewBag.ErrorMessage = "Forbidden.The client did not have permission to access the requested resource.";
                    break;
                case 404:
                    ViewBag.ErrorMessage = "Sorry the resource you requested could not be found";
                    break;
                case 405:
                    ViewBag.ErrorMessage = "Method Not Allowed.The HTTP method in the request was not supported by the resource";
                    break;
                case 409:
                    ViewBag.ErrorMessage = "Conflict.The request could not be completed due to a conflict";
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Internal Server Error.The request was not completed due to an internal error on the server side.";
                    break;
                case 503:
                    ViewBag.ErrorMessage = "Service Unavailable.The server was unavailable.";
                    break;
                default:
                    return RedirectToAction("LogIn", "LogIn");
            }
            return View();
        }

        public IActionResult Index ()
        {
            return View();
        }

        public IActionResult Privacy ()
        {
            return View();
        }
    }
}
