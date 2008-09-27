using System.Windows.Input;
using Eft.Provider;
using NUnit.Framework;

namespace Eft.Core
{
    [TestFixture]
    public class UIAutomationProviderTest
    {
        [Test]
        public void should_return_correct_modifier_key()
        {
            Assert.AreEqual(Key.LeftCtrl, UIAutomationProvider.GetKey(ModifierKeys.Control));
            Assert.AreEqual(Key.LeftAlt, UIAutomationProvider.GetKey(ModifierKeys.Alt));
            Assert.AreEqual(Key.LeftShift, UIAutomationProvider.GetKey(ModifierKeys.Shift));
            Assert.AreEqual(Key.LWin, UIAutomationProvider.GetKey(ModifierKeys.Windows));
            Assert.AreEqual(Key.None, UIAutomationProvider.GetKey(ModifierKeys.None));
        }
    }
}