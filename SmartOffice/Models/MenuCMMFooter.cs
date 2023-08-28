using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class ModelMenuCMMFooter
    {
        public List<MenuCMMFooter> menuCMMFooters { get; set; }
    }
    public class MenuCMMFooter
    {
        public string MenuNameE { get; set; }
        public string MenuNameT { get; set; }
        public string MenuNameJ { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Param { get; set; }
        public string MenuUrl { get; set; }
        public string IconClass { get; set; }
        public string Badges { get; set; }
        public string BadgesName { get; set; }
        public bool Download { get; set; }
        public int DisplayOrder { get; set; }
        public string GroupCateg { get; set; }
        public string GroupName { get; set; }
        public string Image { get; set; }
       
    }
}
