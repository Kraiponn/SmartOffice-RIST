using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class DocumentPermission
    {
        public string DocumentCode { get; set; }
        public string PermissionGroupId { get; set; }
        public string PermissionValue { get; set; }

        public virtual Document DocumentCodeNavigation { get; set; }
        public virtual DocumentPermissionGroup PermissionGroup { get; set; }
    }
}
