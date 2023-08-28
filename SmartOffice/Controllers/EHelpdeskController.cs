using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using Microsoft.EntityFrameworkCore;
namespace SmartOffice.Controllers
{
    public class EHelpdeskController : Controller
    {
        private readonly ESmartOfficeContext _dbContext;
        public EHelpdeskController(ESmartOfficeContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var model = new ModelSubSystems();
            List<SubSystems> subSystems = _dbContext.Set<SubSystems>().FromSql("exec sprHelpAndSupportMenu").ToList();
            model.subSystems = subSystems;
            return View(model);
        }
        public IActionResult Discuss()
        {
            return View();
        }
        public IActionResult Docs()
        {
            return View();
        }
        public IActionResult Articles()
        {
            return View();
        }
        public IActionResult Questions()
        {
            return View();
        }
        public IActionResult Ideas()
        {
            return View();
        }
        public IActionResult Issues()
        {
            return View();
        }
        public IActionResult NewTopic()
        {
            return View();
        }
        public IActionResult NewDoc()
        {
            return View();
        }
        public IActionResult ChatRoom()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            return View();
        }

        //[HttpPost]
        //public IActionResult Upload()
        //{
        //    if (Request.Files.Count > 0)
        //    {
        //        try
        //        {
        //            var file = Request.Files[0];

        //            // Some basic checks...
        //            if (file != null && !FileValidator.ValidSize(file.ContentLength))
        //                return Json("File size too big. Maximum File Size: 500KB");
        //            else if (FileValidator.ValidType(file.ContentType))
        //                return Json("This file extension is not allowed!");
        //            else
        //            {
        //                // Save file to Disk
        //                var fileName = DateTime.Now.ToString("yyyymmddMMss") + "_" + Path.GetFileName(file.FileName);
        //                var filePath = Path.Combine(Server.MapPath("~/Content/uploads/"), fileName);
        //                file.SaveAs(filePath);

        //                string htmlImage = string.Format(
        //                    "<a href=\"/Content/uploads/{0}\" target=\"_blank\">" +
        //                    "<img src=\"/Content/uploads/{0}\" class=\"post-image\">" +
        //                    "</a>", fileName);

        //                using (var db = new ApplicationDbContext())
        //                {
        //                    // Get sender & chat room
        //                    var senderViewModel = ChatHub._Connections.Where(u => u.Username == User.Identity.Name).FirstOrDefault();
        //                    var sender = db.Users.Where(u => u.UserName == senderViewModel.Username).FirstOrDefault();
        //                    var room = db.Rooms.Where(r => r.Name == senderViewModel.CurrentRoom).FirstOrDefault();

        //                    // Build message
        //                    Message msg = new Message()
        //                    {
        //                        Content = Regex.Replace(htmlImage, @"(?i)<(?!img|a|/a|/img).*?>", String.Empty),
        //                        Timestamp = DateTime.Now.Ticks.ToString(),
        //                        FromUser = sender,
        //                        ToRoom = room
        //                    };

        //                    db.Messages.Add(msg);
        //                    db.SaveChanges();

        //                    // Send image-message to group
        //                    var messageViewModel = Mapper.Map<Message, MessageViewModel>(msg);
        //                    var hub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
        //                    hub.Clients.Group(senderViewModel.CurrentRoom).newMessage(messageViewModel);
        //                }

        //                return Json("Success");
        //            }

        //        }
        //        catch (Exception ex)
        //        { return Json("Error while uploading" + ex.Message); }
        //    }

        //    return Json("No files selected");

        //} // Upload
    }
}