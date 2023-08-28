using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartOffice.eReservation.ModelsDocControl;


namespace SmartOffice.eReservation.Models
{

    public class TupleEReservationRoom
    {
        public List<ReservationMasterRoom> reservationMasterRoomsall { get; set; }
        public List<ReservationMasterRoomImage> ReservationMasterRoomImagesall { get; set; }

        public SelectList ddlLocationList { get; set; }
    }

    public partial class ReservationMasterRoomSetting
    {
        public string RoomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LocationId { get; set; }
        public string Image { get; set; }
        public bool? Disable { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        public int NumberOfSeats { get; set; }
        public bool? Projector { get; set; }
        public bool? Computer { get; set; }
        public string TVConferenceIP { get; set; }
        public string ResponsibleBy { get; set; }
        public string ResponsibleName { get; set; }
        public string ResponsibleTelNo { get; set; }
        public string ResponsibleEmail { get; set; }

        public string Building { get; set; }
        public string Code { get; set; }
    }

    public class SelectListItem
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }
}


