
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class ViewEventsTable
    {
        
            public int EventId { get; set; }
            public int PartId { get; set; }
            public string GroupCateg { get; set; }
            public string Subject { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public string Description { get; set; }
            public string ThemeColor { get; set; }
            public bool? IsFullDay { get; set; }
            public string CreatedDate { get; set; }
            public string CreateBy { get; set; }
            public string UpdateDate { get; set; }
            public string UpdateBy { get; set; }
            public string Type { get; set; }

            public virtual AspNetGroup GroupCategNavigation { get; set; }
            public virtual ControlPart Part { get; set; }
        
    }
}
