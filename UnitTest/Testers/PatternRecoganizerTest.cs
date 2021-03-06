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
            MatchPatternRecoganizer recoganizer = new MatchPatternRecoganizer("regex:regexPattern");
            Assert.AreEqual("regexPattern", recoganizer.Pattern);
            Assert.AreSame(recoganizer.Match, Match.Regex);

            recoganizer = new MatchPatternRecoganizer("glob:globPattern");
            Assert.AreEqual("globPattern", recoganizer.Pattern);
            Assert.AreSame(recoganizer.Match, Match.Glob);

            recoganizer = new MatchPatternRecoganizer("exact:exactPattern");
            Assert.AreEqual("exactPattern", recoganizer.Pattern);
            Assert.AreSame(recoganizer.Match, Match.Exact);
        }

        [Test]
        public void should_treat_it_glob_pattern_as_default()
        {
            MatchPatternRecoganizer recoganizer = new MatchPatternRecoganizer("unknown:pattern");
            Assert.AreEqual("unknown:pattern", recoganizer.Pattern);
            Assert.AreSame(recoganizer.Match, Match.Glob);
        }
    }
}