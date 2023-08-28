using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartOffice.eManagement.IResponsitory;
using SmartOffice.IResponsitory;

namespace SmartOffice.Controllers
{
    public class QueryController : Controller
    {
        private readonly IInqueryDataService _IIqueryDataService;
        public QueryController(IInqueryDataService dash)
        {
            _IIqueryDataService = dash;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public string GetUserID()
        {
            string Username = "";
            if (User.Identity.IsAuthenticated == true)
            {
                Username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value.ToString().Trim();
            }

            return Username;
        }

        [HttpPost]
        public JsonResult GetInquiryData(string DocCode,string StartDate, string EndDate)
        {
            var aa = _IIqueryDataService.GetDataInquery(DocCode, StartDate, EndDate, GetUserID());        
            return Json(JsonConvert.SerializeObject(aa, Formatting.Indented));

        }
        [HttpPost]
        public JsonResult GetInquiryData2(string DocCode, string StartDate, string EndDate)
        {
            var aa = _IIqueryDataService.GetDataInquery2(DocCode, StartDate, EndDate, GetUserID());
            return Json(JsonConvert.SerializeObject(aa, Formatting.Indented));

        }
        [HttpPost]
        public JsonResult GetDocumentData()
        {
            var aa = _IIqueryDataService.GetDocmentData();        
            return Json(JsonConvert.SerializeObject(aa, Formatting.Indented));

        }

        [HttpPost]
        public JsonResult GetInputItem(string DocCode)
        {
            var aa = _IIqueryDataService.GetInputItem(DocCode);         
            return Json(JsonConvert.SerializeObject(aa, Formatting.Indented));

        }
    }
}