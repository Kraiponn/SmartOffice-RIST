using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class AspNetGroup
    {
        public AspNetGroup()
        {
            AspNetGroupMenu = new HashSet<AspNetGroupMenu>();
            AspNetUsers = new HashSet<AspNetUsers>();
            ControlConfig = new HashSet<ControlConfig>();
            ImgHeader = new HashSet<ImgHeader>();
            NnewsHeader = new HashSet<NnewsHeader>();
        }

        public string GroupCateg { get; set; }
        public string GroupName { get; set; }
        public string DivisionCode { get; set; }
        public bool? Disable { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public int? OrderNo { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<AspNetGroupMenu> AspNetGroupMenu { get; set; }
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        public virtual ICollection<ControlConfig> ControlConfig { get; set; }
        public virtual ICollection<ImgHeader> ImgHeader { get; set; }
        public virtual ICollection<NnewsHeader> NnewsHeader { get; set; }
    }
}
