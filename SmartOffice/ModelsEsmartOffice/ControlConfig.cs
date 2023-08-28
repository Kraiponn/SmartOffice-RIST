using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class ControlConfig
    {
        public int PartId { get; set; }
        public string GroupCateg { get; set; }
        public int ConfigOrder { get; set; }
        public string TextH { get; set; }
        public string ColorTextH { get; set; }
        public string BgH { get; set; }
        public string CorlorTextD { get; set; }
        public string BgD { get; set; }
        public string ColorButton { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual AspNetGroup GroupCategNavigation { get; set; }
        public virtual ControlPart Part { get; set; }
    }
}
