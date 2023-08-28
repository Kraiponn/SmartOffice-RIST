

using SmartOffice.SurveyApp.ModelsManagementControl;
using System;
using System.Collections.Generic;

namespace SmartOffice.SurveyApp.ViewModel
{
    public class OperatorModel
    {
        public string Ztcno { get; set; }
        public string Ztcname { get; set; }
        public virtual List<SupplementOperation> SupplementOperations { get; set; }
    }
    public partial class SupplementOperation
    {
        public string OperationCode { get; set; }
        public string Ztcno { get; set; }
        public string Ztcname { get; set; }
        public string ItemCateg { get; set; }
        public string InputOption { get; set; }
        public int DisplayOrder { get; set; }
        public string OperationName { get; set; }
        public string OpeGroupCode { get; set; }
        public string InputKind { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }


    }
}
