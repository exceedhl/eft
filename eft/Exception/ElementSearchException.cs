namespace eft.Exception
{
    public class ElementSearchException : System.Exception
    {
        public ElementSearchException(string message) : base(message)
        {
        }

        public ElementSearchException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}