using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace Eft.Win32
{
    [Flags]
    public enum WindowsMessages
    {
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x202,
        WM_COMMAND = 0x111
    }

    ///<summary>
    ///This class is used to PInvoke for win32 functionality that I have not been
    ///able to find in the managed platform
    ///Feel free to updated clients to used managed/safe alternatives
    ///</summary>
    public class APIWrapper
    {
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        //SendInput related
        public const int VK_SHIFT = 0x10;
        public const int VK_CONTROL = 0x11;
        public const int VK_MENU = 0x12;

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const int KEYEVENTF_KEYUP = 0x0002;
        public const int KEYEVENTF_UNICODE = 0x0004;
        public const int KEYEVENTF_SCANCODE = 0x0008;

        public const int MOUSEEVENTF_VIRTUALDESK = 0x4000;

        public const int EM_SETSEL = 0x00B1;
        public const int EM_GETLINECOUNT = 0x00BA;
        public const int EM_LINEFROMCHAR = 0x00C9;

        // GetSystemMetrics
        public const int SM_CXMAXTRACK = 59;
        public const int SM_CYMAXTRACK = 60;
        public const int SM_XVIRTUALSCREEN = 76;
        public const int SM_YVIRTUALSCREEN = 77;
        public const int SM_CXVIRTUALSCREEN = 78;
        public const int SM_CYVIRTUALSCREEN = 79;
        public const int SM_SWAPBUTTON = 23;

        // Window style information
        //public const int GWL_HINSTANCE  = -6;
        //public const int GWL_ID         = -12;
        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;

        public const int WS_VSCROLL = 0x00200000;
        public const int WS_HSCROLL = 0x00100000;
        public const int ES_MULTILINE = 0x0004;
        public const int ES_AUTOVSCROLL = 0x0040;
        public const int ES_AUTOHSCROLL = 0x0080;

        public const int INPUT_MOUSE = 0;
        public const int INPUT_KEYBOARD = 1;

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public int type;
            public INPUTUNION union;
        } ;

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUTUNION
        {
            [FieldOffset(0)] public MOUSEINPUT mouseInput;
            [FieldOffset(0)] public KEYBDINPUT keyboardInput;
        } ;

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        } ;

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        } ;

        [StructLayout(LayoutKind.Sequential)]
        public struct HWND
        {
            private IntPtr _value;

            public static HWND Cast(IntPtr hwnd)
            {
                HWND temp = new HWND();
                temp._value = hwnd;
                return temp;
            }

            public static implicit operator IntPtr(HWND hwnd)
            {
                return hwnd._value;
            }

            public static HWND NULL
            {
                get
                {
                    HWND temp = new HWND();
                    temp._value = IntPtr.Zero;
                    return temp;
                }
            }

            public static bool operator ==(HWND lhs, HWND rhs)
            {
                return lhs._value == rhs._value;
            }

            public static bool operator !=(HWND lhs, HWND rhs)
            {
                return lhs._value != rhs._value;
            }

            public override bool Equals(object oCompare)
            {
                HWND temp = Cast((HWND) oCompare);
                return _value == temp._value;
            }

            public override int GetHashCode()
            {
                return (int) _value;
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SendInput(int nInputs, ref INPUT mi, int cbSize);

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern int MapVirtualKey(int nVirtKey, int nMapType);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetAsyncKeyState(int nVirtKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetKeyState(int nVirtKey);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        internal static extern int GetKeyboardState(byte[] keystate);

        [DllImport("user32.dll", ExactSpelling = true, EntryPoint = "keybd_event", CharSet = CharSet.Auto)]
        internal static extern void Keybd_event(byte vk, byte scan, int flags, int extrainfo);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        internal static extern int SetKeyboardState(byte[] keystate);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HWND hWnd, int nMsg, int wParam, ref Point lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HWND hWnd, int nMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(HWND hWnd, int nMsg, int wParam, int lParam);

        // Overload for WM_GETTEXT
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HWND hWnd, int nMsg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int metric);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowLong(HWND hWnd, int nIndex);

        [DllImport("User32.dll")]
        public static extern int FindWindow(string strClassName, string strWindowName);

        [DllImport("User32.dll")]
        public static extern int FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string strClassName,
                                              string strWindowName);
    }
}