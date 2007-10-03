using System;
using Eft;
using Eft.Elements;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class FinderTest
    {
        [Test]
        public void find_all_and_first()
        {
            Application app = Application.Run("calc");
            Window window = app.FindTopWindows()[0];
            window.FindFirst("MenuItem[name='View']").Click();
            window.FindFirst("MenuItem[name='Scientific']").Click();
            Assert.AreEqual(7, window.Find("RadioButton").Count);
            Assert.AreEqual("Hex", window.FindFirst("RadioButton").Name);
            app.Stop();
        }

        [Test]
        public void find_top_windows()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            Application app = Application.Run(fileName);
            Window mainWindow = app.FindTopWindow("Stub");
            mainWindow.FindFirst("#openNewWindow").Click();
            mainWindow.FindFirst("#openNewWindow").Click();
            Assert.IsNotNull(app.FindTopWindow("new window 0"));
            Assert.IsNotNull(app.FindTopWindow("new window 1"));
            app.Stop();
        }

        // TODO: change this to use stub to test wait and find
        [Test]
        public void wait_and_find()
        {
            Application app = Application.Run("wordpad");
            Window window = app.FindTopWindows()[0];
            window.FindFirst("MenuItem[name='Help']").Click();
            window.FindFirst("MenuItem[name='Help'] MenuItem:last-of-type").Click();
            Element aboutWindow = window.FindFirst("Window[name='About WordPad']");
            Assert.AreEqual("About WordPad", aboutWindow.Name);
            aboutWindow.FindFirst("Button").Click();
            window.FindFirst("MenuItem[name='Help']").Click();
            window.FindFirst("MenuItem[name='Help'] MenuItem:last-of-type").Click();
            Element closeButton = window.Find("Window[name='About WordPad'] Button")[0];
            Assert.AreEqual("OK", closeButton.Name);
            closeButton.Click();

            app.Stop();
        }
    }
}