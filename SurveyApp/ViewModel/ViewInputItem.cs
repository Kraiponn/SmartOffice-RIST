using System;
using System.Collections.Generic;

namespace SmartOffice.SurveyApp.ViewModel
{
    public partial class ViewInputItem
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string InputType { get; set; }
        public string DataType { get; set; }
        public int? DecimalNo { get; set; }
        public string Unit { get; set; }
        public string ValueCode { get; set; }
        public string InputOption { get; set; }
        public string DefaultValue { get; set; }
        public string Calculation { get; set; }
        public bool ReadOnly { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        public int DisplayOrder { get; set; }
        public string Ztcno { get; set; }
        public string Ztcname { get; set; }
        public DateTime KeyTime { get; set; }
        public string FinalResult { get; set; }
        public int SupplementId { get; set; }
        public string OperationName { get; set; }
        public virtual ICollection<ViewValueList> ValueLists { get; set; }
    }

}
