using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.ModelsForm
{
    public class PendingDocModel
    {
        public string DocumentNo { get; set; }       
        public string ReqOperatorName { get; set; }     
        public string ApproveName { get; set; }
        public int SeqNo { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentName { get; set; }
        public string RoleId { get; set; }
        public string ApprovalItemCode { get; set; }
        public string ApprovalItemName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApproverOperatorId { get; set; }
        public string ApproverOperatorName { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        public string DocumentStatus { get; set;}
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? IssuedDate { get; set; }
        
    }
}
