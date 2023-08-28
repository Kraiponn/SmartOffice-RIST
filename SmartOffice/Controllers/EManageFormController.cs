using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartOffice.Class;
using SmartOffice.eManagement.Models;
using SmartOffice.eManagement.ModelsManagementControl;
using SmartOffice.EManagement.IResponsitory;
using SmartOffice.Models.ViewModel;
using SmartOffice.ModelsHRMSLocal;
using SmartOffice.SurveyApp.IResponsitory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SmartOffice.Controllers
{
    public class EManageFormController : Controller
    {
        private readonly IEForm _IEForm;
        private readonly IConfiguration _configuration;
        public EManageFormController(IEForm eForm , IConfiguration configuration)
        {
            _IEForm = eForm;
            this._configuration = configuration;
        }

        [Authorize]
        public async Task<IActionResult> FormIndex()
        {
            string Division = User.Claims.FirstOrDefault(c => c.Type == "Division").Value;
            string Section = User.Claims.FirstOrDefault(c => c.Type == "Section").Value;
            string Department = User.Claims.FirstOrDefault(c => c.Type == "Department").Value;
            string Department2 = User.Claims.FirstOrDefault(c => c.Type == "Department2").Value;
            string Department3 = User.Claims.FirstOrDefault(c => c.Type == "Department3").Value;
            string username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;

            var model = _IEForm.GetGroup("Q", Division, Section, Department, Department2, Department3,username, "Allsurvey");
           
            return await Task.Run(() => View("FormIndexMain", model));
        }

        [Authorize]
        public IActionResult FormList(string strObj, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo)
        {           
            ViewBag.strObj = strObj;
            ViewBag.OperationCode = OperationCode;
            ViewBag.OpeGroupCateg = OpeGroupCateg;
            ViewBag.InputKind = InputKind;
            ViewBag.OperationNo = OperationNo ?? "";

            string username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var result = _IEForm.GetReport(strObj, "Report", OperationCode, OpeGroupCateg, InputKind, OperationNo ?? "", username);


            return  View("FormList",result);
        }

        

        [Authorize]    
        public IActionResult FormInput(string strObj, string OperationCode, string OpeGroupCateg,string InputKind , string OperationNo)
        {
            string username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            TupleForm ItemForm;
            try
            {

                ItemForm = _IEForm.GetGenerate(strObj, "Generate", OperationCode, OpeGroupCateg, InputKind, OperationNo ?? "", username);
                ViewBag.strObj = strObj;
                ViewBag.OperationCode = OperationCode;
                ViewBag.OpeGroupCateg = OpeGroupCateg;
                ViewBag.InputKind = InputKind;
                ViewBag.OperationNo = OperationNo ?? "";

                return  View("FormInput", ItemForm);
            }
            catch
            {
                ItemForm = null;
                return View("FormInput", null);
            }
            finally
            {
                ItemForm = null;
            }
                               
        }

        [Authorize]
        [HttpPost]
        public dynamic MaintenanceForm(TupleForm tupleForm, string strObj, string strObjSub, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo,  IFormCollection frm)
        {
        
            string CreateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var Result = _IEForm.MaintenanceForm(tupleForm, strObj, "Maintenance", strObjSub,  OperationCode, OpeGroupCateg, InputKind, OperationNo ?? "", frm, CreateBy);
          
            return Json(Result);
        }


        [HttpPost]
        public JsonResult GetListTable(string strObj, string OperationCode, string OpeGroupCateg, string InputKind)
        {
            string username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var ItemForm = _IEForm.GetInquiry(strObj, "Inquiry","", OperationCode, OpeGroupCateg, InputKind, "", username);
            var item1 = ItemForm.formOperationItems;
            return Json(new { item1 });
        }

        //------------------------------------------------------------------------------start Dynamic table---------------------------------------------------------------
        //Get Table
        public JsonResult GetTableConst(string strObj, string operationCode, string opeGroupCateg, string inputKind , string operationNo)
        {
            string username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            return Json(_IEForm.GetTableConst(strObj, "Generate", operationCode, opeGroupCateg, inputKind, operationNo ?? "", username));
        }

        //Get column
        public JsonResult GetCol(string code)
        {
            return Json(_IEForm.GetCol(code));
        }

        public async Task<JsonResult> GetValueList(string ValueCode)
        {
            return Json(await _IEForm.GetValueList(ValueCode));
        }

        public JsonResult GetUserInDept(string strObj, string operationCode, string opeGroupCateg, string inputKind, string operationNo)
        {
            string username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var data = _IEForm.GetInquiry(strObj, "Inquiry","", operationCode, opeGroupCateg, inputKind, "", username);

            var results = from A in data.vewHRMSEmployeeSurveyPICs
                                                  
                          select new
                          {
                              id = A.DEPARTMENT3,
                              text = A.DEPARTMENT3,
                          };

           
            return Json(new { data1= data.vewHRMSEmployeeSurveyPICs.Select(x => (x.DEPARTMENT3, x.CODEMPID + " : " + x.NAMEMPE)) , data2 = results.Distinct() });
        }
        //------------------------------------------------------------------------------End Dynamic table---------------------------------------------------------------

        public JsonResult GetFormCondition(string OperationNo, string OperationCode)
        {
            return Json(_IEForm.GetFormCondition(OperationNo, OperationCode));
        }

        public JsonResult GetFunc(string strObj, string operationCode, string opeGroupCateg, string inputKind, string operationNo,string datafind)
        {
            string username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var data = _IEForm.GetInquiry(strObj, "Inquiry","SumInput", operationCode, opeGroupCateg, inputKind, "", username);

            return Json(data.vewNumberofinfectedPersonsSums.Where(x=>x.Keydata == datafind).Select(x=>x.Result));
        }

        public JsonResult GetOperationNo(string strObj, string operationCode, string opeGroupCateg, string inputKind, string operationNo, string datafind)
        {
            string username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var data = _IEForm.GetInquiry(strObj, "Inquiry", "", operationCode, opeGroupCateg, inputKind, "", username);

            return Json(data.vewOperationItemList_Result_CheckOperationNos.Where(x => x.FinalResult == datafind).Select(x => x.OperationNo).FirstOrDefault());
        }

        [Authorize]
        public async Task<IActionResult> SummaryReport()
        {

            //var result = _IEForm.GetSummaryReport("Q004O", 8,2020);


            return await Task.Run(() => View());
        }

        public string GetSummaryReport(string OperationCode, string StartDate, string EndDate)
        {
            return _IEForm.GetSummaryReport(OperationCode, StartDate, EndDate);
        }


        [Authorize]
        public async Task<IActionResult> FormDashboardSummary(string mode ,string name)
        {
            ConnEForm objrun = new ConnEForm(_configuration);
            TupleFormMasterDashboard tupleFormMasterDashboard = new TupleFormMasterDashboard();
            try
            {
                var date = DateTime.Today;
                tupleFormMasterDashboard = objrun.GetMasterCharge(date, date, 0, mode);
                ViewBag.mode = mode ?? "";
                ViewBag.name = name ?? "";

                return await Task.Run(() => View("FormDashboardSummary", tupleFormMasterDashboard));
            }
            catch
            {
                objrun = null;
                tupleFormMasterDashboard = null;
                return await Task.Run(() => View("FormDashboardSummary", null));
            }
            finally
            {
                objrun = null;
                tupleFormMasterDashboard = null;
            }                    
        }


        [HttpPost]
        public JsonResult FormDashboardSummary(string startdate, string enddate, int nochart ,string mode)
        {
            ConnEForm objrun = new ConnEForm(_configuration);
            TupleFormMasterDashboard tupleFormMasterDashboard = new TupleFormMasterDashboard();
            try
            {
                tupleFormMasterDashboard = objrun.GetMasterCharge(Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), nochart, mode);
                return Json(new { chart = tupleFormMasterDashboard.formMasterDashboards });
            }
            catch
            {
                objrun = null;
                tupleFormMasterDashboard = null;
                return null;
            }
            finally
            {
                objrun = null;
                tupleFormMasterDashboard = null;
            }         
        }

        

        [HttpPost]
        public JsonResult GetChartDatalabels(string startdate, string enddate, int nochart, string mode)
        {
            ConnEForm objrun = new ConnEForm(_configuration);
            TupleFormChartDataLabels tupleFormChartData = new TupleFormChartDataLabels();
            try
            {
                tupleFormChartData = objrun.GetChartDatalabels(Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), nochart, mode);

                return Json(new
                {
                    labels = tupleFormChartData.labels.Select(x => new { x._datalabels, x._datanochart, x._datatypechart }).Distinct(),
                    label = tupleFormChartData.labels.Select(x => x._datalabel).Distinct(),
                    data = tupleFormChartData.labels
                });
            }
            catch
            {
                objrun = null;
                tupleFormChartData = null;
                return null;
            }
            finally
            {
                objrun = null;
                tupleFormChartData = null;
            }          
        }
    }
}