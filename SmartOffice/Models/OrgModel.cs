using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class OrgflatModel
    {
        public string OrgID { get; set; }
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int SeqNo { get; set; }
        public string HierarchyID { get; set; }      
    }
    public class OrgModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public List<OrgModel> Children { get; set; }
    }
}
