using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class ConfirmPasswordReset
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ConfirmId { get; set; }
        public bool? ActiveStatus { get; set; }
        public string ComputerName { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? ConfrimDate { get; set; }
    }
}
