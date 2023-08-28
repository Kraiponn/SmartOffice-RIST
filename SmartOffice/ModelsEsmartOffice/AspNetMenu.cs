using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class AspNetMenu
    {
        public AspNetMenu()
        {
            AspNetMenuControlMenuIdentityNavigation = new HashSet<AspNetMenuControl>();
            AspNetMenuControlMenuIdentityParentNavigation = new HashSet<AspNetMenuControl>();
            AspNetMenuRoles = new HashSet<AspNetMenuRoles>();
        }

        public int MenuIdentity { get; set; }
        public string MenuNameE { get; set; }
        public string MenuNameT { get; set; }
        public string MenuNameJ { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Param { get; set; }
        public string MenuUrl { get; set; }
        public string Image { get; set; }
        public string IconClass { get; set; }
        public string Badges { get; set; }
        public string BadgesName { get; set; }
        public bool Download { get; set; }
        public bool? Disable { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual ICollection<AspNetMenuControl> AspNetMenuControlMenuIdentityNavigation { get; set; }
        public virtual ICollection<AspNetMenuControl> AspNetMenuControlMenuIdentityParentNavigation { get; set; }
        public virtual ICollection<AspNetMenuRoles> AspNetMenuRoles { get; set; }
    }
}
