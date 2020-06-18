using System;
using System.Collections.Generic;
using System.Text;

namespace ZahlensystemeRechner
{
    public enum ProtocolMessageType
    {
        Info,
        Result,
        Error
    }
    class ProtocolMessage
    {
        public string Message { get; set; }
        public ProtocolMessageType MessageType{get;}

        public ProtocolMessage(string message, ProtocolMessageType type)
        {
            Message = message;
            MessageType = type;
        }
    }
}
