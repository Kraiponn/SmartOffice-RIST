using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class DocumentQRCode
    {
        public string DocumentCode { get; set; }
        public int CreateNo { get; set; }
        public int Graphic { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Position1 { get; set; }
        public int Position2 { get; set; }
        public int Page { get; set; }
        public string ItemCode { get; set; }
    }
}
