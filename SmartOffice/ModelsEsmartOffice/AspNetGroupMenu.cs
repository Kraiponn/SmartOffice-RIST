using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class AspNetGroupMenu
    {
        public AspNetGroupMenu()
        {
            AspNetMenuControl = new HashSet<AspNetMenuControl>();
        }

        public int GroupMenuId { get; set; }
        public int PartId { get; set; }
        public string GroupCateg { get; set; }
        public int GroupDisplayOrder { get; set; }
        public string GroupMenuName { get; set; }
        public string IconClass { get; set; }
        public string Badges { get; set; }
        public string BadgesName { get; set; }
        public bool? Disable { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual AspNetGroup GroupCategNavigation { get; set; }
        public virtual ControlPart Part { get; set; }
        public virtual ICollection<AspNetMenuControl> AspNetMenuControl { get; set; }
    }
}
