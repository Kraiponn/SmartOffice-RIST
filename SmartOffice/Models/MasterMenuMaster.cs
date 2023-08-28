using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class MasterMenuMaster
    {
        public List<AspNetMenuSetup> menus { get; set; }
        public string UserName { get; set; }
        public string Department { get; set; }
        public string GroupCateg { get; set; }
    }
}
