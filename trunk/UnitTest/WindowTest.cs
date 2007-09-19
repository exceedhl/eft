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

        [Test]
        [ExpectedException(typeof (ControlTypeConversionException))]
        public void should_throw_exception_if_element_is_not_window()
        {
            MockRepository mocks = new MockRepository();
            mockProvider = mocks.CreateMock<IAutomationProvider>();

            Expect.Call(mockProvider.IsWindow).Return(false);
            mocks.ReplayAll();

            new Window(mockProvider);

            mocks.VerifyAll();
        }

        [Test]
        public void should_return_window_if_control_is_a_window()
        {
            MockRepository mocks = new MockRepository();
            mockProvider = mocks.CreateMock<IAutomationProvider>();

            Expect.Call(mockProvider.IsWindow).Return(true);
            mocks.ReplayAll();
            Window window = new Window(mockProvider);
            Assert.IsInstanceOfType(typeof (Element), window);
            mocks.VerifyAll();
        }
    }
}