
using SmartOffice.eReservation.ModelsDocControl;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmartOffice.EReservation.IResponsitory
{
    public interface IEReservationRoom
    {
        Task<dynamic> GetResourcesfillter(string Searchtext);
        Task<dynamic> GetResources(List<string> getResources);
        dynamic GetEvents(string Year, string Month);
        Task<dynamic> SaveEvent(List<IFormFile> files, int ReservationId, string RoomId, string StartDate, string EndDate,
            string OperatorId, string OperatorName,string OperatorBldg, string Bldg, string OperatorOrganizationname, string OperatorPhoneNumber, string RequestDate, string UpdateDate,
            string TotalOperator, string LayoutId, string Subject, string mailuserrequest, string Datemultiple);
        dynamic DeleteEvent(int reservationId, string mailuserrequest);
        dynamic GetLayout(string RoomId);
        List<ReservationMasterRoom>  Getimageh();
        List<ReservationMasterRoomImage> Getimaged(string RoomId);
        List<ReservationAttachFile> GetFileAttachdata( int ReservationId, string Type);
        
        dynamic Deletefile(int reservationId, string type, string filename, string end);
        dynamic ApproveEvent(int reservationId, string mailuserapprove,string userapprove,string nameapprove, string remarks);
        dynamic RejectEvent(int reservationId, string mailuserapprove, string userapprove, string nameapprove, string remarks);


    }
}
