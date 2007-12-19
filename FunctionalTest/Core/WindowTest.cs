using System;
using System.Collections.Generic;
using System.Threading;
using Eft;
using Eft.Elements;
using Eft.Exception;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class WindowTest
    {
        private Application app;
        private Window stubWindow;

        [SetUp]
        public void setup()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            app = Application.Run(fileName);
            stubWindow = app.FindTopWindow("Stub*");
        }

        [Test]
        public void scaling_window()
        {
            stubWindow.Maximize();
            Assert.AreEqual(WindowState.Maximized, stubWindow.WindowState);
            stubWindow.Minimize();
            Assert.AreEqual(WindowState.Minimized, stubWindow.WindowState);
            stubWindow.Restore();
            Assert.AreEqual(WindowState.Normal, stubWindow.WindowState);
        }

        [Test]
        public void should_be_able_to_find_popup_window_but_can_not_change_its_window_state()
        {
            stubWindow.FindFirst("#openWindowTestWindow").Click();
            Window windowTestWindow = app.FindTopWindow("Window test window");
            windowTestWindow.FindFirst("#openPopupWindow").Click();
            Thread.Sleep(1000);
            List<Window> allWindows = app.FindTopWindows();
            Assert.AreEqual(3, allWindows.Count);
            try
            {
                allWindows[0].Maximize();
                Assert.Fail("exception expected");
            }
            catch (OperationNotSupportedException)
            {
                // pass
            }

            try
            {
                WindowState state = allWindows[0].WindowState;
                Assert.Fail("exception expected");
            }
            catch (OperationNotSupportedException)
            {
                // pass
            }
        }

        [Test]
        public void should_throw_exception_if_window_can_not_be_resized()
        {
            stubWindow.FindFirst("#openWindowTestWindow").Click();
            Window windowTestWindow = app.FindTopWindow("Window test window");
            windowTestWindow.FindFirst("#openUnresizableWindow").Click();
            Window unresizableWindow = app.FindTopWindow("Unresizable window");
            Assert.AreEqual(WindowState.Normal, unresizableWindow.WindowState);
            unresizableWindow.Restore();
            Assert.AreEqual(WindowState.Normal, unresizableWindow.WindowState);
            try
            {
                unresizableWindow.Maximize();
                Assert.Fail("exception expected");
            }
            catch (OperationNotSupportedException)
            {
                // pass
            }
            try
            {
                unresizableWindow.Minimize();
                Assert.Fail("exception expected");
            }
            catch (OperationNotSupportedException)
            {
                // pass
            }
        }

        [TearDown]
        public void teardown()
        {
            app.Stop();
        }
    }
}