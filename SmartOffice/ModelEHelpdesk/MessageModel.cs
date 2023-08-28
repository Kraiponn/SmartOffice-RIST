using SmartOffice.Models;
using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.ModelsHRMSLocal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartOffice.EHelpdesk.Models
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Timestamp { get; set; }
        public string FromUserId { get; set; }
        public int ToRoomId { get; set; }
        public virtual HrmsEmployee FromUser { get; set; }
        public virtual Rooms ToRoom { get; set; }
    }
}