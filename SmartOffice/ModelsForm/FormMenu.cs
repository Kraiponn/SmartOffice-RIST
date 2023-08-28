using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.ModelsForm
{
    public class ModelFormMenu
    {
        public List<FormMenu> formMenus { get; set; }
    }
    public class FormMenu
    {
        public int MenuIdentity { get; set; }
        public int MenuIdentityParent { get; set; }
        public int DisplayOrder { get; set; }
        public int GroupMenuID { get; set; }
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
        public bool Disable { get; set; }
        public int PartID { get; set; }
        public string GroupCateg { get; set; }
        public int GroupDisplayOrder { get; set; }
        public string GroupMenuName { get; set; }
        public string GroupName { get; set; }       
    }
}
