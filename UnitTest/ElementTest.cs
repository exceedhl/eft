using Eft;
using Eft.Provider;
using NUnit.Framework;
using Rhino.Mocks;

namespace Eft
{
    [TestFixture]
    public class ElementTest
    {
        private IAutomationProvider mockAutoProvider;
        private MockRepository mocks;
        private Element element;

        [SetUp]
        public void setup()
        {
            mocks = new MockRepository();
            mockAutoProvider = mocks.CreateMock<IAutomationProvider>();
            element = new Element(mockAutoProvider);
        }

        [Test]
        public void name_property()
        {
            Expect.Call(mockAutoProvider.Name).Return("name");
            mocks.ReplayAll();
            Assert.AreEqual("name", element.Name);
            mocks.VerifyAll();
        }
    }
}