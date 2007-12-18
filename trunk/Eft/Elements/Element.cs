using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Eft.Exception;
using Eft.Locators.Selectors;
using Eft.Provider;

namespace Eft.Elements
{
    public class Element
    {
        private const int MAXIMUM_WAIT_TIME_IN_SEC = 30;
        private const int WAIT_INTERVAL_IN_MILLIS = 10;

        protected readonly IAutomationProvider provider;

        public Element(IAutomationProvider provider)
        {
            this.provider = provider;
        }

        public string Name
        {
            get { return provider.Name; }
        }

        public string Id
        {
            get { return provider.Id; }
        }

        public virtual string Text
        {
            get
            {
                if (provider.Text != null)
                {
                    return provider.Text;
                }
                return provider.Name;
            }
        }

        public void Focus()
        {
            provider.Focus();
        }

        public bool IsSelected
        {
            get { return IsChecked; }
        }

        public bool IsChecked
        {
            get { return provider.IsChecked; }
        }

        #region Click related operations

        public Point ClickablePoint
        {
            get { return provider.ClickablePoint; }
        }

        public void DbClick()
        {
            provider.Click(MouseButton.Left, ModifierKeys.None, 2);
        }

        public void Click()
        {
            provider.Click(MouseButton.Left, ModifierKeys.None, 1);
        }

        public void Click(MouseButton mouseButton, ModifierKeys modifierKeys, int times)
        {
            provider.Click(mouseButton, modifierKeys, times);
        }

        public void RightClick()
        {
            provider.Click(MouseButton.Right, ModifierKeys.None, 1);
        }

        public void CtrlClick()
        {
            provider.Click(MouseButton.Left, ModifierKeys.Control, 1);
        }

        public void ShiftClick()
        {
            provider.Click(MouseButton.Left, ModifierKeys.Shift, 1);
        }

        public void Click(int x, int y)
        {
            Rect boundingRect = provider.BoundingRectangle;
            Point point = boundingRect.TopLeft;
            point.Offset(x, y);
            if (!boundingRect.Contains(point))
            {
                throw new IllegalParameterException("Specified click point is out of element's bounding rectangle");
            }
            provider.Click(point);
        }

        #endregion

        #region Text related operations

        public void Type(string text)
        {
            Focus();
            SendKeys.SendWait(text);
        }

        public void ClickAndType(string text)
        {
            Click();
            SendKeys.SendWait(text);
        }

        public void ClearText()
        {
            Click();
            Type("{HOME}+{END}{DEL}");
        }

        #endregion

        #region Find elements

        internal List<Element> Find(Selector selector)
        {
            return provider.Find(selector);
        }

        internal List<Element> FindChildren(SimpleSelector simpleSelector)
        {
            return provider.FindChildren(simpleSelector);
        }

        public List<Element> Find(string selectorString)
        {
            return Find(selectorString, MAXIMUM_WAIT_TIME_IN_SEC);
        }

        public List<Element> Find(string selectorString, int maximumWaitingTimeInSeconds)
        {
            int elaspedTime = 0;
            while (true)
            {
                List<Element> elements = provider.Find(selectorString);
                if (elements.Count > 0)
                {
                    return elements;
                }
                if (elaspedTime > maximumWaitingTimeInSeconds*1000)
                {
                    throw new ElementSearchException(maximumWaitingTimeInSeconds +
                                                     " seconds elapsed, no elements found: " + selectorString);
                }
                Thread.Sleep(WAIT_INTERVAL_IN_MILLIS);
                elaspedTime += WAIT_INTERVAL_IN_MILLIS;
            }
        }

        public Element FindFirst(string selectorString)
        {
            return FindFirst(selectorString, MAXIMUM_WAIT_TIME_IN_SEC);
        }

        public Element FindFirst(string selectorString, int maximumWaitingTimeInSeconds)
        {
            return Find(selectorString, maximumWaitingTimeInSeconds)[0];
        }

        #endregion
    }
}