using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models.ViewModel
{
    public class InternalTelephoneViewModel
    {
        public List<Group> groups { get; set; }
        public SelectList ddlgroups { get; set; }
        public List<groupdata> groupdatas { get; set; }
    }

    public class Group
    {
        public string Group1 { get; set; }
       

    }

    public class SelectListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class groupdata
    {       
        public string Group1 { get; set; }
        public int DisplayGroup1 { get; set; }
        public string Group2 { get; set; }
        public int DisplayGroup2 { get; set; }
        public string Group3 { get; set; }
        public int DisplayGroup3 { get; set; }
    }
}
