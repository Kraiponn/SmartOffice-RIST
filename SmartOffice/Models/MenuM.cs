using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class MasterMmenu
    {
        public List<menu> menus { get; set; }
        public string department { get; set; }
        public string username { get; set; }
        public string GroupCateg { get; set; }
        
    }
  

    public class menu
        {
            public string DocumentGroupCode { get; set; }
            public string DocumentGroupName { get; set; }
            public string GroupCateg { get; set; }
            public string DocumentCode { get; set; }
            public string DocumentName { get; set; }
        }
   
}
