using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmartOffice.Class;
using SmartOffice.Hubs;
using SmartOffice.IResponsitory;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.EReservation.IResponsitory;
using SmartOffice.EAppointment.IResponsitory;
using System.Drawing;
using System.Drawing.Drawing2D;
using SmartOffice.Models.ViewModel;
using System.Xml.Linq;
using SmartOffice.ModelsDocControl;
using System.Xml;
using SmartOffice.Responsitory;

namespace SmartOffice.Controllers
{

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ESmartOfficeContext _context;
        private readonly DocumentControlContext _DocumentContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment _hostingEnvironment;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEAppointment _IEAppointment;
        private readonly IEReservationRoom _IEReservation;
        private readonly IHubContext<NotiHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly ISearchEngineService _SearchEngineService;
        private readonly IESmartNotiRepository _repository;

        public HomeController(UserManager<ApplicationUser> userManager, ESmartOfficeContext context, DocumentControlContext DocumentControlContext,
            SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment,
            IHubContext<NotiHub> notificationUserHubContext,
            IUserConnectionManager userConnectionManager,
            IConfiguration configuration,
            IEAppointment appointment,
            ISearchEngineService SearchEngineService,
            IEReservationRoom reservation,
            IESmartNotiRepository repository)
        {

            _userManager = userManager;
            _context = context;
            _DocumentContext = DocumentControlContext;
            _signInManager = signInManager;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _IEAppointment = appointment;
            _IEReservation = reservation;
            _configuration = configuration;
            _SearchEngineService = SearchEngineService;
            _repository = repository;
        }

        [BindProperty]
        public InputProfile Input { get; set; }

        [Authorize]
        public IActionResult Privacy()
        {
            int DayChangePassword = _configuration.GetValue<int>("AppSettings:DayChangePassword");
            var users = _context.AspNetUsers.Where(x => x.UserName == User.Claims.FirstOrDefault(c => c.Type == "UserName").Value).FirstOrDefault();
            var cc = Convert.ToDateTime(users.LastChangePassword).AddDays(DayChangePassword);
            if (cc < DateTime.Now)
            {
                _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
                return RedirectToAction("Index");
            }


            Input = new InputProfile
            {
                id = User.Claims.FirstOrDefault(c => c.Type == "Id").Value,

                Phone = User.Claims.FirstOrDefault(c => c.Type == "PhoneNumber").Value,

                OperatorSign = users.OperatorSign,
            };
            return View(Input);
        }


        public IActionResult Index()
        {
            //string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            //if (Request.Headers.ContainsKey("X-Forwarded-For"))
            //{
            //    remoteIpAddress = Request.Headers["X-Forwarded-For"];
            //}



            var News = (from p in _context.NnewsDetail
                        join e in _context.NnewsHeader
            on new { p.PartId, p.GroupCateg, p.NewHorder }
            equals new { e.PartId, e.GroupCateg, e.NewHorder }
                        where p.ShowPublic == true
                        select new Nnews
                        {
                            PartId = p.PartId,
                            GroupCateg = p.GroupCateg,
                            NewHorder = p.NewHorder,
                            NewDorder = p.NewDorder,
                            ItemType = p.ItemType,
                            Value = p.Value,
                            Value1 = p.Value1,
                            ShowPublic = p.ShowPublic,

                            NewsType = e.NewsType,
                            ChildType = e.ChildType,
                            Title1 = e.Title1,
                            Title2 = e.Title2,
                            Date = e.Date,
                            ImgPath = e.ImgPath,
                            LinkPath = e.LinkPath,
                            IconClass = e.IconClass,
                            Badges = e.Badges,
                            BadgesName = e.BadgesName,
                            Download = e.Download,
                            Disable = e.Disable,
                            StartDate = e.StartDate,
                            EndDate = e.EndDate,
                            UpdateDate = p.UpdateDate

                        }).OrderByDescending(x => x.UpdateDate).ToList();

            return View(News);
        }
        public IActionResult Welcome()
        {
            return View();
        }
        public IActionResult Index2()
        {
            var AspNetGroup = _context.AspNetGroup.ToList();
            return View(AspNetGroup);
        }

        [HttpPost]
        public IActionResult SearchResultPage(string txtsearch_data)
        {
            ViewBag.texsearch = txtsearch_data.Trim();
            return View();
        }

        [Authorize]
        public IActionResult ComponentContainer()
        {

            return View();
        }
        public IActionResult Comingsoon()
        {

            return View();
        }

        public IActionResult UnSupportPage()
        {
            return View();
        }
        public IActionResult CalendarMainUserControl()
        {
            var username = User.Identity.Name;
            return ViewComponent("CalendarMainUserControl", new { _user = username });
        }
        public IActionResult CalendarList()
        {
            return ViewComponent("CalendarList");
        }
        public IActionResult Organization(string DocCode)
        {
            ViewData["DocCode"] = DocCode;
            //var allitem = _DocumentContext.SupplymentItemDetail.Where(i => i.DocumentCode.Trim() == "SPM001").Select(x => new OrgflatModel
            //{
            //    OrgID = x.DocumentCode,
            //    EmpID = x.DocumentNo,
            //    EmpName = x.Item1,
            //    SeqNo = x.SeqNo,
            //    HierarchyID = x.Item2
            //}).OrderBy(i => i.SeqNo);



            ////List<OrgModel> items = new List<OrgModel>();
            //var firstitem = allitem.Where(i => i.SeqNo == 1).FirstOrDefault();
            ////items.Add(firstitem);
            //var recyr = FillRecursive(allitem.ToList(), "0");
            return View();
        }

        public JsonResult GetOrganization(string DocCode)
        {
            var allitem = _DocumentContext.SupplymentItemDetail.Where(i => i.DocumentCode.Trim() == DocCode).Select(x => new OrgflatModel
            {
                OrgID = x.DocumentCode,
                EmpID = x.DocumentNo,
                EmpName = x.Item1,
                SeqNo = x.SeqNo,
                Title = x.Item3,
                Image = "../image/User/" + x.DocumentNo.ToString().Trim() + ".jpg",
                HierarchyID = x.Item2
            }).OrderBy(i => i.SeqNo);



            //List<OrgModel> items = new List<OrgModel>();
            var firstitem = allitem.Where(i => i.SeqNo == 1).FirstOrDefault();
            //items.Add(firstitem);
            var recyr = FillRecursive(allitem.ToList(), "0");
            return Json(recyr.FirstOrDefault());
        }


        private static List<OrgModel> FillRecursive(List<OrgflatModel> flatObjects, string parentId)
        {
            List<OrgModel> recursiveObjects = new List<OrgModel>();
            foreach (var item in flatObjects.Where(x => x.HierarchyID.Trim() == parentId.Trim()))
            {
                recursiveObjects.Add(new OrgModel
                {
                    Title = item.Title.ToString().Trim(),
                    Name = item.EmpName.ToString().Trim(),
                    Image = item.Image,
                    Children = FillRecursive(flatObjects, item.EmpID)
                });
            }


            return recursiveObjects;
        }
        public IActionResult Viewimage()
        {
            return PartialView("_ViewImage");
        }

        [HttpPost]
        public IActionResult SearchResult(string txtsearchnav)
        {
            if (txtsearchnav != "" && txtsearchnav != null)
            {
                var UserId = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;

                SqlParameter _TxtSearch = new SqlParameter("@TextSearch", txtsearchnav);
                SqlParameter _UserId = new SqlParameter("@userid", UserId);
                List<DocumentItem> dynamics = _DocumentContext.Set<DocumentItem>().FromSql("exec sprFormSearch @TextSearch,@userid", _TxtSearch, _UserId).AsNoTracking().ToList();
                return View(dynamics);
            }

            return View(null);

        }

        public JsonResult GetPendingDoc()
        {

            var GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
            var UserId = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;

            ConnDoc dp = new ConnDoc(_configuration);
            var ApproveDoc = dp.GetWaitApprove().ToList();
            var EditDoc = dp.GetEditDocument().ToList();
            var MyDoc = dp.GetMyDocument(UserId).ToList();
            var GroupDoc = dp.GetGroupIssueDocument(GroupCateg, UserId).ToList();
            var MyApproved = dp.GetMyApproved(UserId).ToList();

            List<string> JobStatus = new List<string>();

            //Approve
            var ApproveDocument = ApproveDoc.Where(i => i.CODEMPID == UserId).Distinct().OrderByDescending(i => i.IssuedDate).Select(x => new
            {
                DocumentNo = x.DocumentNo.Trim(),
                DocumentCode = x.DocumentCode.Trim(),
                DocumentNameE = x.DocumentNameE.Trim(),
                DocumentNameT = x.DocumentNameT.Trim(),
                DocumentNameJ = x.DocumentNameJ.Trim(),
                ReqDescription1 = x.ReqDescription1.Trim(),
                ReqOperatorName = x.ReqOperatorName.Trim(),
                x.IssuedDate,
                x.IssuedDateChange,
                DocumentStatus = x.DocumentStatus.Trim() ?? "",
                x.SeqNo,
                x.approvepast,
                x.Countapprovepast,
                x.CountIssuedDate
            }).Distinct().ToList();
            var dataApprove = new
            {
                rows = ApproveDocument
            };
            //Edit
            var EditDocument = EditDoc.Where(i => i.ApproverOperatorID.Trim() == UserId).Distinct().OrderByDescending(i => i.IssuedDate).Select(x => new
            {
                DocumentNo = x.DocumentNo.Trim(),
                DocumentCode = x.DocumentCode.Trim(),
                DocumentNameE = x.DocumentNameE.Trim(),
                DocumentNameT = x.DocumentNameT.Trim(),
                DocumentNameJ = x.DocumentNameJ.Trim(),
                ReqDescription1 = x.ReqDescription1.Trim(),
                ReqOperatorName = x.ReqOperatorName.Trim(),
                x.IssuedDate,
                x.IssuedDateChange,
                DocumentStatus = x.DocumentStatus.Trim() ?? "",
                x.UpdDate,
                x.SeqNo,
                x.CountIssuedDate
            }).Distinct().ToList();
            var dataEdit = new
            {
                rows = EditDocument
            };

            //My Doc
            var MyDocument = MyDoc.Select(p => new
            {
                DocumentNo = p.DocumentNo.Trim(),
                DocumentCode = p.DocumentCode.ToString().Trim(),
                DocumentNameE = p.DocumentNameE.Trim(),
                DocumentNameT = p.DocumentNameT.Trim(),
                DocumentNameJ = p.DocumentNameJ.Trim(),
                ReqDescription1 = p.ReqDescription1.Trim(),
                ReqOperatorName = p.ReqOperatorName.Trim(),
                p.IssuedDate,
                p.IssuedDateChange,
                DocumentStatus = p.DocumentStatus.Trim() ?? "",
                p.NextApprove,
                p.CountIssuedDate,
                p.YearMonth
            }).ToList().OrderByDescending(i => i.IssuedDate);
            var dataMyDoc = new
            {
                rows = MyDocument
            };

            //My Approved
            var MyAppr = MyApproved.Select(p => new
            {
                DocumentNo = p.DocumentNo.Trim(),
                DocumentCode = p.DocumentCode.ToString().Trim(),
                DocumentNameE = p.DocumentNameE.Trim(),
                ReqDescription1 = p.ReqDescription1.Trim(),
                ReqOperatorName = p.ReqOperatorName.Trim(),
                p.IssuedDate,
                p.IssuedDateChange,
                DocumentStatus = p.DocumentStatus.Trim() ?? "",
                p.FlagUpdDate,
                p.ApprovalDate,
                p.ApprovalDateChange,
                p.YearMonth
            }).ToList().OrderByDescending(i => i.YearMonth).ThenByDescending(i => i.ApprovalDate);
            var dataMyAppr = new
            {
                rows = MyAppr
            };

            //GroupIssue
            var GroupIssueDocument = GroupDoc.Distinct().OrderByDescending(i => i.IssuedDate).Select(x => new
            {
                DocumentNo = x.DocumentNo.Trim(),
                DocumentCode = x.DocumentCode.Trim(),
                DocumentNameE = x.DocumentNameE.Trim(),
                DocumentNameT = x.DocumentNameT.Trim(),
                DocumentNameJ = x.DocumentNameJ.Trim(),
                ReqDescription1 = x.ReqDescription1.Trim(),
                ReqOperatorName = x.ReqOperatorName.Trim(),
                x.IssuedDate,
                x.IssuedDateChange,
                DocumentStatus = x.DocumentStatus.Trim() ?? "",
                x.FlagUpdDate,
                x.ApprovalDate,
                x.ApproverOperatorName,
                x.CountIssuedDate,
                x.YearMonth,

            }).Distinct().ToList();
            var dataGroupIssue = new
            {
                rows = GroupIssueDocument
            };

            //Pending Status menu approve doc
            JobStatus.Add(ApproveDocument.Distinct().Count().ToString());

            //Reject menu approve doc
            JobStatus.Add(ApproveDocument.Where(i => i.DocumentStatus.Trim() == "Reject").Distinct().Count().ToString());


            //Over time menu approve doc
            JobStatus.Add(ApproveDocument.Where(i => (Convert.ToInt32(i.Countapprovepast) >= 7)).Distinct().Count().ToString());


            //Pending Status
            JobStatus.Add(MyDocument.Where(i => i.DocumentStatus != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft").Distinct().Count().ToString());

            //Reject 
            JobStatus.Add(MyDocument.Where(i => i.DocumentStatus.Trim() == "Reject").Distinct().Count().ToString());


            //Over time
            JobStatus.Add(MyDocument.Where(i => i.DocumentStatus != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft"
            && (Convert.ToInt32(i.CountIssuedDate) >= 7)).Distinct().Count().ToString());


            return Json(new { dataapprove = dataApprove, datamydoc = dataMyDoc, itemCount = dataApprove.rows.Count(), dataedit = dataEdit, datastatus = JobStatus, datagroupissue = dataGroupIssue, datamyappr = dataMyAppr });
        }


        public async Task<JsonResult> GetSearchAsync(string textsearch)
        {
            SearchViewModel datasearch = await _SearchEngineService.GetSearch(textsearch, GetUserID());

            if (User.Identity.Name == "" || User.Identity.Name == null)
                datasearch.ListDocumentData = new List<DocumentData>();


            var countall = datasearch.ListDocument.Count() + datasearch.ListDocumentData.Count() + datasearch.ListRoomData.Count() + datasearch.ListTelephoneData.Count();
            return Json(new { datasearch, countall });
        }

        public string GetUserID()
        {
            string Username = "";
            if (User.Identity.IsAuthenticated == true)
            {
                Username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value.ToString().Trim();
            }

            return Username;
        }
        //[Authorize]
        //[Route("WorkArea")]
        public IActionResult WorkAreaMain(string DeptType)
        {
            var u = User.Identity;
            if (User.Identity is ClaimsIdentity identity)
            {

                if (User.Identity.IsAuthenticated)
                {
                    identity.RemoveClaim(identity.FindFirst("GroupCategclick"));
                }
                identity.AddClaim(new Claim("GroupCategclick", DeptType == null ? "" : DeptType.ToString()));
            }
            return View();
        }
        public IActionResult WorkAreaMain2(string DeptType)
        {
            var u = User.Identity;
            if (User.Identity is ClaimsIdentity identity)
            {

                if (User.Identity.IsAuthenticated)
                {
                    identity.RemoveClaim(identity.FindFirst("GroupCategclick"));
                }
                identity.AddClaim(new Claim("GroupCategclick", DeptType == null ? "" : DeptType.ToString()));
            }
            return View();
        }


        [Authorize]
        public IActionResult WorkAreaUser()
        {
            var users = _context.AspNetUsers.Where(x => x.UserName == User.Claims.FirstOrDefault(c => c.Type == "UserName").Value).FirstOrDefault();
            Input = new InputProfile
            {
                id = User.Claims.FirstOrDefault(c => c.Type == "Id").Value,

                Phone = User.Claims.FirstOrDefault(c => c.Type == "PhoneNumber").Value,

                OperatorSign = users.OperatorSign,
            };
            return View("Privacy", Input);
        }

        [Authorize]
        public IActionResult WorkAreaUser2(string DeptType)
        {
            var u = User.Identity;
            if (User.Identity is ClaimsIdentity identity)
            {

                if (User.Identity.IsAuthenticated)
                {
                    identity.RemoveClaim(identity.FindFirst("GroupCategclick"));
                }
                identity.AddClaim(new Claim("GroupCategclick", DeptType == null ? "" : DeptType.ToString()));
            }
            return View();
        }

        public IActionResult CreateForm()
        {

            return View();
        }
        public IActionResult Main()
        {
            return View();
        }




        ////////////////////////////////////////////////////////<Profile>/////////////////////////////////////////////////////////////////////////////////////////



        [Authorize]
        public IActionResult Profiles()
        {
            var users = _context.AspNetUsers.Where(x => x.UserName == User.Claims.FirstOrDefault(c => c.Type == "UserName").Value).FirstOrDefault();
            Input = new InputProfile
            {
                id = User.Claims.FirstOrDefault(c => c.Type == "Id").Value,

                Phone = User.Claims.FirstOrDefault(c => c.Type == "PhoneNumber").Value,

                OperatorSign = users.OperatorSign,
            };
            return View(Input);
        }


        public async Task<IActionResult> UpProfiles()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Privacy");
            }

            var user = await _userManager.FindByIdAsync(Input.id);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("Index");
            }
            var checkchange = false;


            if (Input.Password != null && Input.ConfirmPassword != null && Input.Password == Input.ConfirmPassword)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, code, Input.Password);

                if (result.Succeeded)
                {
                    var userData = _context.AspNetUsers.Where(u => u.UserName == user.UserName).SingleOrDefault();
                    if (userData != null)
                    {
                        userData.LastChangePassword = DateTime.Now;
                        _context.Update(userData);
                        _context.SaveChanges();
                    }

                    checkchange = true;
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            if (user.PhoneNumber != Input.Phone)
            {
                var update = (from c in _context.AspNetUsers
                              where c.Id == user.Id
                              select c).FirstOrDefault();
                if (update != null)
                {
                    update.PhoneNumber = Input.Phone;
                    if (Input.Phone != null)
                    {
                        update.PhoneNumberConfirmed = true;
                    }
                    else
                    {
                        update.PhoneNumberConfirmed = false;
                    }
                    await _context.SaveChangesAsync();
                    checkchange = true;
                }
            }


            if (checkchange == true)
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Privacy");
        }


        [HttpPost]
        public IActionResult Upimg(IFormFile file)
        {

            try
            {
                if (file != null)
                {
                    if (file.Length > 0)
                    {

                        string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\";
                        string newPath = Path.Combine(webRootPath, "User");
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string extension = Path.GetExtension(file.FileName);
                        string _detail_id = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
                        string fname_new = _detail_id + extension;

                        string fullPath = Path.Combine(newPath, fname_new);


                        using (var srcImage = Image.FromStream(file.OpenReadStream(), true, true))
                        {
                            using (var newImage = new Bitmap(160, 160))
                            using (var graphics = Graphics.FromImage(newImage))
                            {
                                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                graphics.DrawImage(srcImage, new Rectangle(0, 0, 160, 160));
                                newImage.Save(fullPath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ViewBag.mm = ex.Message;

                return RedirectToAction("Privacy");
            }
            return RedirectToAction("Privacy");
        }
        ////////////////////////////////////////////////////////</Profile>/////////////////////////////////////////////////////////////////////////////////////////

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult ExchangeRate()
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\File\\fxrate-all.xml";
            string newPath = Path.Combine(webRootPath);
            var rss = new List<RssItem>();
            var feed = XDocument.Load(newPath);
            var items = feed.Descendants("item").ToList();
            foreach (var item in items)
            {
                rss.Add(new RssItem
                {
                    Title = (string)item.Element("title"),
                    Description = (string)item.Element("description"),
                    Link = (string)item.Element("link"),
                    PubDate = (DateTime)item.Element("pubDate")
                });
            }
            var mostRecent = rss.OrderByDescending(r => r.PubDate).Take(5);


            return View();
        }

        /////////////////////////////////////////////////////////////////upsignature////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        public IActionResult upsignature(IFormFile inputFileToLoad, string SignatureDataUrl)
        {

            try
            {
                if (inputFileToLoad != null)
                {
                    if (inputFileToLoad.Length > 0)
                    {

                        string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\";
                        string newPath = Path.Combine(webRootPath, "User");
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        string fileName = ContentDispositionHeaderValue.Parse(inputFileToLoad.ContentDisposition).FileName.Trim('"');
                        string extension = Path.GetExtension(inputFileToLoad.FileName);
                        string _detail_id = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
                        string fname_new = _detail_id + extension;

                        string fullPath = Path.Combine(newPath, fname_new);


                        using (var srcImage = Image.FromStream(inputFileToLoad.OpenReadStream(), true, true))
                        {
                            using (var newImage = new Bitmap(160, 160))
                            using (var graphics = Graphics.FromImage(newImage))
                            {
                                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                graphics.DrawImage(srcImage, new Rectangle(0, 0, 160, 160));
                                newImage.Save(fullPath);
                            }
                        }

                        // Update Statement
                        var update = (from c in _context.AspNetUsers
                                      where c.UserName == _detail_id
                                      select c).FirstOrDefault();
                        if (update != null)
                        {
                            update.OperatorSign = SignatureDataUrl;
                            _context.SaveChanges();
                        }


                    }
                }
            }
            catch (Exception ex)
            {

                ViewBag.mm = ex.Message;

                return RedirectToAction("Privacy");
            }
            return RedirectToAction("Privacy");
        }

        public IActionResult Closeupdate()
        {
            return View();
        }

        public async Task<JsonResult> ControlFlagDoc(String DocumentNo)
        {
            var Data = new { status = true, subject = "Flag", detail = "Complete." };
            try
            {
                var UserId = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;

                var rowdata = _DocumentContext.DocumentFlag.Where(p => p.DocumentNo == DocumentNo && p.UserId == UserId).FirstOrDefault();
                if (rowdata == null)
                {
                    DocumentFlag newkey = new DocumentFlag
                    {
                        DocumentNo = DocumentNo,
                        UserId = UserId,
                        UpdDate = DateTime.Now
                    };
                    _DocumentContext.DocumentFlag.Add(newkey);
                    await _DocumentContext.SaveChangesAsync();
                }
                else
                {
                    _DocumentContext.DocumentFlag.Remove(rowdata);
                    await _DocumentContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Data = new { status = false, subject = "Flag", detail = ex.Message };
            }

            //_repository.Boardcastnotify(DocumentNo,null);
            return Json(Data);
        }



    }
}
