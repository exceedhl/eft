using System;
using Eft;
using Eft.Elements;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class WaitForConditionTest
    {
        private Application app;
        private Window window;
        private Element log;

        [SetUp]
        public void setup()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            app = Application.Run(fileName);
            Window mainWindow = app.FindTopWindow("Stub*");
            mainWindow.FindFirst("#openWaitForConditionTestWindow").Click();
            window = app.FindTopWindow("Wait for condition test window");
            log = window.FindFirst("#log");
        }

        [TearDown]
        public void teardown()
        {
            app.Stop();
        }

        [Test]
        public void wait_for_condition()
        {
            window.FindFirst("#trigger").Click();
            Wait.Until(delegate { return log.Text == "2"; });
            Assert.AreEqual("2", log.Text);
        }

        [Test]
        public void wait_for_something_change()
        {
            window.FindFirst("#trigger").Click();
            Wait.UntilChanged(delegate { return log.Text; });
            Assert.AreEqual("0", log.Text);
        }
    }
}