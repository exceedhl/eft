using Eft.Elements;
using NUnit.Framework;

namespace Eft
{
    [TestFixture]
    public class MatchTest
    {
        [Test]
        public void test_glob_match()
        {
            Assert.IsTrue(Match.Glob("abcd", "abcd*"));
            Assert.IsTrue(Match.Glob("abcd", "abcd"));
            Assert.IsTrue(Match.Glob("abcd", "abc?"));
            Assert.IsTrue(Match.Glob("abcd", "ab?d"));
            Assert.IsTrue(Match.Glob("abcd", "?b?d"));
            Assert.IsTrue(Match.Glob("abcd", "*?d"));
            Assert.IsTrue(Match.Glob("abcd", "*"));
            Assert.IsTrue(Match.Glob(" ", "*"));
            Assert.IsTrue(Match.Glob(" ", "?"));
            Assert.IsTrue(Match.Glob("", "?"));
            Assert.IsTrue(Match.Glob("", "*"));
            Assert.IsTrue(Match.Glob("abcd", "abcd?"));

            Assert.IsFalse(Match.Glob("abcd", "ab?"));
            Assert.IsFalse(Match.Glob("abcd", "abc"));
            Assert.IsFalse(Match.Glob("abcd", "abcde"));
            Assert.IsFalse(Match.Glob("abcd", ""));
        }

        [Test]
        public void test_regex_match()
        {
            Assert.IsTrue(Match.Regex("abcd", "[a-z]*"));
            Assert.IsTrue(Match.Regex("aaaa", "^a{4}$"));
            Assert.IsFalse(Match.Regex("aaaa", "ab"));
            Assert.IsTrue(Match.Regex("", ".*"));
        }

        [Test]
        public void test_exact_match()
        {
            Assert.IsTrue(Match.Exact("ab", "ab"));
            Assert.IsFalse(Match.Exact("ab", "ba"));
            Assert.IsFalse(Match.Exact("ab", "a"));
            Assert.IsFalse(Match.Exact("ab", ""));
        }
    }
}