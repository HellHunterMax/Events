namespace Events.Core.Shared.Exceptions
{

    [Serializable]
    public class FailedSendMailException : Exception
    {
        public FailedSendMailException() { }
        public FailedSendMailException(string message) : base(message) { }
        public FailedSendMailException(string message, Exception inner) : base(message, inner) { }
        protected FailedSendMailException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
