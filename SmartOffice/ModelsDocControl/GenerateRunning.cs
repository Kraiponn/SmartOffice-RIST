using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class GenerateRunning
    {
        public string DocumentCode { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Running { get; set; }
        public bool DocStatus { get; set; }
    }
}
