using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class ImgHeader
    {
        public ImgHeader()
        {
            ImgTextHeader = new HashSet<ImgTextHeader>();
        }

        public int PartId { get; set; }
        public string GroupCateg { get; set; }
        public int ImgOrder { get; set; }
        public string ImgPath { get; set; }
        public bool? ImgActive { get; set; }
        public string ImgType { get; set; }
        public bool? Link { get; set; }
        public string LinkName { get; set; }
        public string LinkPath { get; set; }
        public bool Download { get; set; }
        public bool? Disable { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual AspNetGroup GroupCategNavigation { get; set; }
        public virtual ControlPart Part { get; set; }
        public virtual ICollection<ImgTextHeader> ImgTextHeader { get; set; }
    }
}
