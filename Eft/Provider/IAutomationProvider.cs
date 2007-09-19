using System.Collections.Generic;
using System.Windows;
using Eft.Elements;
using Eft.Locators.Selectors;

namespace Eft.Provider
{
    public interface IAutomationProvider
    {
        string Name { get; }
        string Id { get; }
        string Text { get; }
        Point ClickablePoint { get; }

        bool IsWindow { get; }
        void ChangeWindowState(WindowState windowState);
        WindowState WindowState { get; }

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