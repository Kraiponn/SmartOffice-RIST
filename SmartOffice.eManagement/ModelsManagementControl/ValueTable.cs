using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class ValueTable
    {
        public int Id { get; set; }
        public string TableCode { get; set; }
        public int DisplayOrder { get; set; }
        public string Value { get; set; }
        public string InputType { get; set; }
        public string DataType { get; set; }
        public string ValueCode { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
