using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class DocumentItemProgress
    {
        public int Id { get; set; }
        public string DocumentNo { get; set; }
        public int SeqNo { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public string RoleId { get; set; }
        public string ApprovalItemCode { get; set; }
        public string ApprovalItemNameE { get; set; }
        public string ApprovalItemNameT { get; set; }
        public string ApprovalItemNameJ { get; set; }
        public string Requirement { get; set; }
        public string RequirementRemark { get; set; }
        public bool SkipFlag { get; set; }
        public string AssignFlag { get; set; }
        public string AssignFlagRemark { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApproverOperatorId { get; set; }
        public string ApproverOperatorName { get; set; }
        public string ApproverOperatorSign { get; set; }
        public int? Reject { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        public int? AssignFlagBySeq { get; set; }


    }
}
