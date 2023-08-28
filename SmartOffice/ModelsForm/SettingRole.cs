using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartOffice.Models;
using SmartOffice.ModelsForm;
using SmartOffice.ModelsDocControl;

namespace SmartOffice.ModelsForm
{

    
    public class SettingRole
    {
       

        public List<RoleHrmsEmployee> roleHrmsEmployees { get; set; }
        public List<DataRole> dataRoles { get; set; }

        public List<SelectListItem> select { get; set; }
        public List<SelectListItem> selectrole { get; set; }
    }

    public partial class RoleHrmsEmployee
    {
        public string value { get; set; }
        public string text { get; set; }
       
    }

    public partial class DataRole
    {
        public string value { get; set; }
        public string text { get; set; }

    }
}


