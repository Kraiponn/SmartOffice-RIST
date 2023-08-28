using System;
using System.Collections.Generic;

namespace SmartOffice.EmailCore.Models
{
    public partial class ErrorLog
    {
        public DateTime Date { get; set; }
        public int? ErrorNumber { get; set; }
        public int? ErrorSeverity { get; set; }
        public int? ErrorState { get; set; }
        public string ErrorProcedure { get; set; }
        public int? ErrorLine { get; set; }
        public string ErrorMessage { get; set; }
    }
}
