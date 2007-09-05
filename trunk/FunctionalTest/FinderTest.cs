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
            Assert.AreEqual(7, app.Find("RadioButton").Count);
            Assert.AreEqual("Hex", app.FindFirst("RadioButton").Name);
            app.Stop();
        }

        [Test]
        public void wait_and_find()
        {
            Application app = new Application("wordpad");
            app.Start();
            app.FindFirst("MenuItem[name='Help']").Click();
            app.FindFirst("MenuItem[name='Help'] MenuItem:last-of-type").Click();

            Element aboutWindow = app.WaitAndFindFirst("Window[name='About WordPad']");
            Assert.AreEqual("About WordPad", aboutWindow.Name);
            aboutWindow.FindFirst("Button").Click();

            app.FindFirst("MenuItem[name='Help']").Click();
            app.FindFirst("MenuItem[name='Help'] MenuItem:last-of-type").Click();
            aboutWindow = app.WaitAndFind("Window[name='About WordPad']")[0];
            Assert.AreEqual("About WordPad", aboutWindow.Name);
            aboutWindow.FindFirst("Button").Click();

            app.Stop();
        }
    }
}