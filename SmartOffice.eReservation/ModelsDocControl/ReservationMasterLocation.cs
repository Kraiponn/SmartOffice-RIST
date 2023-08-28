using System;
using System.Collections.Generic;

namespace SmartOffice.eReservation.ModelsDocControl
{
    public partial class ReservationMasterLocation
    {
        public string LocationId { get; set; }
        public string Building { get; set; }
        public string Code { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
