using System.Collections.Generic;
using System.Windows;
using eft.Locators.Selectors;

namespace eft.Provider
{
    public interface IAutomationProvider
    {
        string Name { get; }
        string Id { get; }
        Point GetClickablePoint();
        void Focus();
        void Click();
        void Type(string text);
        List<Element> Find(string selectorString);
        List<Element> Find(Selector selector);
        List<Element> FindChildren(SimpleSelector simpleSelector);
        List<Element> Find(SimpleSelector simpleSelector);
    }
}