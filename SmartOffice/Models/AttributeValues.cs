using System;
using System.Collections.Generic;

namespace SmartOffice.Models
{
    public partial class AttributeValues
    {
        public int AttrId { get; set; }
        public int RecordId { get; set; }
        public string Value { get; set; }

        public virtual Attributes Attr { get; set; }
    }
}
