using SmartOffice.ModelsHRMSLocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models.ViewModel
{
    public class SearchViewModel
    {
        public IEnumerable<DocumentResult> ListDocument { get; set; }
        public IEnumerable<DocumentData> ListDocumentData { get; set; }
        public IEnumerable<RoomData> ListRoomData { get; set; }
        public IEnumerable<InternalTelephone> ListTelephoneData { get; set; }
        
    }
    public class DocumentResult
    {
        public int No { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string DocumentCode { get; set; }
    }
    public class DocumentData
    {
        public int No { get; set; }
        public string DocumentID { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentSubject { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string ReqOperatorName { get; set; }
    }
    public class RoomData
    {   
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Subject { get; set; }
        public string OperatorID { get; set; }
        public string OperatorName { get; set; }

    }
}
