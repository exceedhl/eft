using System.Collections.Generic;
using Eft.Exception;
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

        [Test]
        public void find_elements_by_selector_string()
        {
            List<Element> returnedElements = new List<Element>();
            Expect.Call(mockAutoProvider.Find("selector string")).Return(returnedElements);

            mocks.ReplayAll();
            Assert.AreSame(returnedElements, element.Find("selector string"));
            mocks.VerifyAll();
        }

        [Test]
        public void find_first_should_return_first_found_element()
        {
            List<Element> returnedElements = new List<Element>();
            Element firstElement = new Element(null);
            returnedElements.Add(firstElement);
            returnedElements.Add(new Element(null));
            Expect.Call(mockAutoProvider.Find("selector string")).Return(returnedElements);
            mocks.ReplayAll();
            Assert.AreEqual(firstElement, element.FindFirst("selector string"));
            mocks.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof (ElementSearchException), "No elements found")]
        public void find_first_should_return_null_if_nothing_found()
        {
            List<Element> returnedElements = new List<Element>();
            Expect.Call(mockAutoProvider.Find("selector string")).Return(returnedElements);

            mocks.ReplayAll();
            element.FindFirst("selector string");
            mocks.VerifyAll();
        }

        [Test]
        public void wait_and_find_should_return_once_found_element()
        {
            List<Element> elements = new List<Element>();
            elements.Add(new Element(null));
            Expect.Call(mockAutoProvider.Find("selector")).Return(elements);
            mocks.ReplayAll();
            Assert.AreSame(elements, element.WaitAndFind("selector"));
            mocks.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof (ElementSearchException))]
        public void wait_and_find_should_throw_exception_if_wait_time_out()
        {
            Expect.Call(mockAutoProvider.Find("selector")).Return(new List<Element>()).Repeat.Any();
            mocks.ReplayAll();
            element.WaitAndFind("selector", 1);
            mocks.VerifyAll();
        }
    }
}