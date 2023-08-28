using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class AspNetMenuRoles
    {
        public int MenuIdentity { get; set; }
        public string RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual AspNetMenu MenuIdentityNavigation { get; set; }
        public virtual AspNetRoles Role { get; set; }
    }
}
