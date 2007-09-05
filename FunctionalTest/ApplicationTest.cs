using System;
using System.Diagnostics;
using Eft;
using Eft.Win32;
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

            Assert.IsTrue(APIWrapper.FindWindow("WordPadClass", "Document - WordPad") > 0);
            app.Stop();
            Assert.IsTrue(APIWrapper.FindWindow("WordPadClass", "Document - WordPad") == 0);
        }

        [Test]
        public void start_cmd_process()
        {
            Application app = new Application("cmd");
            app.Start();
            try
            {
                app.MainWindow.FindFirst("*");
                Assert.Fail("exception expected");
            }
            catch (NullReferenceException)
            {
                // pass
            }
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
            Assert.AreEqual("Untitled - Notepad", app.MainWindow.Name);

            app.Stop();
        }
    }
}