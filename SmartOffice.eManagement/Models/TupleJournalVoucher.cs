using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartOffice.eManagement.ModelsManagementControl;
using SmartOffice.eManagement.Models;

namespace SmartOffice.eManagement.Models
{

    public class TupleJournalVoucher
    {

            
        public List<vewJounalVoucherdata> vewJounalVoucherdatas { get; set; }
        public SelectList ddlSectionCodeList { get; set; }


    }
    
    public class SelectListItemModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class vewJounalVoucherdata
    {
        public string OpMonth { get; set; }
        public string LineNumber { get; set; }
        public string Descript { get; set; }
        public string StkTakingAmount { get; set; }
    }

}


