using System;

namespace StepChat.Common.ViewModels
{
    public class ContactItemModel
    {
        public int ApplicationUserId { get; set; }
        public string ContactNickname { get; set; }
        public string FullName { get; set; }
        public bool IsOnline { get; set; }
        public int UnreadMessagesCount { get; set; }

        private string IsOnlineDisplayText => 
            IsOnline ? "В сети" : "Не в сети";

        public string DisplayText
        { 
            get { return $"{FullName} ({IsOnlineDisplayText})"; } 
        }
    }
}
