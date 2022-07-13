using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEL_Dashboard.Controllers
{
    public class ConfigureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
