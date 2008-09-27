namespace Eft.Exception
{
    public class IllegalParameterException : System.Exception
    {
        public IllegalParameterException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        public IllegalParameterException(string message) : base(message)
        {
        }
    }
}