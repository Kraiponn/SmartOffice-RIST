using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class NnewsDetail
    {
        public int PartId { get; set; }
        public string GroupCateg { get; set; }
        public int NewHorder { get; set; }
        public int NewDorder { get; set; }
        public string ItemType { get; set; }
        public string Value { get; set; }
        public string Value1 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public bool? ShowPublic { get; set; }

        public virtual NnewsHeader NnewsHeader { get; set; }
    }
}
