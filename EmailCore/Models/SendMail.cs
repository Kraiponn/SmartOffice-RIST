using System;
using System.Collections.Generic;

namespace SmartOffice.EmailCore.Models
{
    public partial class SendMail
    {
        public int SeqNo { get; set; }
        public string ToAddress { get; set; }
        public string Ccaddress { get; set; }
        public string Bccaddress { get; set; }
        public string ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachments { get; set; }
        public string ExecSql { get; set; }
        public string ExecSqlresultFileName { get; set; }
        public string ExecDatabase { get; set; }
        public DateTime? SendFlag { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
