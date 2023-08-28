using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.IResponsitory
{
    public interface IInqueryDataService
    {
        DataTable GetDataInquery(string DocCode,string StartDate, string EndDate,string UserId);
        DataTable GetDataInquery2(string DocCode, string StartDate, string EndDate, string UserId);
        DataTable GetDocmentData();
        DataTable GetInputItem(string DocCode);
    }
}
