namespace Eft.Exception
{
    public class NotSupportedPropertyException : System.Exception
    {
        public NotSupportedPropertyException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        public NotSupportedPropertyException(string message) : base(message)
        {
        }
    }
}