using System;
using SmartOffice.EReservation.IResponsitory;
using System.Collections.Generic;
using System.Linq;
using SmartOffice.eReservation.ModelsDocControl;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SmartOffice.eReservation.Models;
using System.Drawing;
using System.Text.RegularExpressions;

namespace SmartOffice.EReservation
{
    public class EReservationRoomSettingControl : IEReservationRoomSetting
    {
        private readonly EReservationContext _context;

        private IHostingEnvironment _hostingEnvironment;

        public EReservationRoomSettingControl(EReservationContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public List<ReservationMasterLocation> GetLocationMaster()
        {

            var allfile = _context.ReservationMasterLocation.OrderBy(x => x.Building).ToList();

            return allfile;

        }

        public List<ReservationMasterRoomImage> GetRoomImage(string Room_ID)
        {

            var allfile = _context.ReservationMasterRoomImage.Where(x => x.RoomId == Room_ID).OrderBy(x => x.DisplayOrder).ToList();

            return allfile;

        }

        public List<ReservationMasterRoomLayout> GetRoomLayout(string Room_ID)
        {

            var allfile = _context.ReservationMasterRoomLayout.Where(x => x.RoomId == Room_ID).OrderBy(x => x.DisplayOrder).ToList();

            return allfile;

        }

        public List<SelectListItem> GetLocationMasterlist(string Code)
        {

            List<SelectListItem> allfile = _context.ReservationMasterLocation.Where(x => x.Code == Code).Select(x => new { x.LocationId, x.Building }).Distinct().ToList().Select(p => new SelectListItem()
            {
                ID = p.LocationId,
                Name = p.Building,
            }).ToList();


            //allfile.Add(new SelectListItem { ID = "ALL", Name = "ALL" });

            return allfile.OrderBy(x => x.Name).ToList();
        }

        public List<ReservationMasterRoomSetting> GetRoomMaster(string Code)
        {

            var location = _context.ReservationMasterLocation.ToList();
            var room = _context.ReservationMasterRoom.ToList();

            var allfile = (from Item1 in location
                           join Item2 in room
                           on Item1.LocationId equals Item2.LocationId
                           select new { Item1, Item2 }).ToList()
            .Select(p => new ReservationMasterRoomSetting()
            {
                RoomId = p.Item2.RoomId,
                Name = p.Item2.Name,
                Description = p.Item2.Description,
                LocationId = p.Item2.LocationId,
                Image = p.Item2.Image,
                Disable = p.Item2.Disable,
                AddDate = p.Item2.AddDate,
                UpdDate = p.Item2.UpdDate,
                UserName = p.Item2.UserName,
                ComputerName = p.Item2.ComputerName,
                Building = p.Item1.Building,
                ResponsibleEmail = p.Item2.ResponsibleEmail,
                ResponsibleBy = p.Item2.ResponsibleBy,
                ResponsibleName = p.Item2.ResponsibleName,
                ResponsibleTelNo = p.Item2.ResponsibleTelNo,
                NumberOfSeats = Convert.ToInt32(p.Item2.NumberOfSeats),
                Projector = p.Item2.Projector,
                Computer = p.Item2.Computer,
                TVConferenceIP = p.Item2.TvconferenceIp,
                Code = p.Item1.Code

            }).Where(x => x.Code == Code).OrderBy(x => x.Building).ThenBy(x => x.Name).ToList();


            return allfile;

        }

        public dynamic UpdateLocationMaster(ReservationMasterLocation reservationMasterLocation, string UserName, string ComputerName)
        {

            ReservationMasterLocation _Ctrl = _context.ReservationMasterLocation.Where(p => p.LocationId == reservationMasterLocation.LocationId).FirstOrDefault();

            if (_Ctrl != null)
            {
                _Ctrl.Building = reservationMasterLocation.Building;
                _Ctrl.Code = reservationMasterLocation.Code;
                _Ctrl.UpdDate = DateTime.Now;
                _Ctrl.UserName = UserName;
                _Ctrl.ComputerName = ComputerName;
                try
                {

                    _context.ReservationMasterLocation.Update(_Ctrl);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "UPDATE LOCATION", detail = e.Message };
                    return Data1;
                }
            }
            var Data = new { status = true, subject = "UPDATE LOCATION", detail = "Update location complete." };

            return Data;

        }

        public dynamic DeleteLocationMaster(ReservationMasterLocation reservationMasterLocation)
        {
            try
            {
                ReservationMasterLocation _Ctrl = _context.ReservationMasterLocation.Where(p => p.LocationId == reservationMasterLocation.LocationId).FirstOrDefault();

                if (_Ctrl != null)
                {
                    _context.ReservationMasterLocation.Remove(_Ctrl);
                    _context.SaveChanges();
                    var Data = new { status = true, subject = "DELETE LOCATION", detail = "Delete location complete." };
                    return Data;
                }
                else
                {
                    var Data = new { status = false, subject = "DELETE LOCATION", detail = "System is can't delete location." };
                    return Data;
                }
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "DELETE LOCATION", detail = e.Message };
                return Data1;
            }


        }

        public dynamic DeleteRoomMaster(ReservationMasterRoom reservationMasterRoom)
        {
            try
            {
                ReservationMasterRoom _Ctrl = _context.ReservationMasterRoom.Where(p => p.RoomId == reservationMasterRoom.RoomId).FirstOrDefault();

                var image = _context.ReservationMasterRoomImage.Where(p => p.RoomId == reservationMasterRoom.RoomId).ToList();
                var layout = _context.ReservationMasterRoomLayout.Where(p => p.RoomId == reservationMasterRoom.RoomId).ToList();

                if (_Ctrl != null)
                { //image

                    _context.ReservationMasterRoomImage.RemoveRange(image);
                    _context.ReservationMasterRoomLayout.RemoveRange(layout);
                    _context.ReservationMasterRoom.Remove(_Ctrl);
                    _context.SaveChanges();

                    foreach (var i in image)
                    {
                        if (i.Image != null)
                        {
                            File.Delete(_hostingEnvironment.WebRootPath + "/image/EReservation/Room/Room/" + i.Image);
                        }
                    }

                    foreach (var l in layout)
                    {
                        if (l.Image != null)
                        {
                            File.Delete(_hostingEnvironment.WebRootPath + "/image/EReservation/Room/Layout/" + l.Image);
                        }
                    }
                    if (_Ctrl.Image != null)
                    {
                        File.Delete(_hostingEnvironment.WebRootPath + "/image/EReservation/Room/Room/" + _Ctrl.Image);
                    }

                    var Data = new { status = true, subject = "DELETE  ROOM", detail = "Delete room complete." };
                    return Data;
                }
                else
                {
                    var Data = new { status = false, subject = "DELETE ROOM", detail = "System is can't delete room." };
                    return Data;
                }
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "DELETE ROOM", detail = e.Message };
                return Data1;
            }
        }

        public dynamic DeleteImageMaster(ReservationMasterRoomImage reservationMasterRoomImage)
        {
            try
            {
                ReservationMasterRoomImage _Ctrl = _context.ReservationMasterRoomImage.Where(p => p.RoomId == reservationMasterRoomImage.RoomId &&
                p.DisplayOrder == reservationMasterRoomImage.DisplayOrder).FirstOrDefault();
                if (_Ctrl != null)
                { //image                

                    _context.ReservationMasterRoomImage.Remove(_Ctrl);
                    _context.SaveChanges();
                    File.Delete(_hostingEnvironment.WebRootPath + "/image/EReservation/Room/Room/" + _Ctrl.Image);
                    var Data = new { status = true, subject = "DELETE  IMAGE", detail = "Delete image complete." };
                    return Data;
                }
                else
                {
                    var Data = new { status = false, subject = "DELETE IMAGE", detail = "System is can't delete image." };
                    return Data;
                }
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "DELETE IMAGE", detail = e.Message };
                return Data1;
            }
        }


        public dynamic DeleteLayoutMaster(ReservationMasterRoomLayout reservationMasterRoomLayout)
        {
            try
            {
                ReservationMasterRoomLayout _Ctrl = _context.ReservationMasterRoomLayout.Where(p => p.LayoutId == reservationMasterRoomLayout.LayoutId).FirstOrDefault();
                if (_Ctrl != null)
                { //image


                    _context.ReservationMasterRoomLayout.Remove(_Ctrl);
                    _context.SaveChanges();
                    File.Delete(_hostingEnvironment.WebRootPath + "/image/EReservation/Room/Layout/" + _Ctrl.Image);
                    var Data = new { status = true, subject = "DELETE  LAYOUT", detail = "Delete layout complete." };
                    return Data;
                }
                else
                {
                    var Data = new { status = false, subject = "DELETE LAYOUT", detail = "System is can't delete layout." };
                    return Data;
                }
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "DELETE LAYOUT", detail = e.Message };
                return Data1;
            }
        }


        public dynamic AddLocationMaster(string building, string code, string UserName, string ComputerName)
        {
            try
            {
                var maxid = _context.ReservationMasterLocation.Max(x => x.LocationId);

                var locationid = "L0001";
                if (maxid != null && maxid != "")
                {
                    var id = Convert.ToString(Convert.ToInt32(maxid.Replace("L", "")) + 1);
                    if (id.Length == 1)
                    {
                        locationid = "L000" + id;
                    }
                    else if (id.Length == 2)
                    {
                        locationid = "L00" + id;
                    }
                    else if (id.Length == 3)
                    {
                        locationid = "L0" + id;
                    }
                    else if (id.Length == 4)
                    {
                        locationid = "L" + id;
                    }
                }

                ReservationMasterLocation _Location = new ReservationMasterLocation
                {
                    LocationId = locationid,
                    Building = building,
                    Code = code,
                    ComputerName = ComputerName,
                    UserName = UserName,
                    AddDate = DateTime.Now,
                    UpdDate = DateTime.Now,
                };

                _context.ReservationMasterLocation.Add(_Location);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "ADD LOCATION", detail = e.Message };
                return Data1;
            }

            var Data = new { status = true, subject = "ADD LOCATION", detail = "Add location complete." };

            return Data;

        }

        public dynamic AddRoomMaster(string roomid, string name, string description, string locationid, bool disable,
            string ResponsibleEmail, string ResponsibleBy, string ResponsibleName, string ResponsibleTelNo, int NumberOfSeats,
            string TVConferenceIP, bool Projector, bool Computer, IFormFile files, string UserName, string ComputerName)
        {
            var RoomId = "R0001";
            var fname_new = "";

            if (roomid == null || roomid == "")
            {
                //new
                try
                {

                    var maxid = _context.ReservationMasterRoom.Max(x => x.RoomId);


                    if (maxid != null && maxid != "")
                    {
                        var id = Convert.ToString(Convert.ToInt32(maxid.Replace("R", "")) + 1);
                        if (id.Length == 1)
                        {
                            RoomId = "R000" + id;
                        }
                        else if (id.Length == 2)
                        {
                            RoomId = "R00" + id;
                        }
                        else if (id.Length == 3)
                        {
                            RoomId = "R0" + id;
                        }
                        else if (id.Length == 4)
                        {
                            RoomId = "R" + id;
                        }
                    }

                    ReservationMasterRoom _Room = new ReservationMasterRoom
                    {
                        RoomId = RoomId,
                        LocationId = locationid,
                        Name = name,
                        Description = description,
                        Disable = disable,
                        ComputerName = ComputerName,
                        UserName = UserName,
                        AddDate = DateTime.Now,
                        UpdDate = DateTime.Now,
                        ResponsibleEmail = ResponsibleEmail,
                        ResponsibleBy = ResponsibleBy,
                        ResponsibleName = ResponsibleName,
                        ResponsibleTelNo = ResponsibleTelNo,
                        TvconferenceIp = TVConferenceIP,
                        Computer = Computer,
                        Projector = Projector,
                        NumberOfSeats = NumberOfSeats
                    };



                    string folderName = "EReservation\\Room";
                    string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\" + folderName;
                    string newPath = Path.Combine(webRootPath, "Room");
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }


                    if (files != null)
                    {
                        string extension = Path.GetExtension(files.FileName);
                        fname_new = RoomId + extension;
                        _Room.Image = fname_new;
                        string fullPath = Path.Combine(newPath, fname_new);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            files.CopyTo(stream);
                        }
                    }
                    else
                    {
                        _Room.Image = null;
                    }

                    _context.ReservationMasterRoom.Add(_Room);
                    _context.SaveChanges();

                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "ADD ROOM", detail = e.Message, roomid = "", image = fname_new };
                    return Data1;
                }


            }
            else
            {
                //edit
                RoomId = roomid;
                try
                {

                    ReservationMasterRoom _Room = _context.ReservationMasterRoom.Where(p => p.RoomId == RoomId).FirstOrDefault();
                    if (_Room != null)
                    {

                        _Room.LocationId = locationid;
                        _Room.Name = name;
                        _Room.Description = description;
                        _Room.Disable = disable;
                        _Room.ComputerName = ComputerName;
                        _Room.UserName = UserName;
                        _Room.UpdDate = DateTime.Now;
                        _Room.ResponsibleEmail = ResponsibleEmail;
                        _Room.ResponsibleBy = ResponsibleBy;
                        _Room.ResponsibleName = ResponsibleName;
                        _Room.ResponsibleTelNo = ResponsibleTelNo;
                        _Room.TvconferenceIp = TVConferenceIP;
                        _Room.NumberOfSeats = NumberOfSeats;
                        _Room.Computer = Computer;
                        _Room.Projector = Projector;


                        string folderName = "EReservation\\Room";
                        string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\" + folderName;
                        string newPath = Path.Combine(webRootPath, "Room");
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }


                        if (files != null)
                        {
                            string extension = Path.GetExtension(files.FileName);
                            fname_new = RoomId + extension;
                            _Room.Image = fname_new;
                            string fullPath = Path.Combine(newPath, fname_new);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                files.CopyTo(stream);
                            }

                        }
                        else
                        {
                            //_Room.Image = null;
                        }

                        _context.ReservationMasterRoom.Update(_Room);
                        _context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "UPDATE ROOM", detail = e.Message, roomid = RoomId, image = fname_new };
                    return Data1;
                }
            }

            var Data = new { status = true, subject = "ADD ROOM", detail = "Add room complete.", roomid = RoomId, image = fname_new };

            return Data;
        }





        public dynamic AddImageRoomMaster(string roomId, int displayOrder, string description, IFormFile files, string UserName, string ComputerName)
        {

            try
            {

                //new  
                var _chk = _context.ReservationMasterRoomImage.Where(x => x.RoomId == roomId && x.DisplayOrder == displayOrder).FirstOrDefault();
                if (_chk == null)
                {
                    if (files != null)
                    {
                        ReservationMasterRoomImage _Image = new ReservationMasterRoomImage
                        {
                            RoomId = roomId,
                            DisplayOrder = displayOrder,
                            Description = description,
                            ComputerName = ComputerName,
                            UserName = UserName,
                            AddDate = DateTime.Now,
                            UpdDate = DateTime.Now,
                        };

                        string folderName = "EReservation\\Room";
                        string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\" + folderName;
                        string newPath = Path.Combine(webRootPath, "Room");
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }


                        if (files != null)
                        {
                            string extension = Path.GetExtension(files.FileName);
                            string fname_new = roomId + "_" + Convert.ToString(displayOrder) + extension;
                            _Image.Image = fname_new;
                            string fullPath = Path.Combine(newPath, fname_new);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                files.CopyTo(stream);
                            }
                        }


                        _context.ReservationMasterRoomImage.Add(_Image);
                        _context.SaveChanges();

                    }
                    else
                    {
                        var Data1 = new { status = false, subject = "ADD IMAGE", detail = "Image not valid" };
                        return Data1;
                    }
                }
                else
                {
                    //edit
                    _chk.Description = description;
                    _chk.ComputerName = ComputerName;
                    _chk.UserName = UserName;
                    _chk.UpdDate = DateTime.Now;


                    string folderName = "EReservation\\Room";
                    string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\" + folderName;
                    string newPath = Path.Combine(webRootPath, "Room");
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }


                    if (files != null)
                    {
                        string extension = Path.GetExtension(files.FileName);
                        string fname_new = roomId + "_" + Convert.ToString(displayOrder) + extension;
                        _chk.Image = fname_new;
                        string fullPath = Path.Combine(newPath, fname_new);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            files.CopyTo(stream);
                        }
                    }


                    _context.ReservationMasterRoomImage.Update(_chk);
                    _context.SaveChanges();

                }

            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "UPDATE IMAGE", detail = e.Message };
                return Data1;
            }
            var Data = new { status = true, subject = "ADD IMAGE", detail = "Add image complete." };

            return Data;
        }

        public dynamic AddLayoutRoomMaster(string layoutId, string roomid, int displayOrder, string description, bool disable, IFormFile files, string UserName, string ComputerName)
        {

            var LayoutId = "L0001";

            if (layoutId == null || layoutId == "")
            {
                if (files != null)
                {
                    //new
                    try
                    {

                        var maxid = _context.ReservationMasterRoomLayout.Max(x => x.LayoutId);


                        if (maxid != null && maxid != "")
                        {
                            var id = Convert.ToString(Convert.ToInt32(maxid.Replace("L", "")) + 1);
                            if (id.Length == 1)
                            {
                                LayoutId = "L000" + id;
                            }
                            else if (id.Length == 2)
                            {
                                LayoutId = "L00" + id;
                            }
                            else if (id.Length == 3)
                            {
                                LayoutId = "L0" + id;
                            }
                            else if (id.Length == 4)
                            {
                                LayoutId = "L" + id;
                            }
                        }

                        ReservationMasterRoomLayout _Layout = new ReservationMasterRoomLayout
                        {
                            LayoutId = LayoutId,
                            RoomId = roomid,
                            DisplayOrder = displayOrder,
                            Description = description,
                            Disable = disable,
                            ComputerName = ComputerName,
                            UserName = UserName,
                            AddDate = DateTime.Now,
                            UpdDate = DateTime.Now,
                        };



                        string folderName = "EReservation\\Room";
                        string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\" + folderName;
                        string newPath = Path.Combine(webRootPath, "Layout");
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }


                        if (files != null)
                        {
                            string extension = Path.GetExtension(files.FileName);
                            string fname_new = LayoutId + extension;
                            _Layout.Image = fname_new;
                            string fullPath = Path.Combine(newPath, fname_new);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                files.CopyTo(stream);
                            }
                        }


                        _context.ReservationMasterRoomLayout.Add(_Layout);
                        _context.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        var Data1 = new { status = false, subject = "ADD LAYOUT", detail = e.Message, roomid = "" };
                        return Data1;
                    }

                }
                else
                {
                    var Data1 = new { status = false, subject = "ADD LAYOUT", detail = "Image not valid" };
                    return Data1;
                }
            }
            else
            {
                //edit
                LayoutId = layoutId;
                try
                {

                    ReservationMasterRoomLayout _Layout = _context.ReservationMasterRoomLayout.Where(p => p.LayoutId == LayoutId).FirstOrDefault();
                    if (_Layout != null)
                    {

                        _Layout.RoomId = roomid;
                        _Layout.DisplayOrder = displayOrder;
                        _Layout.Description = description;
                        _Layout.Disable = disable;
                        _Layout.ComputerName = ComputerName;
                        _Layout.UserName = UserName;
                        _Layout.UpdDate = DateTime.Now;


                        string folderName = "EReservation\\Room";
                        string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\" + folderName;
                        string newPath = Path.Combine(webRootPath, "Layout");
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }


                        if (files != null)
                        {
                            string extension = Path.GetExtension(files.FileName);
                            string fname_new = LayoutId + extension;
                            _Layout.Image = fname_new;
                            string fullPath = Path.Combine(newPath, fname_new);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                files.CopyTo(stream);
                            }
                        }


                        _context.ReservationMasterRoomLayout.Update(_Layout);
                        _context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "UPDATE LAYOUT", detail = e.Message };
                    return Data1;
                }
            }

            var Data = new { status = true, subject = "ADD LAYOUT", detail = "Add layout complete." };

            return Data;

        }
    }
}
