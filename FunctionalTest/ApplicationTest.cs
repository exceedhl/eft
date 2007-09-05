using System;
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