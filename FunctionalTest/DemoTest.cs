using System.Threading;
using Eft;
using Eft.Elements;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class DemoTest
    {
        [Test]
        [Ignore]
        public void evolve_test()
        {
            Application server =
                Application.Run(@"C:\works\macsrc\trunk\build-output\server\Macquarie.Evolve.Server.exe");
            Application client = Application.Run(@"C:\Program Files\Microsoft Office\OFFICE11\outlook.exe");

            Element launcherWindow = client.FindTopWindow("Launcher Window");
            launcherWindow.FindFirst("#btnLaunchContactList").Click();

            Element contactListWindow = client.FindTopWindow("My contact list");
            contactListWindow.FindFirst("#imgOpenGlobalContactList").Click();
            contactListWindow.FindFirst("#tbKeywords:last-of-type").ClickAndType("rog");
            contactListWindow.FindFirst("#contactsUsersTabControl [name='Rogerio']").Click();
            contactListWindow.FindFirst("#btnAddSelectedContact").Click();

            client.Stop();
            server.Stop();
        }

        [Test]
        public void click_calculator()
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
        public void right_click_in_wordpad()
        {
            Application app = Application.Run("wordpad");

            Element window = app.FindTopWindows()[0];
            window.FindFirst(".RICHEDIT50W").RightClick();
            // the context menu of wordpad is actually belongs to Desktop
            // Assert.AreEqual("Font...", window.FindFirst("MenuItem#'Item 57696'").Name);

            app.Stop();
        }
    }
}