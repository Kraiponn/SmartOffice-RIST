
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.EAppointment.IResponsitory;
using SmartOffice.EAppointment.ModelsForm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;

namespace SmartOffice.Controllers
{
    public class EAppointmentController : Controller
    {
        private readonly IEAppointment _IEAppointment;
        private readonly ILogger<EAppointmentController> _logger;
        public EAppointmentController(IEAppointment appointment, ILogger<EAppointmentController> logger)
        {
            _IEAppointment = appointment;
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            var UserName = "";
            var Namempe = "";
            var Organizationname = "";
            var PhoneNumber = "";
            if (User.Identity.IsAuthenticated)
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value ?? "";
                Namempe = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value;
                Organizationname = User.Claims.FirstOrDefault(c => c.Type == "Organizationname").Value;
                PhoneNumber = User.Claims.FirstOrDefault(c => c.Type == "PhoneNumber").Value;
            }

            ViewBag.user = UserName;
            return View();
        }

        [Authorize]
        public IActionResult NewAppoint()
        {
            return View();
        }
        ////////////////////////////////////////////////////////<Calendar>/////////////////////////////////////////////////////////////////////////////////////////
        [Authorize]
        public ActionResult Fullcalendar()
        {
            return View();
        }

        public ActionResult FullcalendarM()
        {
            return View();
        }

        //fullcalendar ALL
        public async Task<JsonResult> GetEvents()
        {
            var eventList = await _IEAppointment.GetEvents();
             var eventList2 = eventList.Select(e => new
            {
                eventID = e.AppointmentId,
                title = e.Subject,
                description = e.Description,
                start = e.StartDate,
                end = e.EndDate,
                color = e.ThemeColor,
                allDay = e.IsFullDay,
                e.AppointType,
                 e.UserName
             });
            var rows = eventList2.ToArray();
            return Json(rows);
        }
        //fullcalendar Setting
        public async Task<JsonResult> GetEventsSetting()
        {
            var Username = string.Empty;

            if(User.Identity.Name!=null)
            Username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value.ToString() ?? null;
                
                var eventList = await _IEAppointment.GetEventsSetting(Username);
                var eventList1 = eventList.Select(e => new
                {
                    eventID = e.AppointmentId,
                    title = e.Subject,
                    description = e.Description,
                    start = e.StartDate,
                    end = e.EndDate,
                    color = e.ThemeColor,
                    allDay = e.IsFullDay,
                    e.AppointType,
                    e.UserName
                });

                var rows = eventList1.Distinct().ToArray();
                return Json(rows);
                   
        }

        [Authorize]
        public IActionResult JoinAppointment(int id)
        {
            var Username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value.ToString() ?? null;
            _logger.LogTrace(Username + "JoinAppointment ID: " + id + "To Role ID: ");
            _IEAppointment.JoinAppointment(id, Username);
            return View();
        }


            //fullcalendar private today
            public async Task<JsonResult> GetEventsDay()
        {
            var Username = string.Empty;

            if (User.Identity.Name != null)
                Username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value.ToString() ?? null;

            return Json(await _IEAppointment.GetEventsDay(Username));
        }

        [HttpPost]
        public async Task<JsonResult> SaveEvent(int AppointmentId, string Subject, DateTime StartDate, DateTime EndDate, string Description, string ThemeColor,
            bool IsFullDay, string AppointType, string invitepeople, List<IFormFile> files)
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            _logger.LogInformation(User.Identity.Name + " Save Appointment >>> " + Subject);
            return Json(await _IEAppointment.SaveEventAsync(AppointmentId, Subject, StartDate, EndDate, Description, ThemeColor, IsFullDay, AppointType, UserName, invitepeople, files));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteEvent(Appointment App, List<string> invitepeople)
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var result = await _IEAppointment.DeleteEvent(App, UserName, invitepeople);
            _logger.LogInformation(User.Identity.Name + ">> Cancel Event ID: " + App.AppointmentId);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteInvitepeople(int appointment_ID, string operatorID)
        {
            
            var result = await _IEAppointment.DeleteInvitepeople(appointment_ID, operatorID);
            _logger.LogInformation(User.Identity.Name + ">> Cancel Invite people: " + appointment_ID + " " + operatorID);
            return Json(result);
        }

        //get GetCalendarMain

        public async Task<JsonResult> GetCalendarMain(string newstype)
        {
            var _user = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value ?? null;         
         
                var allevens = await _IEAppointment.GetCalendarMain(_user);

            var myevens = new
            {
                rows = allevens
            };
            return Json(new { data = myevens });
        }
        public async Task<JsonResult> GetCalendarMainUser(string newstype)
        {
            var _user = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value ?? null;

            var allSlide = await _IEAppointment.GetCalendarMainUser(_user);
            return Json(new { data = allSlide });
        }
        public JsonResult GetCalendarMainprivate(string newstype)
        {
            var _user = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var _group = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
            var allSlide = _IEAppointment.GetCalendarMainprivate(newstype, _user);
            return Json(new { data = allSlide });
        }
        public async Task<JsonResult> Getpeople_inClass(int AppointmentId)
        {
            var peopleinclass = await _IEAppointment.Getpeople_inClass(AppointmentId);
           return Json(new{data=peopleinclass});
        }

        //------------------------------------Start Attach File--------------------------------------------------------------------------------------
        //Get File Attach        
        public JsonResult GetFileAttach(int AppointmentId)
        {

            var attachdata = _IEAppointment.GetFileAttachdata(AppointmentId);
            return Json(new { data = attachdata });

        }



        [HttpPost]
        public JsonResult Deletefile(int AppointmentId, string filename)
        {

            return Json(_IEAppointment.Deletefile(AppointmentId, filename));
        }
        //------------------------------------END Attach File--------------------------------------------------------------------------------------

        ////////////////////////////////////////////////////////</Calendar>/////////////////////////////////////////////////////////////////////////////////////////
    }
}