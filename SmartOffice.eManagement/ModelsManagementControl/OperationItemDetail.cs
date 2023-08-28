using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class OperationItemDetail
    {
        public string OperationNo { get; set; }
        public string OperationCode { get; set; }
        public string CategInputOption { get; set; }
        public int CategDisplayOrder { get; set; }
        public string ItemCateg { get; set; }
        public string ItemCategName { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string Remarks3 { get; set; }
        public string Remarks4 { get; set; }
        public string Remarks5 { get; set; }
        public string RemarksTitle1 { get; set; }
        public string RemarksTitle2 { get; set; }
        public string RemarksTitle3 { get; set; }
        public string RemarksTitle4 { get; set; }
        public string RemarksTitle5 { get; set; }
        public string RemarksColor1 { get; set; }
        public string RemarksColor2 { get; set; }
        public string RemarksColor3 { get; set; }
        public string RemarksColor4 { get; set; }
        public string RemarksColor5 { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string InputType { get; set; }
        public string DataType { get; set; }
        public int? DecimalNo { get; set; }
        public bool Required { get; set; }
        public int? Minlength { get; set; }
        public int? Maxlength { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public decimal? Step { get; set; }
        public string Unit { get; set; }
        public string ValueCode { get; set; }
        public string InputOptionItem { get; set; }
        public string DefaultValue { get; set; }
        public bool ReadOnly { get; set; }
        public string DetailOption { get; set; }
        public int ItemListDisplayOrder { get; set; }
        public string FinalResult { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
