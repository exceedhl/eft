namespace eft.Locators.Selectors
{
    public class ElementSelector : SimpleSelector
    {
        public const string UNIVERSAL_SELECTOR_LOCALNAME = "*";
        private readonly string name;

        public ElementSelector(string name)
        {
            this.name = name;
        }

        public string LocalName
        {
            get { return name; }
        }
    }
}