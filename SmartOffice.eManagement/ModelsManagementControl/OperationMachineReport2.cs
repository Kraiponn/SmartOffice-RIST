using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class OperationMachineReport2
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string Seq { get; set; }
        public string Item { get; set; }
        public string Specification { get; set; }
        public string Inv { get; set; }
        public string SupplierName { get; set; }
        public string RecordMmYy { get; set; }
        public string DicisionNo { get; set; }
        public string FinishedTarget { get; set; }
        public string SuspenseAcctNo { get; set; }
        public string Status { get; set; }
        public string PrNo { get; set; }
        public string OrderName { get; set; }
        public string OrderSectCode { get; set; }
        public string InchargeSectCode { get; set; }
        public string SectName { get; set; }
        public string MCNo { get; set; }
        public string ProcessName { get; set; }
        public string ProcessNameProductType { get; set; }
        public string Location { get; set; }
        public int? Qty { get; set; }
        public double? BeginningBalance { get; set; }
        public int? BeginQty { get; set; }
        public double? InvAmt { get; set; }
        public int? InvQty { get; set; }
        public double? TfToSpareparts { get; set; }
        public int? Qty1 { get; set; }
        public double? TfToFixedAssets { get; set; }
        public int? Qty2 { get; set; }
        public double? TotalEndingBalance { get; set; }
        public string OperationExpectToOperationMonth { get; set; }
        public string LastMonthConfirmation { get; set; }
        public string Remark { get; set; }
        public decimal? Unit { get; set; }
        public string ResponToConfirmedBy { get; set; }
        public string Currency { get; set; }
        public string UsefulLiftYears { get; set; }
        public string ResponByNameDept { get; set; }
        public DateTime? DateReturnAcc { get; set; }
        public int SeqId { get; set; }
    }
}
