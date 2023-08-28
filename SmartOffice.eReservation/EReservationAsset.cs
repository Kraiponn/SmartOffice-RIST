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
    public class EReservationAssetControl : IEReservationAsset
    {
        private readonly EReservationContext _context;

        private IHostingEnvironment _hostingEnvironment;
        private readonly ISendEmail _Sendmail;

        public EReservationAssetControl(EReservationContext context, IHostingEnvironment hostingEnvironment, ISendEmail Sendmail)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _Sendmail = Sendmail;
        }

        //public dynamic GetResources()
        //{

        //    var results = from A in _context.ReservationMasterLocation
        //                  join B in _context.ReservationMasterAsset on A.LocationId equals B.LocationId
        //                  where B.Disable == true
        //                  select new
        //                  {
        //                      id = B.AssetId,
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
                          join B in _context.ReservationMasterAsset on A.LocationId equals B.LocationId
                          where B.Disable == true
                          select new
                          {
                              id = B.AssetId,
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
                          join B in _context.ReservationMasterAsset on A.LocationId equals B.LocationId
                          where B.Disable == true && ((getResources.Contains(B.AssetId) && getResources.FirstOrDefault() != null) || (getResources.FirstOrDefault() == null))
                          select new
                          {
                              id = B.AssetId,
                              title = B.Name,
                              building = A.Building,
                              image = B.Image,
                              desc = B.Description,
                              responsibleBy = B.ResponsibleBy,
                              responsibleEmail = B.ResponsibleEmail,
                              responsibleName = B.ResponsibleName,
                              responsibleTelNo = B.ResponsibleTelNo,
                              code = A.Code

                          };

            return await results.Distinct().OrderBy(x => x.building).ThenBy(x => x.title).ToListAsync();

        }


        public dynamic GetLayout(string AssetId)
        {

            var results = _context.ReservationMasterAssetLayout.Where(x => x.Disable == true && x.AssetId == AssetId).ToList().OrderBy(x => x.DisplayOrder);


            return results;
        }

        public List<ReservationMasterAsset> Getimageh()
        {

            var results = _context.ReservationMasterAsset.Where(x=>x.Disable == true);


            return results.ToList();
        }

        public List<ReservationMasterAssetImage> Getimaged(string AssetId)
        {

            var results = _context.ReservationMasterAssetImage.Where(x => x.AssetId == AssetId).OrderBy(x => x.DisplayOrder);


            return results.ToList();
        }

        public dynamic GetEvents(string Year, string Month)
        {

            var resources = from A in _context.ReservationMasterLocation
                            join B in _context.ReservationMasterAsset on A.LocationId equals B.LocationId
                            where B.Disable == true
                            select new
                            {
                                id = B.AssetId,
                                title = B.Name,
                                building = A.Building,
                                image = B.Image,
                                desc = B.Description,
                                code = A.Code,
                                responsibleEmail = B.ResponsibleEmail
                            };

            var results = from A in resources
                          join C in _context.ReservationAsset on A.id equals C.AssetId
                          where (C.EndDate.Value.Year == int.Parse(Year) || C.EndDate.Value.Year == int.Parse(Year) + 1) &&
                          (C.EndDate.Value.Month == int.Parse(Month) || C.EndDate.Value.Month == int.Parse(Month) + 1)
                          select new
                          {
                              reservationId = C.ReservationId,
                              resourceId = C.AssetId,
                              start = Convert.ToDateTime(C.StartDate).ToString("yyyy/MM/dd HH:mm"),
                              end = Convert.ToDateTime(C.EndDate).ToString("yyyy/MM/dd HH:mm"),
                              operatorId = C.OperatorId,
                              operatorName = C.OperatorName,
                              requestDate = Convert.ToDateTime(C.RequestDate).ToString("yyyy/MM/dd HH:mm"),
                              updateDate = Convert.ToDateTime(C.UpdateDate).ToString("yyyy/MM/dd HH:mm"),
                              operatorOrganizationname = C.OperatorOrganizationname,
                              operatorPhoneNumber = C.OperatorPhoneNumber,
                              layoutId = C.LayoutId,
                              title = C.Subject,
                              editable = false,
                              nameasset = A.title,
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

        public async Task<dynamic> SaveEvent(List<IFormFile> files, int ReservationId, string AssetId, string StartDate, string EndDate,
            string OperatorId, string OperatorName, string OperatorBldg, string Bldg, string OperatorOrganizationname, string OperatorPhoneNumber, string RequestDate, string UpdateDate,
            string LayoutId, string Subject, string mailuserrequest, string Datemultiple)
        {
            var ReservationIddata = ReservationId;
            var Type = "Asset";

            if (Convert.ToDateTime(EndDate) >= DateTime.Now && Convert.ToDateTime(StartDate).AddMinutes(25) >= DateTime.Now)
            {

                try
                {
                    var checksatatus = "0";


                    if (ReservationId > 0)
                    {
                        //Update the event
                        var v = _context.ReservationAsset.Where(a => a.ReservationId == ReservationId).FirstOrDefault();
                        if (v != null)
                        {
                            v.Subject = Subject;
                            v.UpdateDate = DateTime.Now;
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
                        var checkdata = _context.ReservationAsset.Where(x => x.AssetId == AssetId && start == x.StartDate && end == x.EndDate).FirstOrDefault();
                        var checkdata1 = _context.ReservationAsset.Where(x => x.AssetId == AssetId && (start == x.StartDate || end == x.EndDate)).FirstOrDefault();
                        var checkdata2 = _context.ReservationAsset.Where(x => x.AssetId == AssetId && (start <= x.StartDate && end >= x.EndDate)).FirstOrDefault();
                        var checkdata3 = _context.ReservationAsset.Where(x => x.AssetId == AssetId && (start < x.EndDate && end >= x.EndDate)).FirstOrDefault();
                        var checkdata4 = _context.ReservationAsset.Where(x => x.AssetId == AssetId && (start >= x.StartDate && end <= x.EndDate)).FirstOrDefault();
                        var checkdata5 = _context.ReservationAsset.Where(x => x.AssetId == AssetId && (start < x.StartDate && end > x.StartDate)).FirstOrDefault();

                        if (checkdata == null && checkdata1 == null && checkdata2 == null && checkdata3 == null && checkdata4 == null && checkdata5 == null)
                        {



                            //add  the event                   
                            var data = new ReservationAsset
                            {

                                AssetId = AssetId,
                                StartDate = Convert.ToDateTime(StartDate),
                                EndDate = Convert.ToDateTime(EndDate),
                                Subject = Subject,
                                RequestDate = DateTime.Now,
                                UpdateDate = DateTime.Now,
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
                            await _context.ReservationAsset.AddAsync(data);
                            await _context.SaveChangesAsync();
                            ReservationIddata = data.ReservationId;
                        }
                        else
                        {
                            var Datadenied = new { status = false, subject = "Oops...", detail = "This Asset cannot be reserved. Please choose another date range.", type = "warning" };
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

                    var Asset = _context.ReservationMasterAsset.Where(i => i.AssetId == AssetId).FirstOrDefault();
                    var location = _context.ReservationMasterLocation.Where(i => i.LocationId == Asset.LocationId).FirstOrDefault();
                    var title = location.Building + " " + Asset.Name;
                    var date = Convert.ToDateTime(StartDate).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(EndDate).ToString("dd/MM/yyyy HH :mm");
                    var user = "Asset Reservation by : " + OperatorName + " " + OperatorOrganizationname + "  " + OperatorPhoneNumber;

                    var subject = "Asset reservation notification ";


                    var detail = Subject;




                    if (mailuserrequest != "")
                    {
                        _Sendmail.EmailReservationAsset(mailuserrequest, Asset.ResponsibleEmail, subject, title, date, user, detail); //To Issue  , CC Responsible
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

                            var checkdata = _context.ReservationAsset.Where(x => x.AssetId == AssetId && start == x.StartDate && end == x.EndDate).FirstOrDefault();

                            var checkdata1 = _context.ReservationAsset.Where(x => x.AssetId == AssetId && (start == x.StartDate || end == x.EndDate)).FirstOrDefault();
                            var checkdata2 = _context.ReservationAsset.Where(x => x.AssetId == AssetId && (start <= x.StartDate && end >= x.EndDate)).FirstOrDefault();
                            var checkdata3 = _context.ReservationAsset.Where(x => x.AssetId == AssetId && (start < x.EndDate && end >= x.EndDate)).FirstOrDefault();
                            var checkdata4 = _context.ReservationAsset.Where(x => x.AssetId == AssetId && (start >= x.StartDate && end <= x.EndDate)).FirstOrDefault();
                            var checkdata5 = _context.ReservationAsset.Where(x => x.AssetId == AssetId && (start < x.StartDate && end > x.StartDate)).FirstOrDefault();

                            if (checkdata == null && checkdata1 == null && checkdata2 == null && checkdata3 == null && checkdata4 == null && checkdata5 == null)
                            {
                                //Add

                                var data = new ReservationAsset
                                {

                                    AssetId = AssetId,
                                    StartDate = start,
                                    EndDate = end,
                                    Subject = Subject,
                                    RequestDate = DateTime.Now,
                                    UpdateDate = DateTime.Now,
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
                                await _context.ReservationAsset.AddAsync(data);
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

                                var AssetDatemultiple = _context.ReservationMasterAsset.Where(i => i.AssetId == AssetId).FirstOrDefault();
                                var locationDatemultiple = _context.ReservationMasterLocation.Where(i => i.LocationId == AssetDatemultiple.LocationId).FirstOrDefault();

                                var titleDatemultiple = locationDatemultiple.Building + " " + AssetDatemultiple.Name;
                                var dateDatemultiple = Convert.ToDateTime(start).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(end).ToString("dd/MM/yyyy HH :mm");
                                var userDatemultiple = "Asset Reservation by : " + OperatorName + " " + OperatorOrganizationname + "  " + OperatorPhoneNumber;

                                var subjectDatemultiple = "Asset reservation notification ";

                                var detailDatemultiple = subjectDatemultiple;



                                if (mailuserrequest != "")
                                {
                                    _Sendmail.EmailReservationAsset(mailuserrequest, AssetDatemultiple.ResponsibleEmail, subjectDatemultiple, titleDatemultiple, dateDatemultiple, userDatemultiple, detailDatemultiple);
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

                var v = _context.ReservationAsset.Where(a => a.ReservationId == reservationId).FirstOrDefault();
                var attach = _context.ReservationAttachFile.Where(a => a.ReservationId == reservationId && a.Type == "Asset");

                if (v != null)
                {
                    if (v.EndDate < DateTime.Now)
                    {
                        Data = new { status = false, subject = "Oops...", detail = "Cannot Delete old data", type = "error" };
                    }
                    else
                    {
                        _context.ReservationAsset.Remove(v);
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

                        var Asset = _context.ReservationMasterAsset.Where(i => i.AssetId == v.AssetId).FirstOrDefault();
                        var location = _context.ReservationMasterLocation.Where(i => i.LocationId == Asset.LocationId).FirstOrDefault();
                        var subject = "Delete Asset reservation ";
                        var title = location.Building + " " + Asset.Name;
                        var date = Convert.ToDateTime(v.StartDate).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(v.EndDate).ToString("dd/MM/yyyy HH :mm");
                        var user = "Delete Asset reservation by : " + v.OperatorName + " " + v.OperatorOrganizationname + "  " + v.OperatorPhoneNumber;
                        var detail = v.Subject;
                        if (mailuserrequest != "")
                        {
                            _Sendmail.EmailReservationAsset(mailuserrequest, Asset.ResponsibleEmail, subject, title, date, user, detail);
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


        public dynamic BorrowEvent(int reservationId, string mailuserapprove, string userapprove, string nameapprove, string remarks)
        {
            try
            {
                var Data = new { status = true, subject = "Success", detail = "Borrow asset complete", type = "success" };

                //Update the event
                var v = _context.ReservationAsset.Where(a => a.ReservationId == reservationId && a.Status == "0").FirstOrDefault();
                if (v != null)
                {
                    //check before borrow ว่าของว่างไหม
                    var checkasset = _context.ReservationAsset.Where(x => x.AssetId == v.AssetId && x.Status == "1" && x.ReservationId != reservationId).FirstOrDefault();
                    if (checkasset != null)
                    {
                        Data = new { status = true, subject = "Warning", detail = "Asset has not been return from " + checkasset.OperatorName + " : " + checkasset.StartDate, type = "warning" };
                    }
                    else
                    {
                        v.Status = "1";
                        v.ApproveID = userapprove;
                        v.ApproveDate = DateTime.Now;
                        v.Remarks = remarks;
                        _context.SaveChanges();

                        //sent mail
                        var Asset = _context.ReservationMasterAsset.Where(i => i.AssetId == v.AssetId).FirstOrDefault();
                        var location = _context.ReservationMasterLocation.Where(i => i.LocationId == Asset.LocationId).FirstOrDefault();
                        var subject = "Borrow asset reservations ";
                        var title = location.Building + " " + Asset.Name;
                        var date = Convert.ToDateTime(v.StartDate).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(v.EndDate).ToString("dd/MM/yyyy HH :mm");
                        var user = "Borrow asset reservation by : " + nameapprove + " Remark >> " + remarks;
                        var detail = v.Subject;
                        if (v.OperatorEmail != "")
                        {
                            _Sendmail.EmailReservationAsset(v.OperatorEmail, "", subject, title, date, user, detail);
                        }
                    }
                }
                else
                {
                    Data = new { status = true, subject = "Warning", detail = "Can not borrow asset,Please check status", type = "warning" };
                }

                return Data;
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "error", detail = e.Message, type = "error" };
                return Data1;
            }
        }


        public dynamic ReturnEvent(int reservationId, string mailuserapprove, string userapprove, string nameapprove, string remarks)
        {
            try
            {
                var Data = new { status = true, subject = "Success", detail = "Return asset complete", type = "success" };

                var v = _context.ReservationAsset.Where(a => a.ReservationId == reservationId && a.Status == "1").FirstOrDefault();
                if (v != null)
                {
                    v.Status = "2";
                    v.ReturnID = userapprove;
                    v.ReturnDate = DateTime.Now;
                    v.Remarks = remarks;
                    _context.SaveChanges();

                    //sent mail
                    var Asset = _context.ReservationMasterAsset.Where(i => i.AssetId == v.AssetId).FirstOrDefault();
                    var location = _context.ReservationMasterLocation.Where(i => i.LocationId == Asset.LocationId).FirstOrDefault();
                    var subject = "Return asset reservations ";
                    var title = location.Building + " " + Asset.Name;
                    var date = Convert.ToDateTime(v.StartDate).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(v.EndDate).ToString("dd/MM/yyyy HH :mm");
                    var user = "Return asset reservation by : " + nameapprove + " Remark >> " + remarks;
                    var detail = v.Subject;
                    if (v.OperatorEmail != "")
                    {
                        _Sendmail.EmailReservationAsset(v.OperatorEmail, "", subject, title, date, user, detail);
                    }
                }
                else
                {
                    Data = new { status = true, subject = "Warning", detail = "Can not return asset,Please check status", type = "warning" };
                }

                return Data;
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "error", detail = e.Message, type = "error" };
                return Data1;
            }
        }

        public dynamic ResetEvent(int reservationId, string mailuserapprove, string userapprove, string nameapprove, string remarks)
        {
            try
            {
                var Data = new { status = true, subject = "Success", detail = "Reset status asset complete", type = "success" };

                var v = _context.ReservationAsset.Where(a => a.ReservationId == reservationId && a.Status == "2").FirstOrDefault();
                var w = _context.ReservationAsset.Where(a => a.ReservationId == reservationId && a.Status == "1").FirstOrDefault();

                if (v != null)
                {
                    var checkasset = _context.ReservationAsset.Where(a => a.AssetId == v.AssetId && a.Status == "1" && a.ReservationId != reservationId).FirstOrDefault();
                    if (checkasset != null)
                    {
                        Data = new { status = true, subject = "Warning", detail = "Asset has not been return from " + checkasset.OperatorName + " : " + checkasset.StartDate, type = "warning" };
                    }
                    else
                    {
                        v.Status = "1";
                        v.ApproveID = userapprove;
                        v.ApproveDate = DateTime.Now;
                        v.ReturnID = null;
                        v.ReturnDate = null;
                        v.Remarks = remarks;
                        _context.SaveChanges();

                        //sent mail
                        var Asset = _context.ReservationMasterAsset.Where(i => i.AssetId == v.AssetId).FirstOrDefault();
                        var location = _context.ReservationMasterLocation.Where(i => i.LocationId == Asset.LocationId).FirstOrDefault();
                        var subject = "Reset status asset reservations ";
                        var title = location.Building + " " + Asset.Name;
                        var date = Convert.ToDateTime(v.StartDate).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(v.EndDate).ToString("dd/MM/yyyy HH :mm");
                        var user = "Reset status asset reservation by : " + nameapprove + " Remark >> " + remarks + " Status Now >> " + "Borrow";
                        var detail = v.Subject;
                        if (v.OperatorEmail != "")
                        {
                            _Sendmail.EmailReservationAsset(v.OperatorEmail, "", subject, title, date, user, detail);
                        }
                    }

                }
                else if (w != null)
                {

                    w.Status = "0";
                    w.ApproveID = null;
                    w.ApproveDate = null;
                    w.ReturnID = null;
                    w.ReturnDate = null;
                    w.Remarks = remarks;
                    _context.SaveChanges();

                    //sent mail
                    var Asset = _context.ReservationMasterAsset.Where(i => i.AssetId == w.AssetId).FirstOrDefault();
                    var location = _context.ReservationMasterLocation.Where(i => i.LocationId == Asset.LocationId).FirstOrDefault();
                    var subject = "Reset status asset reservations ";
                    var title = location.Building + " " + Asset.Name;
                    var date = Convert.ToDateTime(w.StartDate).ToString("dd/MM/yyyy HH :mm") + " - " + Convert.ToDateTime(w.EndDate).ToString("dd/MM/yyyy HH :mm");
                    var user = "Reset status asset reservation by : " + nameapprove + " Remark >> " + remarks + " Status Now >> " + "Reserve";
                    var detail = w.Subject;
                    if (w.OperatorEmail != "")
                    {
                        _Sendmail.EmailReservationAsset(w.OperatorEmail, "", subject, title, date, user, detail);
                    }

                }
                else
                {
                    Data = new { status = true, subject = "Warning", detail = "Can not reset status asset,Please check status", type = "warning" };
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
