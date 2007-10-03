using System;
using Eft;
using Eft.Elements;
using Eft.Exception;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class ClickTest
    {
        private Application app;
        private Window window;
        private Element logText;

        [SetUp]
        public void setup()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            app = Application.Run(fileName);
            Window mainWindow = app.FindTopWindow("Stub");
            mainWindow.FindFirst("#openClickTestWindow").Click();
            window = app.FindTopWindow("click test window");
            logText = window.FindFirst("#log");
        }

        [Test]
        public void left_click()
        {
            window.FindFirst("#pressButton").Click();
            Assert.AreEqual("Left Pressed", logText.Text);
            window.FindFirst("#releaseButton").Click();
            Assert.AreEqual("Left Released", logText.Text);
        }

        [Test]
        public void right_click()
        {
            window.FindFirst("#pressButton").RightClick();
            Assert.AreEqual("Right Pressed", logText.Text);
            window.FindFirst("#releaseButton").RightClick();
            Assert.AreEqual("Right Released", logText.Text);
        }

        [Test]
        [Ignore("it's failed because currently I can not get the clickable area.")]
        public void click_at_some_point_of_an_element()
        {
            Element button = window.FindFirst("#bigButton");
            button.Click(10, 10);
            Assert.AreEqual("10,10", logText.Text);
        }

        [Test]
        [ExpectedException(typeof (IllegalParameterException))]
        public void should_throw_exception_if_click_some_point_out_of_element()
        {
            Element button = window.FindFirst("#bigButton");
            button.Click(-10, -10);
        }

        [Test]
        public void double_click()
        {
            window.FindFirst("#pressButtonWithCount").DbClick();
            Assert.AreEqual("Left Pressed 2", logText.Text);
        }

        [Test]
        public void click_with_holding_keys()
        {
            window.FindFirst("#pressButton").CtrlClick();
            Assert.AreEqual("Control Left Pressed", logText.Text);
            window.FindFirst("#pressButton").ShiftClick();
            Assert.AreEqual("Shift Left Pressed", logText.Text);

            window.FindFirst("#releaseButton").CtrlClick();
            Assert.AreEqual("Control Left Released", logText.Text);
            window.FindFirst("#releaseButton").ShiftClick();
            Assert.AreEqual("Shift Left Released", logText.Text);
        }

        [TearDown]
        public void teardown()
        {
            app.Stop();
        }
    }
}