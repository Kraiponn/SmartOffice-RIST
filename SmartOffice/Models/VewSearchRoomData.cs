using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class VewSearchRoomData
    {
        public string Room_ID { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Subject { get; set; }
        public int? Total_Operator { get; set; }
        public string OperatorPhoneNumber { get; set; }
        public string OperatorName { get; set; }
        public string OperatorID { get; set; }
        public DateTime RequestDate { get; set; }
        public int? Reservation_ID { get; set; }
        
    }
}
