using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Eft.Exception;
using Eft.Locators.Selectors;
using Eft.Provider;
using Eft.Win32;

namespace Eft.Elements
{
    public class Element
    {
        private const int MAXIMUM_WAIT_TIME_IN_SEC = 30;
        private const int WAIT_INTERVAL_IN_MILLIS = 100;

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

        public string Text
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

        public Point ClickablePoint
        {
            get { return provider.ClickablePoint; }
        }

        public void Click()
        {
            provider.Click();
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
            Mouse.MoveToAndClick(point);
        }

        public void RightClick()
        {
            provider.RightClick();
        }

        public void Type(string text)
        {
            provider.Type(text);
        }

        public void ClickAndType(string text)
        {
            Click();
            provider.Type(text);
        }

        public void ClearText()
        {
            Type("{HOME}+{END}{DEL}");
        }

        internal List<Element> Find(Selector selector)
        {
            return provider.Find(selector);
        }

        public Element FindFirst(string selectorString)
        {
            List<Element> els = Find(selectorString);
            if (els.Count == 0)
            {
                throw new ElementSearchException("No elements found: " + selectorString);
            }
            return els[0];
        }

        public List<Element> Find(string selector)
        {
            return provider.Find(selector);
        }

        internal List<Element> FindChildren(SimpleSelector simpleSelector)
        {
            return provider.FindChildren(simpleSelector);
        }

        public List<Element> WaitAndFind(string selectorString)
        {
            return WaitAndFind(selectorString, MAXIMUM_WAIT_TIME_IN_SEC);
        }

        public List<Element> WaitAndFind(string selectorString, int maximumWaitingTimeInSeconds)
        {
            int elaspedTime = 0;
            while (true)
            {
                List<Element> elements = Find(selectorString);
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

        public Element WaitAndFindFirst(string selectorString)
        {
            return WaitAndFindFirst(selectorString, MAXIMUM_WAIT_TIME_IN_SEC);
        }

        public Element WaitAndFindFirst(string selectorString, int maximumWaitingTimeInSeconds)
        {
            return WaitAndFind(selectorString, maximumWaitingTimeInSeconds)[0];
        }

        public void Focus()
        {
            provider.Focus();
        }

        public void DbClick()
        {
            Click();
            Click();
        }
    }
}