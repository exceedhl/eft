namespace Eft.Locators.Selectors
{
    public class Attribute
    {
        private readonly string localName;

        private readonly string value;

        public Attribute(string localName, string value)
        {
            this.localName = localName;
            this.value = value;
        }

        public string LocalName
        {
            get { return localName; }
        }

        public string Value
        {
            get { return value; }
        }
    }
}