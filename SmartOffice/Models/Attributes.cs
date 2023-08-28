using System;
using System.Collections.Generic;

namespace SmartOffice.Models
{
    public partial class Attributes
    {
        public Attributes()
        {
            AttributeValues = new HashSet<AttributeValues>();
        }

        public int AttrId { get; set; }
        public string AttributeName { get; set; }
        public string EntityName { get; set; }
        public string AttributeType { get; set; }
        public string AttributeRef { get; set; }

        public virtual Entities EntityNameNavigation { get; set; }
        public virtual ICollection<AttributeValues> AttributeValues { get; set; }
    }
}
