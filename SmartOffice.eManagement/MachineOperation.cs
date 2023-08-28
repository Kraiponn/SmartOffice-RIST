
using ClosedXML.Report;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Npoi.Mapper;
using NPOI.XSSF.UserModel;
using SmartOffice.eManagement.Models;
using SmartOffice.eManagement.ModelsManagementControl;
using SmartOffice.EManagement.IResponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;




//using TestNetOffice.Models;

namespace SmartOffice.eManagement
{
    public class MachineOperation : IMachineOperation
    {
        private readonly ManagementControlContext _context;
        private IHostingEnvironment _hostingEnvironment;
       
        // dependency inject from container
        public MachineOperation(ManagementControlContext context, IHostingEnvironment hostingEnvironment)
        {

            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public string CONVERT_SORT_DATA(string T_RECORD_MMYY)
        {
            var T_SORT_YY_MM = "";
            if (T_RECORD_MMYY.Substring(0, 3) == "JAN")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "01";
            }
            if (T_RECORD_MMYY.Substring(0, 3) == "FEB")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "02";
            }
            if (T_RECORD_MMYY.Substring(0, 3) == "MAR")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "03";
            }
            if (T_RECORD_MMYY.Substring(0, 3) == "APR")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "04";
            }
            if (T_RECORD_MMYY.Substring(0, 3) == "MAY")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "05";
            }
            if (T_RECORD_MMYY.Substring(0, 3) == "JUN")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "06";
            }
            if (T_RECORD_MMYY.Substring(0, 3) == "JUL")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "07";
            }
            if (T_RECORD_MMYY.Substring(0, 3) == "AUG")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "08";
            }
            if (T_RECORD_MMYY.Substring(0, 3) == "SEP")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "09";
            }
            if (T_RECORD_MMYY.Substring(0, 3) == "OCT")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "10";
            }
            if (T_RECORD_MMYY.Substring(0, 3) == "NOV")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "11";
            }
            if (T_RECORD_MMYY.Substring(0, 3) == "DEC")
            {
                T_SORT_YY_MM = T_RECORD_MMYY.Substring(4, 2) + "12";
            }


            return T_SORT_YY_MM;
        }
        public async Task<dynamic> ReadFromSourceAsync(IFormFile file, string datereturn)
        {
            var Data = new { status = true, subject = "UPLOAD OPERATION MACHINE", detail = "Success", type = "success" };
            try
            {
                // declare npoi mapper
                Mapper mapper = new Mapper(file.OpenReadStream());
                // read raw data and covert to collection of target object
                var sources = mapper.Take<OperationMachine2>("OPERATION").Select(x => x.Value).Where(x => x.Item != null).ToList();

                List<OperationMachine2> dataitem = new List<OperationMachine2>();

                var dateReturnAcc = Convert.ToDateTime(datereturn);
                foreach (var item in sources)
                {
                    string T_ITEM = item.Item == null ? "" : item.Item.Trim();
                    string T_SPECIFICATION = item.Specification == null ? "" : item.Specification.Trim();
                    string T_INV = item.Inv == null ? "" : item.Inv.Trim();
                    string T_SUPPLIER_NAME = item.SupplierName == null ? "" : item.SupplierName.Trim();
                    string T_QTY = item.Qty == null ? "" : item.Qty.ToString().Trim();
                    string T_DECISION = item.DicisionNo == null ? "" : item.DicisionNo.Trim();
                    string T_FINISHED_TARGET = item.FinishedTarget == null ? "" : item.FinishedTarget.Trim();
                    string T_SUSPENSE_ACCT_NO = item.SuspenseAcctNo == null ? "" : item.SuspenseAcctNo.Trim();
                    string T_STATUS = item.Status == null ? "" : item.Status.Trim();
                    string T_PR = item.PrNo == null ? "" : item.PrNo.Trim();
                    string T_ORDER_NAME = item.OrderName == null ? "" : item.OrderName.Trim();
                    string T_ORDER_SECT_CODE = item.OrderSectCode == null ? "" : item.OrderSectCode.Trim();
                    string T_INCHARGE_SECT_CODE = item.InchargeSectCode == null ? "" : item.InchargeSectCode.Trim();
                    string T_SECT_NAME = item.SectName == null ? "" : item.SectName.Trim();
                    string T_M_C_NO = item.MCNo == null ? "" : item.MCNo.Trim();
                    string T_PROCESS_NAME = item.ProcessName == null ? "" : item.ProcessName.Trim();
                    string T_PROCESS_NAME_PRODUCT_TYPE = item.ProcessNameProductType == null ? "" : item.ProcessNameProductType.Trim();
                    string T_LOCATION = item.Location == null ? "" : item.Location.Trim();
                    string T_BEGINNING_BALANCE = item.BeginningBalance == null ? "" : item.BeginningBalance.ToString().Trim();
                    string T_BEGIN_QTY = item.BeginQty == null ? "" : item.BeginQty.ToString().Trim();
                    string T_INV_AMT = item.InvAmt == null ? "" : item.InvAmt.ToString().Trim();
                    string T_INV_QTY = item.InvQty == null ? "" : item.InvQty.ToString().Trim();
                    string T_CURRENCY = item.Currency == null ? "" : item.Currency.Trim();
                    string T_TF_TO_SPAREPARTS = item.TfToSpareparts == null ? "" : item.TfToSpareparts.ToString().Trim();
                    string T_QTY_1 = item.Qty1 == null ? "" : item.Qty1.ToString().Trim();
                    string T_TF_TO_FIXED_ASSETS = item.TfToFixedAssets == null ? "" : item.TfToFixedAssets.ToString().Trim();
                    string T_QTY_2 = item.Qty2 == null ? "" : item.Qty2.ToString().Trim();
                    string T_TOTAL_ENDING_BALANCE = item.TotalEndingBalance == null ? "" : item.TotalEndingBalance.ToString().Trim();
                    string T_OPERATION_EXPECT_TO_OPERATION_MONTH = item.OperationExpectToOperationMonth == null ? "" : item.OperationExpectToOperationMonth.Trim();
                    string T_LAST_MONTH_CONFIRMATION = item.LastMonthConfirmation == null ? "" : item.LastMonthConfirmation.Trim();
                    string T_REMARK = item.Remark == null ? "" : item.Remark.Trim();
                    string T_UNIT = item.Unit == null ? "" : item.Unit.ToString();
                    string T_RESPON_TO_CONFIRMED_BY = item.ResponToConfirmedBy == null ? "" : item.ResponToConfirmedBy.Trim();
                    string T_RESPON_BY_NAME_DEPT = item.ResponByNameDept == null ? "" : item.ResponByNameDept.Trim();
                    string T_RECORD_MMYY = item.RecordMmYy == null ? "" : item.RecordMmYy.Trim();
                    string T_INVENTORY = item.Inventory == null ? "" : item.Inventory.Trim();
                    string T_SORT_YY_MM;
                    // *** Rows ***' 

                    if (T_ITEM.Length != 0)
                    {
                        T_ITEM = T_ITEM.Length <= 100 ? T_ITEM : T_ITEM.Substring(0, 100);
                    }
                    else
                        T_ITEM = "-";


                    if (T_SPECIFICATION.Length != 0)
                    {
                        T_SPECIFICATION = T_SPECIFICATION.Length <= 100 ? T_SPECIFICATION : T_SPECIFICATION.Substring(0, 100);
                    }
                    else
                        T_SPECIFICATION = "-";

                    if (T_INV.Length != 0)
                    {
                        T_INV = T_INV.Length <= 40 ? T_INV : T_INV.Substring(0, 40);
                    }
                    else
                        T_INV = "-";

                    if (T_SUPPLIER_NAME.Length != 0)
                    {
                        T_SUPPLIER_NAME = T_SUPPLIER_NAME.Length <= 80 ? T_SUPPLIER_NAME : T_SUPPLIER_NAME.Substring(0, 80);
                    }
                    else
                        T_SUPPLIER_NAME = "-";


                    if (T_RECORD_MMYY.Length == 0)
                        T_RECORD_MMYY = "-";


                    if (T_DECISION.Length != 0)
                    {
                        T_DECISION = T_DECISION.Length <= 20 ? T_DECISION : T_DECISION.Substring(0, 20);
                    }
                    else
                        T_DECISION = "-";


                    if (T_FINISHED_TARGET.Length == 0)
                        T_FINISHED_TARGET = "-";


                    if (T_SUSPENSE_ACCT_NO.Length != 0)
                    {

                        T_SUSPENSE_ACCT_NO = T_SUSPENSE_ACCT_NO.Length <= 15 ? T_SUSPENSE_ACCT_NO : T_SUSPENSE_ACCT_NO.Substring(0, 15);
                    }
                    else
                        T_SUSPENSE_ACCT_NO = "-";

                    if (T_STATUS.Length != 0)
                    {
                        T_STATUS = T_STATUS.Length <= 20 ? T_STATUS : T_STATUS.Substring(0, 20);
                    }
                    else
                        T_STATUS = "-";

                    if (T_PR.Length != 0)
                    {
                        T_PR = T_PR.Length <= 20 ? T_PR : T_PR.Substring(0, 20);
                    }
                    else
                        T_PR = "-";

                    if (T_ORDER_NAME.Length != 0)
                    {
                        T_ORDER_NAME = T_ORDER_NAME.Length <= 50 ? T_ORDER_NAME : T_ORDER_NAME.Substring(0, 50);
                    }
                    else
                        T_ORDER_NAME = "-";


                    if (T_ORDER_SECT_CODE.Length != 0)
                    {
                        T_ORDER_SECT_CODE = T_ORDER_SECT_CODE.Length <= 10 ? T_ORDER_SECT_CODE : T_ORDER_SECT_CODE.Substring(0, 10);
                    }
                    else
                        T_ORDER_SECT_CODE = "-";


                    if (T_INCHARGE_SECT_CODE.Length != 0)
                    {
                        T_INCHARGE_SECT_CODE = T_INCHARGE_SECT_CODE.Length <= 10 ? T_INCHARGE_SECT_CODE : T_INCHARGE_SECT_CODE.Substring(0, 10);
                    }
                    else
                        T_INCHARGE_SECT_CODE = "-";

                    if (T_SECT_NAME.Length != 0)
                    {
                        T_SECT_NAME = T_SECT_NAME.Length <= 50 ? T_SECT_NAME : T_SECT_NAME.Substring(0, 50);
                    }
                    else
                        T_SECT_NAME = "-";


                    if (T_M_C_NO.Length != 0)
                    {
                        T_M_C_NO = T_M_C_NO.Length <= 50 ? T_M_C_NO : T_M_C_NO.Substring(0, 50);
                    }
                    else
                        T_M_C_NO = "-";


                    if (T_PROCESS_NAME.Length != 0)
                    {
                        T_PROCESS_NAME = T_PROCESS_NAME.Length < 50 ? T_PROCESS_NAME : T_PROCESS_NAME.Substring(0, 50);
                    }
                    else
                        T_PROCESS_NAME = "-";

                    if (T_PROCESS_NAME_PRODUCT_TYPE.Length != 0)
                    {
                        T_PROCESS_NAME_PRODUCT_TYPE = T_PROCESS_NAME_PRODUCT_TYPE.Length <= 50 ? T_PROCESS_NAME_PRODUCT_TYPE : T_PROCESS_NAME_PRODUCT_TYPE.Substring(0, 50);
                    }
                    else
                        T_PROCESS_NAME_PRODUCT_TYPE = "-";

                    if (T_LOCATION.Length != 0)
                    {
                        T_LOCATION = T_LOCATION.Length <= 20 ? T_LOCATION : T_LOCATION.Substring(0, 20);
                    }
                    else
                        T_LOCATION = "-";


                    if (T_QTY.Length == 0)
                        T_QTY = "0";

                    if (T_BEGINNING_BALANCE.Length == 0)
                        T_BEGINNING_BALANCE = "0";

                    if (T_BEGIN_QTY.Length == 0)
                        T_BEGIN_QTY = "0";

                    if (T_INV_AMT.Length == 0)
                        T_INV_AMT = "0";

                    if (T_INV_QTY.Length == 0)
                        T_INV_QTY = "0";

                    if (T_TF_TO_SPAREPARTS.Length == 0)
                        T_TF_TO_SPAREPARTS = "0";

                    if (T_QTY_1.Length == 0)
                        T_QTY_1 = "0";

                    if (T_TF_TO_FIXED_ASSETS.Length == 0)
                        T_TF_TO_FIXED_ASSETS = "0";

                    if (T_QTY_2.Length == 0)
                        T_QTY_2 = "0";

                    if (T_TOTAL_ENDING_BALANCE.Length == 0)
                        T_TOTAL_ENDING_BALANCE = "0";

                    if (T_OPERATION_EXPECT_TO_OPERATION_MONTH.Length != 0)
                    {
                        T_OPERATION_EXPECT_TO_OPERATION_MONTH = T_OPERATION_EXPECT_TO_OPERATION_MONTH.Length <= 50 ? T_OPERATION_EXPECT_TO_OPERATION_MONTH : T_OPERATION_EXPECT_TO_OPERATION_MONTH.Substring(0, 50);
                    }
                    else
                        T_OPERATION_EXPECT_TO_OPERATION_MONTH = "-";


                    if (T_LAST_MONTH_CONFIRMATION.Length != 0)
                    {
                        T_LAST_MONTH_CONFIRMATION = T_LAST_MONTH_CONFIRMATION.Length <= 40 ? T_LAST_MONTH_CONFIRMATION : T_LAST_MONTH_CONFIRMATION.Substring(0, 40);
                    }
                    else
                        T_LAST_MONTH_CONFIRMATION = "-";


                    if (T_REMARK.Length != 0)
                    {
                        T_REMARK = T_REMARK.Length <= 50 ? T_REMARK : T_REMARK.Substring(0, 50);
                    }
                    else
                        T_REMARK = "-";

                    if (T_UNIT.Length == 0)
                        T_UNIT = "0";


                    if (T_RESPON_TO_CONFIRMED_BY.Length != 0)
                    {
                        T_RESPON_TO_CONFIRMED_BY = T_RESPON_TO_CONFIRMED_BY.Length <= 40 ? T_RESPON_TO_CONFIRMED_BY : T_RESPON_TO_CONFIRMED_BY.Substring(0, 40);
                    }
                    else
                        T_RESPON_TO_CONFIRMED_BY = "-";

                    if (T_CURRENCY.Length != 0)
                    {
                        T_CURRENCY = T_CURRENCY.Length <= 3 ? T_CURRENCY : T_CURRENCY.Substring(0, 3);
                    }
                    else
                        T_CURRENCY = "";

                    if (T_RESPON_BY_NAME_DEPT.Length != 0)
                    {
                        T_RESPON_BY_NAME_DEPT = T_RESPON_BY_NAME_DEPT.Length <= 40 ? T_RESPON_BY_NAME_DEPT : T_RESPON_BY_NAME_DEPT.Substring(0, 40);
                    }
                    else
                        T_RESPON_BY_NAME_DEPT = "-";

                    if (T_INVENTORY.Length == 0)
                        T_INVENTORY = "-";


                    T_SORT_YY_MM = CONVERT_SORT_DATA(T_RECORD_MMYY);


                    if (T_ITEM.Contains("TOTAL") == false)
                    {

                        OperationMachine2 row_item = new OperationMachine2()
                        {
                            Item = T_ITEM.Trim().Replace("'", "''"),
                            Specification = T_SPECIFICATION.Trim().Replace("'", "''"),
                            Inv = T_INV.Trim().Replace("'", "''"),
                            SupplierName = T_SUPPLIER_NAME.Trim().Replace("'", "''"),
                            RecordMmYy = T_RECORD_MMYY.Trim().Replace("'", "''"),
                            DicisionNo = T_DECISION.Trim().Replace("'", "''"),
                            FinishedTarget = T_FINISHED_TARGET.Trim().Replace("'", "''"),
                            SuspenseAcctNo = T_SUSPENSE_ACCT_NO.Trim().Replace("'", "''"),
                            Status = T_STATUS.Trim().Replace("'", "''"),
                            PrNo = T_PR.Trim().Replace("'", "''"),
                            OrderName = T_ORDER_NAME.Trim().Replace("'", "''"),
                            OrderSectCode = T_ORDER_SECT_CODE.Trim().Replace("'", "''"),
                            InchargeSectCode = T_INCHARGE_SECT_CODE.Trim().Replace("'", "''"),
                            SectName = T_SECT_NAME.Trim().Replace("'", "''"),
                            MCNo = T_M_C_NO.Trim().Replace("'", "''"),
                            ProcessName = T_PROCESS_NAME.Trim().Replace("'", "''"),
                            ProcessNameProductType = T_PROCESS_NAME_PRODUCT_TYPE.Trim().Replace("'", "''"),
                            Location = T_LOCATION.Trim().Replace("'", "''"),
                            Qty = Convert.ToInt32(T_QTY),
                            BeginningBalance = Convert.ToDouble(T_BEGINNING_BALANCE),
                            BeginQty = Convert.ToInt32(T_BEGIN_QTY),
                            InvAmt = Convert.ToDouble(T_INV_AMT),
                            InvQty = Convert.ToInt32(T_INV_QTY),
                            TfToSpareparts = Convert.ToDouble(T_TF_TO_SPAREPARTS),
                            Qty1 = Convert.ToInt32(T_QTY_1),
                            TfToFixedAssets = Convert.ToDouble(T_TF_TO_FIXED_ASSETS),
                            Qty2 = Convert.ToInt32(T_QTY_2),
                            TotalEndingBalance = Convert.ToDouble(T_TOTAL_ENDING_BALANCE),
                            OperationExpectToOperationMonth = T_OPERATION_EXPECT_TO_OPERATION_MONTH.Trim().Replace("'", "''"),
                            LastMonthConfirmation = T_LAST_MONTH_CONFIRMATION.Trim().Replace("'", "''"),
                            Remark = T_REMARK.Trim().Replace("'", "''"),
                            Unit = Convert.ToDecimal(T_UNIT),
                            ResponToConfirmedBy = T_RESPON_TO_CONFIRMED_BY.Trim().Replace("'", "''"),
                            ResponByNameDept = T_RESPON_BY_NAME_DEPT.Trim().Replace("'", "''"),
                            DateReturnAcc = dateReturnAcc,
                            SortYyMm = T_SORT_YY_MM.Trim().Replace("'", "''"),
                            Currency = T_CURRENCY.Trim().Replace("'", "''"),
                            Inventory = T_INVENTORY.Trim().Replace("'", "''"),

                        };
                        row_item.TotalEndingBalance = row_item.BeginningBalance + row_item.InvAmt - row_item.TfToSpareparts - row_item.TfToFixedAssets;
                        row_item.Qty2 = row_item.Qty + row_item.BeginQty - row_item.InvQty - row_item.Qty1;
                        dataitem.Add(row_item);

                    }
                }
                
                if (dataitem.Count > 0)
                {              
                    _context.Database.ExecuteSqlCommand("TRUNCATE TABLE [OPERATION_MACHINE2]");
                    _context.OperationMachine2.AddRange(dataitem);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                Data = new { status = false, subject = "UPLOAD OPERATION MACHINE", detail = ex.Message, type = "error" };

            }
            return Data;
        }
        public async Task<List<SelectListItem>> GetSection()
        {
            var listitems = await _context.OperationMachine2.Select(i => new SelectListItem
            {
                Value = i.OrderSectCode,
                Text = i.OrderSectCode + ":" + i.SectName.ToUpper()
            }).Where(i => !string.IsNullOrEmpty(i.Value) && !string.IsNullOrEmpty(i.Text)).Distinct().OrderBy(i => i.Text).ToListAsync();
           
          
            return listitems;
        }
        public async Task<List<SelectListItem>> GetOperationName()
        {
            var test = _context.OperationMachine2.Select(i => i.ResponToConfirmedBy).Distinct().ToList();
            var listitems = await _context.OperationMachine2.Select(i => new SelectListItem
            {
                Value = i.ResponToConfirmedBy,
                Text = i.ResponToConfirmedBy
            }).Distinct().OrderBy(i => i.Text).ToListAsync();
           
            return listitems;
        }
        
        public async Task<FileStreamResult> ReportByDept(string Dept)
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\ExcelTemplate\\ReportTemplate.xlsx";
            ReportDtoData ReportDtodata = new ReportDtoData();
            var listitems = await _context.OperationMachine2.Where(i => i.OrderSectCode.Trim() == Dept).OrderBy(i => i.DicisionNo).ThenBy(i => i.RecordMmYy).ToListAsync();
            
            ReportDtodata.Records = listitems;
            int RowNo = 0;
            string DicisionName = "";
            foreach (var item in listitems)
            {                          
                if (DicisionName!= item.DicisionNo)
                {
                    DicisionName = item.DicisionNo;
                    RowNo = 1;
                }
                else
                {
                    RowNo++;
                }
                item.Id = RowNo;
                DicisionName = item.DicisionNo;
            }

            if (listitems.Count() > 0)
            {
                var returndate = Convert.ToDateTime(listitems.First().DateReturnAcc);

                ReportDtodata.ReturnYear = returndate.Year.ToString();
                ReportDtodata.ReturnMonth = returndate.Month.ToString();
                ReportDtodata.ReturnDate = returndate.Day.ToString();
            }
            MemoryStream stream = new MemoryStream();
            //using (MemoryStream stream = new MemoryStream())
            //{
                using (var template = new XLTemplate(webRootPath))
                {
                    template.AddVariable(ReportDtodata);
                    template.Generate();
                    template.Workbook.Worksheet(1).Rows().AdjustToContents();
                template.SaveAs(stream);
                   
                }
            stream.Seek(0, SeekOrigin.Begin);
            string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return new FileStreamResult(stream, mimeType)
            {
                FileDownloadName = "FileasStream.xlsx"
            };
            //}
            //return File(stream, "application/octet-stream"); // returns a FileStreamResult
        }
        public async Task<FileStreamResult> ReportByUser(string UserResponse)
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\ExcelTemplate\\ReportTemplate.xlsx";
            ReportDtoData ReportDtodata = new ReportDtoData();
            var listitems = await _context.OperationMachine2.Where(i => i.ResponToConfirmedBy.Trim() == UserResponse).OrderBy(i => i.DicisionNo).ThenBy(i => i.RecordMmYy).ToListAsync();

            ReportDtodata.Records = listitems;
            int RowNo = 0;
            string DicisionName = "";
            foreach (var item in listitems)
            {
                if (DicisionName != item.DicisionNo)
                {
                    DicisionName = item.DicisionNo;
                    RowNo = 1;
                }
                else
                {
                    RowNo++;
                }
                item.Id = RowNo;
                DicisionName = item.DicisionNo;
            }
            MemoryStream stream = new MemoryStream();
            if (listitems.Count() > 0)
            {
                var returndate = Convert.ToDateTime(listitems.First().DateReturnAcc);

                ReportDtodata.ReturnYear = returndate.Year.ToString();
                ReportDtodata.ReturnMonth = returndate.Month.ToString();
                ReportDtodata.ReturnDate = returndate.Day.ToString();
            }


            //using (MemoryStream stream = new MemoryStream())
            //{


            try
            {
                using (var template = new XLTemplate(webRootPath))
                {
                    template.AddVariable(ReportDtodata);
                    if (listitems.Count() != 0)
                    {
                        template.Generate();
                        template.Workbook.Worksheet(1).Column(11).SetDataValidation().List(template.Workbook.Worksheet(1).Range("AM1:AM11"), true);
                        template.Workbook.Worksheet(1).Column(11).SetDataValidation().IgnoreBlanks = true;
                        template.Workbook.Worksheet(1).Column(11).SetDataValidation().InCellDropdown = true;
                        template.Workbook.Worksheet(1).Column(31).SetDataValidation().List(template.Workbook.Worksheet(1).Range("AP1:AP14"), true);
                        template.Workbook.Worksheet(1).Column(31).SetDataValidation().IgnoreBlanks = true;
                        template.Workbook.Worksheet(1).Column(31).SetDataValidation().InCellDropdown = true;
                        template.Workbook.Worksheet(1).Column(33).SetDataValidation().List(template.Workbook.Worksheet(1).Range("AN1:AN4"), true);
                        template.Workbook.Worksheet(1).Column(33).SetDataValidation().IgnoreBlanks = true;
                        template.Workbook.Worksheet(1).Column(33).SetDataValidation().InCellDropdown = true;
                        template.Workbook.Worksheet(1).Column(37).SetDataValidation().List(template.Workbook.Worksheet(1).Range("AO1:AO6"), true);
                        template.Workbook.Worksheet(1).Column(37).SetDataValidation().IgnoreBlanks = true;
                        template.Workbook.Worksheet(1).Column(37).SetDataValidation().InCellDropdown = true;
                    }

                    //template.Workbook.Worksheet(2).Hide();
                    //template.Workbook.Worksheet(1).Rows().AdjustToContents();
                    template.SaveAs(stream);
                    template.Dispose();

                }

                stream.Seek(0, SeekOrigin.Begin);
                string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";




                if (listitems.Count() == 0)
                {
                    return new FileStreamResult(stream, mimeType)
                    {
                        FileDownloadName = "FileasStream.xlsx"
                    };
                }
                return new FileStreamResult(stream, mimeType)
                {
                    FileDownloadName = "FileasStream.xlsx"
                };
            }
            catch
            {
                return null;
            }

        



            //}
            //return File(stream, "application/octet-stream"); // returns a FileStreamResult
        }
        public Tuple<bool, string> ValidateFileMachineReport(string Filenames,string DocCode,string DocNo)
        {          
            string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\"+ DocCode+"\\"+ DocNo+"\\";
            XSSFWorkbook hssfwb;
            using (FileStream file = new FileStream(webRootPath + Filenames, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new XSSFWorkbook(file);
            }
      
           var Checkvalidate = this.ValidateExcel(hssfwb);
           var HtmlTable = this.GanerateHtmlTable(hssfwb, Filenames);
            return System.Tuple.Create(Checkvalidate, HtmlTable);
        }
        private bool ValidateExcel(XSSFWorkbook FileExcel)
        {
            bool CheckNotBlank = true;

            if (FileExcel == null)
                return false;

            ISheet sheet = FileExcel.GetSheetAt(0);
            for (int row = 14; row <= sheet.LastRowNum - 1; row++)
            {
                if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    var item = GetCellString(sheet.GetRow(row).GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK)).ToString();
                    if (!string.IsNullOrEmpty(item.ToString()))
                    {
                        //var Status = GetCellString(sheet.GetRow(row).GetCell(10, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        //var InchargSecCode = GetCellString(sheet.GetRow(row).GetCell(14, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        //var McNo = GetCellString(sheet.GetRow(row).GetCell(16, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        //var ProcessName = GetCellString(sheet.GetRow(row).GetCell(17, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        //var ProductType = GetCellString(sheet.GetRow(row).GetCell(18, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        //var Location = GetCellString(sheet.GetRow(row).GetCell(19, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        //var Operation = GetCellString(sheet.GetRow(row).GetCell(20, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        //var Inventory = GetCellString(sheet.GetRow(row).GetCell(32, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        //var Response = GetCellString(sheet.GetRow(row).GetCell(35, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        //var useFul = GetCellString(sheet.GetRow(row).GetCell(36, MissingCellPolicy.RETURN_NULL_AND_BLANK));


                        var Status = GetCellString(sheet.GetRow(row).GetCell(10, MissingCellPolicy.RETURN_NULL_AND_BLANK)) == "-" ? "" : GetCellString(sheet.GetRow(row).GetCell(10, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        var InchargSecCode = GetCellString(sheet.GetRow(row).GetCell(14, MissingCellPolicy.RETURN_NULL_AND_BLANK)) == "-" ? "" : GetCellString(sheet.GetRow(row).GetCell(14, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        var McNo = GetCellString(sheet.GetRow(row).GetCell(16, MissingCellPolicy.RETURN_NULL_AND_BLANK)) == "-" ? "" : GetCellString(sheet.GetRow(row).GetCell(16, MissingCellPolicy.RETURN_NULL_AND_BLANK)); ;
                        var ProcessName = GetCellString(sheet.GetRow(row).GetCell(17, MissingCellPolicy.RETURN_NULL_AND_BLANK)) == "-" ? "" : GetCellString(sheet.GetRow(row).GetCell(17, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        var ProductType = GetCellString(sheet.GetRow(row).GetCell(18, MissingCellPolicy.RETURN_NULL_AND_BLANK)) == "-" ? "" : GetCellString(sheet.GetRow(row).GetCell(18, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        var Location = GetCellString(sheet.GetRow(row).GetCell(19, MissingCellPolicy.RETURN_NULL_AND_BLANK)) == "-" ? "" : GetCellString(sheet.GetRow(row).GetCell(19));
                        var Operation = GetCellString(sheet.GetRow(row).GetCell(30, MissingCellPolicy.RETURN_NULL_AND_BLANK)) == "-" ? "" : GetCellString(sheet.GetRow(row).GetCell(30, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        var Inventory = GetCellString(sheet.GetRow(row).GetCell(32, MissingCellPolicy.RETURN_NULL_AND_BLANK)) == "-" ? "" : GetCellString(sheet.GetRow(row).GetCell(32, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        var Remark = GetCellString(sheet.GetRow(row).GetCell(33, MissingCellPolicy.RETURN_NULL_AND_BLANK)) == "-" ? "" : GetCellString(sheet.GetRow(row).GetCell(33, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        var Response = GetCellString(sheet.GetRow(row).GetCell(35, MissingCellPolicy.RETURN_NULL_AND_BLANK)) == "-" ? "" : GetCellString(sheet.GetRow(row).GetCell(35, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                        var useFul = GetCellString(sheet.GetRow(row).GetCell(36, MissingCellPolicy.RETURN_NULL_AND_BLANK)) == "-" ? "" : GetCellString(sheet.GetRow(row).GetCell(36, MissingCellPolicy.RETURN_NULL_AND_BLANK)); ;

                        if ((Operation.ToUpper() == "START") && ((string.IsNullOrEmpty(Status) || string.IsNullOrEmpty(InchargSecCode) || string.IsNullOrEmpty(McNo) || string.IsNullOrEmpty(ProcessName)
                            || string.IsNullOrEmpty(ProductType) || string.IsNullOrEmpty(Location) || string.IsNullOrEmpty(Operation)
                            || string.IsNullOrEmpty(Inventory) || string.IsNullOrEmpty(Remark) || string.IsNullOrEmpty(Response) || string.IsNullOrEmpty(useFul))))
                        {
                            CheckNotBlank = false;
                            break;
                        }

                        if (Operation.ToUpper() == "" || Operation.ToUpper() == "-")
                        {
                            CheckNotBlank = false;
                            break;
                        }
                    }

                }
            }
            return CheckNotBlank;
        }
        private string GanerateHtmlTable(XSSFWorkbook FileExcel,string Filenames)
        {
            var HtmlTable = new StringBuilder();
            HtmlTable.Append("<div class='row'>");
            HtmlTable.Append("<div class='col-12'>");
            HtmlTable.Append("<div class='card'>");
            HtmlTable.Append("<div class='card-header bg-secondary text-secondary'>");
            HtmlTable.Append("<span class='card-title'>"+ Filenames + "</span>");
            HtmlTable.Append("</div>");
            HtmlTable.Append("<div class='card-body p-0'>");
            HtmlTable.Append("<table class='table' style='font-size:8px';>");

            ISheet sheet = FileExcel.GetSheetAt(0);
            for (int row = 13; row <= sheet.LastRowNum; row++)
            {
                if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    var item = GetCellString(sheet.GetRow(row).GetCell(2)).ToString();
                    var RowOpen = "";
                    var RowClose = "";


                    if (!string.IsNullOrEmpty(item) && row != 13)
                    {
                        RowOpen = "<tr>";
                        RowClose="</tr>";
                    }
                    else if(row == 13)
                    {
                        RowOpen = " <thead><tr style='background-color:lightgoldenrodyellow'>";
                        RowClose = "</tr></thead><tbody>";
                    }
                    else
                    {
                        RowOpen = "<tr style='background-color:lightgray'>";
                    }

                    List<int> RedRow =new List<int>() { 10, 14,16,17,18,19,30,32,35,36};
                    List<int> ValueRow = new List<int>() { 1, 2, 5, 6,7, 10, 14, 16, 17, 18, 19,28,29, 30, 32, 35, 36 };
                    if (row == 13)
                    {
                        HtmlTable.Append(RowOpen);
                        for (var i = 1; i <= 36; i++)
                        {
                            if (ValueRow.Contains(i))
                            {
                                XSSFCell oldCell = (XSSFCell)sheet.GetRow(row).GetCell(i, MissingCellPolicy.RETURN_NULL_AND_BLANK); // ancienne cell
                                string TdTag = RedRow.Contains(i) ? "<td style='background-color:red'>" : "<td>";
                                switch (oldCell.CellType)
                                {
                                    case CellType.String:                                                                         
                                            HtmlTable.Append(TdTag + sheet.GetRow(row).GetCell(i).StringCellValue + "</td>");                                                                    
                                        break;
                                    case CellType.Numeric:
                                        if (i!=6 ||i!=7)
                                        {
                                            HtmlTable.Append("<td>" + sheet.GetRow(row).GetCell(i).NumericCellValue.ToString("#,##0.00") + "</td>");
                                        }
                                        else
                                        {
                                            HtmlTable.Append("<td>" + sheet.GetRow(row).GetCell(i).NumericCellValue.ToString("#,##0.00") + "</td>");
                                        }
                                      
                                        break;
                                    case CellType.Blank:
                                        HtmlTable.Append(TdTag + "</td>");
                                        break;
                                    case CellType.Boolean:
                                        HtmlTable.Append(TdTag + sheet.GetRow(row).GetCell(i).BooleanCellValue + "</td>");
                                        break;
                                    case CellType.Formula:
                                        HtmlTable.Append(TdTag + "</td>");
                                        break;
                                    default:
                                        break;
                                }
                            }
                           
                        }
                        HtmlTable.Append(RowClose);                     
                    }
                    else
                    {
                        HtmlTable.Append(RowOpen);
                        for (var i=1;i<=36;i++)
                        {
                            if (ValueRow.Contains(i))
                            {
                                XSSFCell oldCell = (XSSFCell)sheet.GetRow(row).GetCell(i, MissingCellPolicy.RETURN_NULL_AND_BLANK); // ancienne cell

                                if (oldCell!=null)
                                {
                                    switch (oldCell.CellType)
                                    {
                                        case CellType.String:
                                            
                                                HtmlTable.Append("<td>" + sheet.GetRow(row).GetCell(i).StringCellValue + "</td>");
                                            
                                            break;
                                        case CellType.Numeric:
                                            if (i==1)
                                            {
                                               HtmlTable.Append("<td>" + sheet.GetRow(row).GetCell(i).NumericCellValue.ToString() + "</td>");
                                            }
                                            else
                                            {
                                               HtmlTable.Append("<td>" + sheet.GetRow(row).GetCell(i).NumericCellValue.ToString("#,##0.00") + "</td>");
                                            }
                                           
                                            break;
                                        case CellType.Blank:
                                            HtmlTable.Append("<td></td>");
                                            break;
                                        case CellType.Boolean:
                                            HtmlTable.Append("<td>" + sheet.GetRow(row).GetCell(i).BooleanCellValue + "</td>");
                                            break;
                                        case CellType.Formula:
                                            IFormulaEvaluator evaluator = FileExcel.GetCreationHelper().CreateFormulaEvaluator();
                                            CellValue cellValue = evaluator.Evaluate(sheet.GetRow(row).GetCell(i));
                                            HtmlTable.Append("<td>" + cellValue.NumberValue.ToString("#,##0.00") + "</td>");
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else{
                                    HtmlTable.Append("<td></td>");
                                }
                                
                            }
                        }                                                                
                        HtmlTable.Append(RowClose);
                    }                  
                }
            }
            HtmlTable.Append("</tbody>");
            HtmlTable.Append("</table>");
            HtmlTable.Append("</div>");
            HtmlTable.Append("</div>");
            HtmlTable.Append("</div>");
            HtmlTable.Append("</div>");

            return HtmlTable.ToString();
        }
        public MemoryStream PrintReport(dynamic documentItemProgresses ,string File,byte[] PdfApprove, string DocCode, string DocNo)
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\"+ DocCode.Trim()+ "\\"+ DocNo.Trim()+ "\\"+ File.Trim();
            string fontPath = _hostingEnvironment.WebRootPath + "\\fonts\\THSarabunNew.ttf";
            string JPfontPath = _hostingEnvironment.WebRootPath + "\\fonts\\Kokoro.otf";
            XSSFWorkbook FileExcel;
            using (FileStream file = new FileStream(webRootPath, FileMode.Open, FileAccess.Read))
            {
                FileExcel = new XSSFWorkbook(file);
            }
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);
            BaseFont bfjp = BaseFont.CreateFont(JPfontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);
            Font defaultFont = new Font(bf, 12);
            Font boldTableFont = new Font(bf, 5, Font.BOLD);
            Font headTB = new Font(bf, 10, Font.NORMAL);
            Font headTBJP = new Font(bfjp, 10, Font.NORMAL);
            Font headTB1 = new Font(bf, 14, Font.UNDERLINE);
            Font body = new Font(bf, 6, Font.BOLD);
            Font body1 = new Font(bf, 6, Font.NORMAL);
            Font bodysmall = new Font(bf, 14, Font.NORMAL);

            Document document = new Document(PageSize.A3.Rotate(), 13f, 13f, 20f, 30f);
            MemoryStream workStream = new MemoryStream();
            PdfPTable table = new PdfPTable(28);
            table.DefaultCell.Border = 1;
            table.SetWidths(new float[] { 0.2f, 1.00f, 0.5f, 1.00f, 1.00f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 1.00f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.3f, 0.5f, 0.5f });
            table.WidthPercentage = 100;
            table.HeaderRows = 3;


            ISheet sheet = FileExcel.GetSheetAt(0);
            string cellvalueH0 = sheet.GetRow(1).GetCell(1).StringCellValue;
            PdfPCell hcellh0 = new PdfPCell(new Phrase(cellvalueH0, headTB1))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Colspan = 28,
                Border = 0,
                PaddingBottom = 10f
            };
            table.AddCell(hcellh0);

            string cellvalueH1 = sheet.GetRow(3).GetCell(1).StringCellValue;
            PdfPCell hcellh1 = new PdfPCell(new Phrase(cellvalueH1, headTBJP))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Colspan = 28,
                Border = 0,
                PaddingBottom = 10f
            };
            table.AddCell(hcellh1);

            string cellvalueH2 = sheet.GetRow(4).GetCell(1).StringCellValue;
            PdfPCell hcellh2 = new PdfPCell(new Phrase(cellvalueH2, headTB))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Colspan = 28,
                Border = 0,
                PaddingBottom = 10f
            };
            table.AddCell(hcellh2);

         
            for (int row = 13; row <= sheet.LastRowNum; row++)
            {
                if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                {
                    var item = GetCellString(sheet.GetRow(row).GetCell(2)).ToString();        
                    List<int> RedRow = new List<int>() { 10, 14, 16, 17, 18, 19, 30, 32, 35, 36 };
                    List<int> ValueRow = new List<int>() { 1, 2,3,4, 5, 6, 7,8,9, 10,11,12,13, 14,15, 16, 17, 18, 19,28,29, 30,31, 32,33,34, 35, 36 };
                    string cellvalue = "";
                    if (row == 13)
                    {                      
                        for (var i = 1; i <= 36; i++)
                        {
                           
                            if (ValueRow.Contains(i))
                            {
                                XSSFCell oldCell = (XSSFCell)sheet.GetRow(row).GetCell(i); // ancienne cell                           
                                switch (oldCell.CellType)
                                {
                                    case CellType.String:
                                        cellvalue = sheet.GetRow(row).GetCell(i).StringCellValue;                                       
                                        break;
                                    case CellType.Numeric:
                                        cellvalue = sheet.GetRow(row).GetCell(i).NumericCellValue.ToString();                                       
                                        break;
                                    case CellType.Blank:
                                        cellvalue = "";
                                        break;
                                    case CellType.Boolean:
                                        cellvalue = sheet.GetRow(row).GetCell(i).BooleanCellValue.ToString();
                                        break;                                  
                                    default:
                                        break;
                                }
                                PdfPCell tc = new PdfPCell(new Phrase(cellvalue, boldTableFont))
                                {
                                    HorizontalAlignment = Element.ALIGN_CENTER,
                                    BorderWidthLeft = 0.3f,
                                    BorderWidthRight = 0.3f,
                                    BorderWidthTop = 1f,
                                    BorderWidthBottom = 1f,
                                    VerticalAlignment = Element.ALIGN_TOP
                                };

                                if (RedRow.Contains(i)) {
                                    tc.BackgroundColor = new BaseColor(255, 153, 153);
                                }
                                else {
                                    tc.BackgroundColor = new BaseColor(255, 255, 204);
                                };
                                table.AddCell(tc);
                                cellvalue = "";
                            }
                           
                        }                        
                    }
                    else
                    {                    
                        for (var i = 1; i <= 36; i++)
                        {
                            if (ValueRow.Contains(i))
                            {
                                XSSFCell oldCell = (XSSFCell)sheet.GetRow(row).GetCell(i); // ancienne cell
                                var textalign = Element.ALIGN_LEFT;
                              
                                if (oldCell != null)
                                {
                                    switch (oldCell.CellType)
                                    {
                                        case CellType.String:
                                            cellvalue = sheet.GetRow(row).GetCell(i).StringCellValue;
                                            break;
                                        case CellType.Numeric:
                                            if (i != 6 || i != 7)
                                            {
                                                cellvalue = sheet.GetRow(row).GetCell(i).NumericCellValue.ToString();
                                            }
                                            else
                                            {
                                                cellvalue = string.Format("{0:#,0.##########}", sheet.GetRow(row).GetCell(i).NumericCellValue);
                                            }
                                               
                                            textalign = Element.ALIGN_RIGHT;
                                            break;
                                        case CellType.Blank:
                                            cellvalue = "";
                                            break;
                                        case CellType.Boolean:
                                            cellvalue = sheet.GetRow(row).GetCell(i).BooleanCellValue.ToString();
                                            break;
                                        case CellType.Formula:
                                            IFormulaEvaluator evaluator = FileExcel.GetCreationHelper().CreateFormulaEvaluator();
                                            CellValue cellValueData = evaluator.Evaluate(sheet.GetRow(row).GetCell(i));
                                            cellvalue = string.Format("{0:#,0.##########}", cellValueData.NumberValue);
                                            textalign = Element.ALIGN_RIGHT;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                else
                                {
                                    cellvalue = "";
                                }
                                PdfPCell tcc2 = new PdfPCell(new Phrase(cellvalue, body1))
                                {
                                    HorizontalAlignment = textalign,
                                    FixedHeight = 15f,
                                    BorderWidthLeft = 0.3f,
                                    BorderWidthRight = 0.3f,
                                    BorderWidthTop = 0f,
                                    BorderWidthBottom = 0.3f,
                                    VerticalAlignment = Element.ALIGN_MIDDLE,
                                    BorderColor = BaseColor.LightGray
                                };
                                if (string.IsNullOrEmpty(item) && row != sheet.LastRowNum)
                                {
                                    tcc2.BackgroundColor = new BaseColor(224, 224, 224);
                                }
                                else if(row == sheet.LastRowNum)
                                {
                                    tcc2.BackgroundColor = new BaseColor(255, 255, 204);
                                }
                                table.AddCell(tcc2);
                                cellvalue = "";
                            }
                            
                        }
                     
                    }
                }
            }

           


            Image jpg = Image.GetInstance(PdftoImage(PdfApprove),BaseColor.Black,false);
            jpg.ScaleToFit(1100f, 300f);
            jpg.Alignment = Element.ALIGN_LEFT;
           
            

            PdfWriter writer = PdfWriter.GetInstance(document, workStream);
            writer.PageEvent = new ITextEvents(_hostingEnvironment);
            document.Open();



            writer.CloseStream = false;
            ////document.Add(new Paragraph("Hello World!"));
            document.Add(table);
            document.Add(jpg);
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;        
            return workStream;
        }
      
        /// <summary>Abre un archivo de Excel (xls o xlsx) y lo convierte en un DataTable.
        /// LA PRIMERA FILA DEBE CONTENER LOS NOMBRES DE LOS CAMPOS.</summary>
        /// <param name="pRutaArchivo">Ruta completa del archivo a abrir.</param>
        /// <param name="pHojaIndex">Número (basado en cero) de la hoja que se desea abrir. 0 es la primera hoja.</param>
        public DataTable Excel_To_DataTable(string filepath, int sheetindex)
        {
            filepath = _hostingEnvironment.WebRootPath + "\\ExcelTemplate\\data.xlsx";
            sheetindex = 0;
            // --------------------------------- //
            /* REFERENCIAS:
             * NPOI.dll
             * NPOI.OOXML.dll
             * NPOI.OpenXml4Net.dll */
            // --------------------------------- //
            /* USING:
             * using NPOI.SS.UserModel;
             * using NPOI.HSSF.UserModel;
             * using NPOI.XSSF.UserModel; */
            // AUTOR: Ing. Jhollman Chacon R. 2015
            // --------------------------------- //
            DataTable Tabla = null;
            try
            {
                if (System.IO.File.Exists(filepath))
                {

                    IWorkbook workbook = null;  //IWorkbook determina si es xls o xlsx              
                    ISheet worksheet = null;
                    string first_sheet_name = "";

                    using (FileStream FS = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                    {
                        workbook = WorkbookFactory.Create(FS);          //Abre tanto XLS como XLSX
                        worksheet = workbook.GetSheetAt(sheetindex);    //Obtener Hoja por indice
                        first_sheet_name = worksheet.SheetName;         //Obtener el nombre de la Hoja

                        Tabla = new DataTable(first_sheet_name);
                        Tabla.Rows.Clear();
                        Tabla.Columns.Clear();

                        // Leer Fila por fila desde la primera
                        for (int rowIndex = 7; rowIndex <= worksheet.LastRowNum; rowIndex++)
                        {
                            DataRow NewReg = null;
                            IRow row = worksheet.GetRow(rowIndex);
                            IRow row2 = null;
                            IRow row3 = null;

                            if (rowIndex == 0)
                            {
                                row2 = worksheet.GetRow(rowIndex + 1); //Si es la Primera fila, obtengo tambien la segunda para saber el tipo de datos
                                row3 = worksheet.GetRow(rowIndex + 2); //Y la tercera tambien por las dudas
                            }

                            if (row != null) //null is when the row only contains empty cells 
                            {
                                if (rowIndex > 0) NewReg = Tabla.NewRow();

                                int colIndex = 0;
                                //Leer cada Columna de la fila
                                foreach (ICell cell in row.Cells)
                                {
                                    object valorCell = null;
                                    string cellType = "";
                                    string[] cellType2 = new string[2];

                                    if (rowIndex == 0) //Asumo que la primera fila contiene los titlos:
                                    {
                                        for (int i = 0; i < 2; i++)
                                        {
                                            ICell cell2 = null;
                                            if (i == 0) { cell2 = row2.GetCell(cell.ColumnIndex); }
                                            else { cell2 = row3.GetCell(cell.ColumnIndex); }

                                            if (cell2 != null)
                                            {
                                                switch (cell2.CellType)
                                                {
                                                    case CellType.Blank: break;
                                                    case CellType.Boolean: cellType2[i] = "System.Boolean"; break;
                                                    case CellType.String: cellType2[i] = "System.String"; break;
                                                    case CellType.Numeric:
                                                        if (HSSFDateUtil.IsCellDateFormatted(cell2)) { cellType2[i] = "System.DateTime"; }
                                                        else
                                                        {
                                                            cellType2[i] = "System.Double";  //valorCell = cell2.NumericCellValue;
                                                        }
                                                        break;

                                                    case CellType.Formula:
                                                        bool continuar = true;
                                                        switch (cell2.CachedFormulaResultType)
                                                        {
                                                            case CellType.Boolean: cellType2[i] = "System.Boolean"; break;
                                                            case CellType.String: cellType2[i] = "System.String"; break;
                                                            case CellType.Numeric:
                                                                if (HSSFDateUtil.IsCellDateFormatted(cell2)) { cellType2[i] = "System.DateTime"; }
                                                                else
                                                                {
                                                                    try
                                                                    {
                                                                        //DETERMINAR SI ES BOOLEANO
                                                                        if (cell2.CellFormula == "TRUE()") { cellType2[i] = "System.Boolean"; continuar = false; }
                                                                        if (continuar && cell2.CellFormula == "FALSE()") { cellType2[i] = "System.Boolean"; continuar = false; }
                                                                        if (continuar) { cellType2[i] = "System.Double"; continuar = false; }
                                                                    }
                                                                    catch { }
                                                                }
                                                                break;
                                                        }
                                                        break;
                                                    default:
                                                        cellType2[i] = "System.String"; break;
                                                }
                                            }
                                        }

                                        //Resolver las diferencias de Tipos
                                        if (cellType2[0] == cellType2[1]) { cellType = cellType2[0]; }
                                        else
                                        {
                                            if (cellType2[0] == null) cellType = cellType2[1];
                                            if (cellType2[1] == null) cellType = cellType2[0];
                                            if (cellType == "") cellType = "System.String";
                                        }

                                        //Obtener el nombre de la Columna
                                        string colName = "Column_{0}";
                                        try { colName = cell.StringCellValue; }
                                        catch { colName = string.Format(colName, colIndex); }

                                        //Verificar que NO se repita el Nombre de la Columna
                                        foreach (DataColumn col in Tabla.Columns)
                                        {
                                            if (col.ColumnName == colName) colName = string.Format("{0}_{1}", colName, colIndex);
                                        }

                                        //Agregar el campos de la tabla:
                                        DataColumn codigo = new DataColumn(colName, System.Type.GetType(cellType));
                                        Tabla.Columns.Add(codigo); colIndex++;
                                    }
                                    else
                                    {
                                        //Las demas filas son registros:
                                        switch (cell.CellType)
                                        {
                                            case CellType.Blank: valorCell = DBNull.Value; break;
                                            case CellType.Boolean: valorCell = cell.BooleanCellValue; break;
                                            case CellType.String: valorCell = cell.StringCellValue; break;
                                            case CellType.Numeric:
                                                if (HSSFDateUtil.IsCellDateFormatted(cell)) { valorCell = cell.DateCellValue; }
                                                else { valorCell = cell.NumericCellValue; }
                                                break;
                                            case CellType.Formula:
                                                switch (cell.CachedFormulaResultType)
                                                {
                                                    case CellType.Blank: valorCell = DBNull.Value; break;
                                                    case CellType.String: valorCell = cell.StringCellValue; break;
                                                    case CellType.Boolean: valorCell = cell.BooleanCellValue; break;
                                                    case CellType.Numeric:
                                                        if (HSSFDateUtil.IsCellDateFormatted(cell)) { valorCell = cell.DateCellValue; }
                                                        else { valorCell = cell.NumericCellValue; }
                                                        break;
                                                }
                                                break;
                                            default: valorCell = cell.StringCellValue; break;
                                        }
                                        //Agregar el nuevo Registro
                                        if (cell.ColumnIndex <= Tabla.Columns.Count - 1) NewReg[cell.ColumnIndex] = valorCell;
                                    }
                                }
                            }
                            if (rowIndex > 0) Tabla.Rows.Add(NewReg);
                        }
                        Tabla.AcceptChanges();
                    }
                }
                else
                {
                    throw new Exception("ERROR 404: El archivo especificado NO existe.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Tabla;
        }
        private string GetCellString(ICell cell)
        {
            object ValueCell = null;

            if (cell == null)
                return "";
            //Las demas filas son registros:
            switch (cell.CellType)
            {
                case CellType.Blank: ValueCell = DBNull.Value; break;
                case CellType.Boolean: ValueCell = cell.BooleanCellValue; break;
                case CellType.String: ValueCell = cell.StringCellValue; break;
                case CellType.Numeric:
                    if (HSSFDateUtil.IsCellDateFormatted(cell)) { ValueCell = cell.DateCellValue; }
                    else { ValueCell = cell.NumericCellValue; }
                    break;
                case CellType.Formula:
                    switch (cell.CachedFormulaResultType)
                    {
                        case CellType.Blank: ValueCell = DBNull.Value; break;
                        case CellType.String: ValueCell = cell.StringCellValue; break;
                        case CellType.Boolean: ValueCell = cell.BooleanCellValue; break;
                        case CellType.Numeric:
                            if (HSSFDateUtil.IsCellDateFormatted(cell)) { ValueCell = cell.DateCellValue; }
                            else { ValueCell = cell.NumericCellValue; }
                            break;
                    }
                    break;
                default: ValueCell = cell.StringCellValue; break;
            }
            return ValueCell.ToString();


        }
        public System.Drawing.Image PdftoImage(byte[] PdfApprove)
        {
            var doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromBytes(PdfApprove);

            System.Drawing.Image bmp = doc.SaveAsImage(0);
            return bmp;
        }

    }
}

