using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class AspNetMenuControl
    {
        public int MenuIdentity { get; set; }
        public int MenuIdentityParent { get; set; }
        public int DisplayOrder { get; set; }
        public int GroupMenuId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual AspNetGroupMenu GroupMenu { get; set; }
        public virtual AspNetMenu MenuIdentityNavigation { get; set; }
        public virtual AspNetMenu MenuIdentityParentNavigation { get; set; }
    }
}
