using System;
using System.Collections.Generic;

namespace SmartOffice.PRApprove
{
    public partial class ApproveDetail
    {
        public string Prno { get; set; }
        public string IssueSectionCode { get; set; }
        public string ChargeSectionCode { get; set; }
        public string AccountNo { get; set; }
        public string AccountSubNo { get; set; }
        public decimal? LocalCurAmount { get; set; }
        public string LevelAppCode { get; set; }
        public string SubLevelAppCode { get; set; }
        public string OpNoApp { get; set; }
        public string ApproveFlag { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int Seq { get; set; }
        public string AssignTo { get; set; }
    }
}
