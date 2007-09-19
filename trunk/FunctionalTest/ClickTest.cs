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
            Application app = new Application("calc");
            app.Start();

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
            Application app = new Application("wordpad");
            app.Start();

            Element window = app.FindTopWindows()[0];
            window.FindFirst(".RICHEDIT50W").RightClick();
            // the context menu of wordpad is actually belongs to Desktop
            // Assert.AreEqual("Font...", window.FindFirst("MenuItem#'Item 57696'").Name);

            app.Stop();
        }
    }
}