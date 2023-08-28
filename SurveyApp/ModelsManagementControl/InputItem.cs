using System;
using System.Collections.Generic;

namespace SmartOffice.SurveyApp.ModelsManagementControl
{
    public partial class InputItem
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
        public virtual ICollection<ValueList> ValueLists { get; set; }
    }
}
