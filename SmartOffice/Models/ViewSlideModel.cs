
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class ViewSlideModel
    {
        public List<ImgHeader> news { get; set; }
        public string department { get; set; }
    }

    public class ViewSlideTable
    {
        public ViewSlideTable()
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
        public string CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public virtual AspNetGroup GroupCategNavigation { get; set; }
        public virtual ControlPart Part { get; set; }
        public virtual ICollection<ImgTextHeader> ImgTextHeader { get; set; }
    }

    public partial class ViewSlideTable2
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
        public string CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual ImgHeader ImgHeader { get; set; }
    }

    
}
