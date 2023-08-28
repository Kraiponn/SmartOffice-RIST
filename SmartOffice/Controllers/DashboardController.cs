using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartOffice.eManagement.IResponsitory;

namespace SmartOffice.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboard _IDashboard;
        public DashboardController(IDashboard dash)
        {
            _IDashboard = dash;
      
        }
        public IActionResult Index(string DashID)
        {
            ViewData["DashID"] = DashID;
            return View();
        }


        public IActionResult DashboardConfig()
        {
            return View();
        }

   
        public JsonResult GetDashboardData(string DashboardId, string StartDate, string EndDate)
        {
            var aa = _IDashboard.GetDashboard(DashboardId, StartDate, EndDate);
            return Json(JsonConvert.SerializeObject(aa, Formatting.Indented));
        }

      
    }
}