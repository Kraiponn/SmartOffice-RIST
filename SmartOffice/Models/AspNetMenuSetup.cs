using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class AspNetMenuSetup
    {
        public int MenuIdentity { get; set; }
        public string MenuName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Param { get; set; }
        public string MenuUrl { get; set; }
        public string IconClass { get; set; }
        public string Badges { get; set; }
        public string BadgesName { get; set; }
        public bool Download { get; set; }

        
        public int DisplayOrder { get; set; }
        public int MenuIdentityParent { get; set; }

        public int PartId { get; set; }
        public string GroupCateg { get; set; }
        public int GroupMenuId { get; set; }
        public string GroupMenuName { get; set; }
        public int GroupDisplayOrder { get; set; }
        public string GroupIconClass { get; set; }
        public string GroupBadges { get; set; }
        public string GroupBadgesName { get; set; }
    }
}
