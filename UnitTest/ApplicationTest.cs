using System.Collections.Generic;
using Eft.Exception;
using Eft.Provider;
using Eft.Util;
using NUnit.Framework;
using Rhino.Mocks;

namespace Eft
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
        public void wait_and_find_should_return_once_found_element()
        {
            List<Element> elements = new List<Element>();
            elements.Add(new Element(null));
            Expect.Call(mockAutomationProvider.Find("selector")).Return(elements);
            mocks.ReplayAll();
            Assert.AreSame(elements, app.WaitAndFind("selector"));
            mocks.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof (ElementSearchException))]
        public void wait_and_find_should_throw_exception_if_wait_time_out()
        {
            Expect.Call(mockAutomationProvider.Find("selector")).Return(new List<Element>()).Repeat.Any();
            mocks.ReplayAll();
            app.WaitAndFind("selector", 1);
            mocks.VerifyAll();
        }
    }
}