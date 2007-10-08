namespace Eft.Exception
{
    public class OperationNotSupportedException : System.Exception
    {
        public OperationNotSupportedException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        public OperationNotSupportedException(string message) : base(message)
        {
        }
    }
}