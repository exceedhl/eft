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
                Application.Run(@"C:\works\macsrc\trunk\build-output\server\Macquarie.Connect.Server.exe");
            Application client = Application.Run(@"C:\Program Files\Microsoft Office\OFFICE11\outlook.exe");

            Element launcherWindow = client.FindTopWindow("Launcher Window");
            launcherWindow.FindFirst("#btnLaunchContactList").Click();

            Element contactListWindow = client.FindTopWindow("My contact list");
            contactListWindow.FindFirst("#imgOpenGlobalContactList").Click();
            contactListWindow.FindFirst("#tbKeywords:last-of-type").ClickAndType("rog");
            contactListWindow.FindFirst("#contactOrUserTabControl [name='Rogerio']").Click();
            contactListWindow.FindFirst("#btnAddSelectedContact").Click();
            Element contact = contactListWindow.FindFirst("#tcMyContactLists [name='Rogerio']");
            contact.Click();
            contact.RightClick();
            contactListWindow.FindFirst("#miCreateNote").Click();
            Window contactNoteWindow = client.FindTopWindow("*Rogerio Chequer");
            contactNoteWindow.FindFirst("#textContainer").Type("some note");
            contactNoteWindow.FindFirst("#userField").Type("Marc Mcneill");
            contactNoteWindow.FindFirst("#btnSave").Click();

            Wait.UntilChanged(delegate { return launcherWindow.FindFirst("#tbAlertsList").Text; });
            launcherWindow.FindFirst("#btnLaunchAlertList").Click();
            Window alertWindow = client.FindTopWindow("Alerts");
            alertWindow.FindFirst(".AlertDisplayControl:first-of-type").Click();


            client.Stop();
            server.Stop();
        }

        [Test]
        [Ignore]
        public void send_email_contact_note()
        {
            Application server =
                Application.Run(@"C:\works\macsrc\trunk\build-output\server\Macquarie.Connect.Server.exe");
            Application client = Application.Run(@"C:\Program Files\Microsoft Office\OFFICE11\outlook.exe");

            Element launcherWindow = client.FindTopWindow("Launcher Window");
            launcherWindow.FindFirst("#btnLaunchContactList").Click();

            Element contactListWindow = client.FindTopWindow("My contact list");
            contactListWindow.FindFirst("#imgOpenGlobalContactList").Click();
            contactListWindow.FindFirst("#tbKeywords:last-of-type").ClickAndType("rog");
            contactListWindow.FindFirst("#contactOrUserTabControl [name='Rogerio']").Click();
            contactListWindow.FindFirst("#btnAddSelectedContact").Click();
            Element contact = contactListWindow.FindFirst("#tcMyContactLists [name='Rogerio']");
            contact.Click();
            contact.RightClick();
            contactListWindow.FindFirst("#miSendEmail").Click();
            Window email = client.FindTopWindow("*Message*");
            email.FindFirst("[name='Subject:']").ClickAndType("this is a subject");
            email.FindFirst(".'Internet Explorer_Server'").ClickAndType("this is body");
            email.FindFirst(".MsoCommandBar[name='Standard']").Click(15, 15);
            email.FindFirst("[name='Macquarie Connect']").FindFirst(".Button[name='OK']").Click();

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
            Element editBox = window.FindFirst("Edit");
            Wait.Until(delegate { return editBox.Text == "3. "; });
            Assert.AreEqual("3. ", editBox.Text);

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