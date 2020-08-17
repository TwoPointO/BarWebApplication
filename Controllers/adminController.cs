using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BarWebApplication.Controllers
{
    public class adminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}