namespace eft.Exception
{
    public class SelectorTranslationException : System.Exception
    {
        public SelectorTranslationException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        public SelectorTranslationException(string message) : base(message)
        {
        }
    }
}