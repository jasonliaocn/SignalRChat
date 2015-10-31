//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SignalRChatApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChatSession
    {
        public ChatSession()
        {
            this.ChatContents = new HashSet<ChatContent>();
            this.ChatSessionJoinners = new HashSet<ChatSessionJoinner>();
        }
    
        public int Id { get; set; }
        public string Topic { get; set; }
        public int StartBy { get; set; }
        public System.DateTime StartOn { get; set; }
        public bool IsFinished { get; set; }
        public Nullable<System.DateTime> FinishedOn { get; set; }
    
        public virtual ICollection<ChatContent> ChatContents { get; set; }
        public virtual ICollection<ChatSessionJoinner> ChatSessionJoinners { get; set; }
        public virtual User User { get; set; }
    }
}