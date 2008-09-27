using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input;

namespace Eft.Win32
{
    /// <summary>
    /// Provides methods for sending keyboard input
    /// </summary>
    public class Keyboard
    {
        /// <summary>The first X mouse button</summary>
        public const int XButton1 = 0x01;

        /// <summary>The second X mouse button</summary>
        public const int XButton2 = 0x02;


        /// <summary>
        /// Inject keyboard input into the system
        /// </summary>
        /// <param name="key">indicates the key pressed or released. Can be one of the constants defined in the Key enum</param>
        /// <param name="press">true to inject a key press, false to inject a key release</param>
        public static void SendKeyboardInput(Key key, bool press)
        {
            APIWrapper.INPUT ki = new APIWrapper.INPUT();
            ki.type = APIWrapper.INPUT_KEYBOARD;
            ki.union.keyboardInput.wVk = (short) KeyInterop.VirtualKeyFromKey(key);
            ki.union.keyboardInput.wScan = (short) APIWrapper.MapVirtualKey(ki.union.keyboardInput.wVk, 0);

            int dwFlags = 0;
            if (ki.union.keyboardInput.wScan > 0)
            {
                dwFlags |= APIWrapper.KEYEVENTF_SCANCODE;
            }
            if (false == press)
            {
                dwFlags |= APIWrapper.KEYEVENTF_KEYUP;
            }

            ki.union.keyboardInput.dwFlags = dwFlags;
            if (IsExtendedKey(key))
            {
                ki.union.keyboardInput.dwFlags |= APIWrapper.KEYEVENTF_EXTENDEDKEY;
            }

            ki.union.keyboardInput.time = 0;
            ki.union.keyboardInput.dwExtraInfo = new IntPtr(0);
            if (0 == APIWrapper.SendInput(1, ref ki, Marshal.SizeOf(ki)))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Injects a unicode character as keyboard input into the system
        /// </summary>
        /// <param name="key">indicates the key to be pressed or released. Can be any unicode character</param>
        /// <param name="press">true to inject a key press, false to inject a key release</param>
        public static void SendUnicodeKeyboardInput(char key, bool press)
        {
            APIWrapper.INPUT ki = new APIWrapper.INPUT();

            ki.type = APIWrapper.INPUT_KEYBOARD;
            ki.union.keyboardInput.wVk = 0;
            ki.union.keyboardInput.wScan = (short) key;
            ki.union.keyboardInput.dwFlags = APIWrapper.KEYEVENTF_UNICODE | (press ? 0 : APIWrapper.KEYEVENTF_KEYUP);
            ki.union.keyboardInput.time = 0;
            ki.union.keyboardInput.dwExtraInfo = new IntPtr(0);
            if (0 == APIWrapper.SendInput(1, ref ki, Marshal.SizeOf(ki)))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Injects a string of Unicode characters using simulated keyboard input
        /// It should be noted that this overload just sends the whole string
        /// with no pauses, depending on the recieving applications input processing
        /// it may not be able to keep up with the speed, resulting in corruption or
        /// loss of the input data.
        /// </summary>
        /// <param name="data">The unicode string to be sent</param>
        public static void SendUnicodeString(string data)
        {
            InternalSendUnicodeString(data, -1, 0);
        }

        /// <summary>
        /// Injects a string of Unicode characters using simulated keyboard input
        /// with user defined timing.
        /// </summary>
        /// <param name="data">The unicode string to be sent</param>
        /// <param name="sleepFrequency">How many characters to send between sleep calls</param>
        /// <param name="sleepLength">How long, in milliseconds, to sleep for at each sleep call</param>
        public static void SendUnicodeString(string data, int sleepFrequency, int sleepLength)
        {
            if (sleepFrequency < 1)
            {
                throw new ArgumentOutOfRangeException("sleepFrequency");
            }

            if (sleepLength < 0)
            {
                throw new ArgumentOutOfRangeException("sleepLength");
            }

            InternalSendUnicodeString(data, sleepFrequency, sleepLength);
        }

        /// <summary>
        /// Checks whether the specified key is currently up or down
        /// </summary>
        /// <param name="key">The Key to check</param>
        /// <returns>true if the specified key is currently down (being pressed), false if it is up</returns>
        public static bool GetAsyncKeyState(Key key)
        {
            int vKey = KeyInterop.VirtualKeyFromKey(key);
            int resp = APIWrapper.GetAsyncKeyState(vKey);

            if (resp == 0)
            {
                throw new InvalidOperationException("GetAsyncKeyStateFailed");
            }

            return resp < 0;
        }


        // Used internally by the HWND SetFocus code - it sends a hotkey to
        // itself - because it uses a VK that's not on the keyboard, it needs
        // to send the VK directly, not the scan code, which regular
        // SendKeyboardInput does.
        // Note that this method is public, but this class is private, so
        // this is not externally visible.
        internal static void SendKeyboardInputVK(byte vk, bool press)
        {
            APIWrapper.INPUT ki = new APIWrapper.INPUT();
            ki.type = APIWrapper.INPUT_KEYBOARD;
            ki.union.keyboardInput.wVk = vk;
            ki.union.keyboardInput.wScan = 0;
            ki.union.keyboardInput.dwFlags = press ? 0 : APIWrapper.KEYEVENTF_KEYUP;
            ki.union.keyboardInput.time = 0;
            ki.union.keyboardInput.dwExtraInfo = new IntPtr(0);
            if (0 == APIWrapper.SendInput(1, ref ki, Marshal.SizeOf(ki)))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        internal static bool IsExtendedKey(Key key)
        {
            // From the SDK:
            // The extended-key flag indicates whether the keystroke message originated from one of
            // the additional keys on the enhanced keyboard. The extended keys consist of the ALT and
            // CTRL keys on the right-hand side of the keyboard; the INS, DEL, HOME, END, PAGE UP,
            // PAGE DOWN, and arrow keys in the clusters to the left of the numeric keypad; the NUM LOCK
            // key; the BREAK (CTRL+PAUSE) key; the PRINT SCRN key; and the divide (/) and ENTER keys in
            // the numeric keypad. The extended-key flag is set if the key is an extended key. 
            //
            // - docs appear to be incorrect. Use of Spy++ indicates that break is not an extended key.
            // Also, menu key and windows keys also appear to be extended.
            return key == Key.RightAlt
                   || key == Key.RightCtrl
                   || key == Key.NumLock
                   || key == Key.Insert
                   || key == Key.Delete
                   || key == Key.Home
                   || key == Key.End
                   || key == Key.Prior
                   || key == Key.Next
                   || key == Key.Up
                   || key == Key.Down
                   || key == Key.Left
                   || key == Key.Right
                   || key == Key.Apps
                   || key == Key.RWin
                   || key == Key.LWin;

            // Note that there are no distinct values for the following keys:
            // numpad divide
            // numpad enter
        }

        // Injects a string of Unicode characters using simulated keyboard input
        // with user defined timing
        // <param name="data">The unicode string to be sent</param>
        // <param name="sleepFrequency">How many characters to send between sleep calls
        // A sleepFrequency of -1 means to never sleep</param>
        // <param name="sleepLength">How long, in milliseconds, to sleep for at each sleep call</param>
        private static void InternalSendUnicodeString(string data, int sleepFrequency, int sleepLength)
        {
            char[] chardata = data.ToCharArray();
            int counter = -1;

            foreach (char c in chardata)
            {
                // Every sleepFrequency characters, sleep for sleepLength ms to avoid overflowing the input buffer.
                counter++;
                if (counter > sleepFrequency)
                {
                    counter = 0;
                    Thread.Sleep(sleepLength);
                }

                SendUnicodeKeyboardInput(c, true);
                SendUnicodeKeyboardInput(c, false);
            }
        }

        public static void Command(IntPtr wnd)
        {
            APIWrapper.PostMessage(APIWrapper.HWND.Cast(wnd), (int) WindowsMessages.WM_COMMAND, 0x0000e101, 0);
        }
    }
}