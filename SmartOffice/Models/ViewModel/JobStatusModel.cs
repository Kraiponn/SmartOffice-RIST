using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models.ViewModel
{
    public class DataForm
    {
        public List<DeptForm> deptForms { get; set; }
        public List<CountForm> countForms { get; set; }
    }

    public class DataI
    {
        public List<DeptI> deptIs { get; set; }
        public List<CountI> countIs { get; set; }
    }

    public class DataC
    {
        public List<DeptC> deptCs { get; set; }
        public List<CountC> countCs { get; set; }
    }

    public class DataCO
    {
        public List<DeptCO> deptCOs { get; set; }
        public List<CountCO> countCOs { get; set; }
    }

    public class DataD
    {
        public List<DeptD> deptDs { get; set; }
        public List<CountD> countDs { get; set; }
    }

    public class DataP
    {
        public List<DeptP> deptPs { get; set; }
        public List<CountP> countPs { get; set; }
    }
    public class DataR
    {
        public List<DeptR> deptRs { get; set; }
        public List<CountR> countRs { get; set; }
    }

    public class DataTableData
    {
        public List<TableData> tableDatas { get; set; }
       
    }

    public class DeptForm
    {
        public string _data { get; set; }
    }

    public class CountForm
    {
        public int _data { get; set; }
    }

    public class DeptI
    {
        public string _data { get; set; }
    }

    public class CountI
    {
        public int _data { get; set; }
    }
    public class DeptC
    {
        public string _data { get; set; }
    }

    public class CountC
    {
        public int _data { get; set; }
    }
    public class DeptCO
    {
        public string _data { get; set; }
    }

    public class CountCO
    {
        public int _data { get; set; }
    }
    public class DeptD
    {
        public string _data { get; set; }
    }

    public class CountD
    {
        public int _data { get; set; }
    }
    public class DeptP
    {
        public string _data { get; set; }
    }

    public class CountP
    {
        public int _data { get; set; }
    }
    public class DeptR
    {
        public string _data { get; set; }
    }

    public class CountR
    {
        public int _data { get; set; }
    }

    public class TableData
    {
        public string DocumentNo { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public DateTime IssuedDate { get; set; }
        public string ReqOperatorName { get; set; }
        public string DocumentStatus { get; set; }
        public string ReqDescription1 { get; set; }
        public string DocumentCode { get; set; }
    }
}
