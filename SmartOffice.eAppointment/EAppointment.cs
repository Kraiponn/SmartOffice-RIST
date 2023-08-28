using System;
using SmartOffice.EAppointment.IResponsitory;
using System.Collections.Generic;
using System.Linq;
using SmartOffice.EAppointment.ModelsForm;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartOffice.EmailCore.IResponsitory;
using SmartOffice.eAppointment.ModelsForm;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SmartOffice.EAppointment
{
    public class EAppointmentControl : IEAppointment
    {
        private readonly DocumentControlContext _context;
        private readonly ISendEmail _Sendmail;
        private IHostingEnvironment _hostingEnvironment;
        public EAppointmentControl(DocumentControlContext context, ISendEmail Sendmail, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _Sendmail = Sendmail;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<dynamic> GetCalendarMain(string UserName)
        {
            try
            {
                var allSlide = await _context.Appointment.Where(i => i.StartDate.Value.Date >= DateTime.Now.Date && i.UserName == UserName).OrderBy(x => x.StartDate).ToListAsync();
                var allSlide2 = allSlide.Select(p => new
                {
                    p.AppointmentId,
                    p.Subject,
                    StartDate = (DateTime.TryParse(p.StartDate.ToString(), out DateTime parsedDateTimeObj)) ? parsedDateTimeObj.ToString("dd/MM/yyyy HH:mm") : "Unknown",
                    EndDate = (DateTime.TryParse(p.EndDate.ToString(), out parsedDateTimeObj)) ? parsedDateTimeObj.ToString("dd/MM/yyyy HH:mm") : "Unknown",
                    p.Description,
                    p.ThemeColor,
                    p.IsFullDay,
                    UpdDate = (DateTime.TryParse(p.UpdDate.ToString(), out parsedDateTimeObj)) ? parsedDateTimeObj.ToString("dd/MM/yyyy") : "Unknown",
                    p.UserName,
                    p.AppointType,


                }).OrderByDescending(x => x.StartDate).ToList();
                return allSlide2;
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }
        public async Task<dynamic> GetCalendarMainUser(string UserName)
        {
            try
            {
                var allSlide = await _context.Appointment.Where(i => i.StartDate.Value.Year == DateTime.Now.Year && i.UserName == UserName).OrderBy(x => x.StartDate).ToListAsync();
                var allSlide2 = allSlide.Select(p => new
                {
                    p.AppointmentId,
                    p.Subject,
                    StartDate = (DateTime.TryParse(p.StartDate.ToString(), out DateTime parsedDateTimeObj)) ? parsedDateTimeObj.ToString("dd/MM/yyyy HH:mm") : "Unknown",
                    EndDate = (DateTime.TryParse(p.EndDate.ToString(), out parsedDateTimeObj)) ? parsedDateTimeObj.ToString("dd/MM/yyyy HH:mm") : "Unknown",
                    p.Description,
                    p.ThemeColor,
                    p.IsFullDay,
                    UpdDate = (DateTime.TryParse(p.UpdDate.ToString(), out parsedDateTimeObj)) ? parsedDateTimeObj.ToString("dd/MM/yyyy") : "Unknown",
                    p.UserName,
                    p.AppointType,


                }).ToList();
                return allSlide2;
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }
        public async Task<dynamic> GetCalendarMainprivate(string newstype, string _user)
        {
            try
            {
                var allSlide = await _context.Appointment.Where(i => i.UserName == _user).OrderBy(x => x.StartDate).ToListAsync();
                var allSlide2 = allSlide.Select(p => new
                {
                    p.AppointmentId,
                    p.Subject,
                    Start = (DateTime.TryParse(p.StartDate.ToString(), out DateTime parsedDateTimeObj)) ? parsedDateTimeObj.ToString("dd/MM/yyyy HH:mm") : "Unknown",
                    End = (DateTime.TryParse(p.EndDate.ToString(), out parsedDateTimeObj)) ? parsedDateTimeObj.ToString("dd/MM/yyyy HH:mm") : "Unknown",
                    p.Description,
                    p.ThemeColor,
                    p.IsFullDay,
                    CreatedDate = (DateTime.TryParse(p.UpdDate.ToString(), out parsedDateTimeObj)) ? parsedDateTimeObj.ToString("dd/MM/yyyy") : "Unknown",
                    p.UserName,
                    UpdateDate = (DateTime.TryParse(p.UpdDate.ToString(), out parsedDateTimeObj)) ? parsedDateTimeObj.ToString("dd/MM/yyyy") : "Unknown",

                }).OrderByDescending(x => x.Start).ToList();
                return allSlide2;
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }
        //fullcalendar ALL
        public async Task<List<Appointment>> GetEvents()
        {
            return await _context.Appointment.Where(i => i.AppointType == "Events").ToListAsync();
        }
        public async Task<dynamic> Getpeople_inClass(int AppointmentId)
        {
            var listitem = await _context.VewOperatorAppointment.Where(i => i.AppointmentId == AppointmentId && i.OperatorID != null).ToListAsync();
            //var listitem2 = listitem.Select(p => new
            //{
            //    id = p.OperatorID,
            //    text = p.NAMEMPE + " " + p.OperatorID
            //});
            return listitem;
        }
        public async Task<List<VewOperatorAppointment>> GetEventsDay(string UserName)
        {
            //เฉพาะตัวเองวันนี้
            return await _context.VewOperatorAppointment.Where(i => (i.StartDate.Value.Date == DateTime.Now.Date || (i.StartDate.Value.Date < DateTime.Now.Date && i.EndDate.Value.Date >= DateTime.Now.Date)) &&
            ((i.AppointType == "Events" && i.OperatorID == UserName) || (i.AppointType == "Appointment" && i.OperatorID == UserName) || (i.AppointType == "Reminder" && i.UserName == UserName))).ToListAsync();
        }
        public async Task<List<VewOperatorAppointment>> GetEventsSetting(string UserName)
        {
            return await _context.VewOperatorAppointment.Where(i => (i.AppointType == "Events") ||
            ((i.AppointType == "Appointment" && i.OperatorID == UserName) || (i.AppointType == "Appointment" && i.UserName == UserName)) ||
            (i.AppointType == "Reminder" && i.UserName == UserName)).ToListAsync();

        }
        public async void JoinAppointment(int appointmentid, string username)
        {
            var UserItem = _context.AppointmentOperator.Where(i => i.AppointmentId == appointmentid && i.OperatorId == username).FirstOrDefault();
            if (UserItem != null)
                UserItem.Attend = true;
            await _context.SaveChangesAsync();

        }
        public async Task<dynamic> DeleteInvitepeople(int appointment_ID, string operatorID)
        {
            var Data = new { status = true, subject = "Cancel Invite people", detail = "Success", type = "success" };
            try
            {
                var v = await _context.Appointment.Where(a => a.AppointmentId == appointment_ID).FirstOrDefaultAsync();
                var opt = await _context.AppointmentOperator.Where(a => a.AppointmentId == appointment_ID && a.OperatorId == operatorID).ToListAsync();

                //sentmail back when delete                
                var allemail = await _context.VewOperatorAppointment.Where(i => i.AppointmentId == appointment_ID && i.OperatorID == operatorID).Select(i => i.EMAIL1).ToListAsync();
                if (allemail.Count > 0)
                {
                    _Sendmail.EmailInvite(allemail, v.Subject, v.StartDate.Value.ToString("dddd, dd MMMM yyyy h:mm tt"), v.Description, v.UserName, v.AppointmentId, "D");
                }

                //Delete
                _context.AppointmentOperator.RemoveRange(opt);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Data = new { status = false, subject = "Error", detail = ex.ToString(), type = "error" };
                return Data;
            }
            return Data;
        }
        public async Task<dynamic> DeleteEvent(Appointment App, string UserName, List<string> invitepeople)
        {
            var Data = new { status = true, subject = "Delete", detail = "Success", type = "success" };
            try
            {
                var v = await _context.Appointment.Where(a => a.AppointmentId == App.AppointmentId).FirstOrDefaultAsync();
                var opt = await _context.AppointmentOperator.Where(a => a.AppointmentId == App.AppointmentId).ToListAsync();
                if (v != null)
                {
                    //sentmail back when delete                
                    var allemail = await _context.VewOperatorAppointment.Where(i => i.AppointmentId == App.AppointmentId).Select(i => i.EMAIL1).ToListAsync();
                    if (allemail.Count > 0)
                    {
                        _Sendmail.EmailInvite(allemail, v.Subject, v.StartDate.Value.ToString("dddd, dd MMMM yyyy h:mm tt"), App.Description, v.UserName, v.AppointmentId, "D");
                    }

                    //Delete
                    _context.Appointment.Remove(v);
                    _context.AppointmentOperator.RemoveRange(opt);
                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Data = new { status = false, subject = "Error", detail = ex.ToString(), type = "error" };
                return Data;
            }
            return Data;
        }
        public async Task<dynamic> SaveEventAsync(int AppointmentId, string Subject, DateTime StartDate, DateTime EndDate, string Description, string ThemeColor,
        bool IsFullDay, string AppointType, string UserName, string invitepeople, List<IFormFile> files)
        {
            var AppointmentIddata = AppointmentId;
            string ComputerName = Environment.MachineName;
            var Data = new { status = true, subject = "Save", detail = "Success", type = "success" };
            try
            {
                if (AppointmentId != 0)
                {
                    //Update the event
                    var v = await _context.Appointment.Where(a => a.AppointmentId == AppointmentId).FirstOrDefaultAsync();

                    if (v != null)
                    {
                        v.Subject = Subject;
                        v.StartDate = StartDate;                        
                        v.Description = Description;
                        if (Description == null)
                        {
                            v.Description = "";
                        }

                        if (IsFullDay == true)
                        {
                            v.EndDate = null;
                            v.StartDate = StartDate.Date;
                        }
                        else
                        {
                            v.EndDate = EndDate;
                        }
                        v.ThemeColor = ThemeColor;

                        v.IsFullDay = IsFullDay;
                        v.UserName = UserName;
                        v.UpdDate = DateTime.Now;
                        v.AppointType = AppointType;

                        if (invitepeople != "null" && invitepeople != null)
                        {
                            List<AppointmentOperator> appopr = new List<AppointmentOperator>();
                            string[] words = invitepeople.Split(',');
                            foreach (var item in words)
                            {
                                if (item != null)
                                {
                                    var chkinvite = _context.AppointmentOperator.Where(i => i.AppointmentId == AppointmentId && i.OperatorId == item).FirstOrDefault();
                                    if (chkinvite == null)
                                    {
                                        AppointmentOperator iitem = new AppointmentOperator
                                        {
                                            OperatorId = item,
                                            AppointmentId = v.AppointmentId,
                                            Attend = false,

                                        };
                                        appopr.Add(iitem);
                                    }
                                }
                            }

                            _context.AppointmentOperator.AddRange(appopr);
                        }
                        await _context.SaveChangesAsync();

                        //send mail
                        var allemail = await _context.VewOperatorAppointment.Where(i => i.AppointmentId == v.AppointmentId).Select(i => i.EMAIL1).ToListAsync();

                        if (allemail.Count > 0)
                        {
                            _Sendmail.EmailInvite(allemail, "RE: " + v.Subject, v.StartDate.Value.ToString("dddd, dd MMMM yyyy h:mm tt"), v.Description, v.UserName, v.AppointmentId, "");

                        }

                    }
                    else
                    {
                        Data = new { status = false, subject = "Update", detail = "Permission denied", type = "warning" };
                        return Data;
                    }
                }
                else
                {
                    //add
                    var desc = Description;
                    var start = StartDate;
                    DateTime? end  = EndDate;
                    var Theme = ThemeColor;
                    if (Description == null)
                    {
                        desc = "";
                    }

                    if (IsFullDay == true)
                    {
                        start = StartDate.Date;
                        end = null;
                    }
                    var data = new Appointment
                    {
                        Subject = Subject,
                        StartDate = start,
                        EndDate = end,
                        Description = desc,
                        IsFullDay = IsFullDay,
                        ThemeColor = Theme,
                        UserName = UserName,
                        AddDate = DateTime.Now,
                        UpdDate = DateTime.Now,
                        ComputerName = ComputerName,
                        AppointType = AppointType,

                    };

                    await _context.Appointment.AddAsync(data);
                    await _context.SaveChangesAsync();
                    AppointmentIddata = data.AppointmentId;
                    //_context.Events.Add(e);
                    if (invitepeople != "null" && invitepeople != null)
                    {
                        List<AppointmentOperator> appopr = new List<AppointmentOperator>();

                        string[] words = invitepeople.Split(',');
                        foreach (var item in words)
                        {
                            if (item != null)
                            {
                                AppointmentOperator iitem = new AppointmentOperator
                                {
                                    OperatorId = item,
                                    AppointmentId = data.AppointmentId,
                                    Attend = false,

                                };
                                appopr.Add(iitem);
                            }
                        }
                        _context.AppointmentOperator.AddRange(appopr);
                        _context.SaveChanges();

                    }
                    var allemail = await _context.VewOperatorAppointment.Where(i => i.AppointmentId == data.AppointmentId).Select(i => i.EMAIL1).ToListAsync();
                    if (allemail.Count > 0)
                    {
                        _Sendmail.EmailInvite(allemail, data.Subject, data.StartDate.Value.ToString("dddd, dd MMMM yyyy"), data.Description, data.UserName, data.AppointmentId, "");
                    }

                }


                //attach
                List<AppointmentAttachFile> att = new List<AppointmentAttachFile>();
                foreach (IFormFile item in files)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"').Replace(" ", "");
                    AppointmentAttachFile attfile = _context.AppointmentAttachFile.Where(p => p.AppointmentId == AppointmentIddata && p.Filename == fileName).FirstOrDefault();
                    if (attfile == null)
                    {

                        //new

                        string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + "Appointment";
                        string newPath = Path.Combine(webRootPath, Convert.ToString(AppointmentIddata));
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

                        AppointmentAttachFile iitem = new AppointmentAttachFile
                        {
                            AppointmentId = AppointmentIddata,
                            Filename = fileName,
                        };
                        att.Add(iitem);
                    }
                    else
                    {
                        //have
                        attfile.Filename = fileName;

                        File.Delete(_hostingEnvironment.WebRootPath + "/Attach/" + "Appointment/" + Convert.ToString(AppointmentIddata) + "/" + attfile.Filename);

                        string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + "EReservation";
                        string newPath = Path.Combine(webRootPath, Convert.ToString(AppointmentIddata));
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
                _context.AppointmentAttachFile.AddRange(att);
                _context.SaveChanges();


                return Data;
            }
            catch (Exception ex)
            {
                Data = new { status = false, subject = "Error", detail = ex.ToString(), type = "error" };
                return Data;
            }
        }

        //------------------------------------Start Attach File--------------------------------------------------------------------------------------
        //Get File Attach
        public List<AppointmentAttachFile> GetFileAttachdata(int AppointmentId)
        {

            List<AppointmentAttachFile> allfile = _context.AppointmentAttachFile.Where(p => p.AppointmentId == AppointmentId).ToList().Select(p => new AppointmentAttachFile()
            {
                AppointmentId = p.AppointmentId,
                Filename = p.Filename,

            }).ToList();
            return allfile;

        }
        //Delete Attach file
        public dynamic Deletefile(int AppointmentId, string filename)
        {
            try
            {

                var attfile = _context.AppointmentAttachFile.Where(p => p.AppointmentId == AppointmentId && p.Filename == filename).FirstOrDefault();

                if (attfile != null)
                {

                    _context.AppointmentAttachFile.Remove(attfile);
                    _context.SaveChangesAsync();

                    File.Delete(_hostingEnvironment.WebRootPath + "/Attach/" + "Appointment/" + Convert.ToString(AppointmentId) + "/" + attfile.Filename);

                    var Data = new { status = true, subject = "Delete File", detail = "Delete File Complete." };
                    return Data;
                }
                else
                {
                    var Data = new { status = false, subject = "Delete File", detail = "System is can't delete File." };
                    return Data;
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
