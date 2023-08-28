using System;
using SmartOffice.EManagement.IResponsitory;
using System.Collections.Generic;
using System.Linq;
using SmartOffice.eManagement.ModelsManagementControl;
using SmartOffice.eManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SmartOffice.eManagement.Class;


namespace SmartOffice.eManagement
{
    public class EJounalVoucher : IEJounalVoucher
    {
        private readonly ManagementControlContext _context;

        private IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public EJounalVoucher(ManagementControlContext context, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _configuration = configuration;
        }

        public List<SelectListItemModel> GetSectionCode()
        {

            List<SelectListItemModel> allfile = _context.JounalVoucherTempleate.Select(x => x.SectionCode).Distinct().ToList().Select(p => new SelectListItemModel()
            {
                ID = p,
                Name = p,
            }).ToList();


            allfile.Add(new SelectListItemModel { ID = "ALL", Name = "ALL" });

            return allfile.OrderBy(x=>x.Name).ToList();
        }

        public List<JounalVoucherTempleate> Loaddatamaking(string monthperiod, string seccode)
        {
            var dp = new ConnDoc(_configuration);

            
            List<JounalVoucherTempleate> allfile = new List<JounalVoucherTempleate>();

            var seccodefind = _context.JounalVoucherTempleate.Where(x => (seccode == "ALL") || (seccode != "ALL" && x.SectionCode == seccode)).Select(x => x.SectionCode).Distinct().ToList();
            foreach (var seccode2 in seccodefind)
            {
                List<JournalVoucher> JournalVoucher = new List<JournalVoucher>();
                var data = dp.GetVoucher(monthperiod ?? "", seccode2 ?? "").ToList();
                var temp = _context.JounalVoucherTempleate.Where(p => p.SectionCode == seccode2).ToList();

                foreach (var item in temp)
                {

                    decimal amount = 0;
                    var text = item.Text;
                    int yyyy = Convert.ToInt16(monthperiod.Substring(0, 4));
                    var mm = Convert.ToInt16(monthperiod.Substring(4, 2));
                    var startDate = new DateTime(yyyy, mm, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);

                    string MMMYY = endDate.ToString("MMM/yy").Replace("/", "'").ToUpper();
                    string[] multiArray = text.Split(new Char[] { '(', ')', '+', '-' });
                    foreach (string author in multiArray)
                    {
                        if (author.Trim() != "")
                        {
                            var textout = author;
                            var fillter = data.Where(i => i.Descript.ToUpper().Contains(textout.ToUpper()) || i.LineNumber.ToUpper().Contains(textout.ToUpper()));
                            decimal total = 0;
                            foreach (var x in fillter)
                            {
                                total += Convert.ToDecimal(x.StkTakingAmount);

                            }
                            text = text.Replace(textout, Convert.ToString(total));

                        }

                    }

                    if (text != "")
                    {
                        System.Data.DataTable table = new System.Data.DataTable();
                        amount = Convert.ToDecimal(table.Compute(text, String.Empty));

                    }
                    //show data screen
                    JounalVoucherTempleate iitem = new JounalVoucherTempleate
                    {
                        SectionCode = item.SectionCode,
                        Remarks = item.Remarks + " " + MMMYY,
                        Resume = item.Resume,
                        SubledgerType = item.SubledgerType,
                        SupplierEmpCode = item.SupplierEmpCode,
                        SupplierEmpName = item.SupplierEmpName,
                        SuspenseAccountNo = item.SuspenseAccountNo,
                        TaxArea = item.TaxArea,
                        TaxClass = item.TaxClass,
                        TaxEx = item.TaxEx,
                        Text = item.Text,
                        AccountName = item.AccountName,
                        AccountNo = item.AccountNo,
                        Amount = amount,//amount
                        Code = item.Code,
                        DisplayOrder = item.DisplayOrder,
                    };
                    allfile.Add(iitem);

                    JournalVoucher JournalVoucheradd = new JournalVoucher
                    {
                        Monthly = monthperiod,
                        SectionCode = seccode2,
                        DisplayOrder = item.DisplayOrder,
                        Code = item.Code,
                        AccountNo = item.AccountNo,

                        TaxEx = item.TaxEx,
                        TaxArea = item.TaxArea,
                        GrossAmount = amount,

                        SubledgerType = item.SubledgerType,
                        Subledger = item.SuspenseAccountNo,

                        ExplanationRemark = item.Remarks + " " + MMMYY,
                        EndDate = endDate,
                        TaxClass = item.TaxClass,
                        AccountName = item.AccountName,

                    };
                    JournalVoucher.Add(JournalVoucheradd);
                }


                //delete
                var datadelete = _context.JournalVoucher.Where(a => a.Monthly == monthperiod && a.SectionCode == seccode2);
                _context.JournalVoucher.RemoveRange(datadelete);

                //insert
                _context.JournalVoucher.AddRange(JournalVoucher);
                _context.SaveChanges();
            }
            return allfile;

        }
        public List<JournalVoucher> Loaddataexport(string monthperiod, string seccode)
        {

            List<JournalVoucher> JournalVoucher = new List<JournalVoucher>();
            JournalVoucher = _context.JournalVoucher.Where(p => p.Monthly == monthperiod && p.SectionCode == seccode).OrderBy(i => i.DisplayOrder).ToList();
            return JournalVoucher;
        }

    }
}
