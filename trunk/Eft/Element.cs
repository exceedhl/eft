using System.Collections.Generic;
using Eft.Exception;
using Eft.Locators.Selectors;
using Eft.Provider;

namespace Eft
{
    public class Element
    {
        private readonly IAutomationProvider provider;

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

        public void Click()
        {
            provider.Click();
        }

        public void Type(string text)
        {
            provider.Type(text);
        }

        public List<Element> Find(Selector selector)
        {
            return provider.Find(selector);
        }

        public List<Element> Find(string selector)
        {
            return provider.Find(selector);
        }

        public List<Element> FindChildren(SimpleSelector simpleSelector)
        {
            return provider.FindChildren(simpleSelector);
        }

        public Element FindFirst(string selectorString)
        {
            List<Element> els = Find(selectorString);
            if (els.Count == 0)
            {
                throw new ElementSearchException("No elements found");
            }
            return els[0];
        }
    }
}