using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.eManagement.Models
{

    //public class Listdatas
    //{
    //    public string Header { get; set; }
    //    public object Value { get; set; }
    //    public string Itemcode { get; set; }
    //}

    //public class TypeTableModel
    //{
    //    public string Itemcode { get; set; }
    //    public List<Listdatas> Listdata { get; set; }
    //}
    public class Listdatas
    {
        public string Header { get; set; }
        public string Value { get; set; }
        public int ItemId { get; set; }
        public string InputItemCode { get; set; }
        public string InputType { get; set; }
        public string DataType { get; set; }
    }

    public class ListTypeTableModel
    {
        public string Itemcode { get; set; }
        public List<Listdatas> Listdata { get; set; }
    }

   
}
