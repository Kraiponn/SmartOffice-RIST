using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.EManagement.IResponsitory;
using SmartOffice.eManagement.ModelsManagementControl;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using SmartOffice.eManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Extensions.Logging;

namespace SmartOffice.Controllers
{
    public class JournalVoucherController : Controller
    {
        private readonly IEJounalVoucher _IEJounalVoucher;
        private IHostingEnvironment _hostingEnvironment;
        //private readonly ILogger<EmanagementController> _logger;
        public JournalVoucherController(IEJounalVoucher eJounalVoucher, IHostingEnvironment hostingEnvironment 
            //ILogger<EmanagementController> logger
            )
        {

            _IEJounalVoucher = eJounalVoucher;
            _hostingEnvironment = hostingEnvironment;
            //_logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> JournalVoucher()
        {

            var GetdataSecCode = _IEJounalVoucher.GetSectionCode();
            var model = new TupleJournalVoucher
            {

                ddlSectionCodeList = new SelectList(GetdataSecCode, "ID", "Name")
            };
            return await Task.Run(() => View(model));
        }


        public JsonResult Loaddatamaking(string monthperiod, string seccode)
        {
            try
            {
                var allfile = _IEJounalVoucher.Loaddatamaking(monthperiod, seccode);
                return Json(new { data = allfile });
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw new NotImplementedException();
            }
        }

        [HttpPost]
        public IActionResult Loaddataexport(string monthperiod, string seccode)
        {
            try
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    using (ZipOutputStream zos = new ZipOutputStream(stream))
                    {


                        var GetdataSecCode = _IEJounalVoucher.GetSectionCode().Where(x => ((seccode == "ALL") || (seccode != "ALL" && x.ID == seccode)) && x.ID != "ALL").Select(x => x.ID);
                        foreach (var seccode2 in GetdataSecCode)
                        {



                            string pathSource = _hostingEnvironment.WebRootPath + @"\File\JounalVoucher\" + seccode2 + "TemplateVoucher.xls";

                            IWorkbook templateWorkbook;


                            using (FileStream fs = new FileStream(pathSource, FileMode.Open, FileAccess.Read))
                            {
                                templateWorkbook = new HSSFWorkbook(fs);
                            }


                            string sheetName = seccode2;
                            ISheet sheet = templateWorkbook.GetSheet(sheetName);



                            var allfile = _IEJounalVoucher.Loaddataexport(monthperiod, seccode2);
                            //EndDate
                            IRow EndDate_Row = sheet.GetRow(10);
                            ICell EndDate_cell = EndDate_Row.GetCell(2);
                            DateTime dateend = Convert.ToDateTime(allfile.Select(x => x.EndDate).FirstOrDefault());
                            EndDate_cell.SetCellValue(dateend.ToString("dd/MM/yyyy"));

                            //cell
                            int TaxClass = 1;
                            int Code = 2;
                            int AccountNo = 3;
                            int AccountName = 4;
                            int GrossAmount = 7;
                            int ExplanationRemark = 8;
                            int Subledger = 18;
                            int AccountNo2 = 24;
                            int TaxEx = 27;
                            int TaxArea = 28;
                            int GrossAmount2 = 29;
                            int SubledgerType = 34;
                            int Subledger2 = 35;
                            int ExplanationRemark2 = 37;

                            int i = 12;
                            foreach (var j in allfile)
                            {

                                IRow Row = sheet.GetRow(i);

                                //TaxClass
                                ICell TaxClass_cell = Row.GetCell(TaxClass);
                                TaxClass_cell.SetCellValue(j.TaxClass);

                                //Code
                                ICell Code_cell = Row.GetCell(Code);
                                Code_cell.SetCellValue(Convert.ToInt16(j.Code));
                                Code_cell.SetCellType(CellType.Numeric);

                                //AccountNo
                                ICell AccountNo_cell = Row.GetCell(AccountNo);
                                AccountNo_cell.SetCellValue(j.AccountNo);

                                //AccountName
                                ICell AccountName_cell = Row.GetCell(AccountName);
                                AccountName_cell.SetCellValue(j.AccountName);

                                //GrossAmount
                                ICell GrossAmount_cell = Row.GetCell(GrossAmount);
                                GrossAmount_cell.SetCellValue(Convert.ToDouble(j.GrossAmount));
                                GrossAmount_cell.SetCellType(CellType.Numeric);

                                //ExplanationRemark
                                ICell ExplanationRemark_cell = Row.GetCell(ExplanationRemark);
                                ExplanationRemark_cell.SetCellValue(j.ExplanationRemark);

                                //Subledger
                                ICell Subledger_cell = Row.GetCell(Subledger);
                                Subledger_cell.SetCellValue(Convert.ToInt16(j.Subledger));
                                Subledger_cell.SetCellType(CellType.Numeric);
                                //...........................................................................
                                //AccountNo2
                                ICell AccountNo2_cell = Row.GetCell(AccountNo2);
                                AccountNo2_cell.SetCellValue(j.Code + "." + j.AccountNo);

                                //TaxEx
                                ICell TaxEx_cell = Row.GetCell(TaxEx);
                                TaxEx_cell.SetCellValue(j.TaxEx);

                                //TaxArea
                                ICell TaxArea_cell = Row.GetCell(TaxArea);
                                TaxArea_cell.SetCellValue(j.TaxArea);

                                //GrossAmount2
                                ICell GrossAmount2_cell = Row.GetCell(GrossAmount2);
                                GrossAmount2_cell.SetCellValue(Convert.ToDouble(j.GrossAmount));
                                GrossAmount2_cell.SetCellType(CellType.Numeric);

                                //SubledgerType
                                ICell SubledgerType_cell = Row.GetCell(SubledgerType);
                                SubledgerType_cell.SetCellValue(j.SubledgerType);

                                //Subledger2
                                ICell Subledger2_cell = Row.GetCell(Subledger2);
                                Subledger2_cell.SetCellValue(Convert.ToInt16(j.Subledger));
                                Subledger2_cell.SetCellType(CellType.Numeric);

                                //ExplanationRemark2
                                ICell ExplanationRemark2_cell = Row.GetCell(ExplanationRemark2);
                                ExplanationRemark2_cell.SetCellValue(j.ExplanationRemark);

                                i++;
                            }

                            string ContentType = "Application/msexcel";
                            string fileName = seccode2 + monthperiod + ".xls";

                            zos.PutNextEntry(new ZipEntry(fileName));
                            templateWorkbook.Write(zos);

                            zos.CloseEntry();

                            MemoryStream stream2 = new MemoryStream();
                            if (seccode != "ALL") {                            
                            templateWorkbook.Write(stream2);
                            stream2.Position = 0;
                            }
                            templateWorkbook.Close();

                            if (seccode != "ALL")
                            {
                                return File(stream2, ContentType, fileName);
                            }

                        }
                    }
                    return File(stream.ToArray(), "application/zip", "JournalVoucher"+ seccode + monthperiod+".zip");
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw new NotImplementedException();
            }
        }
    }
}