
using SmartOffice.eReservation.ModelsDocControl;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartOffice.eReservation.Models;

namespace SmartOffice.EReservation.IResponsitory
{
    public interface IEReservationRoomSetting
    {
        List<ReservationMasterLocation> GetLocationMaster();
        List<SelectListItem> GetLocationMasterlist(string Code);
        dynamic UpdateLocationMaster(ReservationMasterLocation reservationMasterLocation, string UserName,string ComputerName);
        dynamic DeleteLocationMaster(ReservationMasterLocation reservationMasterLocation);
        dynamic AddLocationMaster(string building, string code, string UserName, string ComputerName);

        List<ReservationMasterRoomSetting> GetRoomMaster(string Code);
        List<ReservationMasterRoomImage> GetRoomImage(string Room_ID);
        List<ReservationMasterRoomLayout> GetRoomLayout(string Room_ID);

        dynamic AddRoomMaster(string roomid, string name, string description, string locationid, bool disable,
            string ResponsibleEmail, string ResponsibleBy, string ResponsibleName, string ResponsibleTelNo, int NumberOfSeats,
            string TVConferenceIP, bool Projector, bool Computer, IFormFile files, string UserName, string ComputerName);
        dynamic DeleteRoomMaster(ReservationMasterRoom reservationMasterRoom);

        dynamic AddImageRoomMaster(string roomId, int displayOrder, string description, IFormFile files, string UserName, string ComputerName);
        dynamic AddLayoutRoomMaster(string layoutId, string roomid, int displayOrder, string description, bool disable, IFormFile files, string UserName, string ComputerName);

        dynamic DeleteImageMaster(ReservationMasterRoomImage reservationMasterRoomImage);
        dynamic DeleteLayoutMaster(ReservationMasterRoomLayout reservationMasterRoomLayout);
    }
}
