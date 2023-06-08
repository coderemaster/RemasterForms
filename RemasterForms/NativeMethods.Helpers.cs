using System;

namespace RemasterForms
{
    internal static partial class NativeMethods // Helpers
    {
        #region ## input

        // IsKeyDown
        public static bool IsKeyDown(int vKey)
        {
            return (GetAsyncKeyState(vKey) & 0x8000) != 0;
        }

        #endregion input

        #region ## menu

        // GetMenuDefaultItem
        public static int GetMenuDefaultItem(IntPtr hMenu)
        {
            return GetMenuDefaultItem(hMenu, false, GMDI_USEDISABLED);
        }

        // GetMenuItemText
        public static string GetMenuItemText(IntPtr hMenu, int item)
        {
            var mii = new MENUITEMINFO(MIIM_STRING);

            if (GetMenuItemInfo(hMenu, item, false, mii))
            {
                mii.cch++;
                mii.dwTypeData = new string('\0', mii.cch);

                if (GetMenuItemInfo(hMenu, item, false, mii))
                    return mii.dwTypeData;
            }

            return null;
        }

        // GetMenuVacantID
        public static int GetMenuVacantID(IntPtr hMenu)
        {
            for (int id = 0x10; id < 0xF000; id += 0x10)
            {
                if (!MenuItemExists(hMenu, id))
                    return id;
            }

            return 0;
        }

        // IsMenuItemEnabled
        public static bool IsMenuItemEnabled(IntPtr hMenu, int item)
        {
            var mii = new MENUITEMINFO(MIIM_STATE);

            if (GetMenuItemInfo(hMenu, item, false, mii))
                return (mii.fState & MFS_GRAYED) != MFS_GRAYED;

            return false;
        }

        // MenuItemExists
        public static bool MenuItemExists(IntPtr hMenu, int item)
        {
            var mii = new MENUITEMINFO();
            return GetMenuItemInfo(hMenu, item, false, mii);
        }

        // SetMenuDefaultItem
        public static void SetMenuDefaultItem(IntPtr hMenu, int item)
        {
            _ = SetMenuDefaultItem(hMenu, item, false);
        }

        // SetMenuItemEnabled
        public static void SetMenuItemEnabled(IntPtr hMenu, int item, bool enabled)
        {
            var mii = new MENUITEMINFO(MIIM_STATE)
            {
                fState = enabled ? MFS_ENABLED : MFS_GRAYED
            };

            _ = SetMenuItemInfo(hMenu, item, false, mii);
        }

        // SetMenuItemID
        public static void SetMenuItemID(IntPtr hMenu, int item, int newID)
        {
            var mii = new MENUITEMINFO(MIIM_ID)
            {
                wID = newID
            };

            _ = SetMenuItemInfo(hMenu, item, false, mii);
        }

        // SetMenuItemText
        public static void SetMenuItemText(IntPtr hMenu, int item, string text)
        {
            var mii = new MENUITEMINFO(MIIM_STRING)
            {
                dwTypeData = text
            };

            _ = SetMenuItemInfo(hMenu, item, false, mii);
        }

        #endregion menu

        #region ## window

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

        #endregion window
    }
}
