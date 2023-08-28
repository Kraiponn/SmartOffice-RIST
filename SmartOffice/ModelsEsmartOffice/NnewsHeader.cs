using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class NnewsHeader
    {
        public NnewsHeader()
        {
            NnewsDetail = new HashSet<NnewsDetail>();
        }

        public int PartId { get; set; }
        public string GroupCateg { get; set; }
        public int NewHorder { get; set; }
        public string NewsType { get; set; }
        public int? ChildType { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public DateTime? Date { get; set; }
        public string ImgPath { get; set; }
        public string LinkPath { get; set; }
        public string IconClass { get; set; }
        public string Badges { get; set; }
        public string BadgesName { get; set; }
        public bool Download { get; set; }
        public bool? Disable { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CountRead { get; set; }

        public virtual AspNetGroup GroupCategNavigation { get; set; }
        public virtual ControlPart Part { get; set; }
        public virtual ICollection<NnewsDetail> NnewsDetail { get; set; }
    }
}
