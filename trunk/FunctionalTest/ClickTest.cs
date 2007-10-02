using System;
using System.Threading;
using Eft;
using Eft.Elements;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class ClickTest
    {
        [Test]
        public void left_click()
        {
            Application app = Application.Run("calc");

            Element window = app.FindTopWindows()[0];
            window.FindFirst("Button[name='1']").Click();
            window.FindFirst("*[name='+']").Click();
            window.FindFirst("[name='2']").Click();
            window.FindFirst("[name='=']").Click();
            Thread.Sleep(500);
            Assert.AreEqual("3. ", window.FindFirst("Edit").Text);

            app.Stop();
        }

        [Test]
        public void right_click()
        {
            Application app = Application.Run("wordpad");

            Element window = app.FindTopWindows()[0];
            window.FindFirst(".RICHEDIT50W").RightClick();
            // the context menu of wordpad is actually belongs to Desktop
            // Assert.AreEqual("Font...", window.FindFirst("MenuItem#'Item 57696'").Name);

            app.Stop();
        }

        private Application app;
        private Window window;

        [SetUp]
        public void setup()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            app = Application.Run(fileName);
            Window mainWindow = app.FindTopWindow("Stub");
            mainWindow.FindFirst("#openClickTestWindow").Click();
            window = app.FindTopWindow("click test window");
        }

        [Test]
        [Ignore("it's failed because currently I can not get the clickable area.")]
        public void click_at_some_point_of_an_element()
        {
            Element button = window.FindFirst("#bigButton");
            Element logText = window.FindFirst("#log");
            button.Click(10, 10);
            Assert.AreEqual("10,10", logText.Text);
        }

        [Test]
        public void double_click()
        {
            window.FindFirst("#doubleClickButton").DbClick();
            Element logText = window.FindFirst("#log");
            Assert.AreEqual("button double clicked", logText.Text);
        }

        [Test]
        [Ignore("not implemented")]
        public void click_with_holding_keys()
        {
        }

        [TearDown]
        public void teardown()
        {
            app.Stop();
        }
    }
}