namespace eft.Locators.Selectors
{
    public interface IParser
    {
        Selector Parse(string selectorString);
    }
}