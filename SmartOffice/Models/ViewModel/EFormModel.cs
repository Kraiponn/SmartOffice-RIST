using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models.ViewModel
{
    public class TupleFormMasterDashboard
    {

        public List<FormMasterDashboard> formMasterDashboards { get; set; }

    }
    public class TupleFormChartData
    {
        public List<Labels> labels { get; set; }
       
        
    }

    public class FormMasterDashboard
    {
        public int nochart { get; set; }        
        public string typechart { get; set; }
        public string sub { get; set; }        
        public string code { get; set; }
        public string namechart { get; set; }
    }

    public class TupleFormChartDataLabels
    {
        public List<Labels> labels { get; set; }
      
        
    }


    public class Labels
    {
        public int _datanochart { get; set; }
        public string _datatypechart { get; set; }
        public string _datalabels { get; set; }
        public string _datalabel { get; set; }
        public string _dataamount { get; set; }

    }
    
   

    

}
