using Eft;
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
            Assert.AreEqual(7, app.MainWindow.Find("RadioButton").Count);
            Assert.AreEqual("Hex", app.MainWindow.FindFirst("RadioButton").Name);
            app.Stop();
        }

        [Test]
        public void wait_and_find()
        {
            Application app = new Application("wordpad");
            app.Start();
            app.MainWindow.FindFirst("MenuItem[name='Help']").Click();
            app.MainWindow.FindFirst("MenuItem[name='Help'] MenuItem:last-of-type").Click();

            Element aboutWindow = app.MainWindow.WaitAndFindFirst("Window[name='About WordPad']");
            Assert.AreEqual("About WordPad", aboutWindow.Name);
            aboutWindow.FindFirst("Button").Click();

            app.MainWindow.FindFirst("MenuItem[name='Help']").Click();
            app.MainWindow.FindFirst("MenuItem[name='Help'] MenuItem:last-of-type").Click();
            Element closeButton = app.MainWindow.WaitAndFind("Window[name='About WordPad'] Button")[0];
            Assert.AreEqual("OK", closeButton.Name);
            closeButton.Click();

            app.Stop();
        }
    }
}