using System.Threading;
using Eft;
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

            Element window = app.MainWindow;
            window.FindFirst("Button[name='1']").Click();
            window.FindFirst("*[name='+']").Click();
            window.FindFirst("[name='2']").Click();
            window.FindFirst("[name='=']").Click();
            Thread.Sleep(500);
            Assert.AreEqual("3. ", window.FindFirst("Edit").Text);

            app.Stop();
        }
    }
}