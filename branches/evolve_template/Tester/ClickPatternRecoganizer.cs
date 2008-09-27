using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Eft.Elements;
using Eft.Tester.Exception;
using Match=System.Text.RegularExpressions.Match;

namespace Eft.Tester
{
    public class ClickPatternRecoganizer
    {
        private const string CLICK_PATTERN_STRING = @"([\#\!\^\+]{0,1})\{(Left|Middle|Right|XButton1|XButton2) (\d+)\}";
        private const string ILLEGAL_CLICK_PATTER_STRING_MESSAGE_FORMAT = "Formatted click string: [{0}] is invalid. The format should be <#|!|^|+>{{Left | Middle | Right | XButton1 | XButton2 n}}";
        private readonly MouseButton mouseButton;
        private readonly ModifierKeys modifierKeys;
        private readonly int times;

        public ClickPatternRecoganizer(string formattedClickString)
        {
            Regex pattern = new Regex(CLICK_PATTERN_STRING);
            Match match = pattern.Match(formattedClickString);
            if (!match.Success)
            {
                throw new IllegalClickPatternException(string.Format(ILLEGAL_CLICK_PATTER_STRING_MESSAGE_FORMAT, formattedClickString));
            }
            string modifierKeysString = match.Groups[1].Value;
            string mouseButtonString = match.Groups[2].Value;
            string clickTimesString = match.Groups[3].Value;

            modifierKeys = ParseModifierKeys(modifierKeysString);
            mouseButton = (MouseButton) Enum.Parse(typeof (MouseButton), mouseButtonString);
            times = int.Parse(clickTimesString);
        }

        private static ModifierKeys ParseModifierKeys(string modifierKeysString)
        {
            switch (modifierKeysString)
            {
                case "#":
                    return ModifierKeys.Windows;
                case "!":
                    return ModifierKeys.Alt;
                case "^":
                    return ModifierKeys.Control;
                case "+":
                    return ModifierKeys.Shift;
                default:
                    return ModifierKeys.None;
            }
        }

        public MouseButton MouseButton
        {
            get { return mouseButton; }
        }

        public ModifierKeys ModifierKeys
        {
            get { return modifierKeys; }
        }

        public int Times
        {
            get { return times; }
        }
    }
}