using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Dtos
{
    public class ChatSessionDto
    {
        public ChatSessionDto()
        {
            Starter = new UserDto();
            Joinner = new List<UserDto>();
            ChatContents = new List<ChatContentDto>();
        }
        public int Id { get; set; }
        public string Topic { get; set; }
        public int StartBy { get; set; }
        public UserDto Starter { get; set; }
        public List<UserDto> Joinner { get; set; }
        public List<ChatContentDto> ChatContents { get; set; }
        public DateTime StartOn { get; set; }
        public bool IsFinished { get; set; }
        public DateTime? FinishedOn { get; set; }
    }
}
