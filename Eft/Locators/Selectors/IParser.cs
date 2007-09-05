namespace Eft.Locators.Selectors
{
    public interface IParser
    {
        Selector Parse(string selectorString);
    }
}