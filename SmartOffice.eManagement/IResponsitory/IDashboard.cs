using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SmartOffice.eManagement.IResponsitory
{
    public interface IDashboard
    {
        DataSet GetDashboard(string DashboardId, string StartDate, string EndDate);
      
    }
}
