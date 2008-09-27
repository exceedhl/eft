namespace Eft.Locators.Selectors
{
    public class Position
    {
        private readonly int offset;
        public static Position Last = new Position(int.MaxValue);

        public Position(int offset)
        {
            this.offset = offset;
        }

        public int Offset
        {
            get { return offset; }
        }
    }
}