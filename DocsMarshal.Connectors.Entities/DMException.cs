using System;

namespace DocsMarshal.Connectors.Entities
{
    public class DMException : Exception
    {
        public Enums.EExceptionReason Reason { get; private set; }
        public string AdvancedMessage { get; private set; }

        public DMException(Enums.EExceptionReason reason) : base(reason.ToString())
        {
            Reason = reason;
            AdvancedMessage = String.Empty;
        }
        public DMException(Enums.EExceptionReason reason, string message) : base(message)
        {
            Reason = reason;
            AdvancedMessage = String.Empty;
        }
        public DMException(Enums.EExceptionReason reason, string message, string advancedMessage) : base(message)
        {
            Reason = reason;
            AdvancedMessage = advancedMessage;
        }
    }
}
