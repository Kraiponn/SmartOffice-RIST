using System;
using System.Collections.Generic;

namespace SmartOffice.EmailCore.Models
{
    public partial class SystemLog
    {
        public int LogId { get; set; }
        public string Pcname { get; set; }
        public string UserName { get; set; }
        public DateTime LoggingDate { get; set; }
        public string LoginUserId { get; set; }
        public string LoginUserName { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
    }
}
