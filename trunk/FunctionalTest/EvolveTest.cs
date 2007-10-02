using Eft;
using Eft.Elements;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    [Ignore]
    public class EvolveTest
    {
        [Test]
        public void spike()
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
    }
}