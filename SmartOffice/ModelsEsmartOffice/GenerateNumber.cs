using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class GenerateNumber
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int? Sequence { get; set; }
        public string TypeId { get; set; }
    }
}
