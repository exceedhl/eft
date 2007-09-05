using System.Text.RegularExpressions;
using eft.Exception;

namespace eft.Locators.Selectors
{
    public class Parser : IParser
    {
        private const string ATTRIBUTE_CONDITION = @"\[(\w+)='(.+)'\]";
        private const string ID_CONDITION = @"#((\w+)|'(.+)')";
        private const string CLASS_CONDITION = @"\.((\w+)|'(.+)')";


        public Selector Parse(string selectorString)
        {
            if (selectorString.Trim().Length == 0)
            {
                throw new SelectorParsingException("Selector can not be empty");
            }
            Scanner scanner = new Scanner(selectorString.Trim());
            if (!scanner.NextSelector())
            {
                throw new SelectorParsingException(selectorString + " is not a legal selector string");
            }
            Selector selector = ParseSelector(scanner);
            if (scanner.IsStringLeft())
            {
                throw new SelectorParsingException(selectorString + " is not a legal selector string");
            }
            return selector;
        }

        private static Selector ParseSelector(Scanner scanner)
        {
            Selector result;
            SimpleSelector simpleSelector = ParseSimpleSelector(scanner);
            string pseudo = scanner.Pseudo;
            string combinator = scanner.Combinator;
            if (combinator.Length > 0)
            {
                result = ParseCombinatedSelector(scanner, combinator, simpleSelector);
            }
            else
            {
                result = simpleSelector;
            }

            if (pseudo.Length > 0)
            {
                result = new PositionalSelector(result, ParsePosition(pseudo));
            }
            return result;
        }

        private static Selector ParseCombinatedSelector(Scanner scanner, string combinator, SimpleSelector simpleSelector)
        {
            Selector result;
            if (!scanner.NextSelector())
            {
                throw new SelectorParsingException("Illegal selector string");
            }
            if (combinator == ">")
            {
                result = new ChildrenSelector(ParseSelector(scanner), simpleSelector);
            }else
            {
                result = new DescendantSelector(ParseSelector(scanner), simpleSelector);
            }
            return result;
        }

        private static SimpleSelector ParseSimpleSelector(Scanner scanner)
        {
            SimpleSelector simpleSelector;
            ElementSelector elementSelector;
            if (scanner.Type.Length == 0)
            {
                elementSelector = new ElementSelector("*");
            }
            else
            {
                elementSelector = new ElementSelector(scanner.Type);
            }
            simpleSelector = elementSelector;
            if (scanner.Attribute.Length > 0)
            {
                simpleSelector = ParseAttributeSelector(elementSelector, scanner.Attribute);
            }
            return simpleSelector;
        }

        private static Position ParsePosition(string value)
        {
            Regex firstOfType = new Regex(":first-of-type");
            Regex lastOfType = new Regex(":last-of-type");
            Regex nthOfType = new Regex(@"nth-of-type\(([0-9]+)\)");

            Match match = firstOfType.Match(value);
            if (match.Success)
            {
                return new Position(0);
            }
            match = lastOfType.Match(value);
            if (match.Success)
            {
                return Position.Last;
            }
            match = nthOfType.Match(value);
            if (match.Success)
            {
                return new Position(int.Parse(match.Groups[1].Value));
            }
            throw new SelectorParsingException("Pseudo selector " + value + " is not supported");
        }

        private static AttributeSelector ParseAttributeSelector(SimpleSelector selector, string attributes)
        {
            AttributeSelector conditionalSelector = new AttributeSelector(selector, ParseAttribute(ref attributes));
            if (attributes.Length == 0)
            {
                return conditionalSelector;
            }
            return ParseAttributeSelector(conditionalSelector, attributes);
        }

        private static Attribute ParseAttribute(ref string value)
        {
            Regex attributeConditionPattern = new Regex(ATTRIBUTE_CONDITION);
            Regex idConditionPattern = new Regex(ID_CONDITION);
            Regex classConditionPattern = new Regex(CLASS_CONDITION);

            Match match = attributeConditionPattern.Match(value);
            if (match.Success)
            {
                value = value.Substring(match.Index + match.Length);
                return new Attribute(match.Groups[1].Value, match.Groups[2].Value);
            }
            match = idConditionPattern.Match(value);
            if (match.Success)
            {
                value = value.Substring(match.Index + match.Length);
                return new Attribute("id", match.Groups[2].Value.Length != 0
                                               ? match.Groups[2].Value
                                               : match.Groups[3].Value);
            }

            match = classConditionPattern.Match(value);
            if (match.Success)
            {
                value = value.Substring(match.Index + match.Length);
                return new Attribute("className", match.Groups[2].Value.Length != 0
                                                      ? match.Groups[2].Value
                                                      : match.Groups[3].Value);
            }
            throw new SelectorParsingException(
                "Attribute or class or id selector format error. The attribute being parsed is: " + value);
        }
    }
}