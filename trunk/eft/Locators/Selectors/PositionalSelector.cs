namespace eft.Locators.Selectors
{
    public class PositionalSelector : Selector
    {
        private readonly Selector selector;
        private readonly Position position;

        public PositionalSelector(Selector selector, Position position)
        {
            this.selector = selector;
            this.position = position;
        }

        public Selector Selector
        {
            get { return selector; }
        }

        public Position Position
        {
            get { return position; }
        }
    }
}