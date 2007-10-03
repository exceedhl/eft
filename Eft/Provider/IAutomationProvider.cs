using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Eft.Elements;
using Eft.Locators.Selectors;

namespace Eft.Provider
{
    public interface IAutomationProvider
    {
        string Name { get; }
        string Id { get; }
        string Text { get; }

        bool IsWindow { get; }
        void ChangeWindowState(WindowState windowState);
        WindowState WindowState { get; }

        Rect BoundingRectangle { get; }

        void Click(MouseButton button, ModifierKeys modifierKeys, int times);
        void Click(Point point);
        Point ClickablePoint { get; }

        void Focus();
        void Type(string text);
        List<Element> Find(string selectorString);
        List<Element> Find(Selector selector);
        List<Element> FindChildren(SimpleSelector simpleSelector);
        List<Element> Find(SimpleSelector simpleSelector);
    }
}