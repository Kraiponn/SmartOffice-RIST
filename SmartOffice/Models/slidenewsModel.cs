using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
   

    public partial class Nnews
    {
        public int PartId { get; set; }
        public string GroupCateg { get; set; }
        public int NewHorder { get; set; }
        public int NewDorder { get; set; }
        public string ItemType { get; set; }
        public string Value { get; set; }
        public string Value1 { get; set; }        
        public bool? ShowPublic { get; set; }
      
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
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
