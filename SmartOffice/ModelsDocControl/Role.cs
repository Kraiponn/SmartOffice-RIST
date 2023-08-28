using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class Role
    {
        public Role()
        {
            ApprovalFlow = new HashSet<ApprovalFlow>();
            OperatorRole = new HashSet<OperatorRole>();
        }

        public string RoleId { get; set; }
        public string ApplicationName { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }

        public virtual ICollection<ApprovalFlow> ApprovalFlow { get; set; }
        public virtual ICollection<OperatorRole> OperatorRole { get; set; }
    }
}
