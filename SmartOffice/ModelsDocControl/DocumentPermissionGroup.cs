using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class DocumentPermissionGroup
    {
        public DocumentPermissionGroup()
        {
            DocumentPermission = new HashSet<DocumentPermission>();
        }

        public string PermissionGroupId { get; set; }
        public string PermissionName { get; set; }

        public virtual ICollection<DocumentPermission> DocumentPermission { get; set; }
    }
}
