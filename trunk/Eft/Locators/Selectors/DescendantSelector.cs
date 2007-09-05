namespace Eft.Locators.Selectors
{
    public class DescendantSelector : Selector
    {
        private readonly Selector ancestorSelector;
        private readonly SimpleSelector simpleSelector;

        public DescendantSelector(Selector ancestorSelector, SimpleSelector simpleSelector)
        {
            this.ancestorSelector = ancestorSelector;
            this.simpleSelector = simpleSelector;
        }

        public Selector AncestorSelector
        {
            get { return ancestorSelector; }
        }

        public SimpleSelector SimpleSelector
        {
            get { return simpleSelector; }
        }
    }
}
