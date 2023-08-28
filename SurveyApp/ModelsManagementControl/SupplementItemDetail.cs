using System;
using System.Collections.Generic;

namespace SmartOffice.SurveyApp.ModelsManagementControl
{
    public partial class SupplementItemDetail
    {
        public int SupplementId { get; set; }
        public string OperationCode { get; set; }
        public string OperationName { get; set; }
        public string Ztcno { get; set; }
        public string Ztcname { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public DateTime KeyTime { get; set; }
        public string FinalResult { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
