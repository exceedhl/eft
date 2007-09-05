using System.Collections.Generic;
using eft;
using eft.Exception;
using eft.Provider;
using NUnit.Framework;
using Rhino.Mocks;
using test.Util;

namespace test.Unit
{
    [TestFixture]
    public class ApplicationTest
    {
        private MockRepository mocks;
        private Application app;
        private IAutomationProvider mockAutomationProvider;

        [SetUp]
        public void setup()
        {
            mocks = new MockRepository();
            mockAutomationProvider = mocks.CreateMock<IAutomationProvider>();
            app = new Application("");
            PrivateAccessor.SetField(app, "automationProvider", mockAutomationProvider);
        }


        [Test]
        public void find_elements_by_selector_string()
        {
            List<Element> returnedElements = new List<Element>();
            Expect.Call(mockAutomationProvider.Find("selector string")).Return(returnedElements);

            mocks.ReplayAll();
            Assert.AreSame(returnedElements, app.Find("selector string"));
            mocks.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof (ElementSearchException), "No elements found")]
        public void find_first_should_return_null_if_nothing_found()
        {
            List<Element> returnedElements = new List<Element>();
            Expect.Call(mockAutomationProvider.Find("selector string")).Return(returnedElements);

            mocks.ReplayAll();
            app.FindFirst("selector string");
            mocks.VerifyAll();
        }

        [Test]
        public void find_first_should_return_first_found_element()
        {
            List<Element> returnedElements = new List<Element>();
            Element firstElement = new Element(null);
            returnedElements.Add(firstElement);
            returnedElements.Add(new Element(null));
            Expect.Call(mockAutomationProvider.Find("selector string")).Return(returnedElements);
            mocks.ReplayAll();
            Assert.AreEqual(firstElement, app.FindFirst("selector string"));
            mocks.VerifyAll();
        }
    }
}