using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Windows.Input;
using Eft.Elements;
using Eft.Exception;
using Eft.Locators;
using Eft.Locators.Selectors;
using Eft.Win32;

namespace Eft.Provider
{
    public class UIAutomationProvider : IAutomationProvider
    {
        private readonly AutomationElement automationElement;
        private readonly SelectorTranslator translator;
        private readonly IParser parser;
        private readonly ElementLocator elementLocator;

        public UIAutomationProvider(AutomationElement automationElement, SelectorTranslator translator, IParser parser)
        {
            this.automationElement = automationElement;
            this.translator = translator;
            this.parser = parser;
            elementLocator = new ElementLocator(this);
        }

        public string Name
        {
            get { return automationElement.Current.Name; }
        }

        public string Id
        {
            get { return automationElement.Current.AutomationId; }
        }

        public string Text
        {
            get
            {
                object pattern;
                if (automationElement.TryGetCurrentPattern(TextPattern.Pattern, out pattern))
                {
                    return ((TextPattern) pattern).DocumentRange.GetText(-1);
                }
                if (automationElement.TryGetCurrentPattern(ValuePattern.Pattern, out pattern))
                {
                    return ((ValuePattern) pattern).Current.Value;
                }
                if (automationElement.TryGetCurrentPattern(RangeValuePattern.Pattern, out pattern))
                {
                    return ((RangeValuePattern) pattern).Current.Value.ToString();
                }
                return null;
            }
        }

        public Point ClickablePoint
        {
            get { return automationElement.GetClickablePoint(); }
        }

        public bool IsWindow
        {
            get
            {
                ControlType controlType =
                    (ControlType) automationElement.GetCurrentPropertyValue(AutomationElement.ControlTypeProperty);
                return controlType == ControlType.Window;
            }
        }

        private WindowPattern GetWindowPattern()
        {
            try
            {
                return (WindowPattern) automationElement.GetCurrentPattern(WindowPattern.Pattern);
            }
            catch (InvalidOperationException e)
            {
                throw new OperationNotSupportedException("This element does not support window pattern", e);
            }
        }

        public void ChangeWindowState(WindowState windowState)
        {
            WindowPattern pattern = GetWindowPattern();
            try
            {
                switch (windowState)
                {
                    case WindowState.Maximized:
                        pattern.SetWindowVisualState(WindowVisualState.Maximized);
                        break;
                    case WindowState.Minimized:
                        pattern.SetWindowVisualState(WindowVisualState.Minimized);
                        break;
                    default:
                        pattern.SetWindowVisualState(WindowVisualState.Normal);
                        break;
                }
            }
            catch (InvalidOperationException e)
            {
                throw new OperationNotSupportedException("Can not resize this window", e);
            }
        }

        public void Focus()
        {
            automationElement.SetFocus();
        }

        public void Click(MouseButton mouseButton, ModifierKeys modifierKeys, int times)
        {
            if (times < 0)
            {
                throw new IllegalParameterException("Click times can not be minus");
            }
            Key key = GetKey(modifierKeys);
            Keyboard.SendKeyboardInput(key, true);
            for (int i = 0; i < times; i++)
            {
                Mouse.MoveToAndClick(ClickablePoint, mouseButton);
            }
            Keyboard.SendKeyboardInput(key, false);
        }

        public static Key GetKey(ModifierKeys modifierKeys)
        {
            switch (modifierKeys)
            {
                case ModifierKeys.Control:
                    return Key.LeftCtrl;
                case ModifierKeys.Alt:
                    return Key.LeftAlt;
                case ModifierKeys.Shift:
                    return Key.LeftShift;
                case ModifierKeys.Windows:
                    return Key.LWin;
                default:
                    return Key.None;
            }
        }

        public void Click(Point point)
        {
            Mouse.MoveToAndClick(point);
        }

        public void Type(string text)
        {
            Focus();
            SendKeys.SendWait(text);
        }

        public WindowState WindowState
        {
            get
            {
                WindowPattern pattern = GetWindowPattern();
                switch (pattern.Current.WindowVisualState)
                {
                    case WindowVisualState.Maximized:
                        return WindowState.Maximized;
                    case WindowVisualState.Minimized:
                        return WindowState.Minimized;
                    default:
                        return WindowState.Normal;
                }
            }
        }

        public Rect BoundingRectangle
        {
            get
            {
                object boudingRect =
                    automationElement.GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty);
                if (boudingRect == AutomationElement.NotSupported)
                {
                    throw new PropertyNotSupportedException("Can not get bounding rectangle of this element");
                }
                return (Rect) boudingRect;
            }
        }

        public List<Element> Find(string selectorString)
        {
            return Find(parser.Parse(selectorString));
        }

        public List<Element> Find(Selector selector)
        {
            return elementLocator.Find(selector);
        }

        public List<Element> FindDescendents(SimpleSelector simpleSelector)
        {
            return FindInScope(simpleSelector, TreeScope.Descendants);
        }

        public List<Element> FindChildren(SimpleSelector simpleSelector)
        {
            return FindInScope(simpleSelector, TreeScope.Children);
        }

        private List<Element> FindInScope(SimpleSelector simpleSelector, TreeScope scope)
        {
            List<Element> elements = new List<Element>();
            AutomationElementCollection all =
                automationElement.FindAll(scope, translator.Translate(simpleSelector));
            foreach (AutomationElement element in all)
            {
                elements.Add(new Element(new UIAutomationProvider(element, translator, parser)));
            }

            return elements;
        }

        public override string ToString()
        {
            return "[ControlType: " + automationElement.Current.LocalizedControlType +
                   "; Name: " + Name +
                   "; AutomationId: " + Id + "]";
        }
    }
}