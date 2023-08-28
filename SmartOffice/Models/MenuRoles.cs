using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class MenuRoles
    {
                   
            public string MenuId { get; set; }
            public string MenuName { get; set; }
            public string ParentMenuId { get; set; }
            public string ParentGroupId { get; set; }
            public string GroupCateg { get; set; }
            public int DisplayOrder { get; set; }
            public string Action { get; set; }
            public string Controller { get; set; }
            public string MenuUrl { get; set; }          
            public string IconClass { get; set; }
            public string Badges { get; set; }
            public string BadgesName { get; set; }
            
        
    }
}
