using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models.ViewModel
{
    
     
    public class DataChartDayDetailNameDepart
    {
        public string _data { get; set; }
    }
    public class DataChartDayDetailunit
    {
        public int _data { get; set; }
    }
    public class DataCharteDashboard
    {
        public List<DataChartDayDetailunit> _dataList { get; set; }
    }

    public class DataChartAllformDoc
    {
        public List<DataChartDayDetailNameDepart> NameDepart { get; set; }
        public List<DataChartDayDetailunit> CountDoc { get; set; }
    }

    public class DataChartcurrentmonthDoc
    {
        public List<DataChartDayDetailNameDepart> NameDepart { get; set; }
        public List<DataChartDayDetailunit> DaftDoc { get; set; }
        public List<DataChartDayDetailunit> ProcessDoc { get; set; }
        public List<DataChartDayDetailunit> CompleteDoc { get; set; }
    }
}
