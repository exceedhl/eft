namespace Eft.Tester.Exception
{
    public class IllegalClickPatternException : System.Exception
    {
        public IllegalClickPatternException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        public IllegalClickPatternException(string message) : base(message)
        {
        }
    }
}