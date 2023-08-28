using System;
using System.Collections.Generic;

namespace SmartOffice.SurveyApp.ModelsManagementControl
{
    public partial class Operation
    {
        public string OperationCode { get; set; }
        public string OperationName { get; set; }
        public string OpeGroupCode { get; set; }
        public string InputKind { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }

    }
}
