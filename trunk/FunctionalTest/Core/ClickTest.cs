using System;
using System.Windows.Input;
using Eft;
using Eft.Elements;
using Eft.Exception;
using NUnit.Framework;

namespace FunctionalTest.Core
{
    [TestFixture]
    public class ClickTest
    {
        private Application app;
        private Window window;
        private Element logText;
        private Element pressButton;
        private Element releaseButton;
        private Element pressButtonWithCount;

        [SetUp]
        public void setup()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            app = Application.Run(fileName);
            Window mainWindow = app.FindTopWindow("Stub*");
            mainWindow.FindFirst("#openClickTestWindow").Click();
            window = app.FindTopWindow("click test window");
            logText = window.FindFirst("#log");
            pressButton = window.FindFirst("#pressButton");
            releaseButton = window.FindFirst("#releaseButton");
            pressButtonWithCount = window.FindFirst("#pressButtonWithCount");
        }

        [Test]
        public void left_click()
        {
            pressButton.Click();
            Assert.AreEqual("Left Pressed", logText.Text);
            window.FindFirst("#releaseButton").Click();
            Assert.AreEqual("Left Released", logText.Text);
        }

        [Test]
        public void right_click()
        {
            pressButton.RightClick();
            Assert.AreEqual("Right Pressed", logText.Text);
            releaseButton.RightClick();
            Assert.AreEqual("Right Released", logText.Text);
        }

        //todo: fix the problem: not all points in bounding rectangle are clickable
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
            pressButtonWithCount.DbClick();
            Assert.AreEqual("Left Pressed 2", logText.Text);
        }

        [Test]
        public void click_with_holding_keys()
        {
            pressButton.CtrlClick();
            Assert.AreEqual("Control Left Pressed", logText.Text);
            pressButton.ShiftClick();
            Assert.AreEqual("Shift Left Pressed", logText.Text);

            releaseButton.CtrlClick();
            Assert.AreEqual("Control Left Released", logText.Text);
            releaseButton.ShiftClick();
            Assert.AreEqual("Shift Left Released", logText.Text);
        }

        [Test]
        public void general_click_once()
        {
            pressButton.Click(MouseButton.Left, ModifierKeys.None, 1);
            Assert.AreEqual("Left Pressed", logText.Text);
            pressButton.Click(MouseButton.Right, ModifierKeys.None, 1);
            Assert.AreEqual("Right Pressed", logText.Text);
            pressButton.Click(MouseButton.Middle, ModifierKeys.None, 1);
            Assert.AreEqual("Middle Pressed", logText.Text);

            releaseButton.Click(MouseButton.Left, ModifierKeys.None, 1);
            Assert.AreEqual("Left Released", logText.Text);
            releaseButton.Click(MouseButton.Right, ModifierKeys.None, 1);
            Assert.AreEqual("Right Released", logText.Text);
            releaseButton.Click(MouseButton.Middle, ModifierKeys.None, 1);
            Assert.AreEqual("Middle Released", logText.Text);

            pressButton.Click(MouseButton.Left, ModifierKeys.Control, 1);
            Assert.AreEqual("Control Left Pressed", logText.Text);
            pressButton.Click(MouseButton.Right, ModifierKeys.Alt, 1);
            Assert.AreEqual("Alt Right Pressed", logText.Text);
            pressButton.Click(MouseButton.Middle, ModifierKeys.Shift, 1);
            Assert.AreEqual("Shift Middle Pressed", logText.Text);

            releaseButton.Click(MouseButton.Left, ModifierKeys.Control, 1);
            Assert.AreEqual("Control Left Released", logText.Text);
            releaseButton.Click(MouseButton.Right, ModifierKeys.Alt, 1);
            Assert.AreEqual("Alt Right Released", logText.Text);
            releaseButton.Click(MouseButton.Middle, ModifierKeys.Shift, 1);
            Assert.AreEqual("Shift Middle Released", logText.Text);
        }

        [Test]
        public void general_click_operation_with_times()
        {
            pressButtonWithCount.Click(MouseButton.Left, ModifierKeys.Control, 1);
            Assert.AreEqual("Control Left Pressed 1", logText.Text);
            logText.ClearText();
            pressButtonWithCount.Click(MouseButton.Right, ModifierKeys.Alt, 2);
            Assert.AreEqual("Alt Right Pressed 2", logText.Text);
            logText.ClearText();
            pressButtonWithCount.Click(MouseButton.Middle, ModifierKeys.Shift, 3);
            Assert.AreEqual("Shift Middle Pressed 3", logText.Text);
            logText.ClickAndType("");
            pressButtonWithCount.Click(MouseButton.Middle, ModifierKeys.Shift, 4);
            Assert.AreEqual("Shift Middle Pressed 4", logText.Text);
        }

        [Test]
        [ExpectedException(typeof (IllegalParameterException))]
        public void should_throw_exception_if_click_times_are_minus()
        {
            pressButtonWithCount.Click(MouseButton.Left, ModifierKeys.Control, -1);
        }

        [TearDown]
        public void teardown()
        {
            app.Stop();
        }
    }
}