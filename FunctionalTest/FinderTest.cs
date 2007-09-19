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
            Application app = new Application("calc");
            app.Start();
            Window window = app.FindTopWindows()[0];
            Assert.AreEqual(7, window.Find("RadioButton").Count);
            Assert.AreEqual("Hex", window.FindFirst("RadioButton").Name);
            app.Stop();
        }

        [Test]
        public void wait_and_find()
        {
            Application app = new Application("wordpad");
            app.Start();
            Window window = app.FindTopWindows()[0];
            window.FindFirst("MenuItem[name='Help']").Click();
            window.FindFirst("MenuItem[name='Help'] MenuItem:last-of-type").Click();

            Element aboutWindow = window.WaitAndFindFirst("Window[name='About WordPad']");
            Assert.AreEqual("About WordPad", aboutWindow.Name);
            aboutWindow.FindFirst("Button").Click();

            window.FindFirst("MenuItem[name='Help']").Click();
            window.FindFirst("MenuItem[name='Help'] MenuItem:last-of-type").Click();
            Element closeButton = window.WaitAndFind("Window[name='About WordPad'] Button")[0];
            Assert.AreEqual("OK", closeButton.Name);
            closeButton.Click();

            app.Stop();
        }

        [Test]
        public void find_top_windows()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            Application app = new Application(fileName);
            app.Start();
            Window mainWindow = app.FindTopWindow("Stub");
            mainWindow.FindFirst("#openNewWindow").Click();
            mainWindow.FindFirst("#openNewWindow").Click();
            Assert.IsNotNull(app.FindTopWindow("new window 0"));
            Assert.IsNotNull(app.FindTopWindow("new window 1"));
            app.Stop();
        }
    }
}