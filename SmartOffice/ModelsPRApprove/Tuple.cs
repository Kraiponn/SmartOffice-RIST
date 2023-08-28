using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartOffice.Models;
using SmartOffice.ModelsForm;
using SmartOffice.ModelsDocControl;
using SmartOffice.PRApprove;


namespace SmartOffice.ModelsPRApprove
{

    public class Tuple1
    {
        public List<ValueList> ValueList { get; set; }
        
    }
   

    public class PRModel
    {
        public string buyerCode { get; set; }
        public string buyerName { get; set; }
        public string count { get; set; }
        

    }

    
}


