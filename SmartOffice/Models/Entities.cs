using System;
using System.Collections.Generic;

namespace SmartOffice.Models
{
    public partial class Entities
    {
        public Entities()
        {
            Attributes = new HashSet<Attributes>();
        }

        public string EntityName { get; set; }
        public string Description { get; set; }
        public string EntityType { get; set; }
        public string FormId { get; set; }

        public virtual ICollection<Attributes> Attributes { get; set; }
    }
}
