using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SmartOffice.ModelsEsmartOffice;

namespace SmartOffice.Models.ViewModel
{
    public class ViewNewsModel
        {          
            public List<NnewsHeader> News { get; set; }        
            public string Department { get; set; }          
        }
    public class ViewNewsTable
    {
        public ViewNewsTable()
        {
            NnewsDetail = new HashSet<NnewsDetail>();
        }

        public int PartId { get; set; }
        public string GroupCateg { get; set; }
        public int NewHorder { get; set; }
        public string NewsType { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Date { get; set; }
        public string ImgPath { get; set; }
        public string LinkPath { get; set; }
        public string IconClass { get; set; }
        public string Badges { get; set; }
        public string BadgesName { get; set; }
        public bool Download { get; set; }
        public bool? Disable { get; set; }
        public string CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public int? ChildType { get; set; }
        public string imagePath { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ItemType { get; set; }
        public string Value1 { get; set; }
        public int? CountRead { get; set; }

        public virtual AspNetGroup GroupCategNavigation { get; set; }
        public virtual ControlPart Part { get; set; }
        public virtual ICollection<NnewsDetail> NnewsDetail { get; set; }
       
    }
    public class ViewNewsdetail
    {
        public int PartId { get; set; }
        public string GroupCateg { get; set; }
        public int NewHorder { get; set; }
        public int NewDorder { get; set; }
        public string ItemType { get; set; }
        public string Value { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string Value1 { get; set; }
        public string ImagePath { get; set; }
        public bool? ShowPublic { get; set; }
        public virtual NnewsHeader NnewsHeader { get; set; }       
       
    }
    
}
