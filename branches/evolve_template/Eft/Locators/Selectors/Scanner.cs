using System.Text.RegularExpressions;

namespace Eft.Locators.Selectors
{
    public class Scanner
    {
        private const string COMBINATORS = @"[>~\+\s]";
        private const string TYPE = @"([a-zA-Z]+|\*)?";
        private const string ATTRIBUTE = @"(\[(\w+)='([^\[]+)'\]|#((\w+)|'([^']+)')|\.((\w+)|'([^']+)'))*";
        private const string PSEUDO = @"(?<pseudo>(?<=\S+):[a-z\-\(\)0-9]+)?";
        private const string COMBINATOR = @"(\s*(?<combinator>" + COMBINATORS + @")\s*(?=\S+))?";

        private readonly Regex css =
            new Regex(COMBINATOR + @"(?<type>" + TYPE + ")(?<attr>" + ATTRIBUTE + ")" + PSEUDO + "$",
                      RegexOptions.RightToLeft | RegexOptions.ExplicitCapture);

        private string selectorString;
        private string type;
        private string attribute;
        private string combinator;
        private string pseudo;

        public Scanner(string selectorString)
        {
            this.selectorString = selectorString;
        }

        public string Type
        {
            get { return type; }
        }

        public string Attribute
        {
            get { return attribute; }
        }

        public string Combinator
        {
            get { return combinator; }
        }

        public string Pseudo
        {
            get { return pseudo; }
        }

        public bool NextSelector()
        {
            if (selectorString.Length == 0) return false;
            Match m = css.Match(selectorString);
            if (m.Success)
            {
                if (m.Groups[0].Value.Trim().Length == 0) return false;
                combinator = m.Groups[1].Value;
                type = m.Groups[2].Value;
                attribute = m.Groups[3].Value;
                pseudo = m.Groups[4].Value;

                selectorString = selectorString.Substring(0, m.Index);
            }
            return m.Success;
        }

        public bool IsStringLeft()
        {
            return selectorString.Length != 0;
        }
    }
}