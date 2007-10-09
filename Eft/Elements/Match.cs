using System.Text.RegularExpressions;

namespace Eft.Elements
{
    public delegate bool StringMatchDelegate(string actual, string pattern);

    public class Match
    {
        public static readonly StringMatchDelegate Glob =
            delegate(string actual, string pattern)
                {
                    pattern = pattern.Replace("*", ".*");
                    pattern = pattern.Replace("?", ".{0,1}");
                    pattern = "^" + pattern + "$";
                    return new Regex(pattern).Match(actual).Success;
                };

        public static readonly StringMatchDelegate Regex =
            delegate(string actual, string pattern) { return new Regex(pattern).Match(actual).Success; };

        public static readonly StringMatchDelegate Exact =
            delegate(string actual, string pattern) { return actual == pattern; };
    }
}