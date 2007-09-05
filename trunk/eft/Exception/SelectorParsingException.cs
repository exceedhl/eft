namespace eft.Exception
{
    public class SelectorParsingException : System.Exception
    {
        public SelectorParsingException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public SelectorParsingException(string message) : base(message)
        {
        }
    }
}