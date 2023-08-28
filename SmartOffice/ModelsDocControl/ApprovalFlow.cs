using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class ApprovalFlow
    {
        public string FlowId { get; set; }
        public int SeqNo { get; set; }
        public string RoleId { get; set; }
        public string ApprovalItemCode { get; set; }
        public string Requirement { get; set; }
        public string RequirementRemark { get; set; }
        public string AssignFlag { get; set; }
        public string AssignFlagRemark { get; set; }
        public int? Reject { get; set; }
        public int? AssignFlagBySeq { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }

    }
}
