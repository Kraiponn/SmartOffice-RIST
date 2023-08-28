using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.eAppointment.ModelsForm
{
    public partial class VewOperatorAppointment
    {
        public int AppointmentId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ThemeColor { get; set; }
        public bool? IsFullDay { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        public string AppointType { get; set; }
        public string OperatorID { get; set; }
        public bool? Attend { get; set; }
        public string NAMEMPE { get; set; }
        public string EMAIL1 { get; set; }
        public string EMAIL2 { get; set; }
    }
}
