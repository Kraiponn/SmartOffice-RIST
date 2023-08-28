using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class JSONResponse
    {
        public List<Components> components { get; set; }

    }
    public class Components
    {
        public string type { get; set; }
        public string key { get; set; }
    
}
    public class GenericObjs
    {
        private List<object> objs = new List<object>();
        public List<object> Objs { get { return objs; } set { objs = value; } }
        public GenericObjs(List<object> Objs) { objs = Objs; }
    }
}
