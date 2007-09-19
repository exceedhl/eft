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
            Process clientProcess = new Process();
            clientProcess.StartInfo.FileName = @"C:\Program Files\Microsoft Office\OFFICE11\outlook.exe";
            clientProcess.Start();
            Thread.Sleep(5000);
            Application client = new Application(clientProcess);


            Element launcherWindow = client.FindTopWindows()[0];
            launcherWindow.FindFirst("#btnLaunchContactList").Click();
            AutomationElement win = AutomationElement.FromHandle(clientProcess.MainWindowHandle);
            AutomationElementCollection winds =
                AutomationElement.RootElement.FindAll(TreeScope.Children,
                                                      new AndCondition(
                                                          new PropertyCondition(AutomationElement.ControlTypeProperty,
                                                                                ControlType.Window),
                                                          PropertyCondition.TrueCondition));
//            Element contactListWindow = launcherWindow.WaitAndFindFirst("#wndContactList");
//            contactListWindow.FindFirst("#imgOpenGlobalContactList").Click();
//            contactListWindow.FindFirst("#tbKeywords").ClickAndType("rog");
//            contactListWindow.FindFirst("[name='Rogerio']").Click();
//            contactListWindow.FindFirst("#btnAddSelectedContact").Click();


//            client.Stop();
//            server.Stop();
        }
    }
}