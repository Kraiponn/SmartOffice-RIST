using System;
using System.Collections.Generic;

namespace SmartOffice.eReservation.ModelsDocControl
{
    public partial class ReservationAttachFile
    {
        public int ReservationId { get; set; }
        public string Type { get; set; }
        public string Filename { get; set; }
    }
}
