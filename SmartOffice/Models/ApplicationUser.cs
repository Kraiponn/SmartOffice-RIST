using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string GroupCateg { get; set; }
      

    }
}
