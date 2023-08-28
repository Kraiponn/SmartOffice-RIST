using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.EReservation.IResponsitory;
using SmartOffice.eReservation.ModelsDocControl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SmartOffice.eReservation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace SmartOffice.Controllers
{
    public class EReservationController : Controller
    {
        //////////////////////////////////////////Start Room///////////////////////////////////////////////////////////////////
        
        private readonly IEReservationRoom _IEReservationRoom;
        private readonly IEReservationRoomSetting _IEReservationRoomSetting;
        private readonly ILogger<EReservationController> _logger;
        private IHttpContextAccessor _accessor;
        private readonly IEReservationAsset _IEReservationAsset;
        public EReservationController(IEReservationRoom eReservationRoom, IEReservationRoomSetting eReservationRoomSetting, ILogger<EReservationController> logger,
            IHttpContextAccessor accessor, IEReservationAsset eReservationAsset)
        {
            _logger = logger;
            _IEReservationRoom = eReservationRoom;
            _IEReservationRoomSetting = eReservationRoomSetting;
            _accessor = accessor;
            _IEReservationAsset = eReservationAsset;
        }
        //[Authorize]
        public IActionResult EReservationRoom()
        {
            var UserName = "";
            var Namempe = "";
            var Organizationname = "";
            var PhoneNumber = "";
            var Bldg = "";
            var Email1 = "";
            var Email2 = "";
            if (User.Identity.IsAuthenticated)
            {
                 UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value ?? "";
                 Namempe = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value;
                 Organizationname = User.Claims.FirstOrDefault(c => c.Type == "Organizationname").Value;
                 PhoneNumber = User.Claims.FirstOrDefault(c => c.Type == "PhoneNumber").Value;
                 Bldg = User.Claims.FirstOrDefault(c => c.Type == "Bldg").Value;
                Email1 = User.Claims.FirstOrDefault(c => c.Type == "Email1").Value;
                Email2 = User.Claims.FirstOrDefault(c => c.Type == "Email2").Value;
            }

            ViewBag.user = UserName ?? "";
            ViewBag.namempe = Namempe ?? "";          
            ViewBag.organizationname = Organizationname ?? "";
            ViewBag.phoneNumber = PhoneNumber ?? "";
            ViewBag.bldg = Bldg ?? "";
            ViewBag.email1 = Email1 ?? "";
            ViewBag.email2 = Email2 ?? "";

            var Getimageh = _IEReservationRoom.Getimageh();
          

            var model = new eReservation.Models.TupleEReservationRoom
            {
                reservationMasterRoomsall = Getimageh,
               
            };

            return View(model);
        }



        //get resources
        [HttpPost]
        public async Task<JsonResult> GetResources(List<string> getResources)
        {            
            var ResourcesList = await _IEReservationRoom.GetResources(getResources);
            
            return Json(new { data = ResourcesList });
        }


        [AllowAnonymous]
        public async Task<JsonResult> GetResourcesfillter(string Searchtext)
        {
            var fillter = await _IEReservationRoom.GetResourcesfillter(Searchtext);

            return Json(fillter);
        }




        //get events
        [HttpPost]
        public JsonResult GetEvents(string Year ,string Month)
        {
            var EventsList = _IEReservationRoom.GetEvents(Year , Month);

            return Json(EventsList);
        }

        public JsonResult GetLayout(string RoomId)
        {
            var LayoutList = _IEReservationRoom.GetLayout(RoomId);

            return Json(new { data = LayoutList });
        }

        public JsonResult Getimaged(string RoomId)
        {
            var imagedList = _IEReservationRoom.Getimaged(RoomId);

            return Json(new { data = imagedList });
        }

        

        //save update event
        [HttpPost]
        public async Task<JsonResult> SaveEvent(List<IFormFile> files, int ReservationId,string RoomId, string StartDate ,string EndDate , 
            string OperatorId, string OperatorName,string OperatorBldg,string Bldg, string OperatorOrganizationname ,string OperatorPhoneNumber,string RequestDate,string UpdateDate, 
            string TotalOperator , string LayoutId,string Subject,string Datemultiple)
        {
            var mailuserrequest = User.Claims.FirstOrDefault(c => c.Type == "Email1").Value ?? "";
            if (mailuserrequest == "")
            {
                mailuserrequest = User.Claims.FirstOrDefault(c => c.Type == "Email2").Value ?? "";
            }
            
            

            _logger.LogInformation(User.Identity.Name + ">>> Reservation >>> " + RoomId +" Time : "+ DateTime.Now.ToString());
            return Json(await _IEReservationRoom.SaveEvent(files, ReservationId, RoomId, StartDate, EndDate, OperatorId, OperatorName, OperatorBldg, Bldg, OperatorOrganizationname, OperatorPhoneNumber,
                RequestDate, UpdateDate, TotalOperator, LayoutId, Subject, mailuserrequest, Datemultiple));
        }

        //delete event
        [HttpPost]
        public JsonResult DeleteEvent(int reservationId)
        {
            var mailuserrequest = User.Claims.FirstOrDefault(c => c.Type == "Email1").Value ?? "";
            if (mailuserrequest == "")
            {
                mailuserrequest = User.Claims.FirstOrDefault(c => c.Type == "Email2").Value ?? "";
            }
            _logger.LogInformation(User.Identity.Name + " >>> Delete Reservation ID >>> " + reservationId + ">>> Time : " + DateTime.Now.ToString());
            return Json(_IEReservationRoom.DeleteEvent(reservationId, mailuserrequest));
        }

        //approve event
        [HttpPost]
        public JsonResult ApproveEvent(int reservationId ,string remarks)
        {
            var userapprove = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value ?? "";
            var nameapprove = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value ?? "";

            var mailuserapprove = User.Claims.FirstOrDefault(c => c.Type == "Email1").Value ?? "";
            if (mailuserapprove == "")
            {
                mailuserapprove = User.Claims.FirstOrDefault(c => c.Type == "Email2").Value ?? "";
            }


            _logger.LogInformation(User.Identity.Name + " >>> Approve Reservation ID >>> " + reservationId + ">>> Time : " + DateTime.Now.ToString());
            return Json(_IEReservationRoom.ApproveEvent(reservationId, mailuserapprove, userapprove, nameapprove, remarks));
        }

        //reject event
        [HttpPost]
        public JsonResult RejectEvent(int reservationId, string remarks)
        {
            var userapprove = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value ?? "";
            var nameapprove = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value ?? "";

            var mailuserapprove = User.Claims.FirstOrDefault(c => c.Type == "Email1").Value ?? "";
            if (mailuserapprove == "")
            {
                mailuserapprove = User.Claims.FirstOrDefault(c => c.Type == "Email2").Value ?? "";
            }


            _logger.LogInformation(User.Identity.Name + " >>> Reject Reservation ID >>> " + reservationId + ">>> Time : " + DateTime.Now.ToString());
            return Json(_IEReservationRoom.RejectEvent(reservationId, mailuserapprove, userapprove, nameapprove, remarks));
        }

        //------------------------------------Start Attach File--------------------------------------------------------------------------------------
        //Get File Attach        
        public JsonResult GetFileAttach(int ReservationId, string Type)
        {
            
                var attachdata = _IEReservationRoom.GetFileAttachdata(ReservationId, Type);
                return Json(new { data = attachdata });
            
        }

        

        [HttpPost]
        public JsonResult Deletefile(int reservationId, string type, string filename,string end)
        {

            return Json(_IEReservationRoom.Deletefile(reservationId, type, filename, end));
        }
        //------------------------------------END Attach File--------------------------------------------------------------------------------------

//------------------------------------Setting--------------------------------------------------------------------------------------

        [Authorize]
        public async Task<IActionResult> EReservationRoomSetting()
        {
            var Code = User.Claims.FirstOrDefault(c => c.Type == "Bldg").Value;

            var GetdataSecCode = _IEReservationRoomSetting.GetLocationMasterlist(Code);
            var model = new TupleEReservationRoom
            {

                ddlLocationList = new SelectList(GetdataSecCode, "ID", "Name")
            };
            return await Task.Run(() => View(model));
  
        }

        public JsonResult GetLocationMaster()
        {
            try
            {
                
                var datamaster = _IEReservationRoomSetting.GetLocationMaster();

                return Json(new { data = datamaster });
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }

        public JsonResult GetRoomMaster()
        {
            try
            {
                var Code = User.Claims.FirstOrDefault(c => c.Type == "Bldg").Value;
                var datamaster = _IEReservationRoomSetting.GetRoomMaster(Code);

                return Json(new { data = datamaster });
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }

        public JsonResult GetRoomImage(string Room_ID)
        {
            try
            {
                var datamaster = _IEReservationRoomSetting.GetRoomImage(Room_ID);

                return Json(new { data = datamaster });
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }

        public JsonResult GetRoomLayout(string Room_ID)
        {
            try
            {
                var datamaster = _IEReservationRoomSetting.GetRoomLayout(Room_ID);

                return Json(new { data = datamaster });
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }

        public ActionResult UpdateLocationMaster(ReservationMasterLocation reservationMasterLocation)
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            return Json(_IEReservationRoomSetting.UpdateLocationMaster(reservationMasterLocation, UserName, ComputerName));
        }

        [HttpPost]
        public JsonResult DeleteLocationMaster(ReservationMasterLocation reservationMasterLocation)
        {

            
            return  Json( _IEReservationRoomSetting.DeleteLocationMaster(reservationMasterLocation));
        }

        public ActionResult AddLocationMaster(string building,string code)
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            return Json(_IEReservationRoomSetting.AddLocationMaster(building,code, UserName, ComputerName));
        }

        

        public ActionResult AddRoomMaster(string roomid, string name, string description, string locationid, string disable,
            string ResponsibleEmail, string ResponsibleBy, string ResponsibleName, string ResponsibleTelNo, string NumberOfSeats,
            string TVConferenceIP, string Projector, string Computer,
            IFormFile files)
        {
            bool disablechk = disable == "on" ? true : false;
            bool Projectorchk = Projector == "on" ? true : false;
            bool Computerchk = Computer == "on" ? true : false;
            var NumberOfSeatschk = NumberOfSeats == "" ? 0 : Convert.ToInt32( NumberOfSeats);

            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            return Json(_IEReservationRoomSetting.AddRoomMaster(roomid,name, description, locationid, disablechk,
                ResponsibleEmail, ResponsibleBy, ResponsibleName, ResponsibleTelNo, NumberOfSeatschk, TVConferenceIP, Projectorchk, Computerchk,
                files, UserName, ComputerName));
        }

        [HttpPost]
        public JsonResult DeleteRoomMaster(ReservationMasterRoom reservationMasterRoom)
        {


            return Json(_IEReservationRoomSetting.DeleteRoomMaster(reservationMasterRoom));
        }


        public ActionResult AddImageRoomMaster(string roomId, int displayOrder, string description, IFormFile files)
        {
           
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            return Json(_IEReservationRoomSetting.AddImageRoomMaster(roomId, displayOrder, description, files, UserName, ComputerName));
        }

        public ActionResult AddLayoutRoomMaster(string layoutId,string roomid, int displayOrder , string description, string disable , IFormFile files)
        {
            bool disablechk = disable == "1" ? true : false;
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            return Json(_IEReservationRoomSetting.AddLayoutRoomMaster(layoutId,roomid, displayOrder,description, disablechk, files, UserName, ComputerName));
        }

        [HttpPost]
        public JsonResult DeleteImageMaster(ReservationMasterRoomImage reservationMasterRoomImage)
        {


            return Json(_IEReservationRoomSetting.DeleteImageMaster(reservationMasterRoomImage));
        }

        [HttpPost]
        public JsonResult DeleteLayoutMaster(ReservationMasterRoomLayout reservationMasterRoomLayout)
        {


            return Json(_IEReservationRoomSetting.DeleteLayoutMaster(reservationMasterRoomLayout));
        }

        //////////////////////////////////////////End Room///////////////////////////////////////////////////////////////////



        //[Authorize]
        public IActionResult EReservationAsset()
        {
            var UserName = "";
            var Namempe = "";
            var Organizationname = "";
            var PhoneNumber = "";
            var Bldg = "";
            var Email1 = "";
            var Email2 = "";
            if (User.Identity.IsAuthenticated)
            {
                UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value ?? "";
                Namempe = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value;
                Organizationname = User.Claims.FirstOrDefault(c => c.Type == "Organizationname").Value;
                PhoneNumber = User.Claims.FirstOrDefault(c => c.Type == "PhoneNumber").Value;
                Bldg = User.Claims.FirstOrDefault(c => c.Type == "Bldg").Value;
                Email1 = User.Claims.FirstOrDefault(c => c.Type == "Email1").Value;
                Email2 = User.Claims.FirstOrDefault(c => c.Type == "Email2").Value;
            }

            ViewBag.user = UserName ?? "";
            ViewBag.namempe = Namempe ?? "";
            ViewBag.organizationname = Organizationname ?? "";
            ViewBag.phoneNumber = PhoneNumber ?? "";
            ViewBag.bldg = Bldg ?? "";
            ViewBag.email1 = Email1 ?? "";
            ViewBag.email2 = Email2 ?? "";

            var Getimageh = _IEReservationAsset.Getimageh();


            var model = new TupleEReservationAsset
            {
                reservationMasterAssetsall = Getimageh,

            };

            return View(model);
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetResourcesfillterAsset(string Searchtext)
        {
            var fillter = await _IEReservationAsset.GetResourcesfillter(Searchtext);

            return Json(fillter);
        }

        public JsonResult GetLayoutAsset(string AssetId)
        {
            var LayoutList = _IEReservationAsset.GetLayout(AssetId);

            return Json(new { data = LayoutList });
        }

        public JsonResult GetimagedAsset(string AssetId)
        {
            var imagedList = _IEReservationAsset.Getimaged(AssetId);

            return Json(new { data = imagedList });
        }

        //get resources
        [HttpPost]
        public async Task<JsonResult> GetResourcesAsset(List<string> getResources)
        {
            var ResourcesList = await _IEReservationAsset.GetResources(getResources);

            return Json(new { data = ResourcesList });
        }

        //get events
        [HttpPost]
        public JsonResult GetEventsAsset(string Year, string Month)
        {
            var EventsList = _IEReservationAsset.GetEvents(Year, Month);

            return Json(EventsList);
        }

        //save update event
        [HttpPost]
        public async Task<JsonResult> SaveEventAsset(List<IFormFile> files, int ReservationId, string AssetId, string StartDate, string EndDate,
            string OperatorId, string OperatorName, string OperatorBldg, string Bldg, string OperatorOrganizationname, string OperatorPhoneNumber, string RequestDate, string UpdateDate,
            string LayoutId, string Subject, string Datemultiple)
        {
            var mailuserrequest = User.Claims.FirstOrDefault(c => c.Type == "Email1").Value ?? "";
            if (mailuserrequest == "")
            {
                mailuserrequest = User.Claims.FirstOrDefault(c => c.Type == "Email2").Value ?? "";
            }



            _logger.LogInformation(User.Identity.Name + ">>> Asset Reservation >>> " + AssetId + " Time : " + DateTime.Now.ToString());
            return Json(await _IEReservationAsset.SaveEvent(files, ReservationId, AssetId, StartDate, EndDate, OperatorId, OperatorName, OperatorBldg, Bldg, OperatorOrganizationname, OperatorPhoneNumber,
                RequestDate, UpdateDate, LayoutId, Subject, mailuserrequest, Datemultiple));
        }

        //delete event
        [HttpPost]
        public JsonResult DeleteEventAsset(int reservationId)
        {
            var mailuserrequest = User.Claims.FirstOrDefault(c => c.Type == "Email1").Value ?? "";
            if (mailuserrequest == "")
            {
                mailuserrequest = User.Claims.FirstOrDefault(c => c.Type == "Email2").Value ?? "";
            }
            _logger.LogInformation(User.Identity.Name + " >>> Delete asset reservation ID >>> " + reservationId + ">>> Time : " + DateTime.Now.ToString());
            return Json(_IEReservationAsset.DeleteEvent(reservationId, mailuserrequest));
        }

        //borrow event
        [HttpPost]
        public JsonResult BorrowEvent(int reservationId, string remarks)
        {
            var userapprove = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value ?? "";
            var nameapprove = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value ?? "";

            var mailuserapprove = User.Claims.FirstOrDefault(c => c.Type == "Email1").Value ?? "";
            if (mailuserapprove == "")
            {
                mailuserapprove = User.Claims.FirstOrDefault(c => c.Type == "Email2").Value ?? "";
            }


            _logger.LogInformation(User.Identity.Name + " >>> Borrow asset reservation ID >>> " + reservationId + ">>> Time : " + DateTime.Now.ToString());
            return Json(_IEReservationAsset.BorrowEvent(reservationId, mailuserapprove, userapprove, nameapprove, remarks));
        }

        //return event
        [HttpPost]
        public JsonResult ReturnEvent(int reservationId, string remarks)
        {
            var userapprove = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value ?? "";
            var nameapprove = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value ?? "";

            var mailuserapprove = User.Claims.FirstOrDefault(c => c.Type == "Email1").Value ?? "";
            if (mailuserapprove == "")
            {
                mailuserapprove = User.Claims.FirstOrDefault(c => c.Type == "Email2").Value ?? "";
            }


            _logger.LogInformation(User.Identity.Name + " >>> Return asset reservation ID >>> " + reservationId + ">>> Time : " + DateTime.Now.ToString());
            return Json(_IEReservationAsset.ReturnEvent(reservationId, mailuserapprove, userapprove, nameapprove, remarks));
        }

        //reset event
        [HttpPost]
        public JsonResult ResetEvent(int reservationId, string remarks)
        {
            var userapprove = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value ?? "";
            var nameapprove = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value ?? "";

            var mailuserapprove = User.Claims.FirstOrDefault(c => c.Type == "Email1").Value ?? "";
            if (mailuserapprove == "")
            {
                mailuserapprove = User.Claims.FirstOrDefault(c => c.Type == "Email2").Value ?? "";
            }


            _logger.LogInformation(User.Identity.Name + " >>> Reset status asset reservation ID >>> " + reservationId + ">>> Time : " + DateTime.Now.ToString());
            return Json(_IEReservationAsset.ResetEvent(reservationId, mailuserapprove, userapprove, nameapprove, remarks));
        }
    }
}