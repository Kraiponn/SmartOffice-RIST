using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class ReservationMasterRoom
    {
        public string RoomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LocationId { get; set; }
        public string Image { get; set; }
        public bool Disable { get; set; }
        public int? NumberOfSeats { get; set; }
        public bool? Projector { get; set; }
        public bool? Computer { get; set; }
        public string TvconferenceIp { get; set; }
        public string ResponsibleBy { get; set; }
        public string ResponsibleName { get; set; }
        public string ResponsibleTelNo { get; set; }
        public string ResponsibleEmail { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }

     
    }
}
