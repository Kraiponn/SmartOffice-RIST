using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartOffice.IResponsitory;
using Microsoft.AspNetCore.Authorization;
using SmartOffice.EManagement.IResponsitory;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SmartOffice.ModelsDocControl;

namespace SmartOffice.Controllers
{
    public class SpecialFormController : Controller
    {
        private readonly IDynamicFormService _IDynamicFormService;
        private readonly IConfiguration _configuration;
        private readonly IMachineOperation _machineOperation;

        public SpecialFormController(IConfiguration configuration, IDynamicFormService IDynamicFormService, IMachineOperation machineOperation)
        {
            this._configuration = configuration;
            _IDynamicFormService = IDynamicFormService;
            _machineOperation = machineOperation;
        }
        //------------------------------------Start Get dynamic Form--------------------------------------------------------------------------------------

        [Authorize]
        public async Task<IActionResult> Index(string code, string docno, string mode, string seq)
        {
            try
            {
                ViewBag.code = code ?? "";
                ViewBag.docno = docno ?? "";
                ViewBag.mode = mode ?? "";
                ViewBag.seq = seq ?? "";
                var CreateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
                var model = await _IDynamicFormService.GetModelAsync(code, docno, mode, seq, CreateBy);
                //return RedirectToAction("Comingsoon", "Home");
                return await Task.Run(() => View("Index", model));
            }
            catch (Exception ex)
            {
                //for refresh page 
                return Content(ex.Message);
            }
        }
        public async Task<JsonResult> ValidateFileMachineReport(string DocCode, string DocNo)
        {
            var Result = await _IDynamicFormService.CheckValidateFileMachine(DocCode, DocNo);
            return Json(Result);
        }
        public JsonResult GetUserFlow(string DocCode)
        {
            var UserFlow = _IDynamicFormService.GetUserFlow(DocCode);
            return Json(UserFlow);
        }
        public async Task<ActionResult> PrintReport(string DocCode, string DocNo ,string File)
        {          
            var Allfile =await  _IDynamicFormService.GetListAttachFileAsync(DocNo);
            if (Allfile.Count>=1)
            {
                var progress = _IDynamicFormService.Getprogress(DocCode, DocNo ?? "");
                var PdfApprove = _IDynamicFormService.StreamPDF(DocNo ?? "", DocCode);
                var workStream = _machineOperation.PrintReport(progress, Allfile[0].ToString(), PdfApprove, DocCode, DocNo);
                return new FileStreamResult(workStream, "application/pdf");
            }

            return null;
        }
        public async Task<JsonResult> GetItemDocNoAsync()
        {
            var Allitem = await _IDynamicFormService.GetItemMachineProcess();
            return Json(Allitem);
        }

        public async Task<JsonResult> GetOperationNameCompare()
        {

            var AllValueList = await _IDynamicFormService.GetOperationName();

            var OpName = await _machineOperation.GetOperationName();
            var ReturnItem = new List<ValueList>();
            foreach (var item in OpName)
            {
                var CheckExit = AllValueList.Where(i => i.ValueE.ToLower().Trim() == item.Text.Trim().ToLower()).FirstOrDefault();
                if (CheckExit != null)
                {
                    ReturnItem.Add(new ValueList()
                    {
                        Id = CheckExit.Id,
                        ValueE = CheckExit.ValueE
                    });
                 
                }
               
            }
              
            return Json(ReturnItem);
        }
    }
}