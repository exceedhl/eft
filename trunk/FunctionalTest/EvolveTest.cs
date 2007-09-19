using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using Eft;
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
                new Application(@"C:\works\macsrc\trunk\build-output\server\Macquarie.Evolve.Server.exe");
            server.Start();
            Application client = new Application(@"C:\Program Files\Microsoft Office\OFFICE11\outlook.exe");
            client.Start();

            Element launcherWindow = client.FindTopWindow("Launcher Window");
            launcherWindow.FindFirst("#btnLaunchContactList").Click();
            
            Element contactListWindow = client.FindTopWindow("My contact list");
            contactListWindow.FindFirst("#imgOpenGlobalContactList").Click();
            contactListWindow.FindFirst("#tbKeywords").ClickAndType("rog");
            contactListWindow.FindFirst("[name='Rogerio']").Click();
            contactListWindow.FindFirst("#btnAddSelectedContact").Click();

            client.Stop();
            server.Stop();
        }
    }
}