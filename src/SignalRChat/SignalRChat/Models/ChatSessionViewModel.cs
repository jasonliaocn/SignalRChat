using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Models
{
    public class ChatSessionViewModel
    {
        public ChatSessionViewModel()
        {
            Joinner = new List<UserViewModel>();
            ChatContents = new List<ChatContentViewModel>();
        }
        public int SessionId { get; set; }
        public string Topic { get; set; }
        public int StartBy { get; set; }
        public UserViewModel Starter { get; set; }
        public List<UserViewModel> Joinner { get; set; }

        public List<ChatContentViewModel> ChatContents { get; set; }
        public DateTime StartOn { get; set; }

        public bool IsFinished { get; set; }
    }
}