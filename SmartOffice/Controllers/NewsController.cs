using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models;
using SmartOffice.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using SmartOffice.ModelsEsmartOffice;
using System.Drawing;
using Microsoft.Extensions.Logging;

namespace SmartOffice.Controllers
{
    public class NewsController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        private readonly ESmartOfficeContext _ESmartcontext;
        private readonly ILogger<NewsController> _logger;
        public NewsController(IHostingEnvironment hostingEnvironment, ESmartOfficeContext context, ILogger<NewsController> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _ESmartcontext = context;
            _logger = logger;
        }



        /*************************************************************** Main New Start ************************************************************************/

        //Add News
        [HttpPost]
        public ActionResult OnPostUpload(List<IFormFile> files, string title, string content, String Newstype, string childtype, String HeadersName, bool Disable, DateTime start_date, DateTime end_date)
        {
            //var CreateBy = User.Claims.Select(c => c.Type == "UserName").FirstOrDefault().ToString() ;
            var CreateBy = User.Claims.Where(c => c.Type == "UserName").Select(c => c.Value).SingleOrDefault();
            var NewsGroup = User.Claims.Where(c => c.Type == "GroupCateg").Select(c => c.Value).SingleOrDefault();
            DateTime _start_date = new DateTime();
            DateTime _end_date = new DateTime();
            int partid = 7;
            if (Newstype.ToUpper() == "FOOTER")
            {
                partid = 8;
            }
            if (Newstype.ToUpper() == "NEWS")
            {
                childtype = "1";
                _start_date = start_date;
                _end_date = end_date;

            }

            int maxLength = 0;
            var DataNewsH = _ESmartcontext.NnewsHeader.Where(p => p.PartId == partid && p.GroupCateg == NewsGroup).ToList();  
            if (DataNewsH.Count > 0 ){
                maxLength = _ESmartcontext.NnewsHeader.Where(p => p.PartId == partid && p.GroupCateg == NewsGroup).Max(x => x.NewHorder);
            }
       
            if (files != null && files.Count > 0)
            {           
               

                NnewsHeader _news = new NnewsHeader()
                {
                    PartId = partid,
                    Title1 = title.Trim(),
                    Title2 = content,
                    Disable = Disable,
                    CreatedDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreateBy = CreateBy,
                    UpdateBy = CreateBy,
                    GroupCateg = NewsGroup,
                    NewsType = Newstype,
                    NewHorder = maxLength + 1,
                    BadgesName = HeadersName,
                    StartDate = _start_date,
                    EndDate = _end_date,
                    ChildType = int.Parse(childtype)
                };
                List<NnewsDetail> Ldetail = new List<NnewsDetail>();
                string folderName = NewsGroup;
                string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\" + folderName;
                string newPath = Path.Combine(webRootPath, Newstype);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                int i = 0;
                int maxLengthD = 0;
                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                        string extension = Path.GetExtension(item.FileName);
                        string _detail_id = GenerateNewsDetailID();
                        string fname_new = _detail_id + extension;
                        string fullPath = Path.Combine(newPath, fname_new);
                        if (i >= 1)
                        {
                            maxLengthD = maxLengthD + 1;
                        }
                        else
                        {
                            maxLengthD = maxLength;
                        }

                        NnewsDetail news_item = new NnewsDetail()
                        {
                            PartId = _news.PartId,
                            GroupCateg = _news.GroupCateg,
                            NewHorder = _news.NewHorder,
                            NewDorder = maxLengthD + 1,
                            ItemType = CheckFileType(extension),
                            Value1 = fileName,
                            Value = fname_new,
                            CreatedDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            CreateBy = CreateBy,
                            UpdateBy = CreateBy,
                            ShowPublic = false,
                        };
                        i++;
                        Ldetail.Add(news_item);


                        if (news_item.ItemType == "image" && Newstype.ToUpper() == "NEWS")
                        {

                            using (var srcImage = Image.FromStream(item.OpenReadStream(), true, true))
                            {
                                using (var newImage = new Bitmap(1200, 675))
                                using (var graphics = Graphics.FromImage(newImage))
                                {
                                    graphics.DrawImage(srcImage, new Rectangle(0, 0, 1200, 675));
                                    newImage.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                            }
                        }
                        else if (news_item.ItemType == "image")
                        {
                            using (var srcImage = Image.FromStream(item.OpenReadStream(), true, true))
                            {
                                srcImage.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                        }
                        else
                        {
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                item.CopyTo(stream);
                            }
                        }
                    }
                }
                try
                {
                    /* using(TransactionScope scope = new TransactionScope())
                   { */
                    _ESmartcontext.NnewsHeader.Add(_news);
                    _ESmartcontext.NnewsDetail.AddRange(Ldetail);
                    _ESmartcontext.SaveChanges();


                    // _Doccontext.SaveChangesAsync();
                    /*  scope.Complete();
                 } */
                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "ADD NEWS", detail = e.Message };
                    return Json(Data1);
                }

            }
            else
            {                     
                NnewsHeader _news = new NnewsHeader()
                {
                    PartId = partid,
                    Title1 = title.Trim(),
                    Title2 = content,
                    Disable = Disable,
                    CreatedDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreateBy = CreateBy,
                    UpdateBy = CreateBy,
                    GroupCateg = NewsGroup,
                    StartDate = _start_date,
                    EndDate = _end_date,
                    NewsType = Newstype,
                    NewHorder = maxLength + 1,
                    BadgesName = HeadersName,
                    ChildType = int.Parse(childtype)
                };
                try
                {

                    _ESmartcontext.NnewsHeader.Add(_news);
                    _ESmartcontext.SaveChanges();
                    _logger.LogInformation(User.Identity.Name + " >> Add New.  >>> Subject:  "+ _news.Title1);
                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "ADD NEWS", detail = e.Message };
                    return Json(Data1);
                }
            }
            var Data = new { status = true, subject = "ADD NEWS", detail = "System add news complete." };
            return Json(Data);
        }

        //Update News
        [HttpPost]
        public ActionResult UpdateNews(List<IFormFile> files, String DocCat, String NewsOrder, string Title, string Status, string NewsDetail, string childtype, string NewsType, bool Disable, DateTime start_date, DateTime end_date, string HeadersName)
        {
            int partid = 7;
            DateTime _start_date = new DateTime();
            DateTime _end_date = new DateTime();
            var username = User.Claims.Where(c => c.Type == "UserName").Select(c => c.Value).SingleOrDefault();          
            var userGroup = User.Claims.Where(c => c.Type == "GroupCateg").Select(c => c.Value).SingleOrDefault();
            if (NewsType.ToUpper() == "FOOTER")
            {
                partid = 8;
            }
            if (NewsType.ToUpper() == "NEWS")
            {
                _start_date = start_date;
                _end_date = end_date;

            }
            NnewsHeader newss = _ESmartcontext.NnewsHeader.Where(p => p.PartId == partid && p.GroupCateg == DocCat && p.NewHorder == int.Parse(NewsOrder)).FirstOrDefault();

            if (newss != null)
            {

                newss.Title1 = Title.Trim();
                newss.Title2 = NewsDetail;
                newss.UpdateDate = DateTime.Now;
                newss.UpdateBy = username;
                newss.StartDate = _start_date;
                newss.EndDate = _end_date;
                newss.Disable = Disable;
                newss.BadgesName = HeadersName;

                if (newss.NewsType.ToUpper() != "NEWS")
                {
                    newss.ChildType = int.Parse(childtype);
                }


                List<NnewsDetail> Ldetail = new List<NnewsDetail>();
                string folderName = newss.GroupCateg;
                string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\" + folderName;
                string newPath = Path.Combine(webRootPath, newss.NewsType);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                int i = 0;
                int maxLengthD = 0;
                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {
                        string ContentType = item.ContentType;
                        string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                        string extension = Path.GetExtension(item.FileName);
                        string _detail_id = GenerateNewsDetailID();
                        string fname_new = _detail_id + extension;
                        string fullPath = Path.Combine(newPath, fname_new);

                        if (i >= 1)
                        {
                            maxLengthD = maxLengthD + 1;
                        }
                        else
                        {
                            maxLengthD = _ESmartcontext.NnewsDetail.Where(p => p.PartId == partid
                                                                                && p.GroupCateg == userGroup
                                                                                && p.NewHorder == newss.NewHorder).Select(x=>x.NewDorder).DefaultIfEmpty(0).Max();
                        }

                        NnewsDetail news_item = new NnewsDetail()
                        {
                            PartId = newss.PartId,
                            GroupCateg = newss.GroupCateg,
                            NewHorder = newss.NewHorder,
                            NewDorder = maxLengthD + 1,
                            ItemType = CheckFileType(extension),
                            Value1 = fileName,
                            Value = fname_new,
                            UpdateBy = username,
                            UpdateDate= DateTime.Now,
                            CreateBy = username,
                            CreatedDate = DateTime.Now,
                            ShowPublic = false,
                        };
                        i++;
                        Ldetail.Add(news_item);


                        if (news_item.ItemType == "image")
                        {

                            using (var srcImage = Image.FromStream(item.OpenReadStream(), true, true))
                            {
                                using (var newImage = new Bitmap(900, 540))
                                using (var graphics = Graphics.FromImage(newImage))
                                {

                                    graphics.DrawImage(srcImage, new Rectangle(0, 0, 900, 540));
                                    newImage.Save(fullPath);
                                }
                            }
                        }
                        else if (news_item.ItemType == "image")
                        {
                            using (var srcImage = Image.FromStream(item.OpenReadStream(), true, true))
                            {
                                srcImage.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                        }
                        else
                        {
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                item.CopyTo(stream);
                            }
                        }
                    }
                }
                try
                {
                    /* using(TransactionScope scope = new TransactionScope())
                   { */
                    _ESmartcontext.NnewsHeader.Update(newss);
                    _ESmartcontext.NnewsDetail.AddRange(Ldetail);
                    _ESmartcontext.SaveChanges();
                    _logger.LogInformation(User.Identity.Name + " Update aticle >>> "+ newss.Title1);
                    // _Doccontext.SaveChangesAsync();
                    /*  scope.Complete();
                 } */
                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "UPDATE ITEMS", detail = e.Message };
                    return Json(Data1);
                }
            }
            var Data = new { status = true, subject = "UPDATE ITEMS", detail = "Update item Complete." };
            return Json(Data);
        }

        //Delete News
        [HttpPost]
        public async Task<JsonResult> DeleteNews(NnewsHeader _news)
        {
            int partid = 7;
            if (_news.NewsType.ToUpper() == "FOOTER")
            {
                partid = 8;
            }
            NnewsHeader TNews = await _ESmartcontext.NnewsHeader.Where(p => p.PartId == partid && p.GroupCateg == _news.GroupCateg && p.NewHorder == int.Parse(_news.NewHorder.ToString())).FirstOrDefaultAsync();
            var Ndetail = await _ESmartcontext.NnewsDetail.Where(p => p.PartId == partid && p.GroupCateg == _news.GroupCateg && p.NewHorder == int.Parse(_news.NewHorder.ToString())).ToListAsync();
            if (TNews != null)
            {
                try
                {
                    _ESmartcontext.NnewsHeader.Remove(TNews);
                    foreach (var item in Ndetail)
                    {
                        System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/image/" + TNews.GroupCateg + "/" + _news.NewsType + "/" + item.Value);

                    }
                    _ESmartcontext.NnewsDetail.RemoveRange(Ndetail);
                    await _ESmartcontext.SaveChangesAsync();
                    _logger.LogInformation(User.Identity.Name + " >> Delete New.  >>> Subject:  " + TNews.Title1);
                    var Data = new { status = true, subject = "Delete Item", detail = "Delete Item Complete." };
                    return Json(Data);
                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "Delete Item", detail = e.Message };
                    return Json(Data1);
                }
            }
            else
            {
                var Data = new { status = false, subject = "Delete Item", detail = "System is can't delete item." };
                return Json(Data);
            }
            //return new JsonResult { Data = new { status = true, subject = "Delete Model", detail = "Delete Model  : " + delete_model.model_name } };

        }

        //Delete File or picture in News topic
        public async Task<JsonResult> DeletePicture(String DocCat, String NewHorder, string NewsDOrder, string PartId)
        {

            int partid = int.Parse(PartId);


            var TNews = _ESmartcontext.NnewsDetail.Where(p => p.PartId == partid && p.GroupCateg == DocCat && p.NewHorder == int.Parse(NewHorder) && p.NewDorder == int.Parse(NewsDOrder)).FirstOrDefault();
            if (TNews != null)
            {
                NnewsHeader newss = _ESmartcontext.NnewsHeader.Where(p => p.PartId == partid && p.GroupCateg == DocCat && p.NewHorder == int.Parse(NewHorder)).FirstOrDefault();
                _ESmartcontext.NnewsDetail.Remove(TNews);

                System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/image/" + newss.GroupCateg + "/" + newss.NewsType + "/" + TNews.Value);


                await _ESmartcontext.SaveChangesAsync();

                var Data = new { status = true, subject = "Delete Picture", detail = "Delete Picture Complete." };
                return Json(Data);
            }
            else
            {
                var Data = new { status = false, subject = "Delete Picture", detail = "System is can't delete Picture." };
                return Json(Data);
            }
            //return new JsonResult { Data = new { status = true, subject = "Delete Model", detail = "Delete Model  : " + delete_model.model_name } };

        }
        

        public async Task<JsonResult> SetShowPublic(String DocCat, String NewHorder, string NewsDOrder, string PartId)
        {

            int partid = int.Parse(PartId);


            var TNews = _ESmartcontext.NnewsDetail.Where(p => p.PartId == partid && p.GroupCateg == DocCat && p.NewHorder == int.Parse(NewHorder) && p.NewDorder == int.Parse(NewsDOrder)).FirstOrDefault();
            if (TNews != null && TNews.ShowPublic == false)
            {

                TNews.ShowPublic = true;
                await _ESmartcontext.SaveChangesAsync();

                var Data = new { status = true, subject = "Set Picture", detail = "set Picture Complete." };
                return Json(Data);
            }
            else if(TNews != null && TNews.ShowPublic == true)
            {
                TNews.ShowPublic = false;
                await _ESmartcontext.SaveChangesAsync();
                var Data = new { status = true, subject = "Set Picture", detail = "set Picture Complete." };
                return Json(Data);
            }
            else
            {
                var Data = new { status = false, subject = "set Picture", detail = "System is can't set Picture." };
                return Json(Data);
            }
            //return new JsonResult { Data = new { status = true, subject = "Delete Model", detail = "Delete Model  : " + delete_model.model_name } };

        }
        //Check File type picture oe document

        private string CheckFileType(string fe)
        {
            string filetype = "";
            switch (fe.ToLower())
            {
                case ".jpg":
                case ".gif":
                case ".png":
                case ".jpeg":
                case ".tiff":
                    filetype = "image";
                    break;
                case ".pdf":
                case ".docx":
                case ".doc":
                case ".docm":
                case ".dotx":
                case ".dotm":
                case ".xls":
                case ".xlsx":
                case ".xlsm":
                case ".xltx":
                case ".xltm":
                case ".xlsb":
                case ".xlam":
                case ".pptx":
                case ".pptm":
                case ".ppsx":
                case ".ppsm":
                case ".zip":
                case ".7z":
                    filetype = "document";
                    break;
                case ".mp4":
                case ".avi":
                case ".m4v":
                case ".mov":
                case ".mpg":
                case ".mpeg":
                case ".swf":
                    filetype = "video";
                    break;
            }
            return filetype;
        }

        //Generate primary key News ID
        private string GenerateNewsID()
        {


            var strSeqNo = string.Empty;
            var strPrefix = "NW"; // Prefix : NW-
            var intLength = 9; // Length : NW-XXXXX

            var intYear = DateTime.Now.Year;
            var intMonth = DateTime.Now.Month;
            var intSequence = 0;


            GenerateNumber nowkey = _ESmartcontext.GenerateNumber.Where(x => x.Year == intYear && x.Month == intMonth && x.TypeId == "NW").FirstOrDefault();

            if (nowkey != null)
            {
                intSequence = Convert.ToInt32(nowkey.Sequence);
            }


            //*** Insert new month (when new month)
            if (intSequence == 0)
            {
                intSequence = 1;
                GenerateNumber newkey = new GenerateNumber
                {
                    Year = intYear,
                    Month = intMonth,
                    Sequence = intSequence,
                    TypeId = "NW"
                };
                _ESmartcontext.GenerateNumber.Add(newkey);
                _ESmartcontext.SaveChanges();
            }


            //*** Update new sequence
            GenerateNumber nowkeyup = _ESmartcontext.GenerateNumber.Where(x => x.Year == intYear && x.Month == intMonth && x.TypeId == "NW").FirstOrDefault();
            if (nowkeyup != null)
            {
                int nextstep = int.Parse(nowkeyup.Sequence.ToString()) + 1;
                nowkeyup.Sequence = nextstep;
                _ESmartcontext.SaveChanges();
            }


            //*** Display sequence
            strSeqNo = string.Format("{0}-{1}-{2}-{3}", strPrefix, intYear,
                intMonth.ToString().PadLeft(2, '0'), intSequence.ToString().PadLeft(intLength, '0'));

            return strSeqNo;
        }

        //Generate primary key NewsDetail ID
        private string GenerateNewsDetailID()
        {


            var strSeqNo = string.Empty;
            var strPrefix = "ND"; // Prefix : NW-
            var intLength = 9; // Length : NW-XXXXX

            var intYear = DateTime.Now.Year;
            var intMonth = DateTime.Now.Month;
            var intSequence = 0;


            GenerateNumber nowkey = _ESmartcontext.GenerateNumber.Where(x => x.Year == intYear && x.Month == intMonth && x.TypeId == "ND").FirstOrDefault();

            if (nowkey != null)
            {

                intSequence = Convert.ToInt32(nowkey.Sequence);
            }


            //*** Insert new month (when new month)
            if (intSequence == 0)
            {
                intSequence = 1;

                GenerateNumber newkey = new GenerateNumber
                {
                    Year = intYear,
                    Month = intMonth,
                    Sequence = intSequence,
                    TypeId = "ND"
                };
                _ESmartcontext.GenerateNumber.Add(newkey);
                _ESmartcontext.SaveChanges();
            }


            //*** Update new sequence
            GenerateNumber nowkeyup = _ESmartcontext.GenerateNumber.Where(x => x.Year == intYear && x.Month == intMonth && x.TypeId == "ND").FirstOrDefault();
            if (nowkeyup != null)
            {
                int nextstep = int.Parse(nowkeyup.Sequence.ToString()) + 1;
                nowkeyup.Sequence = nextstep;
                _ESmartcontext.SaveChanges();
            }
            //*** Display sequence
            strSeqNo = string.Format("{0}-{1}-{2}-{3}", strPrefix, intYear,
            intMonth.ToString().PadLeft(2, '0'), intSequence.ToString().PadLeft(intLength, '0'));

            return strSeqNo;
        }

        //function Return News to view
        public JsonResult GetNewsAsync(String newstype)
        {
            int partid = 7;
            if (newstype.ToUpper() == "FOOTER")
            {
                partid = 8;
            }

            try
            {
                var _user = User.Claims.Where(c => c.Type == "UserName").Select(c => c.Value).SingleOrDefault();
                var _group = User.Claims.Where(c => c.Type == "GroupCateg").Select(c => c.Value).SingleOrDefault();
                List<ViewNewsTable> allNews = _ESmartcontext.NnewsHeader.Where(i => i.PartId == partid && i.GroupCateg == _group && i.NewsType == newstype)
                .ToList()
                .Select(p => new ViewNewsTable()
                {
                    PartId = p.PartId,
                    Title1 = p.Title1,
                    Disable = p.Disable,
                    CreatedDate = Convert.ToDateTime(p.CreatedDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    CreateBy = p.CreateBy,
                    UpdateDate = Convert.ToDateTime(p.UpdateDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    GroupCateg = p.GroupCateg,
                    NewsType = p.NewsType,
                    Title2 = p.Title2,
                    NewHorder = p.NewHorder,
                    UpdateBy = p.UpdateBy,
                    ChildType = p.ChildType,
                    BadgesName = p.BadgesName,
                    StartDate = p.StartDate != null ? p.StartDate.Value.Date.ToString("dd-MM-yyyy") : "",
                    EndDate = p.EndDate != null ? p.EndDate.Value.Date.ToString("dd-MM-yyyy") : "",
                    imagePath = p.NnewsDetail.Count() == 0 ? "" : "../image/" + p.GroupCateg + "/" + p.NewsType + "/" + p.NnewsDetail.FirstOrDefault().Value,
                    ItemType = p.NnewsDetail.Count() == 0 ? "" : p.NnewsDetail.FirstOrDefault().ItemType,
                    Value1 = p.NnewsDetail.Count() == 0 ? "" : p.NnewsDetail.FirstOrDefault().Value1,
                }).OrderByDescending(i => i.NewHorder).ToList();
                return Json(new { data = allNews });
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }


        //Function return picture
        public JsonResult GetPictureAsync(String DocCat, String NewsOrder, String PartId)
        {
            try
            {
                var _news = _ESmartcontext.NnewsHeader.Where(p => p.PartId == int.Parse(PartId) && p.GroupCateg == DocCat && p.NewHorder == int.Parse(NewsOrder)).FirstOrDefault();
                List<ViewNewsdetail> allNews = _ESmartcontext.NnewsDetail.Where(i => i.PartId == int.Parse(PartId) && i.GroupCateg == DocCat && i.NewHorder == _news.NewHorder).ToList().Select(p => new ViewNewsdetail()
                {
                    PartId = p.PartId,
                    GroupCateg = p.GroupCateg,
                    NewHorder = p.NewHorder,
                    NewDorder = p.NewDorder,
                    ItemType = p.ItemType,
                    Value = p.Value,
                    Value1 = p.Value1,
                    ImagePath = PathfileCheck(_news.GroupCateg, p.Value, p.ItemType, _news.NewsType),
                    ShowPublic = p.ShowPublic

                }).ToList();
                return Json(new { data = allNews });
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw new NotImplementedException();
            }
        }

        private string PathfileCheck(string NewsGroup, string DetailContent, string DetailType, string newstype)
        {
            string filepath = "";

            if (DetailType == "image" || DetailType == "video" || DetailType == "document")
            {
                return "../image/" + NewsGroup + "/" + newstype + "/" + DetailContent;
            }
            else if (DetailType == "")
            {
                return filepath;
            }
            else
            {
                return "../image/icon/Docs-icon.png";
            }
        }
        public IActionResult Index(String DocCat, String NewsOrder)
        {
            var _news = _ESmartcontext.NnewsHeader.Where(p => p.PartId == 7 && p.GroupCateg == DocCat && p.NewHorder == int.Parse(NewsOrder)).FirstOrDefault();
            if (_news.ChildType == 6)
            {
                return ViewComponent("NewsControl", new { DeptType = DocCat, newstyle = "Pr", _NewHOrder = _news.NewHorder.ToString(), });
            }
            else
            {
                return ViewComponent("NewsControl", new { DeptType = DocCat, newstyle = "NewsDetail", _NewHOrder = _news.NewHorder.ToString(), });
            }
        }
        public IActionResult Pageshow(String DocCat, String NewsOrder)
        {
            var _news = _ESmartcontext.NnewsHeader.Where(p => p.PartId == 8 && p.GroupCateg == DocCat && p.NewHorder == int.Parse(NewsOrder)).FirstOrDefault();
            if (_news.ChildType == 6)
            {
                return ViewComponent("NewsControl", new { DeptType = DocCat, newstyle = "Viewpage", _NewHOrder = _news.NewHorder.ToString(), });
            }
            else
            {
                return ViewComponent("NewsControl", new { DeptType = DocCat, newstyle = "ViewpageNews", _NewHOrder = _news.NewHorder.ToString(), });
            }
        }
        public IActionResult ViewAll(String newstyle, String DeptType)
        {
            return ViewComponent("NewsControl", new { newstyle, DeptType });
        }

        /************************************************ Main New End ***************************************************************/
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ESmartcontext.Dispose();
            }
            base.Dispose(disposing);
        }

        /************************************************ Main Header slide start ***************************************************************/
        //Add Header
        public JsonResult AddSlide(IFormFile filesImage, IFormFile filesAttach, bool ImgActive, String ImgType, String Link, String LinkName, String LinkPath, bool Disable, DateTime startdate, DateTime enddate)
        {
            var CreateBy = User.Claims.Where(c => c.Type == "UserName").Select(c => c.Value).SingleOrDefault();
            var NewsGroup = User.Claims.Where(c => c.Type == "GroupCateg").Select(c => c.Value).SingleOrDefault();
            var UpdateBy = CreateBy;
            int partid = 6;
            int maxLength = _ESmartcontext.ImgHeader.Where(p => p.PartId == partid && p.GroupCateg == NewsGroup).Select(x=>x.ImgOrder).DefaultIfEmpty(0).Max();

            ImgHeader _Header = new ImgHeader
            {
                PartId = partid,
                GroupCateg = NewsGroup,
                ImgOrder = maxLength + 1,
                Disable = Disable,
                ImgType = ImgType == "1" ? "FULL" : ImgType == "2" ?  "HALF" : "LEFT",
                ImgActive = ImgActive,
                CreateBy = CreateBy,
                UpdateBy = UpdateBy,
                StartDate = startdate,
                EndDate = enddate
            };

            string folderName = NewsGroup;
            string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\" + folderName;
            string newPath = Path.Combine(webRootPath, "Header");
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (filesImage != null)
            {
                if (filesImage.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(filesImage.ContentDisposition).FileName.Trim('"');
                    string extension = Path.GetExtension(filesImage.FileName);
                    string _detail_id = GenerateNewsDetailID();
                    string fname_new = _detail_id + extension;
                    _Header.ImgPath = fname_new;
                    string fullPath = Path.Combine(newPath, fname_new);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        filesImage.CopyTo(stream);
                    }

                    //using (var srcImage = Image.FromStream(filesImage.OpenReadStream(), true, true))
                    //{
                    //    using (var newImage = new Bitmap(1920, 1080))
                    //    using (var graphics = Graphics.FromImage(newImage))
                    //    {
                    //        graphics.DrawImage(srcImage, new Rectangle(0, 0, 1920, 1080));
                    //        newImage.Save(fullPath);
                    //    }
                    //}

                    if (Link == "2" || Link == "3")
                    {
                        if (filesAttach != null)
                        {
                            if (filesAttach.Length > 0)
                            {
                                webRootPath = _hostingEnvironment.WebRootPath + "\\File\\" + folderName;
                                newPath = Path.Combine(webRootPath, "Header");
                                if (!Directory.Exists(newPath))
                                {
                                    Directory.CreateDirectory(newPath);
                                }
                                fileName = ContentDispositionHeaderValue.Parse(filesAttach.ContentDisposition).FileName.Trim('"');
                                extension = Path.GetExtension(filesAttach.FileName);
                                _detail_id = GenerateNewsDetailID();
                                fname_new = _detail_id + extension;
                                _Header.LinkPath = fname_new;

                                fullPath = Path.Combine(newPath, fname_new);

                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    filesAttach.CopyTo(stream);
                                }
                            }
                        }
                        _Header.LinkName = LinkName;
                        _Header.Download = true;
                        _Header.Link = false;

                        if (Link == "3")
                        {
                            _Header.Link = true;
                        }
                    }
                    else if (Link == "1")
                    {
                        _Header.LinkPath = LinkPath;
                        _Header.LinkName = LinkName;
                        _Header.Link = true;
                        _Header.Download = false;
                    }
                    else if (Link == "0")
                    {
                        _Header.LinkPath = "";
                        _Header.LinkName = "";
                        _Header.Link = false;
                        _Header.Download = false;
                    }

                    try
                    {

                        _ESmartcontext.ImgHeader.Add(_Header);
                        _ESmartcontext.SaveChanges();
                        //update active
                        var chkactive = _ESmartcontext.ImgHeader.Where(p => p.PartId == partid && p.GroupCateg == NewsGroup && p.ImgActive == true && 
                        p.ImgOrder != _Header.ImgOrder && p.Disable == _Header.Disable && p.ImgType != "LEFT").ToList();
                        if (chkactive.Count() > 0 && _Header.ImgActive == true && _Header.ImgType != "LEFT")
                        {
                            foreach (var item in chkactive)
                            {
                                item.ImgActive = false;

                            }
                            _ESmartcontext.ImgHeader.UpdateRange(chkactive);
                            _ESmartcontext.SaveChanges();
                        }

                        //update active
                        var chkactiveleft = _ESmartcontext.ImgHeader.Where(p => p.PartId == partid && p.GroupCateg == NewsGroup && p.ImgActive == true &&
                        p.ImgOrder != _Header.ImgOrder && p.Disable == _Header.Disable && p.ImgType == "LEFT").ToList();
                        if (chkactiveleft.Count() > 0 && _Header.ImgActive == true && _Header.ImgType == "LEFT")
                        {
                            foreach (var item in chkactiveleft)
                            {
                                item.ImgActive = false;

                            }
                            _ESmartcontext.ImgHeader.UpdateRange(chkactiveleft);
                            _ESmartcontext.SaveChanges();
                        }

                    }
                    catch (Exception e)
                    {
                        var Data1 = new { status = false, subject = "ADD SLIDE", detail = e.Message };
                        return Json(Data1);
                    }
                }
            }
            var Data = new { status = true, subject = "ADD SLIDE", detail = "System add slide complete." };
            return Json(Data);
        }

        //Update Header
        [HttpPost]
        public ActionResult UpdateSlide(IFormFile filesImage, IFormFile filesAttach, int imgOrder, bool ImgActive, String ImgType, String Link, String LinkName, String LinkPath, bool Disable, DateTime startdate, DateTime enddate)
        {
            var CreateBy = User.Claims.Where(c => c.Type == "UserName").Select(c => c.Value).SingleOrDefault();
            var NewsGroup = User.Claims.Where(c => c.Type == "GroupCateg").Select(c => c.Value).SingleOrDefault();
            int partid = 6;
            ImgHeader _Headerup = _ESmartcontext.ImgHeader.Where(p => p.PartId == partid && p.GroupCateg == NewsGroup && p.ImgOrder == imgOrder).FirstOrDefault();

            if (_Headerup != null)
            {
                var downloadold = _Headerup.Download;
                var LinkPathold = _Headerup.LinkPath;
                var ImgPathold = _Headerup.ImgPath;

                _Headerup.Disable = Disable;
                _Headerup.ImgType = ImgType == "1" ? "FULL" : ImgType == "2" ? "HALF" : "LEFT";
                _Headerup.ImgActive = ImgActive;
                _Headerup.UpdateBy = CreateBy;
                _Headerup.UpdateDate = DateTime.Now;
                _Headerup.StartDate = startdate;
                _Headerup.EndDate = enddate;

                string folderName = NewsGroup;

                if (filesImage != null)
                {
                    if (filesImage.Length > 0)
                    {
                        string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\" + folderName;
                        string newPath = Path.Combine(webRootPath, "Header");
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        string fileName = ContentDispositionHeaderValue.Parse(filesImage.ContentDisposition).FileName.Trim('"');
                        string extension = Path.GetExtension(filesImage.FileName);
                        string _detail_id = GenerateNewsDetailID();
                        string fname_new = _detail_id + extension;
                        _Headerup.ImgPath = fname_new;
                        string fullPath = Path.Combine(newPath, fname_new);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            filesImage.CopyTo(stream);
                        }
                        //using (var srcImage = Image.FromStream(filesImage.OpenReadStream(), true, true))
                        //{
                        //    using (var newImage = new Bitmap(1920, 1080))
                        //    using (var graphics = Graphics.FromImage(newImage))
                        //    {
                        //        graphics.DrawImage(srcImage, new Rectangle(0, 0, 1920, 1080));
                        //        newImage.Save(fullPath);
                        //    }
                        //}
                    }
                }

                if (Link == "2" || Link == "3")
                {
                    if (filesAttach != null)
                    {
                        if (filesAttach.Length > 0)
                        {
                            string webRootPath = _hostingEnvironment.WebRootPath + "\\File\\" + folderName;
                            string newPath = Path.Combine(webRootPath, "Header");
                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }
                            string fileName = ContentDispositionHeaderValue.Parse(filesAttach.ContentDisposition).FileName.Trim('"');
                            string extension = Path.GetExtension(filesAttach.FileName);
                            string _detail_id = GenerateNewsDetailID();
                            string fname_new = _detail_id + extension;
                            _Headerup.LinkPath = fname_new;

                            string fullPath = Path.Combine(newPath, fname_new);

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                filesAttach.CopyTo(stream);
                            }
                        }
                    }
                    _Headerup.LinkName = LinkName;
                    _Headerup.Download = true;
                    _Headerup.Link = false;

                    if (Link == "3")
                    {
                        _Headerup.Link = true;
                    }
                }
                else if (Link == "1")
                {
                    _Headerup.LinkPath = LinkPath;
                    _Headerup.LinkName = LinkName;
                    _Headerup.Link = true;
                    _Headerup.Download = false;
                }
                else if (Link == "0")
                {
                    _Headerup.LinkPath = "";
                    _Headerup.LinkName = "";
                    _Headerup.Link = false;
                    _Headerup.Download = false;
                }
                try
                {

                    _ESmartcontext.ImgHeader.Update(_Headerup);
                    _ESmartcontext.SaveChanges();
                    //update active
                    var chkactive = _ESmartcontext.ImgHeader.Where(p => p.PartId == partid && p.GroupCateg == NewsGroup && p.ImgActive == true &&
                    p.ImgOrder != _Headerup.ImgOrder && p.Disable == _Headerup.Disable && p.ImgType != "LEFT").ToList();
                    if (chkactive.Count() > 0 && _Headerup.ImgActive == true && _Headerup.ImgType != "LEFT")
                    {
                        foreach (var item in chkactive)
                        {
                            item.ImgActive = false;

                        }
                        _ESmartcontext.ImgHeader.UpdateRange(chkactive);
                        _ESmartcontext.SaveChanges();
                    }

                    //update active
                    var chkactiveleft = _ESmartcontext.ImgHeader.Where(p => p.PartId == partid && p.GroupCateg == NewsGroup && p.ImgActive == true &&
                    p.ImgOrder != _Headerup.ImgOrder && p.Disable == _Headerup.Disable && p.ImgType == "LEFT").ToList();
                    if (chkactiveleft.Count() > 0 && _Headerup.ImgActive == true && _Headerup.ImgType == "LEFT")
                    {
                        foreach (var item in chkactiveleft)
                        {
                            item.ImgActive = false;

                        }
                        _ESmartcontext.ImgHeader.UpdateRange(chkactiveleft);
                        _ESmartcontext.SaveChanges();
                    }

                    //delete attach
                    if (filesAttach != null)
                    {
                        if (filesAttach.Length > 0)
                        {
                            if (downloadold == true && LinkPathold != "")
                            {
                                System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/File/" + _Headerup.GroupCateg + "/Header/" + LinkPathold);
                            }

                        }
                    }
                    else if (Link == "0")
                    {
                        if (downloadold == true && LinkPathold != "")
                        {
                            System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/File/" + _Headerup.GroupCateg + "/Header/" + LinkPathold);
                        }
                    }

                    if (filesImage != null)
                    {
                        if (filesImage.Length > 0)
                        {

                            System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/image/" + _Headerup.GroupCateg + "/Header/" + ImgPathold);
                        }
                    }

                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "UPDATE SLIDE", detail = e.Message };
                    return Json(Data1);
                }
            }
            var Data = new { status = true, subject = "UPDATE SLIDE", detail = "System update slide complete." };
            return Json(Data);
        }

        //get header slide
        public JsonResult GetSlideAsync(String newstype)
        {
            try
            {
                var _user = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
                var _group = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
                List<ViewSlideTable> allSlide = _ESmartcontext.ImgHeader.Where(i => (i.PartId == 6 && i.GroupCateg == _group))
                .ToList()
                .Select(p => new ViewSlideTable()
                {

                    PartId = p.PartId,
                    GroupCateg = p.GroupCateg,
                    ImgOrder = p.ImgOrder,
                    ImgPath = "../image/" + p.GroupCateg + "/" + "Header" + "/" + p.ImgPath,
                    ImgActive = p.ImgActive,
                    ImgType = p.ImgType,
                    Link = p.Link,
                    LinkName = p.LinkName,
                    LinkPath = p.LinkPath,
                    Download = p.Download,
                    Disable = p.Disable,
                    CreatedDate = Convert.ToDateTime(p.CreatedDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    CreateBy = p.CreateBy,
                    UpdateDate = Convert.ToDateTime(p.UpdateDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    UpdateBy = p.UpdateBy,
                    StartDate = Convert.ToDateTime(p.StartDate).ToString("dd/MM/yyyy"),
                    EndDate = Convert.ToDateTime(p.EndDate).ToString("dd/MM/yyyy"),

                }).ToList();
                return Json(new { data = allSlide });
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }

        //delete header slide
        [HttpPost]
        public async Task<JsonResult> DeleteHeader(ImgHeader _Header)
        {
            int partid = 6;
            ImgHeader imgH = await _ESmartcontext.ImgHeader.Where(p => p.PartId == partid && p.GroupCateg == _Header.GroupCateg && p.ImgOrder == int.Parse(_Header.ImgOrder.ToString())).FirstOrDefaultAsync();
            var textH = await _ESmartcontext.ImgTextHeader.Where(p => p.PartId == partid && p.GroupCateg == _Header.GroupCateg && p.ImgOrder == int.Parse(_Header.ImgOrder.ToString())).ToListAsync();
            if (imgH != null)
            {
                foreach (var item in textH)
                {
                    if (item.Download == true && item.LinkPath != "")
                    {
                        System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/File/" + imgH.GroupCateg + "/Header/" + item.LinkPath);
                    }
                }

                if (imgH.Download == true && imgH.LinkPath != "")
                {
                    System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/File/" + imgH.GroupCateg + "/Header/" + imgH.LinkPath);
                }

                System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/image/" + imgH.GroupCateg + "/Header/" + imgH.ImgPath);

                _ESmartcontext.ImgTextHeader.RemoveRange(textH);
                _ESmartcontext.ImgHeader.Remove(imgH);
                await _ESmartcontext.SaveChangesAsync();

                var Data = new { status = true, subject = "Delete Item", detail = "Delete Item Complete." };
                return Json(Data);
            }
            else
            {
                var Data = new { status = false, subject = "Delete Item", detail = "System is can't delete item." };
                return Json(Data);
            }
            //return new JsonResult { Data = new { status = true, subject = "Delete Model", detail = "Delete Model  : " + delete_model.model_name } };

        }

        //Get text header
        public JsonResult GettextheaderAsync(String DocCat, int NewsOrder, String PartId)
        {
            try
            {
                List<ViewSlideTable2> allText = _ESmartcontext.ImgTextHeader.Where(i => i.PartId == int.Parse(PartId) && i.GroupCateg == DocCat && i.ImgOrder == NewsOrder).ToList().Select(p => new ViewSlideTable2()
                {
                    PartId = p.PartId,
                    GroupCateg = p.GroupCateg,
                    ImgOrder = p.ImgOrder,
                    TextHorder = p.TextHorder,
                    TextH = p.TextH,
                    TextD = p.TextD,
                    Link = p.Link,
                    LinkPath = p.LinkPath,
                    Download = p.Download,
                    CreatedDate = Convert.ToDateTime(p.CreatedDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    CreateBy = p.CreateBy,
                    UpdateDate = Convert.ToDateTime(p.UpdateDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    UpdateBy = p.UpdateBy

                }).ToList();
                return Json(new { data = allText });
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw new NotImplementedException();
            }
        }

        //Add text Header
        public JsonResult AddSlideT(IFormFile filesAttach, int ImgOrder, string Link, string LinkName, string LinkPath, string TextH, string TextD)
        {               
            var CreateBy = User.Claims.Where(c => c.Type == "UserName").Select(c => c.Value).SingleOrDefault();
            var NewsGroup = User.Claims.Where(c => c.Type == "GroupCateg").Select(c => c.Value).SingleOrDefault();

            int partid = 6;
            int maxLength = _ESmartcontext.ImgTextHeader.Where(p => p.PartId == partid && p.GroupCateg == NewsGroup && p.ImgOrder == ImgOrder).Select(x=>x.TextHorder).DefaultIfEmpty(0).Max();
            ImgTextHeader _Header = new ImgTextHeader
            {
                PartId = partid,
                GroupCateg = NewsGroup,
                ImgOrder = ImgOrder,
                TextHorder = maxLength + 1,
                CreateBy = CreateBy,
                UpdateBy = CreateBy,
                TextH = TextH,
                TextD = TextD
            };

            string folderName = NewsGroup;
            string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\" + folderName;
            string newPath = Path.Combine(webRootPath, "Header");
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (Link == "2" || Link == "3")
            {
                if (filesAttach != null)
                {
                    if (filesAttach.Length > 0)
                    {
                        webRootPath = _hostingEnvironment.WebRootPath + "\\File\\" + folderName;
                        newPath = Path.Combine(webRootPath, "Header");
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        string fileName = ContentDispositionHeaderValue.Parse(filesAttach.ContentDisposition).FileName.Trim('"');
                        string extension = Path.GetExtension(filesAttach.FileName);
                        string _detail_id = GenerateNewsDetailID();
                        string fname_new = _detail_id + extension;
                        _Header.LinkPath = fname_new;

                        string fullPath = Path.Combine(newPath, fname_new);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            filesAttach.CopyTo(stream);
                        }
                    }
                }

                _Header.Download = true;
                _Header.Link = false;

                if (Link == "3")
                {
                    _Header.Link = true;
                }
            }
            else if (Link == "1")
            {
                _Header.LinkPath = LinkPath;
                _Header.Link = true;
                _Header.Download = false;
            }
            else if (Link == "0")
            {
                _Header.LinkPath = "";
                _Header.Link = false;
                _Header.Download = false;
            }

            try
            {

                _ESmartcontext.ImgTextHeader.Add(_Header);
                _ESmartcontext.SaveChanges();


            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "ADD TEXT SLIDE", detail = e.Message };
                return Json(Data1);
            }


            var Data = new { status = true, subject = "ADD TEXT SLIDE", detail = "System add text slide complete.", cat = _Header.GroupCateg, imgorder = _Header.ImgOrder, part = _Header.PartId };
            return Json(Data);

        }

        //Update Header Text
        [HttpPost]
        public ActionResult UpdateSlideT(IFormFile filesAttach, int ImgOrder, string Link, string LinkName, string LinkPath, string TextH, string TextD, int TextHorder)
        {

            var CreateBy = User.Claims.Where(c => c.Type == "UserName").Select(c => c.Value).SingleOrDefault();
            var NewsGroup = User.Claims.Where(c => c.Type == "GroupCateg").Select(c => c.Value).SingleOrDefault();
            int partid = 6;
            ImgTextHeader _Headerup = _ESmartcontext.ImgTextHeader.Where(p => p.PartId == partid && p.GroupCateg == NewsGroup && p.ImgOrder == ImgOrder && p.TextHorder == TextHorder).FirstOrDefault();

            if (_Headerup != null)
            {
                var downloadold = _Headerup.Download;
                var LinkPathold = _Headerup.LinkPath;

                _Headerup.UpdateBy = CreateBy;
                _Headerup.UpdateDate = DateTime.Now;
                _Headerup.TextH = TextH;
                _Headerup.TextD = TextD;

                string folderName = NewsGroup;


                if (Link == "2" || Link == "3")
                {
                    if (filesAttach != null)
                    {
                        if (filesAttach.Length > 0)
                        {
                            string webRootPath = _hostingEnvironment.WebRootPath + "\\File\\" + folderName;
                            string newPath = Path.Combine(webRootPath, "Header");
                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }
                            string fileName = ContentDispositionHeaderValue.Parse(filesAttach.ContentDisposition).FileName.Trim('"');
                            string extension = Path.GetExtension(filesAttach.FileName);
                            string _detail_id = GenerateNewsDetailID();
                            string fname_new = _detail_id + extension;
                            _Headerup.LinkPath = fname_new;

                            string fullPath = Path.Combine(newPath, fname_new);

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                filesAttach.CopyTo(stream);
                            }
                        }
                    }

                    _Headerup.Download = true;
                    _Headerup.Link = false;

                    if (Link == "3")
                    {
                        _Headerup.Link = true;
                    }
                }
                else if (Link == "1")
                {
                    _Headerup.LinkPath = LinkPath;
                    _Headerup.Link = true;
                    _Headerup.Download = false;
                }
                else if (Link == "0")
                {
                    _Headerup.LinkPath = "";
                    _Headerup.Link = false;
                    _Headerup.Download = false;
                }
                try
                {

                    _ESmartcontext.ImgTextHeader.Update(_Headerup);
                    _ESmartcontext.SaveChanges();

                    //delete attach
                    if (filesAttach != null)
                    {
                        if (filesAttach.Length > 0)
                        {
                            if (downloadold == true && LinkPathold != "")
                            {
                                System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/File/" + _Headerup.GroupCateg + "/Header/" + LinkPathold);
                            }

                        }
                    }
                    else if (Link == "0")
                    {
                        if (downloadold == true && LinkPathold != "")
                        {
                            System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/File/" + _Headerup.GroupCateg + "/Header/" + LinkPathold);
                        }
                    }
                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "UPDATE TEXT SLIDE", detail = e.Message };
                    return Json(Data1);
                }
            }
            var Data = new { status = true, subject = "UPDATE TEXT SLIDE", detail = "System update text slide complete.", cat = _Headerup.GroupCateg, imgorder = _Headerup.ImgOrder, part = _Headerup.PartId };
            return Json(Data);
        }

        //delete text slide
        [HttpPost]
        public async Task<JsonResult> DeleteHeaderT(String DocCat, String NewHorder, string NewsDOrder, string PartId)
        {
            int partid = 6;

            ImgTextHeader textH = await _ESmartcontext.ImgTextHeader.Where(p => p.PartId == partid && p.GroupCateg == DocCat && p.ImgOrder == int.Parse(NewHorder.ToString()) && p.TextHorder == int.Parse(NewsDOrder.ToString())).FirstOrDefaultAsync();
            if (textH != null)
            {
                if (textH.Download == true && textH.LinkPath != "")
                {
                    System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/File/" + textH.GroupCateg + "/Header/" + textH.LinkPath);
                }

                _ESmartcontext.ImgTextHeader.Remove(textH);
                await _ESmartcontext.SaveChangesAsync();
                var Data = new { status = true, subject = "Delete Item", detail = "Delete Item Complete." };
                return Json(Data);
            }
            else
            {
                var Data = new { status = false, subject = "Delete Item", detail = "System is can't delete item." };
                return Json(Data);
            }
            //return new JsonResult { Data = new { status = true, subject = "Delete Model", detail = "Delete Model  : " + delete_model.model_name } };
        }
        /************************************************ Main Header slide End ***************************************************************/


        /************************************************ News Component Service Start ***************************************************************/
        
        public JsonResult GetAllNews(string DeptType)
        {
            if (DeptType == "Mainnews")
            {
                var test = _ESmartcontext.NnewsHeader.Where(i => i.NewsType == "News" && i.ChildType == 1 && i.Disable == true).Include(l => l.NnewsDetail).OrderByDescending(i => i.CreatedDate).Take(100).ToList();
                var listnews = _ESmartcontext.NnewsHeader.Where(i => i.NewsType == "News" && i.ChildType == 1 && i.Disable == true).Include(l => l.NnewsDetail).OrderByDescending(i => i.CreatedDate).Take(100).Select(g => new
                {
                    CreatedDate = Convert.ToDateTime(g.CreatedDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    linkimage = _ESmartcontext.NnewsDetail.FirstOrDefault().ItemType != "" && g.NnewsDetail.Count() != 0 ? "../image/" + g.GroupCateg + "/" + g.NewsType + "/" + g.NnewsDetail.OrderBy(i=>i.CreatedDate).FirstOrDefault().Value : "/image/icon-information-symbol.svg",
                    nohtml_detail = g.Title2 != null ? Regex.Replace(g.Title2, @"<[^>]*>", String.Empty) : "",
                    g.GroupCateg,
                    g.NewHorder,
                    g.Title1,
                    g.Title2,                  
                    UpdateDate = Convert.ToDateTime(g.UpdateDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    itemType = g.NnewsDetail.OrderBy(i => i.CreatedDate).FirstOrDefault().ItemType,                
                }).ToList();
                var Data = new { status = listnews.OrderByDescending(i =>i.CreatedDate)};
                return Json(Data);

            }
            else if (DeptType == "Allnews")
            {
                var listnews = _ESmartcontext.NnewsHeader.Where(i => i.NewsType == "News" && i.ChildType == 1).Include(l => l.NnewsDetail).OrderByDescending(i => i.CreatedDate).Take(100).Select(g => new
                {
                    linkimage = _ESmartcontext.NnewsDetail.FirstOrDefault().ItemType != "" && g.NnewsDetail.Count() != 0 ? "../image/" + g.GroupCateg + "/" + g.NewsType + "/" + g.NnewsDetail.OrderBy(i => i.CreatedDate).FirstOrDefault().Value : "/image/icon-information-symbol.svg",
                    nohtml_detail = g.Title2 != null ? Regex.Replace(g.Title2, @"<[^>]*>", String.Empty) : "",
                    g.GroupCateg,
                    g.NewHorder,
                    g.Title1,
                    g.Title2,
                    CreatedDate = Convert.ToDateTime(g.CreatedDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    UpdateDate = Convert.ToDateTime(g.UpdateDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    itemType = g.NnewsDetail.FirstOrDefault().ItemType,
                }).ToList();
                var Data = new { status = listnews.OrderByDescending(i => i.CreatedDate)};
                return Json(Data);
            }
            else
            {
                var listnews = _ESmartcontext.NnewsHeader.Where(i => i.NewsType == "News" && i.ChildType == 1 && i.Disable == false && i.GroupCateg == DeptType).Include(l => l.NnewsDetail).OrderByDescending(i => i.CreatedDate).Take(100).Select(g => new
                {
                    linkimage = _ESmartcontext.NnewsDetail.FirstOrDefault().ItemType != "" && g.NnewsDetail.Count() != 0 ? "../image/" + g.GroupCateg + "/" + g.NewsType + "/" + g.NnewsDetail.OrderBy(i => i.CreatedDate).FirstOrDefault().Value : "/image/icon-information-symbol.svg",
                    nohtml_detail = g.Title2 != null ? Regex.Replace(g.Title2, @"<[^>]*>", String.Empty) : "",
                    g.GroupCateg,
                    g.NewHorder,
                    g.Title1,
                    g.Title2,
                    CreatedDate = Convert.ToDateTime(g.CreatedDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    UpdateDate = Convert.ToDateTime(g.UpdateDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                    itemType = g.NnewsDetail.OrderBy(i => i.CreatedDate).FirstOrDefault().ItemType,
                }).ToList();
                var Data = new { status = listnews.OrderByDescending(i => i.CreatedDate)};
                return Json(Data);
            }


            
        }
        public JsonResult SearchNews(string title, string DeptType)
        {
            if (DeptType == "Mainnews")
            {
                if (title != null)
                {
                    var listnews = _ESmartcontext.NnewsHeader.Where(i => i.NewsType == "News" && i.ChildType == 1 && i.Disable == true  && i.Title1.Contains(title.Trim())).Include(l => l.NnewsDetail).Take(100).OrderByDescending(i => i.NewHorder).Select(g => new
                    {
                        linkimage = _ESmartcontext.NnewsDetail.FirstOrDefault().ItemType != "" && g.NnewsDetail.Count() != 0 ? "../image/" + g.GroupCateg + "/" + g.NewsType + "/" + g.NnewsDetail.FirstOrDefault().Value : "/image/icon-information-symbol.svg",
                        nohtml_detail = g.Title2 != null ? Regex.Replace(g.Title2, @"<[^>]*>", String.Empty) : "",
                        g.GroupCateg,
                        g.NewHorder,
                        g.Title1,
                        g.Title2,
                        CreatedDate = Convert.ToDateTime(g.CreatedDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                        UpdateDate = Convert.ToDateTime(g.UpdateDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                        itemType = g.NnewsDetail.FirstOrDefault().ItemType,
                    }).ToList();
                    var Data = new { status = listnews.ToList() };
                    return Json(Data);
                }
                else
                {
                    var listnews = _ESmartcontext.NnewsHeader.Where(i => i.NewsType == "News" && i.ChildType == 1 && i.Disable == true).Include(l => l.NnewsDetail).Take(100).OrderByDescending(i => i.NewHorder).Select(g => new
                    {
                        linkimage = _ESmartcontext.NnewsDetail.FirstOrDefault().ItemType != "" && g.NnewsDetail.Count() != 0 ? "../image/" + g.GroupCateg + "/" + g.NewsType + "/" + g.NnewsDetail.FirstOrDefault().Value : "/image/icon-information-symbol.svg",
                        nohtml_detail = g.Title2 != null ? Regex.Replace(g.Title2, @"<[^>]*>", String.Empty) : "",
                        g.GroupCateg,
                        g.NewHorder,
                        g.Title1,
                        g.Title2,
                        CreatedDate = Convert.ToDateTime(g.CreatedDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                        UpdateDate = Convert.ToDateTime(g.UpdateDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                        itemType = g.NnewsDetail.FirstOrDefault().ItemType,
                    }).ToList();
                    var Data = new { status = listnews.ToList() };
                    return Json(Data);
                }
            }
            else
            {

                if (title != null)
                {
                    var listnews = _ESmartcontext.NnewsHeader.Where(i => i.NewsType == "News" && i.ChildType == 1 && i.GroupCateg == DeptType &&  i.Title1.Contains(title.Trim())).Include(l => l.NnewsDetail).Take(100).OrderByDescending(i => i.NewHorder).Select(g => new
                    {
                        linkimage = _ESmartcontext.NnewsDetail.FirstOrDefault().ItemType != "" && g.NnewsDetail.Count() != 0 ? "../image/" + g.GroupCateg + "/" + g.NewsType + "/" + g.NnewsDetail.FirstOrDefault().Value : "/image/icon-information-symbol.svg",
                        nohtml_detail = g.Title2 != null ? Regex.Replace(g.Title2, @"<[^>]*>", String.Empty) : "",
                        g.GroupCateg,
                        g.NewHorder,
                        g.Title1,
                        g.Title2,
                        CreatedDate = Convert.ToDateTime(g.CreatedDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                        UpdateDate = Convert.ToDateTime(g.UpdateDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                        itemType = g.NnewsDetail.FirstOrDefault().ItemType,
                    }).ToList();
                    var Data = new { status = listnews.ToList() };
                    return Json(Data);
                }
                else
                {
                    var listnews = _ESmartcontext.NnewsHeader.Where(i => i.NewsType == "News" && i.ChildType == 1 && i.GroupCateg == DeptType).Include(l => l.NnewsDetail).Take(100).OrderByDescending(i => i.NewHorder).Select(g => new
                    {
                        linkimage = _ESmartcontext.NnewsDetail.FirstOrDefault().ItemType != "" && g.NnewsDetail.Count() != 0 ? "../image/" + g.GroupCateg + "/" + g.NewsType + "/" + g.NnewsDetail.FirstOrDefault().Value : "/image/icon-information-symbol.svg",
                        nohtml_detail = g.Title2 != null ? Regex.Replace(g.Title2, @"<[^>]*>", String.Empty) : "",
                        g.GroupCateg,
                        g.NewHorder,
                        g.Title1,
                        g.Title2,
                        CreatedDate = Convert.ToDateTime(g.CreatedDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                        UpdateDate = Convert.ToDateTime(g.UpdateDate).ToString("dddd, dd MMMM yyyy HH:mm"),
                        itemType = g.NnewsDetail.FirstOrDefault().ItemType,
                    }).ToList();
                    var Data = new { status = listnews.ToList() };
                    return Json(Data);
                }
            }





        }

    }
}