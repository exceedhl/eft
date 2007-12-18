using System.Windows.Input;
using Eft.Elements;
using Eft.Tester;
using Eft.Tester.Exception;
using NUnit.Framework;

namespace Eft.Testers
{
    [TestFixture]
    public class ClickPatternRecoganizerTest
    {
        [Test]
        [ExpectedException(typeof(IllegalClickPatternException))]
        public void should_throw_exception_if_click_pattern_illegal()
        {
            new ClickPatternRecoganizer("{1}");
        }

        [Test]
        public void should_recoganize_modifier_keys_from_string()
        {
            ClickPatternRecoganizer recoganizer = new ClickPatternRecoganizer("^{Left 1}");
            Assert.AreEqual(ModifierKeys.Control, recoganizer.ModifierKeys);

            recoganizer = new ClickPatternRecoganizer("+{Left 1}");
            Assert.AreEqual(ModifierKeys.Shift, recoganizer.ModifierKeys);

            recoganizer = new ClickPatternRecoganizer("!{Left 1}");
            Assert.AreEqual(ModifierKeys.Alt, recoganizer.ModifierKeys);

            recoganizer = new ClickPatternRecoganizer("#{Left 1}");
            Assert.AreEqual(ModifierKeys.Windows, recoganizer.ModifierKeys);

            recoganizer = new ClickPatternRecoganizer("{Left 1}");
            Assert.AreEqual(ModifierKeys.None, recoganizer.ModifierKeys);
        }

        [Test]
        public void should_recoganize_mouse_button_from_string()
        {
            ClickPatternRecoganizer recoganizer = new ClickPatternRecoganizer("^{Left 1}");
            Assert.AreEqual(MouseButton.Left, recoganizer.MouseButton);

            recoganizer = new ClickPatternRecoganizer("^{Middle 1}");
            Assert.AreEqual(MouseButton.Middle, recoganizer.MouseButton);

            recoganizer = new ClickPatternRecoganizer("^{Right 1}");
            Assert.AreEqual(MouseButton.Right, recoganizer.MouseButton);

            recoganizer = new ClickPatternRecoganizer("^{XButton1 1}");
            Assert.AreEqual(MouseButton.XButton1, recoganizer.MouseButton);

            recoganizer = new ClickPatternRecoganizer("^{XButton2 1}");
            Assert.AreEqual(MouseButton.XButton2, recoganizer.MouseButton);
        }

        [Test]
        public void should_recoganize_click_times()
        {
            ClickPatternRecoganizer recoganizer = new ClickPatternRecoganizer("^{Left 1}");
            Assert.AreEqual(1, recoganizer.Times);

            recoganizer = new ClickPatternRecoganizer("^{Left 2}");
            Assert.AreEqual(2, recoganizer.Times);

            recoganizer = new ClickPatternRecoganizer("^{Left 20}");
            Assert.AreEqual(20, recoganizer.Times);
        }


    }
}