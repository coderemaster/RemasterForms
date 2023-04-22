using System;

namespace RemasterForms
{
    internal static partial class NativeMethods // Helpers
    {
        // GetClientRect
        public static RECT GetClientRect(IntPtr hWnd)
        {
            var rect = new RECT();
            _ = GetClientRect(hWnd, ref rect);
            return rect;
        }

        // GetWindowRect
        public static RECT GetWindowRect(IntPtr hWnd)
        {
            var rect = new RECT();
            _ = GetWindowRect(hWnd, ref rect);
            return rect;
        }

        // IsKeyDown
        public static bool IsKeyDown(int vKey)
        {
            return (GetAsyncKeyState(vKey) & 0x8000) != 0;
        }

        // IsMenuItemEnabled
        public static bool IsMenuItemEnabled(IntPtr hMenu, int item)
        {
            var mii = new MENUITEMINFO(MIIM_STATE);

            _ = GetMenuItemInfo(hMenu, item, false, mii);

            return (mii.fState & MFS_GRAYED) != MFS_GRAYED;
        }

        // MapPoint
        public static POINT MapPoint(IntPtr hWndFrom, IntPtr hWndTo, POINT pt)
        {
            MapWindowPoints(hWndFrom, hWndTo, ref pt, 1);
            return pt;
        }

        // MapRect
        public static RECT MapRect(IntPtr hWndFrom, IntPtr hWndTo, RECT rect)
        {
            MapWindowPoints(hWndFrom, hWndTo, ref rect, 2);
            return rect;
        }

        // SetMenuItemEnabled
        public static bool SetMenuItemEnabled(IntPtr hMenu, int item, bool value)
        {
            var mii = new MENUITEMINFO(MIIM_STATE)
            {
                fState = value ? MFS_ENABLED : MFS_GRAYED
            };

            return SetMenuItemInfo(hMenu, item, false, mii);
        }

        // SetMenuItemID
        public static bool SetMenuItemID(IntPtr hMenu, int item, int newID)
        {
            var mii = new MENUITEMINFO(MIIM_ID)
            {
                wID = newID
            };

            return SetMenuItemInfo(hMenu, item, false, mii);
        }
    }
}
