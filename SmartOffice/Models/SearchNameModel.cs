using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsHRMSLocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class SearchNameModel
    {
        public virtual IEnumerable<InternalTelephone> Telephones { get; set; }
        public virtual IEnumerable<DocumentItem> DocumentItems{ get; set; }

}
}
