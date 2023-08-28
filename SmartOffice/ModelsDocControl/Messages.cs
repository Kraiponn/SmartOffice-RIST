using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class Messages
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Timestamp { get; set; }
        public string FromUserId { get; set; }
        public int ToRoomId { get; set; }
    }
}
