using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RemasterForms
{
    internal static partial class NativeMethods // Methods
    {
        // AdjustWindowRectEx
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AdjustWindowRectEx(ref RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

        // AnimateWindow
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AnimateWindow(IntPtr hWnd, int time, int flags);

        // BitBlt
        [DllImport("gdi32", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hDC, int x, int y, int width, int height, IntPtr hSrcDC, int xSrc, int ySrc, int rop);

        // CreateCompatibleBitmap
        [DllImport("gdi32", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateCompatibleBitmap([In] IntPtr hDC, int width, int height);

        // CreateCompatibleDC
        [DllImport("gdi32", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateCompatibleDC([In] IntPtr hDC);

        // DeleteDC
        [DllImport("gdi32", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteDC([In] IntPtr hDC);

        // DeleteObject
        [DllImport("gdi32", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        // DwmExtendFrameIntoClientArea
        [DllImport("dwmapi")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

        // DwmDefWindowProc
        [DllImport("dwmapi")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DwmDefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, out IntPtr lResult);
        //
        [DllImport("dwmapi")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DwmDefWindowProc(IntPtr hWnd, int msg, int wParam, int lParam, out IntPtr lResult);
        //
        [DllImport("dwmapi")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DwmDefWindowProc(IntPtr hWnd, int msg, int wParam, POINTS lParam, out IntPtr lResult);

        // DwmGetColorizationColor
        [DllImport("dwmapi")]
        public static extern int DwmGetColorizationColor(out int pcrColorization, [MarshalAs(UnmanagedType.Bool)] out bool pfOpaqueBlend);

        // DwmGetWindowAttribute
        [DllImport("dwmapi")]
        public static extern int DwmGetWindowAttribute(IntPtr hWnd, int attr, out IntPtr attrValue, int attrSize);
        //
        [DllImport("dwmapi")]
        public static extern int DwmGetWindowAttribute(IntPtr hWnd, int attr, out RECT attrValue, int attrSize);

        // DwmSetWindowAttribute
        [DllImport("dwmapi")]
        public static extern int DwmSetWindowAttribute(IntPtr hWnd, int attr, ref int attrValue, int attrSize);
        //
        [DllImport("dwmapi")]
        public static extern int DwmSetWindowAttribute(IntPtr hWnd, int attr, ref COLORREF attrValue, int attrSize);

        // EnableMenuItem
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int EnableMenuItem(IntPtr hMenu, int uIDEnableItem, int uEnable);

        // GetAsyncKeyState
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern short GetAsyncKeyState(int vkey);

        // GetClientRect
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetClientRect(IntPtr hWnd, [In, Out] ref RECT rect);

        // GetDC
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        // GetMenuDefaultItem
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetMenuDefaultItem(IntPtr hMenu, bool byPos, int flags);

        // GetMenuItemCount
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        // GetMenuItemInfo
        [SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "MENUITEMINFO.dwTypeData")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMenuItemInfo(IntPtr hmenu, int item, bool byPos, [In, Out] MENUITEMINFO lpmii);

        // GetSystemMenu
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool revert);

        // GetWindowDC
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        // GetWindowLong
        public static IntPtr GetWindowLong(IntPtr hWnd, int index)
        {
            return (IntPtr.Size == 4)
                ? GetWindowLong32(hWnd, index)
                : GetWindowLongPtr64(hWnd, index);
        }
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "return")]
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLong32(IntPtr hWnd, int index);
        // 
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int index);

        // GetWindowPlacement
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT placement);

        // GetWindowRect
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref RECT rect);

        // InsertMenuItem
        [SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "MENUITEMINFO.dwTypeData")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InsertMenuItem(IntPtr hmenu, int item, bool fByPosition, [In] MENUITEMINFO lpmi);

        // MapWindowPoints
        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, [MarshalAs(UnmanagedType.U4)] int cPoints);
        // 
        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref POINT pt, [MarshalAs(UnmanagedType.U4)] int cPoints);

        // MoveWindow
        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

        // PostMessage
        [DllImport("user32", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "3")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, int wParam, ref int lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "3")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, int wParam, POINTS lParam);

        // RegisterWindowMessage
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage([MarshalAs(UnmanagedType.LPWStr)] string message);

        // ReleaseCapture
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReleaseCapture();

        // ReleaseDC
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);

        // RemoveMenu
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

        // SelectObject
        [DllImport("gdi32", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SelectObject([In] IntPtr hDC, [In] IntPtr hObject);

        // SendMessage
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "return")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "3")]
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "return")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "return")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref int lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "return")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "return")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lParam);
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "3")]
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "return")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, POINTS lParam);

        // SetMenuDefaultItem
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetMenuDefaultItem(IntPtr hMenu, int uItem, bool fByPos);

        // SetMenuItemInfo
        [SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "MENUITEMINFO.dwTypeData")]
        [DllImport("user32", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetMenuItemInfo(IntPtr hMenu, int uItem, bool fByPosition, [In] MENUITEMINFO lpmii);

        // SetWindowLong
        public static IntPtr SetWindowLong(IntPtr hWnd, int index, IntPtr newLong)
        {
            return (IntPtr.Size == 4)
                ? SetWindowLong32(hWnd, index, newLong)
                : SetWindowLongPtr64(hWnd, index, newLong);
        }
        // 
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "return")]
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        private static extern IntPtr SetWindowLong32(IntPtr hWnd, int index, IntPtr newLong);
        // 
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        [DllImport("user32", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int index, IntPtr newLong);

        // SetWindowPlacement
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT placement);

        // SetWindowPos
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

        // SetWindowTheme
        [DllImport("uxtheme", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        // ShowWindow
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // TrackMouseEvent
        [DllImport("user32", ExactSpelling = true)]
        public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT lpEventTrack);

        // TrackPopupMenuEx
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int TrackPopupMenuEx(IntPtr hMenu, int flags, int x, int y, IntPtr hWnd, TPMPARAMS tpm);

        // WindowFromPoint
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "0")]
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr WindowFromPoint(POINT pt);

        // UpdateLayeredWindow
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "6")]
        [DllImport("user32", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, COLORREF crKey, [In] ref BLENDFUNCTION pblend, int dwFlags);
    }
}
