using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class DocumentUploadPdfstamp
    {
        public string DocumentCode { get; set; }
        public int PageTo { get; set; }
        public int PageForm { get; set; }
        public string Stamp { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

       
    }
}
