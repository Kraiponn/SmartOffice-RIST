using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartOffice.eManagement.Models;
using SmartOffice.eManagement.ModelsManagementControl;
using SmartOffice.EManagement.IResponsitory;
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
    public class EManageUserController : Controller
    {
        private readonly IEUser _IEUser;
        public EManageUserController(IEUser eUser)
        {
            _IEUser = eUser;
        }

        public IActionResult Store()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> MessageSetting()
        {
            var GetdataItemCateg = _IEUser.GetItemCateglist();
            var model = new TupleUser
            {

                ddlItemCateg = new SelectList(GetdataItemCateg, "ID", "Name"),
                
            };
            return await Task.Run(() => View(model));
        }

        public JsonResult GetMessage()
        {
            try
            {
                var datamaster = _IEUser.GetMessage();

                return Json(new { data = datamaster });
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }


        public ActionResult AddMessage(string itemCateg, string displayOrder, string message, string startDate, string endDate)
        {            
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            string ComputerName = Environment.MachineName;

            return Json(_IEUser.AddMessage(itemCateg, displayOrder, message, startDate, endDate,  UserName,  ComputerName));
        }

        [HttpPost]
        public JsonResult DeleteMessage(GetMessage  getMessage)
        {
            return Json(_IEUser.DeleteMessage(getMessage));
        }

    }
}