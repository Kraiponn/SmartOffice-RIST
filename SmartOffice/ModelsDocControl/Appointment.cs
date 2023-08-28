using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public string AppointType { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ThemeColor { get; set; }
        public bool? IsFullDay { get; set; }
        public bool? SendingEmail { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
