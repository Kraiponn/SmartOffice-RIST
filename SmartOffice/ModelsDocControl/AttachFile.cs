using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class AttachFile
    {
        public int No { get; set; }
        public string DocumentNo { get; set; }
        public string FileName { get; set; }
        public string DocumentCode { get; set; }
        public string Path { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        public int? TotalPage { get; set; }
    }
}
