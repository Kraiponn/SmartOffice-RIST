using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartOffice.EManagement.IResponsitory;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.ModelsForm;

namespace SmartOffice.Controllers
{
    public class MachinOperationController : Controller
    {
        private readonly IMachineOperation _machineOperation;
        private readonly ESmartOfficeContext _context;
        public MachinOperationController(IMachineOperation machineOperation, ESmartOfficeContext context)
        {
            _machineOperation = machineOperation;          
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            ViewData["section"] = await _machineOperation.GetSection();
            ViewData["responseUser"] = await _machineOperation.GetOperationName();
            //var htmlstring = _machineOperation.CheckDataExcel();
            return View();
        }
        public async Task<JsonResult> OnPostImportFromExcel(string datereturn)
        {
            
            IFormFile file = Request.Form.Files[0];
            var result = await _machineOperation.ReadFromSourceAsync(file, datereturn);
            return Json(result);
        }
        public async Task<FileStreamResult> GetReportByDept(string Deptcode)
        {
            string page = HttpContext.Request.Query["Deptcode"].ToString();
            return await _machineOperation.ReportByDept(Deptcode);           
        }
        public async Task<FileStreamResult> GetReportByResponse(string UserResponse)
        {
            string page = HttpContext.Request.Query["UserResponse"].ToString();
            return await _machineOperation.ReportByUser(UserResponse);
        }      
        public JsonResult GetMenu()
        {
            try
            {
                var datamaster = _context.Set<FormMenu>().FromSql("exec sprFormMcOprMenu").AsNoTracking().Where(x => !(String.IsNullOrEmpty(x.Param)))
                .OrderBy(x => x.GroupName).ThenBy(x => x.MenuNameT).ThenBy(x => x.MenuNameE).ThenBy(x => x.MenuNameJ).ToList();

                return Json(new { data = datamaster });
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }
      
    }
}