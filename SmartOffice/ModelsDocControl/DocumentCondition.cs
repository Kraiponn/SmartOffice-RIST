using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class DocumentCondition
    {
        public int Id { get; set; }
        public string DocumentCode { get; set; }
        public string Template { get; set; }
        public string Design { get; set; }
        public string Condition { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }
    }
}
