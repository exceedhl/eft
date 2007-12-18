using System;
using Eft.Elements;
using Eft.Tester;
using NUnit.Framework;

namespace FunctionalTest.Testers
{
    [TestFixture]
    public class ClickTest
    {
        private Tester i;

        [SetUp]
        public void setup()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            i = Tester.Run(fileName);
        }

        [TearDown]
        public void teardown()
        {
            i.Retire();
        }

        [Test]
        public void should_be_able_to_click()
        {
            i.Click("#openClickTestWindow");
            Wait.UntilChanged(delegate { return i.WindowCount; });
            i.AssertWindowCount(2);
        }

        [Test]
        public void should_be_able_to_handle_complex_clicking()
        {
            i.Click("#openClickTestWindow");
            i.SelectWindow("click test window");
            i.Click("#pressButton", "^{Left 1}");
            i.AssertText("#log", "Control Left Pressed");
        }


    }
}