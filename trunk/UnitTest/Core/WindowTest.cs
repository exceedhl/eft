using Eft.Elements;
using Eft.Exception;
using Eft.Provider;
using NUnit.Framework;
using Rhino.Mocks;

namespace Eft
{
    [TestFixture]
    public class WindowTest
    {
        private IAutomationProvider mockProvider;
        private MockRepository mocks;

        [SetUp]
        public void setup()
        {
            mocks = new MockRepository();
            mockProvider = mocks.CreateMock<IAutomationProvider>();
        }

        [Test]
        [ExpectedException(typeof (ControlTypeConversionException))]
        public void should_throw_exception_if_element_is_not_window()
        {
            Expect.Call(mockProvider.IsWindow).Return(false);
            mocks.ReplayAll();

            new Window(mockProvider);

            mocks.VerifyAll();
        }

        [Test]
        public void should_return_window_if_control_is_a_window()
        {
            Expect.Call(mockProvider.IsWindow).Return(true);
            mocks.ReplayAll();
            Window window = new Window(mockProvider);
            Assert.IsInstanceOfType(typeof (Element), window);
            mocks.VerifyAll();
        }

        [Test]
        public void should_use_automation_provider_to_maximize_window()
        {
            Expect.Call(mockProvider.IsWindow).Return(true);
            mockProvider.ChangeWindowState(WindowState.Maximized);
            mocks.ReplayAll();
            new Window(mockProvider).Maximize();
            mocks.VerifyAll();
        }

        [Test]
        public void should_use_automation_provider_to_minimize_window()
        {
            Expect.Call(mockProvider.IsWindow).Return(true);
            mockProvider.ChangeWindowState(WindowState.Minimized);
            mocks.ReplayAll();
            new Window(mockProvider).Minimize();
            mocks.VerifyAll();
        }

        [Test]
        public void should_use_automation_provider_to_restore_window()
        {
            Expect.Call(mockProvider.IsWindow).Return(true);
            mockProvider.ChangeWindowState(WindowState.Normal);
            mocks.ReplayAll();
            new Window(mockProvider).Restore();
            mocks.VerifyAll();
        }

        [Test]
        public void window_state()
        {
            Expect.Call(mockProvider.IsWindow).Return(true);
            Expect.Call(mockProvider.WindowState).Return(WindowState.Maximized);

            mocks.ReplayAll();
            Assert.AreEqual(WindowState.Maximized, new Window(mockProvider).WindowState);
            mocks.VerifyAll();
        }
    }
}