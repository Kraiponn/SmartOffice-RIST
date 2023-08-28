using System;
using System.Collections.Generic;

namespace SmartOffice.SurveyApp.ViewModel
{
    public partial class ViewValueList
    {
        public string ValueCode { get; set; }
        public string Value { get; set; }
        public string ValueText { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
