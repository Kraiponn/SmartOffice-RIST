using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartOffice.Models
{
    [Table("Document_Item")]
    public class DocItem
    {
        [Key]
        public string DocumentNo { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public string DocumentGroupCode { get; set; }
        public string DocumentKind { get; set; }
        public string OpeGroupCateg { get; set; }
        public string EmailSend { get; set; }
        public string AttachedFile { get; set; }
        public string AttachedFile1 { get; set; }
        public string AttachedFile2 { get; set; }
        public string StandardNo { get; set; }
        public string ReviseNo { get; set; }
        public string Remark { get; set; }
        public string Language { get; set; }
        public DateTime IssuedDate { get; set; }
        public string ReqDescription1 { get; set; }
        public string ReqDescription2 { get; set; }
        public string ReqDescription3 { get; set; }
        public string ReqOperatorId { get; set; }
        public string ReqOperatorName { get; set; }
        public string DocumentStatus { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
