using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
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

        public Point GetClickablePoint()
        {
            return automationElement.GetClickablePoint();
        }

        public void Focus()
        {
            automationElement.SetFocus();
        }

        public void Click()
        {
            Mouse.MoveTo(automationElement.GetClickablePoint());
            Mouse.Click(automationElement.GetClickablePoint());
        }

        public void RightClick()
        {
            Mouse.MoveTo(automationElement.GetClickablePoint());
            Mouse.RightClick(automationElement.GetClickablePoint());
        }

        public void Type(string text)
        {
            Focus();
            SendKeys.SendWait(text);
        }

        public List<Element> Find(string selectorString)
        {
            return Find(parser.Parse(selectorString));
        }

        public List<Element> Find(Selector selector)
        {
            return elementLocator.Find(selector);
        }

        public List<Element> Find(SimpleSelector simpleSelector)
        {
            return FindFrom(simpleSelector, TreeScope.Descendants);
        }

        public List<Element> FindChildren(SimpleSelector simpleSelector)
        {
            return FindFrom(simpleSelector, TreeScope.Children);
        }

        private List<Element> FindFrom(SimpleSelector simpleSelector, TreeScope scope)
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
    }
}