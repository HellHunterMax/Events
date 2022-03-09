namespace Events.Core.Shared.OperationResults
{
    public class OperationResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; } = String.Empty;
        public Exception? Exception { get; private set; }


        public static OperationResult Succeeded()
        {
            return new OperationResult { Success = true };
        }

        public static OperationResult Failed(Exception ex, string message)
        {
            return new OperationResult { Success = false, Exception = ex, Message = message };
        }
    }
    public class OperationResult<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; } = String.Empty;
        public T? Payload { get; private set; }
        public Exception? Exception { get; private set; }


        public static OperationResult<T> Succeeded(T payload)
        {
            return new OperationResult<T> { Success = true, Payload = payload };
        }

        public static OperationResult<T> Failed(Exception ex, string message)
        {
            return new OperationResult<T> { Success = false, Exception = ex, Message = message };
        }
    }
}
