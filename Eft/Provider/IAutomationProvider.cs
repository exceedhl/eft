using System.Collections.Generic;
using System.Windows;
using Eft.Locators.Selectors;

namespace Eft.Provider
{
    public interface IAutomationProvider
    {
        string Name { get; }
        string Id { get; }

        string Text { get; }

        Point GetClickablePoint();
        void Focus();
        void Click();
        void RightClick();
        void Type(string text);
        List<Element> Find(string selectorString);
        List<Element> Find(Selector selector);
        List<Element> FindChildren(SimpleSelector simpleSelector);
        List<Element> Find(SimpleSelector simpleSelector);
    }
}