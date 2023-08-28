using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartOffice.Controllers
{
    public class FlowController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AssignFlow()
        {
            return View();
        }
        public IActionResult PopupFlow()
        {
            return PartialView("PopupFlow");
        }
    }
}