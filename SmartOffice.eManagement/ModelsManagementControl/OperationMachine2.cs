using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class OperationMachine2
    {
        public int Id { get; set; }
        [Column("ITEM")]
        public string Item { get; set; }
        [Column("SPECIFICATION")]
        public string Specification { get; set; }
        [Column("INV.")]
        public string Inv { get; set; }
        [Column("SUPPLIER NAME")]
        public string SupplierName { get; set; }
        [Column("RECORD_MM_YY")]
        public string RecordMmYy { get; set; }
        [Column("DICISION NO.")]
        public string DicisionNo { get; set; }
        [Column("FINISHED TARGET")]
        public string FinishedTarget { get; set; }
        [Column("SUSPENSE ACCT. NO.")]
        public string SuspenseAcctNo { get; set; }
        [Column("STATUS")]
        public string Status { get; set; }
        [Column("PR NO.")]
        public string PrNo { get; set; }
        [Column("ORDER NAME")]
        public string OrderName { get; set; }
        [Column("ORDER SECT CODE")]
        public string OrderSectCode { get; set; }
        [Column("INCHARGE SECT CODE")]
        public string InchargeSectCode { get; set; }
        [Column("SECT NAME")]
        public string SectName { get; set; }
        [Column("M/C NO.")]
        public string MCNo { get; set; }
        [Column("PROCESS NAME")]
        public string ProcessName { get; set; }
        [Column("PRODUCT TYPE")]
        public string ProcessNameProductType { get; set; }
        [Column("LOCATION")]
        public string Location { get; set; }
        [Column("QTY.")]
        public int? Qty { get; set; }
        [Column("BEGINNING BALANCE")]
        public double? BeginningBalance { get; set; }
        [Column("BEGINNING BALANCE Q'TY")]
        public int? BeginQty { get; set; }
        [Column("INV.AMOUNT")]
        public double? InvAmt { get; set; }

        [Column(23)]
        public int? InvQty { get; set; }

        [Column("T/F TO SPARE PARTS")]
        public double? TfToSpareparts { get; set; }

        [Column(25)]
        public int? Qty1 { get; set; }

        [Column("T/F TO FIXED ASSETS")]
        public double? TfToFixedAssets { get; set; }

        [Column(27)]
        public int? Qty2 { get; set; }

        [Column("TOTAL ENDING BALANCE")]
        public double? TotalEndingBalance { get; set; }

        [Column("OPERATION/ EXPECT TO OPERATION MONTH")]
        public string OperationExpectToOperationMonth { get; set; }

        [Column("LAST MONTH CONFIRMATION")]
        public string LastMonthConfirmation { get; set; }

        [Column("INVENTORY")]
        public string Inventory { get; set; }

        [Column("REMARK (INDICATE F/A NO. IF TYPE CHANGE PART)")]
        public string Remark { get; set; }

        [Column("UNIT")]
        public decimal? Unit { get; set; }

        [Column(33)]
        public string ResponToConfirmedBy { get; set; }

        public string Currency { get; set; }
        [Column("USEFUL LIFT (YEARS)")]
        public string UsefulLiftYears { get; set; }
        public string ResponByNameDept { get; set; }
        public DateTime? DateReturnAcc { get; set; }
        public string SortYyMm { get; set; }
    }
}
