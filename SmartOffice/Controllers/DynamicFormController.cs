using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartOffice.ModelsForm;
using SmartOffice.ModelsDocControl;
using System;
using System.Linq;
using SmartOffice.Class;
using SmartOffice.IResponsitory;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using SmartOffice.Models.ViewModel;
using Newtonsoft.Json;
using System.Xml;
using SmartOffice.Models;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;

namespace SmartOffice.Controllers
{
    public class DynamicFormController : Controller
    {

        private readonly IDynamicFormService _IDynamicFormService;
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _hostingEnvironment;

        public DynamicFormController(IConfiguration configuration, IDynamicFormService iDynamicFormService, IHostingEnvironment hostingEnvironment)
        {
            this._configuration = configuration;
            _IDynamicFormService = iDynamicFormService;
            _hostingEnvironment = hostingEnvironment;

        }


        [HttpGet]
        public IActionResult _Timeline(string DocNo)
        {
            ConnDoc dp = new ConnDoc(_configuration);
            var model = new Tuple1
            {

                Flows = dp.GetFlow(DocNo ?? "").ToList()
            };
            return PartialView("_Timeline", model);
        }


        public IActionResult CreateForm()
        {
            return View();
        }



        public async Task<IActionResult> CountFile()
        {
            await _IDynamicFormService.SetCountPageAttachfileAsync();
            return View();
        }


        public IActionResult ViewForm(string id)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult EditForm(string id)
        {
            ViewBag.id = id;
            return View();
        }
        //------------------------------------Start Form Menu--------------------------------------------------------------------------------------
        [Authorize]
        public IActionResult FormMenu()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetMenu()
        {
            return Json(_IDynamicFormService.GetMenu());
        }

        [HttpPost]
        public async Task<JsonResult> ListApproved(List<string> DocumentNo)
        {
            string CreateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var CreateName = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value;
            string Division = User.Claims.FirstOrDefault(c => c.Type == "Division").Value;
            string Section = User.Claims.FirstOrDefault(c => c.Type == "Section").Value;
            string Department = User.Claims.FirstOrDefault(c => c.Type == "Department").Value;
            string Department2 = User.Claims.FirstOrDefault(c => c.Type == "Department2").Value;
            string Department3 = User.Claims.FirstOrDefault(c => c.Type == "Department3").Value;
            string GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
            return Json(await _IDynamicFormService.ListApproved(DocumentNo, CreateBy, CreateName, Division, Section, Department, Department2, Department3, GroupCateg)); ;
        }

        //------------------------------------End Form Menu--------------------------------------------------------------------------------------
        //------------------------------------Start Dashboard--------------------------------------------------------------------------------------
        [Authorize]
        public IActionResult EDashboard(string id)
        {
            ViewBag.id = id;
            return View();
        }
        //------------------------------------End Dashboard--------------------------------------------------------------------------------------
        //------------------------------------Start JobStatus--------------------------------------------------------------------------------------
        [Authorize]
        public IActionResult JobStatus()
        {

            return View();
        }

        [HttpPost]
        public JsonResult GetDataForm(string startdate, string enddate, string mode)
        {
            ConnJobStatus objrun = new ConnJobStatus(_configuration);
            DataForm dataForm = new DataForm();

            string ChartCatg = "Form";
            if (mode != null)
            {
                mode = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            }

            dataForm = objrun.GetDataForm(ChartCatg, Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), mode);

            return Json(new { datadept = dataForm.deptForms, datacount = dataForm.countForms });

        }
        [HttpPost]
        public JsonResult GetDataI(string startdate, string enddate, string mode)
        {
            ConnJobStatus objrun = new ConnJobStatus(_configuration);
            DataI dataI = new DataI();

            string ChartCatg = "I";
            if (mode != null)
            {
                mode = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            }
            dataI = objrun.GetDataI(ChartCatg, Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), mode);

            return Json(new { datadept = dataI.deptIs, datacount = dataI.countIs });

        }
        [HttpPost]
        public JsonResult GetDataC(string startdate, string enddate, string mode)
        {
            ConnJobStatus objrun = new ConnJobStatus(_configuration);
            DataC dataC = new DataC();

            string ChartCatg = "C";
            if (mode != null)
            {
                mode = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            }
            dataC = objrun.GetDataC(ChartCatg, Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), mode);

            return Json(new { datadept = dataC.deptCs, datacount = dataC.countCs });

        }
        [HttpPost]
        public JsonResult GetDataCO(string startdate, string enddate, string mode)
        {
            ConnJobStatus objrun = new ConnJobStatus(_configuration);
            DataCO dataCO = new DataCO();

            string ChartCatg = "CO";
            if (mode != null)
            {
                mode = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            }
            dataCO = objrun.GetDataCO(ChartCatg, Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), mode);

            return Json(new { datadept = dataCO.deptCOs, datacount = dataCO.countCOs });

        }
        [HttpPost]
        public JsonResult GetDataD(string startdate, string enddate, string mode)
        {
            ConnJobStatus objrun = new ConnJobStatus(_configuration);
            DataD dataD = new DataD();

            string ChartCatg = "D";
            if (mode != null)
            {
                mode = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            }
            dataD = objrun.GetDataD(ChartCatg, Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), mode);

            return Json(new { datadept = dataD.deptDs, datacount = dataD.countDs });

        }
        [HttpPost]
        public JsonResult GetDataP(string startdate, string enddate, string mode)
        {
            ConnJobStatus objrun = new ConnJobStatus(_configuration);
            DataP dataP = new DataP();

            string ChartCatg = "P";
            if (mode != null)
            {
                mode = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            }
            dataP = objrun.GetDataP(ChartCatg, Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), mode);

            return Json(new { datadept = dataP.deptPs, datacount = dataP.countPs });

        }
        [HttpPost]
        public JsonResult GetDataR(string startdate, string enddate, string mode)
        {
            ConnJobStatus objrun = new ConnJobStatus(_configuration);
            DataR dataR = new DataR();

            string ChartCatg = "R";
            if (mode != null)
            {
                mode = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            }
            dataR = objrun.GetDataR(ChartCatg, Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), mode);

            return Json(new { datadept = dataR.deptRs, datacount = dataR.countRs });

        }

        [HttpPost]
        public JsonResult GetTableData(string startdate, string enddate, string mode, string label, string title)
        {
            ConnJobStatus objrun = new ConnJobStatus(_configuration);
            DataTableData dataTableData = new DataTableData();

            var userid = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;

            dataTableData = objrun.GetTableData(Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), mode, userid, label, title);

            return Json(new { data = dataTableData.tableDatas });
        }


        //------------------------------------End JobStatus--------------------------------------------------------------------------------------

        //------------------------------------Start Get dynamic Form--------------------------------------------------------------------------------------
        [Route("DynamicForm/Index")]
        [Authorize]

        public async Task<IActionResult> Index(string code, string docno, string mode, string seq)
        {
            try
            {
                ViewBag.code = code ?? "";
                ViewBag.docno = docno ?? "";
                ViewBag.mode = mode ?? "";
                ViewBag.seq = seq ?? "";
                var CreateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;

                var model = await _IDynamicFormService.GetModelAsync(code, docno, mode, seq, CreateBy);
                return View(model);
            }
            catch (Exception ex)
            {
                //for refresh page 
                return Content(ex.Message);
            }
        }

        //------------------------------------End Get dynamic Form--------------------------------------------------------------------------------------


        //------------------------------------Start save document--------------------------------------------------------------------------------------
        [Authorize]
        [HttpPost]
        //[RequestSizeLimit(209715200)]
        public async Task<JsonResult> UpdateForm(Tuple1 Tuple1, IFormCollection frm, string DocNo, string DocCode, string QueryMode,
            string Comment, string Namebtn, string Idbtn, string Valbtn, string Mode, string signature, string radiochksign, string Seq)
        {
            string CreateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var CreateName = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value;

            string Division = User.Claims.FirstOrDefault(c => c.Type == "Division").Value;
            string Section = User.Claims.FirstOrDefault(c => c.Type == "Section").Value;
            string Department = User.Claims.FirstOrDefault(c => c.Type == "Department").Value;
            string Department2 = User.Claims.FirstOrDefault(c => c.Type == "Department2").Value;
            string Department3 = User.Claims.FirstOrDefault(c => c.Type == "Department3").Value;
            string GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
            var Result = await _IDynamicFormService.UpdateForm(Tuple1, frm, DocNo, DocCode, QueryMode, Comment, Namebtn, Idbtn, Valbtn, Mode, signature, radiochksign, CreateBy, CreateName, Division, Section, Department, Department2, Department3, GroupCateg, Seq);
            return Json(Result);
            /*  return null*/
        }
        //------------------------------------End save document--------------------------------------------------------------------------------------

        //------------------------------------Start Transfer document--------------------------------------------------------------------------------------
        [Authorize]
        [HttpPost]
        //[RequestSizeLimit(209715200)]
        public async Task<JsonResult> TransferForm(string DocNo, string DocCode, string Namebtn, string Idbtn, string Valbtn, string seq, string OPIDTransfer)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            string Division = User.Claims.FirstOrDefault(c => c.Type == "Division").Value;
            string Section = User.Claims.FirstOrDefault(c => c.Type == "Section").Value;
            string Department = User.Claims.FirstOrDefault(c => c.Type == "Department").Value;
            string Department2 = User.Claims.FirstOrDefault(c => c.Type == "Department2").Value;
            string Department3 = User.Claims.FirstOrDefault(c => c.Type == "Department3").Value;
            string GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;

            var Result = await _IDynamicFormService.TransferForm(DocNo, DocCode, Namebtn, Idbtn, Valbtn, userid, seq, OPIDTransfer, Division, Section, Department, Department2, Department3, GroupCateg);
            return Json(Result);
            /*  return null*/
        }

        //------------------------------------End Transfer document--------------------------------------------------------------------------------------

        //------------------------------------Start Attach File--------------------------------------------------------------------------------------
        //Get File Attach
        public JsonResult GetFileAttach(string DocNo, string DocCode)
        {
            return Json(_IDynamicFormService.GetFileAttach(DocNo, DocCode));
        }


        //Delete Attach file
        public async Task<JsonResult> Deletefile(String DocNo, String Filename)
        {
            var CreateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            return Json(await _IDynamicFormService.DeletefileAsync(DocNo, Filename, CreateBy));
        }

        //Add Attach file
        [HttpPost]
        public async Task<JsonResult> Attachfile(List<IFormFile> files, string DocNo, string DocCode, string Addressfile)
        {
            var CreateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            return Json(await _IDynamicFormService.Attachfile(files, DocNo, DocCode, Addressfile, CreateBy));
        }
        //Add Attach file
        [HttpPost]
        public JsonResult UploadImage(List<IFormFile> files, string DocNo, string DocCode, string ItemCode)
        {
            var CreateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            return Json(_IDynamicFormService.UploadImage(files, DocNo, DocCode, ItemCode, CreateBy));
        }
        //Delete Attach file
        public async Task<JsonResult> DeleteImage(String DocNo, String ItemCode)
        {
            var CreateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            return Json(await _IDynamicFormService.DeleteImage(DocNo, ItemCode, CreateBy));
        }

        //------------------------------------End Attach File--------------------------------------------------------------------------------------


        [HttpGet]
        public JsonResult Base64PDF(string DocNo, string DocCode)
        {
            var aa = _IDynamicFormService.Base64PDF(DocNo, DocCode);
            return Json(aa);
        }

        [HttpGet]
        public JsonResult Base64PDFManual(string DocCode)
        {
            var aa = _IDynamicFormService.Base64PDFManual(DocCode);
            return Json(aa);
        }


        //------------------------------------End PDF--------------------------------------------------------------------------------------

        public async Task<JsonResult> DeleteDraft(String DocNo)
        {
            var CreateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var CreateName = User.Claims.FirstOrDefault(c => c.Type == "Namempe").Value;
            var Division = User.Claims.FirstOrDefault(c => c.Type == "Division").Value;
            var Section = User.Claims.FirstOrDefault(c => c.Type == "Section").Value;
            var Department = User.Claims.FirstOrDefault(c => c.Type == "Department").Value;
            var Department2 = User.Claims.FirstOrDefault(c => c.Type == "Department2").Value;
            var Department3 = User.Claims.FirstOrDefault(c => c.Type == "Department3").Value;
            var GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
            return Json(await _IDynamicFormService.DeleteDraft(DocNo, CreateBy, CreateName, Division, Section, Department, Department2, Department3, GroupCateg));
        }


        //------------------------------------------------------------------------------Dynamic table---------------------------------------------------------------
        //Get Table
        public JsonResult GetTableConst(string code, string docno)
        {
            return Json(_IDynamicFormService.GetTableConst(code, docno));
        }

        //Get column
        public JsonResult GetCol(string code)
        {
            return Json(_IDynamicFormService.GetCol(code));
        }


        //Get Data in foreach table
        public JsonResult GetDataInTable(string docid)
        {
            return Json(_IDynamicFormService.GetDataInTable(docid));
        }

        public FileResult FileDownload(string fileurl)
        {
            try
            {
                string decodedUrl = HttpUtility.UrlDecode(fileurl);
                var myString = decodedUrl.Replace("//", "\\\\").Replace(@"/", "\\").Replace(@"\", "\\");
                byte[] fileBytes = System.IO.File.ReadAllBytes(myString);

                var filename = myString.Split('\\').Last();
                if (fileBytes != null && fileBytes.Any())
                {
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
                }
                else
                {
                    throw new Exception(String.Format("No report found with file path {0}", fileurl));
                }
            }
            catch
            {
                throw new Exception(String.Format("No report found with file path {0}", fileurl));
            }


        }

        //------------------------------------------------------------------------------------------------------------

        public IActionResult RoleSetting()
        {
            var GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
            var model = _IDynamicFormService.GetSettingRoleModel(GroupCateg);
            return View(model);
        }

        public JsonResult Getmasterrole()
        {
            var GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
            var model = _IDynamicFormService.Getmasterrole(GroupCateg);
            return Json(model);

        }

        public JsonResult GetDoc()
        {
            var GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
            var model = _IDynamicFormService.GetDoc(GroupCateg);
            return Json(model);
        }

        public JsonResult GetmasterFlow(string flowid)
        {
            var model = _IDynamicFormService.GetmasterFlow(flowid);
            return Json(model);
        }


        public JsonResult Getmasteruser(string role)
        {
            var model = _IDynamicFormService.Getmasteruser(role);
            return Json(model);
        }

        public JsonResult UpdateRoleMaster(Role role)
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
            var model = _IDynamicFormService.UpdateRoleMaster(role, UserName, GroupCateg);
            return Json(model);
        }

        public JsonResult DeleteRoleMaster(Role role)
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            return Json(_IDynamicFormService.DeleteRoleMaster(role, UserName));
        }

        public JsonResult DeleteUserMaster(OperatorRole user)
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            return Json(_IDynamicFormService.DeleteUserMaster(user, UserName));
        }

        public JsonResult AddRoleMaster(string remarks)
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
            return Json(_IDynamicFormService.AddRoleMaster(remarks, UserName, GroupCateg));
        }

        public JsonResult AddUserRoleMaster(string listboxuser, string RoleId)
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            return Json(_IDynamicFormService.AddUserRoleMaster(listboxuser, RoleId, UserName));
        }

        public JsonResult AddDocMaster(string flowID, string seqNo, string roleID, string roleIDold,
            string requirement, string requirementRemark, string assignFlag, string assignFlagRemark,
            string assignFlagBySeq, string reject)
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            return Json(_IDynamicFormService.AddDocMaster(flowID, seqNo, roleID, roleIDold, requirement, requirementRemark, assignFlag, assignFlagRemark, UserName, assignFlagBySeq, reject));
        }

        public JsonResult GetFormCondition(string docno, string doccode)
        {
            return Json(_IDynamicFormService.GetFormCondition(docno, doccode));
        }


        public async Task<JsonResult> GetValueList(string ValueCode)
        {
            return Json(await _IDynamicFormService.GetValueList(ValueCode));
        }


        [HttpGet]
        public JsonResult GetSpecialFlow(string id)
        {
            var res = _IDynamicFormService.GetSpecialFlow(id);
            var json = JsonConvert.SerializeObject(res, Newtonsoft.Json.Formatting.Indented);
            return Json(json);
        }

        [HttpGet]
        public JsonResult GetFlowDetail(string id)
        {
            var res = _IDynamicFormService.GetFlowDetail(id);
            var json = JsonConvert.SerializeObject(res, Newtonsoft.Json.Formatting.Indented);
            return Json(json);
        }

        [HttpPost]
        public async Task<JsonResult> AssignFlowsAsync(string id, List<int> Allseq, List<FlowsPeople> AllPeople)
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var res = await _IDynamicFormService.AssignFlowsAsync(id, Allseq, AllPeople, UserName);
            var json = JsonConvert.SerializeObject(res, Newtonsoft.Json.Formatting.Indented);
            return Json(json);
        }
        [HttpGet]
        public JsonResult GetAllEmp()
        {
            var res = _IDynamicFormService.GetAllEmp();
            var json = JsonConvert.SerializeObject(res, Newtonsoft.Json.Formatting.Indented);
            return Json(json);
        }

        [HttpGet]
        public JsonResult GetMyDoc()
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            //var dataMyDoc = new
            //{
            //    rows = _IDynamicFormService.GetMyDoc(UserName)
            //};
            return Json(_IDynamicFormService.GetMyDoc(UserName));
        }

        [HttpGet]
        public JsonResult GetGroupDoc()
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            var GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
            //var dataMyDoc = new
            //{
            //    rows = _IDynamicFormService.GetMyDoc(UserName)
            //};
            return Json(_IDynamicFormService.GetGroupDoc(GroupCateg, UserName));
        }


        [HttpGet]
        public JsonResult GetMyApproved()
        {
            var UserName = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
            //var dataMyDoc = new
            //{
            //    rows = _IDynamicFormService.GetMyDoc(UserName)
            //};
            return Json(_IDynamicFormService.GetMyApproved(UserName));
        }

        public async Task<JsonResult> Getuserautodetail(string userid)
        {
            return Json(await _IDynamicFormService.Getuserautodetail(userid));
        }

        //Get Table
        public JsonResult Getempdata(string empid)
        {
            return Json(_IDynamicFormService.Getempdata(empid));
        }

        [Authorize]
        public IActionResult ExportForm()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ExportForm(string code, string from, string to, string fromapp, string toapp, string fromseqno, string toseqno, string fromempno, string toempno, string status)
        {
            string username = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value.ToString().Trim();
            var dates = DateTime.Now.ToString("yyyyMMddHHmmss");
            var botsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath + "\\TempFile\\" + dates);

            try
            {
                var checkdata = _IDynamicFormService.LoadForm(code, from, to, fromapp, toapp, fromseqno, toseqno , fromempno , toempno , status, dates, username);
                if (checkdata.Count > 0)
                {
                    var zipFileMemoryStream = new MemoryStream();
                    if (Directory.Exists(botsFolderPath))
                    {
                        var botFilePaths = Directory.GetFiles(botsFolderPath);
                        using (ZipArchive archive = new ZipArchive(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
                        {
                            foreach (var botFilePath in botFilePaths)
                            {
                                var botFileName = Path.GetFileName(botFilePath);
                                var entry = archive.CreateEntry(botFileName);
                                using (var entryStream = entry.Open())
                                using (var fileStream = System.IO.File.OpenRead(botFilePath))
                                {
                                    await fileStream.CopyToAsync(entryStream);
                                }
                            }
                        }
                        zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
                        Directory.Delete(botsFolderPath, true);
                    }
                    return File(zipFileMemoryStream, "application/octet-stream", dates + ".zip");
                }
                else
                {
                    return Content("No Data");
                }
            }
            catch (Exception ex)
            {
                if (Directory.Exists(botsFolderPath))
                {
                    Directory.Delete(botsFolderPath, true);
                }
                return Content(ex.Message);
            }
        }
    }
}
