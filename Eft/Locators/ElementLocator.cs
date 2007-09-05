using System.Collections.Generic;
using Eft.Exception;
using Eft.Locators.Selectors;
using Eft.Provider;

namespace Eft.Locators
{
    public class ElementLocator
    {
        private readonly IAutomationProvider provider;

        public ElementLocator(IAutomationProvider provider)
        {
            this.provider = provider;
        }

        public List<Element> Find(Selector selector)
        {
            if (selector is SimpleSelector)
            {
                return provider.Find((SimpleSelector) selector);
            }
            if (selector is ChildrenSelector)
            {
                return Find((ChildrenSelector) selector);
            }
            if (selector is DescendantSelector)
            {
                return Find((DescendantSelector)selector);
            }
            if (selector is PositionalSelector)
            {
                return Find((PositionalSelector) selector);
            }
            throw new ElementSearchException("Unsupported selector: " + selector);
        }

        private List<Element> Find(ChildrenSelector selector)
        {
            List<Element> elements = Find(selector.AncestorSelector);
            List<Element> result = new List<Element>();
            foreach (Element element in elements)
            {
                result.AddRange(element.FindChildren(selector.SimpleSelector));
            }
            return result;
        }

        private List<Element> Find(DescendantSelector selector)
        {
            List<Element> elements = Find(selector.AncestorSelector);
            List<Element> result = new List<Element>();
            foreach (Element element in elements)
            {
                result.AddRange(element.Find(selector.SimpleSelector));
            }
            return result;
        }

        private List<Element> Find(PositionalSelector selector)
        {
            List<Element> result = new List<Element>();
            List<Element> elements = Find(selector.Selector);
            if (elements.Count == 0)
            {
                throw new ElementSearchException("No elements found");
            }
            if (selector.Position == Position.Last)
            {
                result.Add(elements[elements.Count - 1]);
            }
            else
            {
                if (selector.Position.Offset > elements.Count - 1)
                {
                    throw new ElementSearchException("Position: " + selector.Position +
                                                     " is out of index");
                }
                result.Add(elements[selector.Position.Offset]);
            }
            return result;
        }
    }
}