using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class InputItem
    {
        public string ItemCode { get; set; }
        public string ItemNameE { get; set; }
        public string ItemNameT { get; set; }
        public string ItemNameJ { get; set; }
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
        public string InputOption { get; set; }
        public string DefaultValue { get; set; }
        public bool ReadOnly { get; set; }
        public string DetailOption { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
