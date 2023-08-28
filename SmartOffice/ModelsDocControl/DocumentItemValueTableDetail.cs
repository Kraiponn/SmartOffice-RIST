using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class DocumentItemValueTableDetail
    {
        public int Id { get; set; }
        public string TableCode { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentCode { get; set; }
        public int DisplayOrder { get; set; }
        public string FinalResult { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        public string ValueE { get; set; }
        public string ValueT { get; set; }
        public string ValueJ { get; set; }
        public string ItemCode { get; set; }
        public int ItemId { get; set; }
        public string InputItemCode { get; set; }
        public string InputType { get; set; }
        public string DataType { get; set; }

       
    }
}
