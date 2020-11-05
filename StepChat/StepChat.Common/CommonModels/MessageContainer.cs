using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepChat.Common.CommonModels
{
    public enum MessageTypes
    {
        ContactListStatusUpdate,
        NewIncomingMessage,
        AuthMessage,
        AppMessage,
        ToServerMessage,
        FromHttpToWeb,
        GroupListUpdate
    }

    public class MessageContainer<T>
    {
        public MessageTypes MessageType { get; set; }
        public T Body { get; set; }
    }
}
