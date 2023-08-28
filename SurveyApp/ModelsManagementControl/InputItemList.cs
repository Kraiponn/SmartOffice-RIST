using System;
using System.Collections.Generic;

namespace SmartOffice.SurveyApp.ModelsManagementControl
{
    public partial class InputItemList
    {
        public string ItemCateg { get; set; }
        public string ItemCode { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
