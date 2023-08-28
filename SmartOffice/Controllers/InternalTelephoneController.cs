using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models.ViewModel;
using SmartOffice.ModelsHRMSLocal;


using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartOffice.Controllers
{
    public class InternalTelephoneController : Controller
    {
        private readonly HRMSLocalContext _context;

        private IHostingEnvironment _hostingEnvironment;

        public InternalTelephoneController(HRMSLocalContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

       
        public IActionResult Index(int ? displaygroup1  ,int ? displaygroup2 , int? displaygroup3)
        {
            var groupcode = displaygroup1 ?? 0;

            var model = new InternalTelephoneViewModel();
            List<Models.ViewModel.SelectListItem> allfile = _context.InternalTelephone.Select(x => new { x.Group1, x.DisplayGroup1 }).Distinct().ToList().Select(p => new Models.ViewModel.SelectListItem()
            {
                ID = Convert.ToInt32(p.DisplayGroup1),
                Name = p.Group1,
            }).ToList();


            allfile.Add(new Models.ViewModel.SelectListItem { ID = 0, Name = "ALL" });

            if (groupcode != 0)
            {

                model.ddlgroups = new SelectList(allfile.OrderBy(x => x.ID).ToList(), "ID", "Name");

                model.groupdatas = _context.InternalTelephone.Where(x => x.DisplayGroup1 == groupcode).Select(x => new { x.Group1, x.DisplayGroup1, x.Group2, x.DisplayGroup2, x.Group3, x.DisplayGroup3 })
                .OrderBy(x => x.DisplayGroup1).ThenBy(x => x.DisplayGroup2).ThenBy(x => x.DisplayGroup3).Distinct().ToList().Select(p => new groupdata
                {
                    Group1 = p.Group1,
                    DisplayGroup1 = Convert.ToInt32(p.DisplayGroup1),
                    Group2 = p.Group2,
                    DisplayGroup2 = Convert.ToInt32(p.DisplayGroup2),
                    Group3 = p.Group3,
                    DisplayGroup3 = Convert.ToInt32(p.DisplayGroup3)
                }).ToList();

            }
            else
            {

                model.ddlgroups = new SelectList(allfile.OrderBy(x => x.ID).ToList(), "ID", "Name");

                model.groupdatas = _context.InternalTelephone.Select(x => new { x.Group1, x.DisplayGroup1, x.Group2, x.DisplayGroup2, x.Group3, x.DisplayGroup3 })
            .OrderBy(x => x.DisplayGroup1).ThenBy(x => x.DisplayGroup2).ThenBy(x => x.DisplayGroup3).Distinct().ToList().Select(p => new groupdata
            {
                Group1 = p.Group1,
                DisplayGroup1 = Convert.ToInt32(p.DisplayGroup1),
                Group2 = p.Group2,
                DisplayGroup2 = Convert.ToInt32(p.DisplayGroup2),
                Group3 = p.Group3,
                DisplayGroup3 = Convert.ToInt32(p.DisplayGroup3)
            }).ToList();

            }
            return View(model);
        }


        [HttpPost]
        public IActionResult Index(int groupcode)
        {
            var model = new InternalTelephoneViewModel();
            List<Models.ViewModel.SelectListItem> allfile = _context.InternalTelephone.Select(x => new { x.Group1, x.DisplayGroup1 }).Distinct().ToList().Select(p => new Models.ViewModel.SelectListItem()
            {
                ID = Convert.ToInt32(p.DisplayGroup1),
                Name = p.Group1,
            }).ToList();


            allfile.Add(new Models.ViewModel.SelectListItem { ID = 0, Name = "ALL" });

            if(groupcode != 0) {

                model.ddlgroups = new SelectList(allfile.OrderBy(x => x.ID).ToList(), "ID", "Name");
                
                model.groupdatas = _context.InternalTelephone.Where(x => x.DisplayGroup1 == groupcode).Select(x => new { x.Group1, x.DisplayGroup1, x.Group2, x.DisplayGroup2, x.Group3, x.DisplayGroup3 })
                .OrderBy(x => x.DisplayGroup1).ThenBy(x => x.DisplayGroup2).ThenBy(x => x.DisplayGroup3).Distinct().ToList().Select(p => new groupdata
                {
                    Group1 = p.Group1,
                    DisplayGroup1 = Convert.ToInt32(p.DisplayGroup1),
                    Group2 = p.Group2,
                    DisplayGroup2 = Convert.ToInt32(p.DisplayGroup2),
                    Group3 = p.Group3,
                    DisplayGroup3 = Convert.ToInt32(p.DisplayGroup3)
                }).ToList();
           
            }
            else
            {

                model.ddlgroups = new SelectList(allfile.OrderBy(x => x.ID).ToList(), "ID", "Name");

                    model.groupdatas = _context.InternalTelephone.Select(x => new { x.Group1, x.DisplayGroup1, x.Group2, x.DisplayGroup2, x.Group3, x.DisplayGroup3 })
                .OrderBy(x => x.DisplayGroup1).ThenBy(x => x.DisplayGroup2).ThenBy(x => x.DisplayGroup3).Distinct().ToList().Select(p => new groupdata
                {
                    Group1 = p.Group1,
                    DisplayGroup1 = Convert.ToInt32(p.DisplayGroup1),
                    Group2 = p.Group2,
                    DisplayGroup2 = Convert.ToInt32(p.DisplayGroup2),
                    Group3 = p.Group3,
                    DisplayGroup3 = Convert.ToInt32(p.DisplayGroup3)
                }).ToList();
               
            }
            return View(model);
        }


        [Authorize]
        public IActionResult ImportFile()
        {
            
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public JsonResult GetInternalTelephone(int displaygroup1 , int displaygroup2 , int displaygroup3)
        {
            try
            {

                if (displaygroup1 == 0) //ALL
                {
                    var datamaster = _context.InternalTelephone
                        .OrderBy(x=>x.DisplayGroup1).ThenBy(x=>x.DisplayGroup2).ThenBy(x => x.DisplayGroup3).ThenBy(x=>x.Display).ToList();
                    return Json(new { data = datamaster });
                }
                else if  (displaygroup1 != 0 && displaygroup2 == 0 && displaygroup3 == 0) //Group1
                {

                    var datamaster = _context.InternalTelephone.Where(x => x.DisplayGroup1 == displaygroup1 )
                        .OrderBy(x => x.DisplayGroup1).ThenBy(x => x.DisplayGroup2).ThenBy(x => x.DisplayGroup3).ThenBy(x => x.Display).ToList();
                    return Json(new { data = datamaster });
                }else if (displaygroup1 != 0 && displaygroup2 != 0 && displaygroup3 == 0) //Group2
                {
                    var datamaster = _context.InternalTelephone.Where(x => x.DisplayGroup1 == displaygroup1 && x.DisplayGroup2 == displaygroup2)
                        .OrderBy(x => x.DisplayGroup1).ThenBy(x => x.DisplayGroup2).ThenBy(x => x.DisplayGroup3).ThenBy(x => x.Display).ToList();
                    return Json(new { data = datamaster });
                }
                else //Group3
                {
                    var datamaster = _context.InternalTelephone.Where(x => x.DisplayGroup1 == displaygroup1 && x.DisplayGroup2 == displaygroup2 && x.DisplayGroup3 == displaygroup3)
                        .OrderBy(x => x.DisplayGroup1).ThenBy(x => x.DisplayGroup2).ThenBy(x => x.DisplayGroup3).ThenBy(x => x.Display).ToList();
                    return Json(new { data = datamaster });
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }


        [HttpPost]
        public IActionResult ImportFile(IFormFile file)
        {
            var Message = "";
            ViewBag.Message = Message;
            var i = 0;
            try
            {
                if (file != null)
                {

                    List<string> records = new List<string>();
                    string webRootPath = _hostingEnvironment.WebRootPath + "\\File\\";
                    string newPath = Path.Combine(webRootPath, "InternalTelephone");

                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string extension = Path.GetExtension(file.FileName);

                    string fname_new = "Data" + extension;


                    string fullPath = Path.Combine(newPath, fname_new);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }



                    FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string line = reader.ReadToEnd();
                        records = new List<string>(line.Split('\n'));
                    }

                   
                    foreach (string record in records)
                    {
                        if (i != 0)
                        {
                            InternalTelephone internalTelephone = new InternalTelephone();
                            string[] textpart = record.Replace("\r", "").Split(',');
                            //internalTelephone.No = Convert.ToInt16(textpart[0]);
                            internalTelephone.Group1 = textpart[1];
                            internalTelephone.DisplayGroup1 = Convert.ToInt16(textpart[2]);
                            internalTelephone.Group2 = textpart[3];
                            internalTelephone.DisplayGroup2 = Convert.ToInt16(textpart[4]);
                            internalTelephone.Group3 = textpart[5];
                            internalTelephone.DisplayGroup3 = Convert.ToInt16(textpart[6]);
                            internalTelephone.Display = Convert.ToInt16(textpart[7]);
                            internalTelephone.Title1 = textpart[8];
                            internalTelephone.Title2 = textpart[9];
                            internalTelephone.Id = textpart[11].Replace("/", "");
                            internalTelephone.Name = textpart[12];
                            internalTelephone.TelNo = textpart[13];
                            internalTelephone.FaxNo = textpart[14];

                            var Rowid = textpart[0] == "" || textpart[0] == null ? 0 : Convert.ToInt16(textpart[0]);

                            var checkdata = _context.InternalTelephone.Where(x => x.No == Rowid ).FirstOrDefault();
                            if (Rowid != 0)
                            {
                                if (textpart[15] == "D") //delete
                                {
                                    if (checkdata != null)
                                    {
                                        _context.InternalTelephone.Remove(checkdata);
                                        _context.SaveChanges();
                                    }

                                    Message = Message + "<br/>Row=" + i + " RowID =" + Rowid + "--->Delete OK";
                                }
                                else
                                {
                                    if (checkdata != null) //update
                                    {

                                        checkdata.Group1 = textpart[1];
                                        checkdata.DisplayGroup1 = Convert.ToInt16(textpart[2]);
                                        checkdata.Group2 = textpart[3];
                                        checkdata.DisplayGroup2 = Convert.ToInt16(textpart[4]);
                                        checkdata.Group3 = textpart[5];
                                        checkdata.DisplayGroup3 = Convert.ToInt16(textpart[6]);
                                        checkdata.Display = Convert.ToInt16(textpart[7]);
                                        checkdata.Title1 = textpart[8];
                                        checkdata.Title2 = textpart[9];
                                        checkdata.Id = textpart[11].Replace("/","");
                                        checkdata.Name = textpart[12];
                                        checkdata.TelNo = textpart[13];
                                        checkdata.FaxNo = textpart[14];

                                        _context.SaveChanges();
                                        Message = Message + "<br/>Row=" + i + " RowID =" + Rowid + "--->Update OK";
                                    }
                                    else
                                    {
                                    _context.SaveChanges();
                                    Message = Message + "<br/>Row=" + i + " RowID =" + Rowid + "--->Update NG";
                                    }  
                                }
                            }
                            else //Add
                            {
                                _context.InternalTelephone.Add(internalTelephone);
                                _context.SaveChanges();
                                Message = Message + "<br/>Row=" + i + " RowID =" + Rowid + "--->Add OK";
                            }
                                
                        }
                        i++;
                    }
                    
                    return View();

                }
                else
                {
                    Message =  "Please select the file first to upload.";
                }
            }
            catch (Exception ex)
            {
                Message = Message + "<br/>Row=" + i  + "--->Error " + ex.Message.ToString();
            }

            ViewBag.Message = Message;
            return View();

        }
    }
}