using System;
using System.Collections.Generic;

namespace SmartOffice.eReservation.ModelsDocControl
{
    public partial class ReservationMasterRoomLayout
    {
        public string LayoutId { get; set; }
        public string RoomId { get; set; }
        public int DisplayOrder { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool? Disable { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
