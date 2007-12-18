using System.Reflection;
using Eft.Elements;

namespace Eft.Tester
{
    public class PatternRecoganizer
    {
        private readonly string pattern;
        private readonly StringMatchDelegate match;

        public PatternRecoganizer(string formattedPattern)
        {
            foreach (FieldInfo field in typeof (Match).GetFields())
            {
                string patternPrefix = field.Name.ToLower() + ":";
                if (formattedPattern.StartsWith(patternPrefix))
                {
                    pattern = formattedPattern.Substring(patternPrefix.Length);
                    match = (StringMatchDelegate) field.GetValue(null);
                    return;
                }
            }
            pattern = formattedPattern;
            match = Elements.Match.Glob;
        }

        public string Pattern
        {
            get { return pattern; }
        }

        public StringMatchDelegate Match
        {
            get { return match; }
        }
    }
}