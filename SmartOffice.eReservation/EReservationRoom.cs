using System;
using SmartOffice.EReservation.IResponsitory;
using System.Collections.Generic;
using System.Linq;
using SmartOffice.eReservation.ModelsDocControl;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SmartOffice.EmailCore.IResponsitory;

namespace SmartOffice.EReservation
{
    public class EReservationRoomControl : IEReservationRoom
    {
        private readonly EReservationContext _context;

        private IHostingEnvironment _hostingEnvironment;
        private readonly ISendEmail _Sendmail;

        public EReservationRoomControl(EReservationContext context, IHostingEnvironment hostingEnvironment, ISendEmail Sendmail)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _Sendmail = Sendmail;
        }

        //public dynamic GetResources()
        //{

        //    var results = from A in _context.ReservationMasterLocation
        //                  join B in _context.ReservationMasterRoom on A.LocationId equals B.LocationId
        //                  where B.Disable == true
        //                  select new
        //                  {
        //                      id = B.RoomId,
        //                      title = B.Name,
        //                      building = A.Building,
        //                      image = B.Image,
        //                      desc = B.Description

        //                  };

        //    return results.Distinct().ToList().OrderBy(x => x.building).ThenBy(x => x.title);
        //}
        public async Task<dynamic> GetResourcesfillter(string Searchtext)
        {


            var results = from A in _context.ReservationMasterLocation
                          join B in _context.ReservationMasterRoom on A.LocationId equals B.LocationId
                          where B.Disable == true
                          select new
                          {
                              id = B.RoomId,
                              text = A.Building + ":" + B.Name,
                          };

            if (Searchtext == "" || Searchtext == null)
            {
                return await results.Distinct().OrderBy(x => x.text).ToListAsync();
            }
            var fillter = results.Where(i => i.text.Contains(Searchtext.ToUpper())).Distinct().OrderBy(x => x.text);
            return await fillter.ToListAsync();
        }

        public async Task<dynamic> GetResources(List<string> getResources)
        {

            var results = from A in _context.ReservationMasterLocation
                          join B in _context.ReservationMasterRoom on A.LocationId equals B.LocationId
                          where B.Disable == true && ((getResources.Contains(B.RoomId) && getResources.FirstOrDefault() != null) || (getResources.FirstOrDefault() == null))
                          select new
                          {
                              id = B.RoomId,
                              title = B.Name,
                              building = A.Building,
                              image = B.Image,
                              desc = B.Description,
                              seats = B.NumberOfSeats,
                              projector = B.Projector,
                              computer = B.Computer,
                              tvconferenceIp = B.TvconferenceIp,
                              responsibleBy = B.ResponsibleBy,
                              responsibleEmail = B.ResponsibleEmail,
                              responsibleName = B.ResponsibleName,
                              responsibleTelNo = B.ResponsibleTelNo,
                              code = A.Code

                          };

            return await results.Distinct().OrderBy(x => x.building).ThenBy(x => x.title).ToListAsync();

        }


        public dynamic GetLayout(string RoomId)
        {

            var results = _context.ReservationMasterRoomLayout.Where(x => x.Disable == true && x.RoomId == RoomId).ToList().OrderBy(x => x.DisplayOrder);


            return results;
        }

        public List<ReservationMasterRoom> Getimageh()
        {

            var results = _context.ReservationMasterRoom.Where(x=>x.Disable == true);


            return results.ToList();
        }

        public List<ReservationMasterRoomImage> Getimaged(string RoomId)
        {

            var results = _context.ReservationMasterRoomImage.Where(x => x.RoomId == RoomId).OrderBy(x => x.DisplayOrder);


            return results.ToList();
        }

        public dynamic GetEvents(string Year, string Month)
        {

            var resources = from A in _context.ReservationMasterLocation
                            join B in _context.ReservationMasterRoom on A.LocationId equals B.LocationId
                            where B.Disable == true
                            select new
                            {
                                id = B.RoomId,
                                title = B.Name,
                                building = A.Building,
                                image = B.Image,
                                desc = B.Description,
                                code = A.Code,
                                responsibleEmail = B.ResponsibleEmail
                            };

            var results = from A in resources
                          join C in _context.ReservationRoom on A.id equals C.RoomId
                          where  (C.EndDate.Value.Year == int.Parse(Year) || C.EndDate.Value.Year == int.Parse(Year) + 1) && 
                          (C.EndDate.Value.Month == int.Parse(Month) || C.EndDate.Value.Month == int.Parse(Month) + 1)
                          select new
                          {
                              reservationId = C.ReservationId,
                              resourceId = C.RoomId,
                              start = Convert.ToDateTime(C.StartDate).ToString("yyyy/MM/dd HH:mm"),
                              end = Convert.ToDateTime(C.EndDate).ToString("yyyy/MM/dd HH:mm"),
                              operatorId = C.OperatorId,
                              operatorName = C.OperatorName,
                              requestDate = Convert.ToDateTime(C.RequestDate).ToString("yyyy/MM/dd HH:mm"),
                              updateDate = Convert.ToDateTime(C.UpdateDate).ToString("yyyy/MM/dd HH:mm"),
                              operatorOrganizationname = C.OperatorOrganizationname,
                              operatorPhoneNumber = C.OperatorPhoneNumber,
                              totalOperator = C.TotalOperator,
                              layoutId = C.LayoutId,
                              title = C.Subject,
                              editable = false,
                              nameroom = A.title,
                              building = A.building,
                              image = A.image,
                              desc = A.desc,
                              code = A.code,
                              operatorbldg = C.OperatorBldg,
                              bldg = C.Bldg,
                              status = C.Status,
                              responsibleEmail = A.responsibleEmail,
                              remarks = C.Remarks
                          };

            return results.Distinct().ToList();
        }

        public async Task<dynamic> SaveEvent(List<IFormFile> files, int ReservationId, string RoomId, string StartDate, string EndDate,
            string OperatorId, string OperatorName, string OperatorBldg, string Bldg, string OperatorOrganizationname, string OperatorPhoneNumber, string RequestDate, string UpdateDate,
            string TotalOperator, string LayoutId, string Subject, string mailuserrequest, string Datemultiple)
        {
            var ReservationIddata = ReservationId;
            var Type = "Room";

            if (Convert.ToDateTime(EndDate) >= DateTime.Now && Convert.ToDateTime(StartDate).AddMinutes(25) >= DateTime.Now)
            {

                try
                {
                    var checksatatus = "";


                    if (ReservationId > 0)
                    {
                        //Update the event
                        var v = _context.ReservationRoom.Where(a => a.ReservationId == ReservationId).FirstOrDefault();
                        if (v != null)
                        {
                            checksatatus = v.Status;

                            if (checksatatus == "2")
                            {
                                var Datadenied = new { status = false, subject = "Oops...", detail = "This booking has been rejected by room attendant.Please delete from the system.", type = "error" };
                                return Datadenied;
                            }

                            v.Subject = Subject;
                            v.UpdateDate = DateTime.Now;
                            v.TotalOperator = TotalOperator == "" || TotalOperator == null ? 0 : Convert.ToInt16(TotalOperator);
                            v.LayoutId = LayoutId;
                            v.OperatorOrganizationname = OperatorOrganizationname;
                            v.OperatorPhoneNumber = OperatorPhoneNumber;
                            v.OperatorBldg = OperatorBldg;
                            v.Bldg = Bldg;
                            v.OperatorEmail = mailuserrequest;
                        }
                        else
                        {
                            var Datadenied = new { status = false, subject = "Oops...", detail = "Reservation Permission denied", type = "error" };
                            return Datadenied;
                        }
                    }
                    else
                    {
                        var start = Convert.ToDateTime(StartDate);
                        var end = Convert.ToDateTime(EndDate);
                        var checkdata = _context.ReservationRoom.Where(x => x.RoomId == RoomId && start == x.StartDate && end == x.EndDate).FirstOrDefault();
                        var checkdata1 = _context.ReservationRoom.Where(x => x.RoomId == RoomId && (start == x.StartDate || end == x.EndDate)).FirstOrDefault();
                        var checkdata2 = _context.ReservationRoom.Where(x => x.RoomId == RoomId && (start <= x.StartDate && end >= x.EndDate)).FirstOrDefault();
                        var checkdata3 = _context.ReservationRoom.Where(x => x.RoomId == RoomId && (start < x.EndDate && end >= x.EndDate)).FirstOrDefault();
                        var checkdata4 = _context.ReservationRoom.Where(x => x.RoomId == RoomId && (start >= x.StartDate && end <= x.EndDate)).FirstOrDefault();
                        var checkdata5 = _context.ReservationRoom.Where(x => x.RoomId == RoomId && (start < x.StartDate && end > x.StartDate)).FirstOrDefault();

                        if (checkdata == null && checkdata1 == null && checkdata2 == null && checkdata3 == null && checkdata4 == null && checkdata5 == null)
                        {

                            if (OperatorBldg == Bldg)
                            {
                                checksatatus = "0";
                            }
                            else
                            {
                                checksatatus = "1";
                            }

                            //add  the event                   
                            var data = new ReservationRoom
                            {

                                RoomId = RoomId,
                                StartDate = Convert.ToDateTime(StartDate),
                                EndDate = Convert.ToDateTime(EndDate),
                                Subject = Subject,
                                RequestDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
                                TotalOperator = TotalOperator == "" || TotalOperator == null ? 0 : Convert.ToInt16(TotalOperator),
                                LayoutId = LayoutId,
                                OperatorId = OperatorId,
                                OperatorName = OperatorName,
                                OperatorOrganizationname = OperatorOrganizationname,
                                OperatorPhoneNumber = OperatorPhoneNumber,
                                OperatorBldg = OperatorBldg,
                                Bldg = Bldg,
                                Status = checksatatus,
                                OperatorEmail = mailuserrequest

                            };
                            await _context.ReservationRoom.AddAsync(data);
                            await _context.SaveChangesAsync();
                            ReservationIddata = data.ReservationId;
                        }
                        else
                        {
                            var Datadenied = new { status = false, subject = "Oops...", detail = "This room cannot be reserved. Please choose another date range.", type = "warning" };
                            return Datadenied;
                        }

                    }
                    //attach
                    List<ReservationAttachFile> appopr = new List<ReservationAttachFile>();
                    foreach (IFormFile item in files)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"').Replace(" ", "");
                        ReservationAttachFile attfile = _context.ReservationAttachFile.Where(p => p.ReservationId == ReservationIddata && p.Type == Type && p.Filename == fileName).FirstOrDefault();
                        if (attfile == null)
                        {

                            //new

                            string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + "EReservation\\" + Type;
                            string newPath = Path.Combine(webRootPath, Convert.ToString(ReservationIddata));
                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }
                            string extension = Path.GetExtension(item.FileName);
                            string fullPath = Path.Combine(newPath, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                item.CopyTo(stream);
                            }

                            ReservationAttachFile iitem = new ReservationAttachFile
                            {
                                ReservationId = ReservationIddata,
                                Type = Type,
                                Filename = fileName,
                            };
                            appopr.Add(iitem);
                        }
                        else
                        {
                            //have
                            attfile.Filename = fileName;

                            File.Delete(_hostingEnvironment.WebRootPath + "/Attach/" + "EReservation/" + Type + "/" + Convert.ToString(ReservationIddata) + "/" + attfile.Filename);

                            string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + "EReservation\\" + Type;
                            string newPath = Path.Combine(webRootPath, Convert.ToString(ReservationIddata));
                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }
                            string extension = Path.GetExtension(item.FileName);
                            string fullPath = Path.Combine(newPath, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                item.CopyTo(stream);
                            }
                        }
                    }
                    _context.ReservationAttachFile.AddRange(appopr);
                    _context.SaveChanges();

                    var room = _context.ReservationMasterRoom.Where(i => i.RoomId == RoomId).FirstOrDefault();
                    var location = _context.ReservationMasterLocation.Where(i => i.LocationId == room.LocationId).FirstOrDefault();
                    var title = location.Building + " " + room.Name;
                    var date = Convert.ToDateTime(StartDate).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(EndDate).ToString("dd/MM/yyyy HH :mm");
                    var user = "Book a meeting room by : " + OperatorName + " " + OperatorOrganizationname + "  " + OperatorPhoneNumber;

                    var subject = "Meeting room reservation notification ";
                    if (checksatatus == "1") //ไม่ใช่ตึกตัวเอง
                    {
                        subject = "Waiting for you to approve meeting room reservations ";
                    }

                    var detail = Subject;



                    if (checksatatus == "1") //ไม่ใช่ตึกตัวเอง
                    {
                        if (room.ResponsibleEmail != "")
                        {
                            _Sendmail.EmailReservationRoom(room.ResponsibleEmail, mailuserrequest, subject, title, date, user, detail);
                        }
                    }
                    else if (checksatatus == "0")
                    {
                        if (mailuserrequest != "")
                        {
                            _Sendmail.EmailReservationRoom(mailuserrequest, room.ResponsibleEmail, subject, title, date, user, detail);
                        }
                    }





                    //.................................................................................................................................................

                    //case multiple day 
                    var MsgDatemultiple = "";
                    if (Datemultiple != "" && Datemultiple != null)
                    {
                        string[] words = Datemultiple.Split(',');
                        foreach (string word in words)
                        {
                            var ReservationIddataDatemultiple = 0;
                            var start = Convert.ToDateTime(word + " " + Convert.ToDateTime(StartDate).ToString("HH:mm"));
                            var end = Convert.ToDateTime(word + " " + Convert.ToDateTime(EndDate).ToString("HH:mm"));
                            bool mail = false;

                            var checkdata = _context.ReservationRoom.Where(x => x.RoomId == RoomId && start == x.StartDate && end == x.EndDate).FirstOrDefault();

                            var checkdata1 = _context.ReservationRoom.Where(x => x.RoomId == RoomId && (start == x.StartDate || end == x.EndDate)).FirstOrDefault();
                            var checkdata2 = _context.ReservationRoom.Where(x => x.RoomId == RoomId && (start <= x.StartDate && end >= x.EndDate)).FirstOrDefault();
                            var checkdata3 = _context.ReservationRoom.Where(x => x.RoomId == RoomId && (start < x.EndDate && end >= x.EndDate)).FirstOrDefault();
                            var checkdata4 = _context.ReservationRoom.Where(x => x.RoomId == RoomId && (start >= x.StartDate && end <= x.EndDate)).FirstOrDefault();
                            var checkdata5 = _context.ReservationRoom.Where(x => x.RoomId == RoomId && (start < x.StartDate && end > x.StartDate)).FirstOrDefault();

                            if (checkdata == null && checkdata1 == null && checkdata2 == null && checkdata3 == null && checkdata4 == null && checkdata5 == null)
                            {
                                //Add

                                var data = new ReservationRoom
                                {

                                    RoomId = RoomId,
                                    StartDate = start,
                                    EndDate = end,
                                    Subject = Subject,
                                    RequestDate = DateTime.Now,
                                    UpdateDate = DateTime.Now,
                                    TotalOperator = TotalOperator == "" || TotalOperator == null ? 0 : Convert.ToInt16(TotalOperator),
                                    LayoutId = LayoutId,
                                    OperatorId = OperatorId,
                                    OperatorName = OperatorName,
                                    OperatorOrganizationname = OperatorOrganizationname,
                                    OperatorPhoneNumber = OperatorPhoneNumber,
                                    OperatorBldg = OperatorBldg,
                                    Bldg = Bldg,
                                    Status = checksatatus,
                                    OperatorEmail = mailuserrequest

                                };
                                await _context.ReservationRoom.AddAsync(data);
                                await _context.SaveChangesAsync();
                                ReservationIddataDatemultiple = data.ReservationId;

                                MsgDatemultiple = MsgDatemultiple + " " + word;
                                mail = true;
                            }
                            else if (checkdata != null)
                            {
                                //Edit
                                if (checkdata.OperatorId == OperatorId)
                                {
                                    //My
                                    checkdata.Subject = Subject;
                                    checkdata.UpdateDate = DateTime.Now;
                                    checkdata.TotalOperator = TotalOperator == "" || TotalOperator == null ? 0 : Convert.ToInt16(TotalOperator);
                                    checkdata.LayoutId = LayoutId;
                                    checkdata.OperatorOrganizationname = OperatorOrganizationname;
                                    checkdata.OperatorPhoneNumber = OperatorPhoneNumber;
                                    checkdata.OperatorBldg = OperatorBldg;
                                    checkdata.Bldg = Bldg;
                                    checkdata.OperatorEmail = mailuserrequest;

                                    ReservationIddataDatemultiple = checkdata.ReservationId;
                                    MsgDatemultiple = MsgDatemultiple + " " + word;
                                    mail = true;
                                }
                            }

                            if (mail == true)
                            {
                                //attach
                                List<ReservationAttachFile> appoprDatemultiple = new List<ReservationAttachFile>();
                                foreach (IFormFile item in files)
                                {
                                    string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"').Replace(" ", "");
                                    ReservationAttachFile attfile = _context.ReservationAttachFile.Where(p => p.ReservationId == ReservationIddataDatemultiple && p.Type == Type && p.Filename == fileName).FirstOrDefault();
                                    if (attfile == null)
                                    {

                                        //new

                                        string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + "EReservation\\" + Type;
                                        string newPath = Path.Combine(webRootPath, Convert.ToString(ReservationIddataDatemultiple));
                                        if (!Directory.Exists(newPath))
                                        {
                                            Directory.CreateDirectory(newPath);
                                        }
                                        string extension = Path.GetExtension(item.FileName);
                                        string fullPath = Path.Combine(newPath, fileName);
                                        using (var stream = new FileStream(fullPath, FileMode.Create))
                                        {
                                            item.CopyTo(stream);
                                        }

                                        ReservationAttachFile iitem = new ReservationAttachFile
                                        {
                                            ReservationId = ReservationIddataDatemultiple,
                                            Type = Type,
                                            Filename = fileName,
                                        };
                                        appoprDatemultiple.Add(iitem);
                                    }
                                    else
                                    {
                                        //have
                                        attfile.Filename = fileName;

                                        File.Delete(_hostingEnvironment.WebRootPath + "/Attach/" + "EReservation/" + Type + "/" + Convert.ToString(ReservationIddataDatemultiple) + "/" + attfile.Filename);

                                        string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + "EReservation\\" + Type;
                                        string newPath = Path.Combine(webRootPath, Convert.ToString(ReservationIddataDatemultiple));
                                        if (!Directory.Exists(newPath))
                                        {
                                            Directory.CreateDirectory(newPath);
                                        }
                                        string extension = Path.GetExtension(item.FileName);
                                        string fullPath = Path.Combine(newPath, fileName);
                                        using (var stream = new FileStream(fullPath, FileMode.Create))
                                        {
                                            item.CopyTo(stream);
                                        }
                                    }
                                }
                                _context.ReservationAttachFile.AddRange(appoprDatemultiple);
                                _context.SaveChanges();

                                var roomDatemultiple = _context.ReservationMasterRoom.Where(i => i.RoomId == RoomId).FirstOrDefault();
                                var locationDatemultiple = _context.ReservationMasterLocation.Where(i => i.LocationId == roomDatemultiple.LocationId).FirstOrDefault();

                                var titleDatemultiple = locationDatemultiple.Building + " " + roomDatemultiple.Name;
                                var dateDatemultiple = Convert.ToDateTime(start).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(end).ToString("dd/MM/yyyy HH :mm");
                                var userDatemultiple = "Book a meeting room by : " + OperatorName + " " + OperatorOrganizationname + "  " + OperatorPhoneNumber;

                                var subjectDatemultiple = "Meeting room reservation notification ";
                                if (checksatatus == "1") //ไม่ใช่ตึกตัวเอง
                                {
                                    subjectDatemultiple = "Waiting for you to approve meeting room reservations ";
                                }
                                var detailDatemultiple = Subject;


                                if (checksatatus == "1") //ไม่ใช่ตึกตัวเอง
                                {
                                    if (roomDatemultiple.ResponsibleEmail != "")
                                    {
                                        _Sendmail.EmailReservationRoom(roomDatemultiple.ResponsibleEmail, mailuserrequest, subjectDatemultiple, titleDatemultiple, dateDatemultiple, userDatemultiple, detailDatemultiple);
                                    }
                                }
                                else if (checksatatus == "0")
                                {
                                    if (mailuserrequest != "")
                                    {
                                        _Sendmail.EmailReservationRoom(mailuserrequest, roomDatemultiple.ResponsibleEmail, subjectDatemultiple, titleDatemultiple, dateDatemultiple, userDatemultiple, detailDatemultiple);
                                    }
                                }

                            }
                        }
                    }


                    var Data = new { status = true, subject = "Success", detail = "Reservation complete + " + MsgDatemultiple, type = "success" };
                    return Data;
                }
                catch (Exception ex)
                {
                    var Data = new { status = false, subject = "Oops...", detail = ex.ToString(), type = "error" };
                    return Data;
                }
            }
            else
            {
                var Data = new { status = false, subject = "Oops...", detail = "Cannot reservation", type = "error" };
                return Data;
            }

        }

        public dynamic DeleteEvent(int reservationId, string mailuserrequest)
        {
            try
            {
                var Data = new { status = true, subject = "Success", detail = "Delete complete", type = "success" };

                var v = _context.ReservationRoom.Where(a => a.ReservationId == reservationId).FirstOrDefault();
                var attach = _context.ReservationAttachFile.Where(a => a.ReservationId == reservationId && a.Type == "Room");

                if (v != null)
                {
                    if (v.EndDate < DateTime.Now && v.Status != "2")
                    {
                        Data = new { status = false, subject = "Oops...", detail = "Cannot Delete old data", type = "error" };
                    }
                    else
                    {
                        _context.ReservationRoom.Remove(v);
                        //attach
                        if (attach != null)
                        {
                            _context.ReservationAttachFile.RemoveRange(attach);

                            foreach (var item in attach)
                            {

                                File.Delete(_hostingEnvironment.WebRootPath + "/Attach/" + "EReservation/" + item.Type + "/" + Convert.ToString(item.ReservationId) + "/" + item.Filename);

                            }

                        }
                        _context.SaveChanges();

                        var room = _context.ReservationMasterRoom.Where(i => i.RoomId == v.RoomId).FirstOrDefault();
                        var location = _context.ReservationMasterLocation.Where(i => i.LocationId == room.LocationId).FirstOrDefault();
                        var subject = "Delete meeting room reservation ";
                        var title = location.Building + " " + room.Name;
                        var date = Convert.ToDateTime(v.StartDate).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(v.EndDate).ToString("dd/MM/yyyy HH :mm");
                        var user = "Delete a meeting room reservation by : " + v.OperatorName + " " + v.OperatorOrganizationname + "  " + v.OperatorPhoneNumber;
                        var detail = v.Subject;
                        if (mailuserrequest != "")
                        {

                            _Sendmail.EmailReservationRoom(mailuserrequest, room.ResponsibleEmail, subject, title, date, user, detail);

                        }

                    }

                }
                return Data;
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "error", detail = e.Message, type = "error" };
                return Data1;
            }
        }


        public dynamic ApproveEvent(int reservationId, string mailuserapprove, string userapprove, string nameapprove, string remarks)
        {
            try
            {
                var Data = new { status = true, subject = "Success", detail = "Confirm complete", type = "success" };

                //Update the event
                var v = _context.ReservationRoom.Where(a => a.ReservationId == reservationId && a.Status == "1").FirstOrDefault();
                if (v != null)
                {
                    v.Status = "0";
                    v.ApproveID = userapprove;
                    v.ApproveDate = DateTime.Now;
                    v.Remarks = remarks;
                    _context.SaveChanges();

                    //sent mail
                    var room = _context.ReservationMasterRoom.Where(i => i.RoomId == v.RoomId).FirstOrDefault();
                    var location = _context.ReservationMasterLocation.Where(i => i.LocationId == room.LocationId).FirstOrDefault();
                    var subject = "Confirmed meeting room reservations ";
                    var title = location.Building + " " + room.Name;
                    var date = Convert.ToDateTime(v.StartDate).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(v.EndDate).ToString("dd/MM/yyyy HH :mm");
                    var user = "Confirmed a meeting room reservation by : " + nameapprove + " Remark >> " + remarks;
                    var detail = v.Subject;
                    if (v.OperatorEmail != "")
                    {
                        _Sendmail.EmailReservationRoom(v.OperatorEmail, "", subject, title, date, user, detail);
                    }
                }

                return Data;
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "error", detail = e.Message, type = "error" };
                return Data1;
            }
        }


        public dynamic RejectEvent(int reservationId, string mailuserapprove, string userapprove, string nameapprove, string remarks)
        {
            try
            {
                var Data = new { status = true, subject = "Success", detail = "Reject complete", type = "success" };

                var v = _context.ReservationRoom.Where(a => a.ReservationId == reservationId && a.Status == "1").FirstOrDefault();
                if (v != null)
                {
                    v.Status = "2";
                    v.ApproveID = userapprove;
                    v.ApproveDate = DateTime.Now;
                    v.Remarks = remarks;
                    _context.SaveChanges();

                    //sent mail
                    var room = _context.ReservationMasterRoom.Where(i => i.RoomId == v.RoomId).FirstOrDefault();
                    var location = _context.ReservationMasterLocation.Where(i => i.LocationId == room.LocationId).FirstOrDefault();
                    var subject = "Reject meeting room reservations ";
                    var title = location.Building + " " + room.Name;
                    var date = Convert.ToDateTime(v.StartDate).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(v.EndDate).ToString("dd/MM/yyyy HH :mm");
                    var user = "Reject a meeting room reservation by : " + nameapprove + " Remark >> " + remarks + " ,Please delete from the system.";
                    var detail = v.Subject;
                    if (v.OperatorEmail != "")
                    {
                        _Sendmail.EmailReservationRoom(v.OperatorEmail, "", subject, title, date, user, detail);
                    }
                }

                return Data;
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "error", detail = e.Message, type = "error" };
                return Data1;
            }
        }


        //------------------------------------Start Attach File--------------------------------------------------------------------------------------
        //Get File Attach
        public List<ReservationAttachFile> GetFileAttachdata(int ReservationId, string Type)
        {

            List<ReservationAttachFile> allfile = _context.ReservationAttachFile.Where(p => p.ReservationId == ReservationId && p.Type == Type).ToList().Select(p => new ReservationAttachFile()
            {
                ReservationId = p.ReservationId,
                Type = p.Type,
                Filename = p.Filename,

            }).ToList();
            return allfile;

        }


        //Delete Attach file
        public dynamic Deletefile(int reservationId, string type, string filename, string end)
        {
            try
            {
                if (Convert.ToDateTime(end) < DateTime.Now)
                {
                    var Data = new { status = false, subject = "Oops...", detail = "Cannot Delete old data" };
                    return Data;
                }
                else
                {
                    var attfile = _context.ReservationAttachFile.Where(p => p.ReservationId == reservationId && p.Type == type && p.Filename == filename).FirstOrDefault();

                    if (attfile != null)
                    {

                        _context.ReservationAttachFile.Remove(attfile);
                        _context.SaveChangesAsync();

                        File.Delete(_hostingEnvironment.WebRootPath + "/Attach/" + "EReservation/" + type + "/" + Convert.ToString(reservationId) + "/" + attfile.Filename);

                        var Data = new { status = true, subject = "Delete File", detail = "Delete File Complete." };
                        return Data;
                    }
                    else
                    {
                        var Data = new { status = false, subject = "Delete File", detail = "System is can't delete File." };
                        return Data;
                    }
                }
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "Delete File", detail = e.Message };
                return Data1;
            }
        }
        //------------------------------------End Attach File--------------------------------------------------------------------------------------
    }
}
