using System.Windows.Automation;
using Eft.Exception;
using Eft.Locators.Selectors;

namespace Eft.Locators
{
    public class SelectorTranslator
    {
        public Condition Translate(SimpleSelector selector)
        {
            if (selector is ElementSelector)
            {
                ElementSelector elementSelector = selector as ElementSelector;
                if (elementSelector.LocalName == ElementSelector.UNIVERSAL_SELECTOR_LOCALNAME)
                {
                    return Condition.TrueCondition;
                }
                return
                    new PropertyCondition(AutomationElement.ControlTypeProperty,
                                          ControlTypeSearcher.GetControlType(elementSelector.LocalName));
            }
            else if (selector is AttributeSelector)
            {
                AttributeSelector attributeSelector = selector as AttributeSelector;
                return
                    new AndCondition(Translate(attributeSelector.SimpleSelector),
                                     Translate(attributeSelector.Attribute));
            }
            return null;
        }

        public PropertyCondition Translate(Attribute attribute)
        {
            switch (attribute.LocalName)
            {
                case "id":
                    return new PropertyCondition(AutomationElement.AutomationIdProperty, attribute.Value);
                case "name":
                    return new PropertyCondition(AutomationElement.NameProperty, attribute.Value);
                case "className":
                    return new PropertyCondition(AutomationElement.ClassNameProperty, attribute.Value);
                default:
                    throw new SelectorTranslationException("Attribute " + attribute.LocalName +
                                                           " is not supported by ui automation");
            }
        }
    }
}