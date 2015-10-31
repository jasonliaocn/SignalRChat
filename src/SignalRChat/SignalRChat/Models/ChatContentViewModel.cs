using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Models
{
    public class ChatContentViewModel
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SendBy { get; set; }
        public int SendTo { get; set; }
        public string MessageContent { get; set; }
        public System.DateTime SendOn { get; set; }
    }
}