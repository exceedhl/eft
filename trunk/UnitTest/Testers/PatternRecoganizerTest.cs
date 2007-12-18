using Eft.Elements;
using Eft.Tester;
using NUnit.Framework;

namespace Eft.Testers
{
    [TestFixture]
    public class PatternRecoganizerTest
    {
        [Test]
        public void should_recoganize_pattern_by_its_prefix()
        {
            PatternRecoganizer recoganizer = new PatternRecoganizer("regex:regexPattern");
            Assert.AreEqual("regexPattern", recoganizer.Pattern);
            Assert.AreSame(recoganizer.Match, Match.Regex);

            recoganizer = new PatternRecoganizer("glob:globPattern");
            Assert.AreEqual("globPattern", recoganizer.Pattern);
            Assert.AreSame(recoganizer.Match, Match.Glob);

            recoganizer = new PatternRecoganizer("exact:exactPattern");
            Assert.AreEqual("exactPattern", recoganizer.Pattern);
            Assert.AreSame(recoganizer.Match, Match.Exact);
        }

        [Test]
        public void should_treat_it_glob_pattern_as_default()
        {
            PatternRecoganizer recoganizer = new PatternRecoganizer("unknown:pattern");
            Assert.AreEqual("unknown:pattern", recoganizer.Pattern);
            Assert.AreSame(recoganizer.Match, Match.Glob);
        }
    }
}