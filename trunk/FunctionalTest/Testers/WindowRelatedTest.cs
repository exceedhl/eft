using Eft.Tester;
using NUnit.Framework;

namespace FunctionalTest.Testers
{
    [TestFixture]
    public class WindowRelatedTest
    {
        private Tester i;

        [SetUp]
        public void setup()
        {
            i = Tester.Run("calc");
        }

        [TearDown]
        public void teardown()
        {
            i.Retire();
        }

        [Test]
        public void should_select_one_window_defaultly()
        {
            i.AssertWindowTitle("Calculator");
        }

        [Test]
        public void should_use_glob_pattern_to_locate_window_defaultly()
        {
            i.SelectWindow("*Calcul*");
            i.AssertWindowTitle("Calculator");
        }

        [Test]
        public void should_support_glob_and_regex_and_exact_pattern_explicitly_while_selecting_window()
        {
            i.SelectWindow("exact:Calculator");
            i.AssertWindowTitle("Calculator");
            i.SelectWindow("glob:*Calculato?");
            i.AssertWindowTitle("Calculator");
            i.SelectWindow("regex:Cal.*");
            i.AssertWindowTitle("Calculator");
        }

        [Test]
        public void should_be_able_to_test_window_title_using_patterns()
        {
            i.SelectWindow("Calculator");
            i.AssertWindowTitle("Calc*");
            i.AssertWindowTitle("exact:Calculator");
            i.AssertWindowTitle("regex:Cal.*tor");
            i.AssertWindowTitle("glob:?alc*");
        }

        [Test]
        [ExpectedException(typeof(AssertionException))]
        public void should_throw_exception_if_title_pattern_not_match_actual_title()
        {
            i.AssertWindowTitle("wrong title");
        }

        [Test]
        public void should_be_able_to_assert_window_count()
        {
            i.AssertWindowCount(1);
        }

        [Test]
        [ExpectedException(typeof(AssertionException))]
        public void should_throw_exception_if_window_count_not_match()
        {
            i.AssertWindowCount(0);
        }

    }
}