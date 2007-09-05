using System.Windows.Automation;
using eft.Exception;
using eft.Locators;
using eft.Locators.Selectors;
using NUnit.Framework;
using Rhino.Mocks;

namespace eft.Locators
{
    [TestFixture]
    public class SelectorTranslatorTest
    {
        private SelectorTranslator translator;
        private MockRepository mocks;

        [SetUp]
        public void setup()
        {
            mocks = new MockRepository();
            mocks.CreateMock<IParser>();
            translator = new SelectorTranslator();
        }

        [Test]
        public void translate_universal_selector()
        {
            ElementSelector selector = new ElementSelector("*");
            Assert.AreEqual(Condition.TrueCondition, translator.Translate(selector));
        }

        [Test]
        public void translate_element_selector()
        {
            ElementSelector selector = new ElementSelector("Button");
            PropertyCondition condition = (PropertyCondition) translator.Translate(selector);
            Assert.AreEqual(AutomationElement.ControlTypeProperty, condition.Property);
            Assert.AreEqual(ControlType.Button.Id, condition.Value);
            Assert.AreEqual(ControlType.Window.Id,
                            ((PropertyCondition) translator.Translate(new ElementSelector("Window"))).Value);
        }

        [Test]
        [ExpectedException(typeof (SelectorTranslationException), "Unknown element type Unknown")]
        public void should_throw_exception_if_element_type_is_unrecoganizable()
        {
            ElementSelector selector = new ElementSelector("Unknown");
            translator.Translate(selector);
        }

        [Test]
        public void translate_conditional_selector_with_element_selector_and_attribute_condition()
        {
            AttributeSelector selector =
                new AttributeSelector(new ElementSelector("Button"), new Attribute("name", "someName"));
            AndCondition condition = (AndCondition) translator.Translate(selector);
            Assert.AreEqual(2, condition.GetConditions().GetLength(0));
            Assert.AreEqual(AutomationElement.ControlTypeProperty,
                            ((PropertyCondition) condition.GetConditions()[0]).Property);
            Assert.AreEqual(AutomationElement.NameProperty, ((PropertyCondition) condition.GetConditions()[1]).Property);
            Assert.AreEqual("someName", ((PropertyCondition) condition.GetConditions()[1]).Value);

            selector =
                new AttributeSelector(new ElementSelector("*"), new Attribute("name", "someName"));
            condition = (AndCondition) translator.Translate(selector);
            Assert.AreEqual(2, condition.GetConditions().GetLength(0));
            Assert.AreEqual(Condition.TrueCondition, condition.GetConditions()[0]);
        }

        [Test]
        public void translate_conditional_selector_with_conditional_selector_and_attribute_condition()
        {
            AttributeSelector conditionalSelector =
                new AttributeSelector(new ElementSelector("Button"), new Attribute("id", "some id"));
            ;
            AttributeSelector selector =
                new AttributeSelector(conditionalSelector, new Attribute("name", "someName"));
            AndCondition condition = (AndCondition) translator.Translate(selector);
            Assert.AreEqual(2, condition.GetConditions().GetLength(0));
            AndCondition firstCondition = (AndCondition) condition.GetConditions()[0];
            Assert.AreEqual(AutomationElement.NameProperty, ((PropertyCondition) condition.GetConditions()[1]).Property);
            Assert.AreEqual("someName", ((PropertyCondition) condition.GetConditions()[1]).Value);

            Assert.AreEqual(2, firstCondition.GetConditions().GetLength(0));
            Assert.AreEqual(AutomationElement.ControlTypeProperty,
                            ((PropertyCondition) firstCondition.GetConditions()[0]).Property);
            Assert.AreEqual(AutomationElement.AutomationIdProperty,
                            ((PropertyCondition) firstCondition.GetConditions()[1]).Property);
            Assert.AreEqual("some id", ((PropertyCondition) firstCondition.GetConditions()[1]).Value);
        }

        [Test]
        public void translate_attribute_condition()
        {
            PropertyCondition condition = translator.Translate(new Attribute("name", "some name"));
            Assert.AreEqual(AutomationElement.NameProperty, condition.Property);
            Assert.AreEqual("some name", condition.Value);

            condition = translator.Translate(new Attribute("id", "some id"));
            Assert.AreEqual(AutomationElement.AutomationIdProperty, condition.Property);
            Assert.AreEqual("some id", condition.Value);

            condition = translator.Translate(new Attribute("className", "class name"));
            Assert.AreEqual(AutomationElement.ClassNameProperty, condition.Property);
            Assert.AreEqual("class name", condition.Value);
        }

        [Test]
        [ExpectedException(typeof (SelectorTranslationException))]
        public void should_throw_exception_if_attribute_is_not_supported_by_ui_automation()
        {
            translator.Translate(new Attribute("unknown attribute", "irrelevant"));
        }
    }
}