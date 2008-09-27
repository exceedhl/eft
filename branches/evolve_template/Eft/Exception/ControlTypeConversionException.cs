namespace Eft.Exception
{
    public class ControlTypeConversionException : System.Exception
    {
        public ControlTypeConversionException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        public ControlTypeConversionException(string message) : base(message)
        {
        }
    }
}