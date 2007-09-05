namespace eft.Locators.Selectors
{
    public class AttributeSelector : SimpleSelector
    {
        private readonly SimpleSelector simpleSelector;
        private readonly Attribute attribute;

        public AttributeSelector(SimpleSelector simpleSelector, Attribute attribute)
        {
            this.simpleSelector = simpleSelector;
            this.attribute = attribute;
        }

        public SimpleSelector SimpleSelector
        {
            get { return simpleSelector; }
        }

        public Attribute Attribute
        {
            get { return attribute; }
        }
    }
}