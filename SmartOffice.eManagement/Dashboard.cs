using Microsoft.Extensions.Configuration;
using SmartOffice.eManagement.Class;
using SmartOffice.eManagement.IResponsitory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SmartOffice.eManagement
{
    public class Dashboard :IDashboard
    {
        private readonly IConfiguration _configuration;
        public Dashboard( IConfiguration configuration)
        {           
            _configuration = configuration;           
        }
        public DataSet GetDashboard(string DashboardId, string StartDate, string EndDate)
        {
            var dp = new ConnDashboard(_configuration);
            var data = dp.GetDashboard(DashboardId, StartDate, EndDate);
            return data;
        }

        
    }
}
