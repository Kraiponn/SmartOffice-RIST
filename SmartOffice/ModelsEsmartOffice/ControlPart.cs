using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class ControlPart
    {
        public ControlPart()
        {
            AspNetGroupMenu = new HashSet<AspNetGroupMenu>();
            ControlConfig = new HashSet<ControlConfig>();            
            ImgHeader = new HashSet<ImgHeader>();
            NnewsHeader = new HashSet<NnewsHeader>();
        }

        public int PartId { get; set; }
        public string PartName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string PathUrl { get; set; }
        public bool? Disable { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual ICollection<AspNetGroupMenu> AspNetGroupMenu { get; set; }
        public virtual ICollection<ControlConfig> ControlConfig { get; set; }                
        public virtual ICollection<ImgHeader> ImgHeader { get; set; }        
        public virtual ICollection<NnewsHeader> NnewsHeader { get; set; }
    }
}
