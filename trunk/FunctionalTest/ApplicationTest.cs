using System;
using System.Diagnostics;
using Eft;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class ApplicationTest
    {
        [Test]
        public void start_and_stop_application()
        {
            Application app = new Application("wordpad");
            app.Start();
            Assert.AreEqual("Document - WordPad", app.FindTopWindows()[0].Name);
            app.Stop();
        }

        [Test]
        public void should_wait_for_app_window_to_be_ready_to_interact()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            Application app = new Application(fileName);
            app.Start();
            Assert.IsNotNull(app.FindTopWindow("Stub"));
            app.Stop();
        }

        [Test]
        public void start_cmd_process()
        {
            Application app = new Application("cmd");
            app.Start();
            Assert.AreEqual(@"C:\WINDOWS\system32\cmd.exe", app.FindTopWindows()[0].Name);
            app.Stop();
        }

        [Test]
        public void take_over_existing_application_with_window()
        {
            Process p = new Process();
            p.StartInfo.FileName = "notepad";
            p.Start();
            p.WaitForInputIdle(3000);

            Application app = Application.FromProcessName("notepad")[0];
            Assert.AreEqual("Untitled - Notepad", app.FindTopWindows()[0].Name);

            app.Stop();
        }
    }
}