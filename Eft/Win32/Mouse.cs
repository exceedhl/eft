using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Automation;
using Eft.Elements;

namespace Eft.Win32
{
    public class Mouse
    {
        internal static void MoveToAndClick(AutomationElement el)
        {
            if (el == null)
            {
                throw new ArgumentNullException("el");
            }
            Point clickablePoint = el.GetClickablePoint();
            MoveTo(clickablePoint);
            Click(clickablePoint);
        }

        public static void MoveToAndClick(Point point)
        {
            MoveTo(point);
            Click(point);
        }

        public static void MoveToAndClick(Point point, MouseButton button)
        {
            MoveTo(point);
            MouseDown(point, button);
            MouseUp(point, button);
        }

        private static void MouseUp(Point pt, MouseButton button)
        {
            SendMouseInputFlags flag;
            switch (button)
            {
                case MouseButton.Left:
                    flag = SendMouseInputFlags.LeftUp;
                    break;
                case MouseButton.Right:
                    flag = SendMouseInputFlags.RightUp;
                    break;
                case MouseButton.Middle:
                    flag = SendMouseInputFlags.MiddleUp;
                    break;
                default:
                    flag = SendMouseInputFlags.XUp;
                    break;
            }
            SendMouseInput(pt.X, pt.Y, 0, flag | SendMouseInputFlags.Absolute);
        }

        private static void MouseDown(Point pt, MouseButton button)
        {
            SendMouseInputFlags flag;
            switch (button)
            {
                case MouseButton.Left:
                    flag = SendMouseInputFlags.LeftDown;
                    break;
                case MouseButton.Right:
                    flag = SendMouseInputFlags.RightDown;
                    break;
                case MouseButton.Middle:
                    flag = SendMouseInputFlags.MiddleDown;
                    break;
                default:
                    flag = SendMouseInputFlags.XDown;
                    break;
            }
            SendMouseInput(pt.X, pt.Y, 0, flag | SendMouseInputFlags.Absolute);
        }

        public static void SendMouseInput(int x, int y, int data, SendMouseInputFlags flags)
        {
            SendMouseInput(x, y, data, flags);
        }

        /// <summary>
        /// Move the mouse to an element. 
        ///
        /// IMPORTANT!
        /// 
        /// Do not call MoveToAndClick (actually, do not make any calls to UIAutomationClient) 
        /// from the UI thread if your test is in the same process as the UI being tested.  
        /// UIAutomation calls back into Avalon core for UI information (e.g. ClickablePoint) 
        /// and must be on the UI thread to get this information.  If your test is making calls 
        /// from the UI thread you are going to deadlock...
        internal static void MoveTo(AutomationElement el)
        {
            if (el == null)
            {
                throw new ArgumentNullException("el");
            }
            MoveTo(el.GetClickablePoint());
        }

        public static void MoveTo(Point pt)
        {
            SendMouseInput(pt.X, pt.Y, 0, SendMouseInputFlags.Move | SendMouseInputFlags.Absolute);
        }

        public static void LeftButtonDown(Point pt)
        {
            SendMouseInput(pt.X, pt.Y, 0, SendMouseInputFlags.LeftDown | SendMouseInputFlags.Absolute);
        }

        public static void LeftButtonUp(Point pt)
        {
            SendMouseInput(pt.X, pt.Y, 0, SendMouseInputFlags.LeftUp | SendMouseInputFlags.Absolute);
        }

        public static void RightButtonDown(Point pt)
        {
            SendMouseInput(pt.X, pt.Y, 0, SendMouseInputFlags.RightDown | SendMouseInputFlags.Absolute);
        }

        public static void RightButtonUp(Point pt)
        {
            SendMouseInput(pt.X, pt.Y, 0, SendMouseInputFlags.RightUp | SendMouseInputFlags.Absolute);
        }

        public static void Click(Point pt)
        {
            LeftButtonDown(pt);
            LeftButtonUp(pt);
        }

        public static void RightClick(Point pt)
        {
            RightButtonDown(pt);
            RightButtonUp(pt);
        }

        /// <summary>
        /// Inject pointer input into the system
        /// </summary>
        /// <param name="x">x coordinate of pointer, if Move flag specified</param>
        /// <param name="y">y coordinate of pointer, if Move flag specified</param>
        /// <param name="data">wheel movement, or mouse X button, depending on flags</param>
        /// <param name="flags">flags to indicate which type of input occurred - move, button press/release, wheel move, etc.</param>
        /// <remarks>x, y are in pixels. If Absolute flag used, are relative to desktop origin.</remarks>
        /// 
        /// <outside_see conditional="false">
        /// This API does not work inside the secure execution environment.
        /// <exception cref="System.Security.Permissions.SecurityPermission"/>
        /// </outside_see>
        public static void SendMouseInput(double x, double y, int data, SendMouseInputFlags flags)
        {
            int intflags = (int) flags;

            if ((intflags & (int) SendMouseInputFlags.Absolute) != 0)
            {
                int vscreenWidth = APIWrapper.GetSystemMetrics(APIWrapper.SM_CXVIRTUALSCREEN);
                int vscreenHeight = APIWrapper.GetSystemMetrics(APIWrapper.SM_CYVIRTUALSCREEN);
                int vscreenLeft = APIWrapper.GetSystemMetrics(APIWrapper.SM_XVIRTUALSCREEN);
                int vscreenTop = APIWrapper.GetSystemMetrics(APIWrapper.SM_YVIRTUALSCREEN);

                // Absolute input requires that input is in 'normalized' coords - with the entire
                // desktop being (0,0)...(65535,65536). Need to convert input x,y coords to this
                // first.
                //
                // In this normalized world, any pixel on the screen corresponds to a block of values
                // of normalized coords - eg. on a 1024x768 screen,
                // y pixel 0 corresponds to range 0 to 85.333,
                // y pixel 1 corresponds to range 85.333 to 170.666,
                // y pixel 2 correpsonds to range 170.666 to 256 - and so on.
                // Doing basic scaling math - (x-top)*65536/Width - gets us the start of the range.
                // However, because int math is used, this can end up being rounded into the wrong
                // pixel. For example, if we wanted pixel 1, we'd get 85.333, but that comes out as
                // 85 as an int, which falls into pixel 0's range - and that's where the pointer goes.
                // To avoid this, we add on half-a-"screen pixel"'s worth of normalized coords - to
                // push us into the middle of any given pixel's range - that's the 65536/(Width*2)
                // part of the formula. So now pixel 1 maps to 85+42 = 127 - which is comfortably
                // in the middle of that pixel's block.
                // The key ting here is that unlike points in coordinate geometry, pixels take up
                // space, so are often better treated like rectangles - and if you want to target
                // a particular pixel, target its rectangle's midpoint, not its edge.
                x = ((x - vscreenLeft)*65536)/vscreenWidth + 65536/(vscreenWidth*2);
                y = ((y - vscreenTop)*65536)/vscreenHeight + 65536/(vscreenHeight*2);

                intflags |= APIWrapper.MOUSEEVENTF_VIRTUALDESK;
            }

            APIWrapper.INPUT mi = new APIWrapper.INPUT();
            mi.type = APIWrapper.INPUT_MOUSE;
            mi.union.mouseInput.dx = (int) x;
            mi.union.mouseInput.dy = (int) y;
            mi.union.mouseInput.mouseData = data;
            mi.union.mouseInput.dwFlags = intflags;
            mi.union.mouseInput.time = 0;
            mi.union.mouseInput.dwExtraInfo = new IntPtr(0);
            if (APIWrapper.SendInput(1, ref mi, Marshal.SizeOf(mi)) == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}