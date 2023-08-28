using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class ReservationMasterRoomImage
    {
        public string RoomId { get; set; }
        public int DisplayOrder { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
