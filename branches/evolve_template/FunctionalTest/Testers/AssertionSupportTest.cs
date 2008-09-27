using System;
using Eft.Tester;
using NUnit.Framework;

namespace FunctionalTest.Testers
{
    [TestFixture]
    public class AssertionSupportTest
    {
        [Test]
        public void should_be_able_to_assert_toggle_state()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            Tester i = Tester.Run(fileName);
            i.Click("#openTextTestWindow");
            i.SelectWindow("text test window");

            i.AssertNotSelected("#checkBox");
            i.Click("#checkBox");
            i.AssertSelected("#checkBox");

            i.Retire();
        }

        [Test]
        public void should_throw_exception_if_selection_assertion_failed()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            Tester i = Tester.Run(fileName);
            i.Click("#openTextTestWindow");
            i.SelectWindow("text test window");

            try
            {
                i.AssertSelected("#checkBox");
                Assert.Fail("exception expected");
            }
            catch (AssertionException)
            {
                // pass
            }
            i.Click("#checkBox");
            try
            {
                i.AssertNotSelected("#checkBox");
            }
            catch (AssertionException)
            {
                // pass
            }
            i.Retire();
        }
    }
}