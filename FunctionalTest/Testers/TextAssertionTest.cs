using System;
using Eft.Tester;
using NUnit.Framework;

namespace FunctionalTest.Testers
{
    [TestFixture]
    public class TextAssertionTest
    {
        private Tester i;

        [SetUp]
        public void setup()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            i = Tester.Run(fileName);
            i.Click("#openTextTestWindow");
            i.SelectWindow("text test window");
        }

        [TearDown]
        public void teardown()
        {
            i.Retire();
        }

        [Test]
        public void should_be_able_to_assert_text_of_some_element_according_to_some_pattern()
        {
            i.AssertText("#textBlock", "exact:text block");
            i.AssertText("#textBlock", "glob:?ext block*");
            i.AssertText("#textBlock", "regex:.*t bloc.+");
            i.AssertText("#textBlock", "?ext block*");
        }

        [Test]
        [ExpectedException(typeof (AssertionException))]
        public void should_throw_exception_if_pattern_not_match_actual_text()
        {
            i.AssertText("#textBlock", "exact:wrong text");
        }

        [Test]
        public void type_text()
        {
            i.AssertText("#textBox", "text box");
            i.ClearText("#textBox");
            i.Type("#textBox", "new value");
            i.AssertText("#textBox", "new value");
            i.ClearText("#textBox");
            i.ClickAndType("#textBox", "text");
            i.AssertText("#textBox", "text");
        }

    }
}