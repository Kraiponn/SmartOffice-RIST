using System;
using System.Collections.Generic;

namespace SmartOffice.EAppointment.ModelsForm
{
    public partial class AppointmentOperator
    {
        public int AppointmentId { get; set; }
        public string OperatorId { get; set; }
        public bool? Attend { get; set; }
    }
}
