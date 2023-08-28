using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class SupplementItem
    {
        public string SupplementId { get; set; }
        public string SupplementCateg { get; set; }
        public string MachineNo { get; set; }
        public string OpeGroupCode { get; set; }
        public string Ztcno { get; set; }
        public string ZtcnoSuffix { get; set; }
        public DateTime KeyTime { get; set; }
        public string OperationCode { get; set; }
        public string OperationName { get; set; }
        public DateTime? StartTime { get; set; }
        public string StartOperatorId { get; set; }
        public string StartOperatorName { get; set; }
        public DateTime? EndTime { get; set; }
        public string EndOperatorId { get; set; }
        public string EndOperatorName { get; set; }
        public string FinalResult { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
