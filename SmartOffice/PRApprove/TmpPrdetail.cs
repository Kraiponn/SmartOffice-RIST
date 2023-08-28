using System;
using System.Collections.Generic;

namespace SmartOffice.PRApprove
{
    public partial class TmpPrdetail
    {
        public int RecNo { get; set; }
        public string PrnoTop { get; set; }
        public int? PrnoLine { get; set; }
        public string Prno { get; set; }
        public string Prctgr { get; set; }
        public string Item { get; set; }
        public string Specification { get; set; }
        public string ItemCode { get; set; }
        public string OrderPattern { get; set; }
        public string DesignatedMaker { get; set; }
        public string MakerName { get; set; }
        public string Reason { get; set; }
        public string AccountNo { get; set; }
        public string AccountSubNo { get; set; }
        public string DevelopmentNo { get; set; }
        public string SuspenseAccountNo { get; set; }
        public string LicenceNo { get; set; }
        public string Remarks { get; set; }
        public string DecisionFormNo { get; set; }
        public string ReqSupplier { get; set; }
        public string ReqSupplierName { get; set; }
        public string ReqSupplierReason { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public string UnitName { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? RequestUnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public string Currency { get; set; }
        public string MutualAgreement { get; set; }
        public string BasisofUnitPrice { get; set; }
        public string QuotationNo { get; set; }
        public DateTime? ResultsDay { get; set; }
        public int? DeliveryPlaceCode { get; set; }
        public DateTime? ReqDlvdate { get; set; }
        public string ChargeSectionCode { get; set; }
        public string DeliverySectionCode { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string Propename { get; set; }
        public string OpeTelno { get; set; }
        public string OpeFaxno { get; set; }
        public string TerminalId { get; set; }
        public DateTime OpeDate { get; set; }
        public decimal? LocalCurUnitPrice { get; set; }
        public decimal? LocalCurAmount { get; set; }
        public DateTime? SlipNoInDate { get; set; }
        public int? SlipNoInSeqNo { get; set; }
        public string BuyerCode { get; set; }
        public string FixSupplier { get; set; }
        public string FixSupplierCode { get; set; }
        public string FixItem { get; set; }
        public string FixSpecification { get; set; }
        public DateTime? FixDvldate { get; set; }
        public DateTime? FixDvldate1 { get; set; }
        public DateTime? FixDvldate2 { get; set; }
        public decimal? FixQty1 { get; set; }
        public decimal? FixQty2 { get; set; }
        public string FixItemCode { get; set; }
        public decimal? FixQty { get; set; }
        public decimal? FixUnitPrice { get; set; }
        public decimal? FixAmount { get; set; }
        public string FixAccountNo { get; set; }
        public string FixAccountSubNo { get; set; }
        public string AfterApp { get; set; }
        public string FixCurrency { get; set; }
        public string FixUnit { get; set; }
        public string Tax { get; set; }
        public decimal? FixTotalAmount { get; set; }
        public string FinFlag { get; set; }
        public DateTime? PurDvldate { get; set; }
        public string GroupReceipt { get; set; }
        public string FixItemCategory1 { get; set; }
        public string FixItemCategory2 { get; set; }
        public string FixItemCategory3 { get; set; }
        public string ItemCategory1 { get; set; }
        public string ItemCategory2 { get; set; }
        public string ItemCategory3 { get; set; }
        public string FixSpecialNote { get; set; }
        public string SpecialNote { get; set; }
    }
}
