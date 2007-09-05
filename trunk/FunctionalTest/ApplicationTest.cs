using System;
using System.Collections.Generic;
using eft;
using eft.Win32;
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
        public void find_window_by_title()
        {
            Application app = new Application("wordpad");
            app.Start();

            string title = "Document - WordPad";
            List<Window> windows = app.FindWindow(title);
            Assert.AreEqual(1, windows.Count);
            Assert.AreEqual(title, windows[0].Title);
            app.Stop();
        }

        [Test]
        public void start_cmd_process()
        {
            Application app = new Application("cmd");
            app.Start();
            try
            {
                app.FindFirst("*");
                Assert.Fail("exception expected");
            }
            catch (NullReferenceException)
            {
                // pass
            }
            app.Stop();
        }
    }
}