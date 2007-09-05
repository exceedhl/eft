using eft.Exception;
using eft.Locators.Selectors;
using NUnit.Framework;

namespace eft.Locators.Selectors
{
    [TestFixture]
    public class ParserTest
    {
        private Parser parser;

        [SetUp]
        public void setup()
        {
            parser = new Parser();
        }

        [Test]
        public void should_trim_space()
        {
            Selector selector = parser.Parse("  type  ");
            Assert.IsInstanceOfType(typeof (ElementSelector), selector);
            Assert.AreEqual("type", ((ElementSelector) selector).LocalName);
        }

        [Test]
        public void parse_type_selector()
        {
            Selector selector = parser.Parse("*");
            Assert.IsInstanceOfType(typeof (ElementSelector), selector);
            Assert.AreEqual("*", ((ElementSelector) selector).LocalName);

            selector = parser.Parse("Button");
            Assert.IsInstanceOfType(typeof (ElementSelector), selector);
            Assert.AreEqual("Button", ((ElementSelector) selector).LocalName);
        }

        [Test]
        public void parse_attribute_selector()
        {
            AttributeSelector selector = (AttributeSelector) parser.Parse("[attribute='value']");
            Assert.AreEqual("*", ((ElementSelector) selector.SimpleSelector).LocalName);
            Assert.AreEqual("attribute", selector.Attribute.LocalName);
            Assert.AreEqual("value", selector.Attribute.Value);

            string value = "some\"\'/?<>,. -_)(*&^%$#@!~ \t value";
            selector = (AttributeSelector) parser.Parse("[attribute='" + value + "']");
            Assert.AreEqual(value, selector.Attribute.Value);
        }

        [Test]
        public void parse_complex_attribute_selector()
        {
            AttributeSelector selector = (AttributeSelector) parser.Parse("Type[attribute='value']");
            Assert.AreEqual("Type", ((ElementSelector) selector.SimpleSelector).LocalName);
            Assert.AreEqual("attribute", selector.Attribute.LocalName);
            Assert.AreEqual("value", selector.Attribute.Value);

            string andSelector = "Type[name='some name']#someid";
            selector = (AttributeSelector) parser.Parse(andSelector);
            Assert.AreEqual("id", selector.Attribute.LocalName);
            Assert.AreEqual("someid", selector.Attribute.Value);

            AttributeSelector attributeSelector = (AttributeSelector) selector.SimpleSelector;
            Assert.AreEqual("name", attributeSelector.Attribute.LocalName);
            Assert.AreEqual("some name", attributeSelector.Attribute.Value);

            ElementSelector elementSelector = (ElementSelector) attributeSelector.SimpleSelector;
            Assert.AreEqual("Type", elementSelector.LocalName);
        }

        [Test]
        public void parse_id_selector()
        {
            AttributeSelector selector = (AttributeSelector) parser.Parse("#idvalue");
            Assert.AreEqual("*", ((ElementSelector) selector.SimpleSelector).LocalName);
            Assert.AreEqual("id", selector.Attribute.LocalName);
            Assert.AreEqual("idvalue", selector.Attribute.Value);

            selector = (AttributeSelector) parser.Parse("#'some id'");
            Assert.AreEqual("some id", selector.Attribute.Value);
        }

        [Test]
        [ExpectedException(typeof (SelectorParsingException))]
        public void id_value_can_not_contain_single_quote()
        {
            parser.Parse("#'some \'id\''");
        }

        [Test]
        [ExpectedException(typeof (SelectorParsingException))]
        public void class_value_can_not_contain_single_quote()
        {
            parser.Parse("#'some \'class\''");
        }

        [Test]
        [ExpectedException(typeof (SelectorParsingException))]
        public void attribute_value_can_not_contain_brackets()
        {
            parser.Parse("[name='[some']");
        }

        [Test]
        public void attribute_value_can_contain_single_quote()
        {
            AttributeSelector selector = (AttributeSelector) parser.Parse("[name='some 'value'']");
            Assert.AreEqual("some 'value'", selector.Attribute.Value);
        }


        [Test]
        public void parse_class_selector()
        {
            AttributeSelector selector = (AttributeSelector) parser.Parse(".classNameValue");
            Assert.AreEqual("*", ((ElementSelector) selector.SimpleSelector).LocalName);
            Assert.AreEqual("className", selector.Attribute.LocalName);
            Assert.AreEqual("classNameValue", selector.Attribute.Value);

            selector = (AttributeSelector) parser.Parse(".'some class'");
            Assert.AreEqual("some class", selector.Attribute.Value);
        }

        [Test]
        [ExpectedException(typeof (SelectorParsingException),
            "type[error] is not a legal selector string")]
        public void should_throw_exception_if_attribute_illegal()
        {
            parser.Parse("type[error]");
        }


        [Test]
        public void parse_positional_selector()
        {
            PositionalSelector selector = (PositionalSelector) parser.Parse("Type:first-of-type");
            Assert.AreEqual("Type", ((ElementSelector) selector.Selector).LocalName);
            Assert.AreEqual(0, selector.Position.Offset);

            selector = (PositionalSelector) parser.Parse("Type:last-of-type");
            Assert.AreEqual(Position.Last, selector.Position);

            selector = (PositionalSelector) parser.Parse("Type:nth-of-type(3)");
            Assert.AreEqual(3, selector.Position.Offset);
        }

        [Test]
        public void parse_children_selector()
        {
            ChildrenSelector selector = (ChildrenSelector) parser.Parse("Button > Item");
            Assert.AreEqual("Button", ((ElementSelector) selector.AncestorSelector).LocalName);
            Assert.AreEqual("Item", ((ElementSelector) selector.SimpleSelector).LocalName);

            selector = (ChildrenSelector) parser.Parse("Button > Item > Some");
            ElementSelector first = (ElementSelector) (((ChildrenSelector) selector.AncestorSelector).AncestorSelector);
            ElementSelector second = (ElementSelector) ((ChildrenSelector) selector.AncestorSelector).SimpleSelector;
            ElementSelector third = (ElementSelector) selector.SimpleSelector;
            Assert.AreEqual("Button", first.LocalName);
            Assert.AreEqual("Item", second.LocalName);
            Assert.AreEqual("Some", third.LocalName);

            PositionalSelector positionalSelector = (PositionalSelector) parser.Parse("A > B:first-of-type");
            Assert.AreEqual(0, positionalSelector.Position.Offset);
            Assert.AreEqual("B",
                            ((ElementSelector) ((ChildrenSelector) positionalSelector.Selector).SimpleSelector).
                                LocalName);
            Assert.AreEqual("A",
                            ((ElementSelector) ((ChildrenSelector) positionalSelector.Selector).AncestorSelector).
                                LocalName);

            ChildrenSelector childrenSelector = (ChildrenSelector) parser.Parse("A#'some id' > B#'another id'");
            Assert.AreEqual("id", ((AttributeSelector) childrenSelector.SimpleSelector).Attribute.LocalName);
            Assert.AreEqual("another id", ((AttributeSelector) childrenSelector.SimpleSelector).Attribute.Value);
        }

        [Test]
        public void parse_descendant_selector()
        {
            DescendantSelector selector = (DescendantSelector)parser.Parse("Button Item");
            Assert.AreEqual("Button", ((ElementSelector)selector.AncestorSelector).LocalName);
            Assert.AreEqual("Item", ((ElementSelector)selector.SimpleSelector).LocalName);

            selector = (DescendantSelector)parser.Parse("Button Item Some");
            ElementSelector first = (ElementSelector)(((DescendantSelector)selector.AncestorSelector).AncestorSelector);
            ElementSelector second = (ElementSelector)((DescendantSelector)selector.AncestorSelector).SimpleSelector;
            ElementSelector third = (ElementSelector)selector.SimpleSelector;
            Assert.AreEqual("Button", first.LocalName);
            Assert.AreEqual("Item", second.LocalName);
            Assert.AreEqual("Some", third.LocalName);

            selector = (DescendantSelector)parser.Parse("Button > Item Some");
            first = (ElementSelector)(((ChildrenSelector)selector.AncestorSelector).AncestorSelector);
            second = (ElementSelector)((ChildrenSelector)selector.AncestorSelector).SimpleSelector;
            third = (ElementSelector)selector.SimpleSelector;
            Assert.AreEqual("Button", first.LocalName);
            Assert.AreEqual("Item", second.LocalName);
            Assert.AreEqual("Some", third.LocalName);
        }

        [Test]
        [ExpectedException(typeof (SelectorParsingException), "Selector can not be empty")]
        public void should_throw_exception_if_passing_empty_string()
        {
            parser.Parse("  ");
        }

        [Test]
        public void should_throw_exception_if_type_illegal()
        {
            try
            {
                parser.Parse("34");
                Assert.Fail("exception expected");
            }
            catch (SelectorParsingException e)
            {
                Assert.IsTrue(e.Message.Contains("34 is not a legal selector string"));
            }

            try
            {
                parser.Parse("^*#$");
                Assert.Fail("exception expected");
            }
            catch (SelectorParsingException e)
            {
                Assert.IsTrue(e.Message.Contains("is not a legal selector string"));
            }
            try
            {
                parser.Parse("abc#");
                Assert.Fail("exception expected");
            }
            catch (SelectorParsingException e)
            {
                Assert.IsTrue(e.Message.Contains("is not a legal selector string"));
            }
            try
            {
                parser.Parse("34abcd");
                Assert.Fail("exception expected");
            }
            catch (SelectorParsingException e)
            {
                Assert.IsTrue(e.Message.Contains("34abcd is not a legal selector string"));
            }
            try
            {
                parser.Parse("Type:not_:*&+-supported");
                Assert.Fail("exception expected");
            }
            catch (SelectorParsingException e)
            {
                Assert.IsTrue(e.Message.Contains("Type:not_:*&+-supported is not a legal selector string"));
            }
            try
            {
                parser.Parse(":pseudo");
                Assert.Fail("exception expected");
            }
            catch (SelectorParsingException e)
            {
                Assert.IsTrue(e.Message.Contains(":pseudo is not a legal selector string"));
            }
            try
            {
                parser.Parse("ToolBar[name='Formatting' > ComboBox > Edit");
                Assert.Fail("exception expected");
            }
            catch (SelectorParsingException e)
            {
                Assert.IsTrue(e.Message.Contains("Illegal selector string"));
            }
        }
    }
}