using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartOffice.ModelsHRMSLocal;
using SmartOffice.SurveyApp.IResponsitory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyApp _ISurveyApp;
        private readonly HRMSLocalContext _HRdbcontext;
        private IHttpContextAccessor _accessor;
        public SurveyController(ISurveyApp surveyapp, HRMSLocalContext HRdbcontext,IHttpContextAccessor accessor)
        {
            _ISurveyApp = surveyapp;
            _HRdbcontext = HRdbcontext;
            _accessor = accessor;
        }


        public async Task<IActionResult> InputForm(string ItemCateNo, string OperationCode, string ZTCNo, string KeyDate)
        {
            var Username = User.Identity.Name;
            if (string.IsNullOrEmpty(OperationCode))
            {
                var ItemForm = await _ISurveyApp.GetQuestions(ItemCateNo, Username);
                return View(ItemForm);
            }
            else
            {
                var ItemForm = await _ISurveyApp.GetQuestionsUpdate(ItemCateNo, OperationCode, ZTCNo, KeyDate);
                return View("InputForm", ItemForm);
            }
           
        }

       [Authorize]
        public async Task<IActionResult> Index()
        {
            var Username = User.Identity.Name;
            var result = await _ISurveyApp.GetOperatorDataAsync(Username);
            return View(result);
        }



        [Authorize]
        public IActionResult DailyCheckReport()
        {
            var listitems = _HRdbcontext.HrmsEmployee.Where(i => i.Inactive == null).Select(i => new SelectListItem
            {
                Value = i.Department,
                Text = i.Department
            }).Distinct().ToList();
            var listitemss = new SelectListItem()
            {
                Value = "",
                Text = "--- select division ---"
            };
            listitems.Insert(0, listitemss);



            List<SelectListItem> listitems2 = new List<SelectListItem>();
            List<SelectListItem> listitems3 = new List<SelectListItem>();




            ViewData["listitems"] = listitems;
            ViewData["listitems2"] = listitems2;
            ViewData["listitems3"] = listitems3;



            var Username = User.Identity.Name;
            var result = _ISurveyApp.ReportDailyCheck("", "", "", Username);
            return View(result);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DailyCheckReport(string DepName,string DepName2,string DepName3,string Employeeid)
        {
            var listitems = _HRdbcontext.HrmsEmployee.Where(i => i.Inactive == null).Select(i => new SelectListItem
            {
                Value = i.Department,
                Text = i.Department
            }).Distinct().ToList();
            var listitemss = new SelectListItem()
            {
                Value = "",
                Text = "--- select division ---"
            };
            listitems.Insert(0, listitemss);

            var listitems2 = _HRdbcontext.HrmsEmployee.Where(i => i.Inactive == null && i.Department == DepName).Select(i => new SelectListItem
            {
                Value = i.Department2,
                Text = i.Department2
            }).Distinct().ToList();
            var listitems2s = new SelectListItem()
            {
                Value = "",
                Text = "--- select department ---"
            };
            listitems2.Insert(0, listitems2s);

            var listitems3 = _HRdbcontext.HrmsEmployee.Where(i => i.Inactive == null && i.Department2 == DepName2).Select(i => new SelectListItem
            {
                Value = i.Department3,
                Text = i.Department3
            }).Distinct().ToList();
            var listitems3s = new SelectListItem()
            {
                Value = "",
                Text = "--- select section ---"
            };
            listitems3.Insert(0, listitems3s);

            ViewData["listitems"] = listitems;
            ViewData["listitems2"] = listitems2;
            ViewData["listitems3"] = listitems3;
            listitems = null;
            listitems2 = null;
            listitems3 = null;
            if (DepName==null)
                DepName = "";

            if (DepName2 == null)
                DepName2 = "";

            if (DepName3 == null)
                DepName3 = "";

            if (Employeeid == null)
                Employeeid = "";

            var Username = User.Identity.Name;
            var result = _ISurveyApp.ReportDailyCheck(DepName, DepName2, DepName3, Employeeid);
            return View(result);
        }


        //public List<dynamic> ToDynamic(this DataTable dt)
        //{
        //    var dynamicDt = new List<dynamic>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        dynamic dyn = new ExpandoObject();
        //        dynamicDt.Add(dyn);
        //        foreach (DataColumn column in dt.Columns)
        //        {
        //            var dic = (IDictionary<string, object>)dyn;
        //            dic[column.ColumnName] = row[column];
        //        }
        //    }
        //    return dynamicDt;
        //}

        [HttpPost]
        public async Task<IActionResult> InputForm(IFormCollection form)
        {
            var Username = User.Identity.Name;
          
            var CreateName = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value;
            string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            var result = await _ISurveyApp.SaveFormData(form, Username, CreateName, ComputerName);

            if (result == true)
            {
                return View("CompleteInput");
            }
            else
            {
                return View();
            }

        }

        public IActionResult CompleteInput()
        {
            return View();
        }
        public async Task<JsonResult> GetHistoryInputFormAsync()
        {
            var Username = User.Identity.Name;
            var FormHis = await _ISurveyApp.GetHistoryInputFormAsync(Username);
            return Json(new { FormHis });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteFormData(string ItemCateNo, string OperationCode, string ZTCNo, string KeyDate)
        {
            var Username = User.Identity.Name;
            return Json(await _ISurveyApp.DeleteFormData(ItemCateNo, OperationCode, ZTCNo, KeyDate));
        }
        [HttpGet]
        public ActionResult GetDepartments(string Division)
        {
            if (!string.IsNullOrWhiteSpace(Division))
            {

                IEnumerable<SelectListItem> department = _HRdbcontext.HrmsEmployee.Where(i => i.Inactive == null && i.Department == Division).Select(i => new SelectListItem
                {
                    Value = i.Department2,
                    Text = i.Department2
                }).Distinct().ToList();
                var listitems2s = new SelectListItem()
                {
                    Value = "",
                    Text = "--- select department ---"
                };
           
                return Json(department);
            }
            return null;
        }

        [HttpGet]
        public ActionResult GetSections(string Department)
        {
            if (!string.IsNullOrWhiteSpace(Department))
            {
                IEnumerable<SelectListItem> department = _HRdbcontext.HrmsEmployee.Where(i => i.Inactive == null && i.Department2 == Department).Select(i => new SelectListItem
                {
                    Value = i.Department3,
                    Text = i.Department3
                }).Distinct().ToList();
                
                return Json(department);
            }
            return null;
        }
    }
}