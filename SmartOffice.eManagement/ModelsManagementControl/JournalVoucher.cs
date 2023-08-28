using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class JournalVoucher
    {
        public string Monthly { get; set; }
        public string SectionCode { get; set; }
        public int DisplayOrder { get; set; }
        public string Code { get; set; }
        public string AccountNo { get; set; }
        public string TaxEx { get; set; }
        public string TaxArea { get; set; }
        public decimal? GrossAmount { get; set; }
        public string SubledgerType { get; set; }
        public string Subledger { get; set; }
        public string ExplanationRemark { get; set; }
        public DateTime? EndDate { get; set; }
        public string TaxClass { get; set; }
        public string AccountName { get; set; }
    }
}
