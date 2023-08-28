
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models.ViewModel
{
    public class FooterDeptViewModel
    {
          public virtual ICollection<ControlConfig> ControlConfigs{ get; set; }
          public virtual ICollection<NnewsHeader> NnewsHeaders{ get; set; }
    }
}
