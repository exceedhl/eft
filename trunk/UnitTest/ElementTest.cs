using System.Collections.Generic;
using System.Windows;
using Eft.Elements;
using Eft.Exception;
using Eft.Provider;
using NUnit.Framework;
using Rhino.Mocks;

namespace Eft
{
    [TestFixture]
    public class ElementTest
    {
        private IAutomationProvider mockProvider;
        private MockRepository mocks;
        private Element element;

        [SetUp]
        public void setup()
        {
            mocks = new MockRepository();
            mockProvider = mocks.CreateMock<IAutomationProvider>();
            element = new Element(mockProvider);
        }

        [Test]
        public void name_property()
        {
            Expect.Call(mockProvider.Name).Return("name");
            mocks.ReplayAll();
            Assert.AreEqual("name", element.Name);
            mocks.VerifyAll();
        }

        [Test]
        public void wait_and_find_should_return_once_found_element()
        {
            List<Element> elements = new List<Element>();
            elements.Add(new Element(null));
            Expect.Call(mockProvider.Find("selector")).Return(elements);
            mocks.ReplayAll();
            Assert.AreSame(elements, element.Find("selector"));
            mocks.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof (ElementSearchException), "1 seconds elapsed, no elements found: selector")]
        public void wait_and_find_should_throw_exception_if_wait_time_out()
        {
            Expect.Call(mockProvider.Find("selector")).Return(new List<Element>()).Repeat.Any();
            mocks.ReplayAll();
            element.Find("selector", 1);
            mocks.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof (PropertyNotSupportedException), "message")]
        public void should_throw_exception_if_provider_does_not_support_getting_bounding_rectangle()
        {
            Expect.Call(mockProvider.BoundingRectangle).Throw(new PropertyNotSupportedException("message"));
            mocks.ReplayAll();
            element.Click(0, 0);
            mocks.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof (IllegalParameterException),
            "Specified click point is out of element's bounding rectangle")]
        public void should_throw_exception_if_offset_is_out_of_bounding()
        {
            Expect.Call(mockProvider.BoundingRectangle).Return(new Rect(0, 0, 10, 10));
            mocks.ReplayAll();
            element.Click(11, 11);
            mocks.VerifyAll();
        }

        [Test]
        public void should_call_provider_click_to_click_some_point()
        {
            Expect.Call(mockProvider.BoundingRectangle).Return(new Rect(10, 10, 10, 10));
            mockProvider.Click(new Point(15, 15));
            mocks.ReplayAll();
            element.Click(5, 5);
            mocks.VerifyAll();
        }
    }
}