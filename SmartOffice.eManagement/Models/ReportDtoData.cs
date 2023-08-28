using SmartOffice.eManagement.ModelsManagementControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.eManagement.Models
{
    class ReportDtoData
    {
        public string ReturnYear { get; set; }
        public string ReturnMonth { get; set; }
        public string ReturnDate { get; set; }
        public IEnumerable<OperationMachine2> Records{ get; set; }
    }
}
