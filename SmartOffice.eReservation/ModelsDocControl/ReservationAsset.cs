using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.eReservation.ModelsDocControl
{
    public partial class ReservationAsset
    {
        public int ReservationId { get; set; }
        public string AssetId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Subject { get; set; }
        public string LayoutId { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string OperatorOrganizationname { get; set; }
        public string OperatorPhoneNumber { get; set; }
        public string OperatorBldg { get; set; }
        public string OperatorEmail { get; set; }
        public string Bldg { get; set; }
        public string Status { get; set; }
        public string ApproveID { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ReturnID { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Remarks { get; set; }
    }
}
