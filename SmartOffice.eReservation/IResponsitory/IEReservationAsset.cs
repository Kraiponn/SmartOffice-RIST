
using SmartOffice.eReservation.ModelsDocControl;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmartOffice.EReservation.IResponsitory
{
    public interface IEReservationAsset
    {
        Task<dynamic> GetResourcesfillter(string Searchtext);
        Task<dynamic> GetResources(List<string> getResources);
        dynamic GetEvents(string Year, string Month);
        Task<dynamic> SaveEvent(List<IFormFile> files, int ReservationId, string RoomId, string StartDate, string EndDate,
            string OperatorId, string OperatorName, string OperatorBldg, string Bldg, string OperatorOrganizationname, string OperatorPhoneNumber, string RequestDate, string UpdateDate,
            string LayoutId, string Subject, string mailuserrequest, string Datemultiple);
        dynamic DeleteEvent(int reservationId, string mailuserrequest);
        dynamic GetLayout(string AssetId);
        List<ReservationMasterAsset>  Getimageh();
        List<ReservationMasterAssetImage> Getimaged(string AssetId);
        //List<ReservationAttachFile> GetFileAttachdata( int ReservationId, string Type);
        
        //dynamic Deletefile(int reservationId, string type, string filename, string end);
        dynamic BorrowEvent(int reservationId, string mailuserapprove,string userapprove,string nameapprove, string remarks);
        dynamic ReturnEvent(int reservationId, string mailuserapprove, string userapprove, string nameapprove, string remarks);
        dynamic ResetEvent(int reservationId, string mailuserapprove, string userapprove, string nameapprove, string remarks);

    }
}
