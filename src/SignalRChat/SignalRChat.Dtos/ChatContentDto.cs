using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Dtos
{
    public class ChatContentDto
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        public int SendBy { get; set; }

        public int SendTo { get; set; }

        public string MessageContent { get; set; }

        public DateTime SendOn { get; set; }
    }
}
