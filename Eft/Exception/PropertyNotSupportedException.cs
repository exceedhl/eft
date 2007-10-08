namespace Eft.Exception
{
    public class PropertyNotSupportedException : System.Exception
    {
        public PropertyNotSupportedException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        public PropertyNotSupportedException(string message) : base(message)
        {
        }
    }
}