using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class DocumentOperatorTransfer
    {
        public string DocumentNo { get; set; }
        public DateTime TransferDate { get; set; }
        public string DocumentCode { get; set; }
        public int SeqNo { get; set; }
        public string RoleId { get; set; }
        public string OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string OperatorIdnew { get; set; }
        public string OperatorNameNew { get; set; }
        public string ComputerName { get; set; }

        public virtual DocumentItem DocumentNoNavigation { get; set; }
    }
}
