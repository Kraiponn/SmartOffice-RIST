using System;
using System.Collections.Generic;

namespace SmartOffice.SurveyApp.ModelsManagementControl
{
    public partial class JounalVoucherTempleate
    {
        public string SectionCode { get; set; }
        public int DisplayOrder { get; set; }
        public string Text { get; set; }
        public string TaxClass { get; set; }
        public string Code { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public decimal? Amount { get; set; }
        public string Remarks { get; set; }
        public string SuspenseAccountNo { get; set; }
        public string SupplierEmpCode { get; set; }
        public string SupplierEmpName { get; set; }
        public string Resume { get; set; }
        public string TaxEx { get; set; }
        public string TaxArea { get; set; }
        public string SubledgerType { get; set; }
    }
}
