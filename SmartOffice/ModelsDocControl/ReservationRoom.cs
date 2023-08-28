using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class ReservationRoom
    {
        public int ReservationId { get; set; }
        public string RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Subject { get; set; }
        public int? TotalOperator { get; set; }
        public string LayoutId { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string OperatorOrganizationname { get; set; }
        public string OperatorPhoneNumber { get; set; }
        public string OperatorEmail { get; set; }
        public string OperatorBldg { get; set; }
        public string Bldg { get; set; }
        public string Status { get; set; }
        public string ApproveId { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string Remarks { get; set; }
    }
}
