using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class ImgTextHeader
    {
        public int PartId { get; set; }
        public string GroupCateg { get; set; }
        public int ImgOrder { get; set; }
        public int TextHorder { get; set; }
        public string TextH { get; set; }
        public string TextD { get; set; }
        public bool? Link { get; set; }
        public string LinkPath { get; set; }
        public bool Download { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual ImgHeader ImgHeader { get; set; }
    }
}
