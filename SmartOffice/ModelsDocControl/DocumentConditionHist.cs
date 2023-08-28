using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class DocumentConditionHist
    {
        public int Id { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentCode { get; set; }
        public string Template { get; set; }
        public string Design { get; set; }
        public string Condition { get; set; }
        public string Value { get; set; }

      
    }
}
