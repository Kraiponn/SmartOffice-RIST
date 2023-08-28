using System;
using System.Collections.Generic;

namespace SmartOffice.PRApprove
{
    public partial class AssignAuthorize
    {
        public string Operator { get; set; }
        public string OpeName { get; set; }
        public string TempAuthorizeNo { get; set; }
        public string TempAuthorizeName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string PositionName { get; set; }
        public string SectionName { get; set; }
        public string DepartmentName { get; set; }
        public string DivName { get; set; }
        public string Hq { get; set; }
        public string Reason { get; set; }
        public string DeleteFlag { get; set; }
        public int Seq { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
