using System;
using Eft;
using Eft.Elements;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class WindowTest
    {
        private Application app;

        [SetUp]
        public void setup()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            app = Application.Run(fileName);
        }

        [Test]
        public void scaling_window()
        {
            Window window = app.FindTopWindow("Stub");
            window.Maximize();
            Assert.AreEqual(WindowState.Maximized, window.WindowState);
            window.Minimize();
            Assert.AreEqual(WindowState.Minimized, window.WindowState);
            window.Restore();
            Assert.AreEqual(WindowState.Normal, window.WindowState);
        }

        [TearDown]
        public void teardown()
        {
            app.Stop();
        }
    }
}