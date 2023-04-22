using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemasterForms
{
    internal static partial class NativeMethods // Constants
    {
        #region ## DWM enums
        public enum DWMNCRENDERINGPOLICY : int
        {
            DWMNCRP_USEWINDOWSTYLE, // Enable/disable non-client rendering based on window style
            DWMNCRP_DISABLED,       // Disabled non-client rendering; window style is ignored
            DWMNCRP_ENABLED,        // Enabled non-client rendering; window style is ignored
            DWMNCRP_LAST
        }

        public enum DWMWINDOWATTRIBUTE : int
        {
            DWMWA_NCRENDERING_ENABLED = 1,                  // [get] Is non-client rendering enabled/disabled
            DWMWA_NCRENDERING_POLICY,                       // [set] DWMNCRENDERINGPOLICY - Non-client rendering policy
            DWMWA_TRANSITIONS_FORCEDISABLED,                // [set] Potentially enable/forcibly disable transitions
            DWMWA_ALLOW_NCPAINT,                            // [set] Allow contents rendered in the non-client area to be visible on the DWM-drawn frame.
            DWMWA_CAPTION_BUTTON_BOUNDS,                    // [get] Bounds of the caption button area in window-relative space.
            DWMWA_NONCLIENT_RTL_LAYOUT,                     // [set] Is non-client content RTL mirrored
            DWMWA_FORCE_ICONIC_REPRESENTATION,              // [set] Force this window to display iconic thumbnails.
            DWMWA_FLIP3D_POLICY,                            // [set] Designates how Flip3D will treat the window.
            DWMWA_EXTENDED_FRAME_BOUNDS,                    // [get] Gets the extended frame bounds rectangle in screen space
            DWMWA_HAS_ICONIC_BITMAP,                        // [set] Indicates an available bitmap when there is no better thumbnail representation.
            DWMWA_DISALLOW_PEEK,                            // [set] Don't invoke Peek on the window.
            DWMWA_EXCLUDED_FROM_PEEK,                       // [set] LivePreview exclusion information
            DWMWA_CLOAK,                                    // [set] Cloak or uncloak the window
            DWMWA_CLOAKED,                                  // [get] Gets the cloaked state of the window
            DWMWA_FREEZE_REPRESENTATION,                    // [set] BOOL, Force this window to freeze the thumbnail without live update
            DWMWA_PASSIVE_UPDATE_MODE,                      // [set] BOOL, Updates the window only when desktop composition runs for other reasons
            DWMWA_USE_HOSTBACKDROPBRUSH,                    // [set] BOOL, Allows the use of host backdrop brushes for the window.
            DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19, // https://github.com/AraHaan/sdk-api/blob/c19f1c8a148b930444dce998d3c717c8fb7751e1/sdk-api-src/content/dwmapi/ne-dwmapi-dwmwindowattribute.md
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,             // [set] BOOL, Allows a window to either use the accent color, or dark, according to the user Color Mode preferences.
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,            // [set] WINDOW_CORNER_PREFERENCE, Controls the policy that rounds top-level window corners
            DWMWA_BORDER_COLOR,                             // [set] COLORREF, The color of the thin border around a top-level window
            DWMWA_CAPTION_COLOR,                            // [set] COLORREF, The color of the caption
            DWMWA_TEXT_COLOR,                               // [set] COLORREF, The color of the caption text
            DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,           // [get] UINT, width of the visible border around a thick frame window
            DWMWA_LAST
        }

        public enum DWM_WINDOW_CORNER_PREFERENCE : int
        {
            DWMWCP_DEFAULT    = 0,  // Let the system decide whether or not to round window corners
            DWMWCP_DONOTROUND = 1,  // Never round window corners
            DWMWCP_ROUND      = 2,  // Round the corners if appropriate
            DWMWCP_ROUNDSMALL = 3   // Round the corners if appropriate, with a small radius
        }
        #endregion DWM enums

        #region ## * (BitBlt codes)
        public const int
        SRCCOPY        = 0x00CC0020,
        SRCPAINT       = 0x00EE0086,
        SRCAND         = 0x008800C6,
        SRCINVERT      = 0x00660046,
        SRCERASE       = 0x00440328,
        NOTSRCCOPY     = 0x00330008,
        NOTSRCERASE    = 0x001100A6,
        MERGECOPY      = 0x00C000CA,
        MERGEPAINT     = 0x00BB0226,
        PATCOPY        = 0x00F00021,
        PATPAINT       = 0x00FB0A09,
        PATINVERT      = 0x005A0049,
        DSTINVERT      = 0x00550009,
        BLACKNESS      = 0x00000042,
        WHITENESS      = 0x00FF0062,
        NOMIRRORBITMAP = unchecked((int)0x80000000),
        CAPTUREBLT     = 0x40000000;
        #endregion *

        #region ## AB*
        public const int
        ABE_LEFT             = 0,
        ABE_TOP              = 1,
        ABE_RIGHT            = 2,
        ABE_BOTTOM           = 3,
        ABM_NEW              = 0x00000000,
        ABM_REMOVE           = 0x00000001,
        ABM_QUERYPOS         = 0x00000002,
        ABM_SETPOS           = 0x00000003,
        ABM_GETSTATE         = 0x00000004,
        ABM_GETTASKBARPOS    = 0x00000005,
        ABM_ACTIVATE         = 0x00000006,
        ABM_GETAUTOHIDEBAR   = 0x00000007,
        ABM_SETAUTOHIDEBAR   = 0x00000008,
        ABM_WINDOWPOSCHANGED = 0x00000009,
        ABM_SETSTATE         = 0x0000000A,
        ABM_GETAUTOHIDEBAREX = 0x0000000B,
        ABM_SETAUTOHIDEBAREX = 0x0000000C,
        ABN_STATECHANGE      = 0x0000000,
        ABN_POSCHANGED       = 0x0000001,
        ABN_FULLSCREENAPP    = 0x0000002,
        ABN_WINDOWARRANGE    = 0x0000003,
        ABS_AUTOHIDE         = 0x0000001,
        ABS_ALWAYSONTOP      = 0x0000002;
        #endregion AB*
        #region ## AC*
        public const int
        AC_SRC_OVER  = 0x00,
        AC_SRC_ALPHA = 0x01;
        #endregion AC*
        #region ## AW*
        public const int
        AW_HOR_POSITIVE = 0x00000001,
        AW_HOR_NEGATIVE = 0x00000002,
        AW_VER_POSITIVE = 0x00000004,
        AW_VER_NEGATIVE = 0x00000008,
        AW_CENTER       = 0x00000010,
        AW_HIDE         = 0x00010000,
        AW_ACTIVATE     = 0x00020000,
        AW_SLIDE        = 0x00040000,
        AW_BLEND        = 0x00080000;
        #endregion AW*
        #region ## GMDI*
        public const int
        GMDI_USEDISABLED  = 0x0001,
        GMDI_GOINTOPOPUPS = 0x0002;
        #endregion GMDI*
        #region ## GWL*
        public const int
        GWL_STYLE   = -16,
        GWL_EXSTYLE = -20;
        #endregion GWL*
        #region ## HT*
        public const int
        HTERROR       = -2,
        HTTRANSPARENT = -1,
        HTNOWHERE     = 0,
        HTCLIENT      = 1,
        HTCAPTION     = 2,
        HTSYSMENU     = 3,
        HTGROWBOX     = 4,
        HTMENU        = 5,
        HTHSCROLL     = 6,
        HTVSCROLL     = 7,
        HTMINBUTTON   = 8,
        HTMAXBUTTON   = 9,
        HTLEFT        = 10,
        HTRIGHT       = 11,
        HTTOP         = 12,
        HTTOPLEFT     = 13,
        HTTOPRIGHT    = 14,
        HTBOTTOM      = 15,
        HTBOTTOMLEFT  = 16,
        HTBOTTOMRIGHT = 17,
        HTBORDER      = 18,
        HTSIZE        = HTGROWBOX,
        HTREDUCE      = HTMINBUTTON,
        HTZOOM        = HTMAXBUTTON,
        HTSIZEFIRST   = HTLEFT,
        HTSIZELAST    = HTBOTTOMRIGHT,
        HTOBJECT      = 19,
        HTCLOSE       = 20,
        HTHELP        = 21;
        #endregion HT*
        #region ## MFS*
        public const int
        MF_INSERT          = 0x00000000,
        MF_CHANGE          = 0x00000080,
        MF_APPEND          = 0x00000100,
        MF_DELETE          = 0x00000200,
        MF_REMOVE          = 0x00001000,
        MF_BYCOMMAND       = 0x00000000,
        MF_BYPOSITION      = 0x00000400,
        MF_SEPARATOR       = 0x00000800,
        MF_ENABLED         = 0x00000000,
        MF_GRAYED          = 0x00000001,
        MF_DISABLED        = 0x00000002,
        MF_UNCHECKED       = 0x00000000,
        MF_CHECKED         = 0x00000008,
        MF_USECHECKBITMAPS = 0x00000200,
        MF_STRING          = 0x00000000,
        MF_BITMAP          = 0x00000004,
        MF_OWNERDRAW       = 0x00000100,
        MF_POPUP           = 0x00000010,
        MF_MENUBARBREAK    = 0x00000020,
        MF_MENUBREAK       = 0x00000040,
        MF_UNHILITE        = 0x00000000,
        MF_HILITE          = 0x00000080,
        MF_DEFAULT         = 0x00001000,
        MF_SYSMENU         = 0x00002000,
        MF_HELP            = 0x00004000,
        MF_RIGHTJUSTIFY    = 0x00004000,
        MF_MOUSESELECT     = 0x00008000;
        [Obsolete]
        public const int
        MF_END             = 0x00000080;
        public const int 
		MFT_STRING         = MF_STRING,
        MFT_BITMAP         = MF_BITMAP,
        MFT_MENUBARBREAK   = MF_MENUBARBREAK,
        MFT_MENUBREAK      = MF_MENUBREAK,
        MFT_OWNERDRAW      = MF_OWNERDRAW,
        MFT_RADIOCHECK     = 0x00000200,
        MFT_SEPARATOR      = MF_SEPARATOR,
        MFT_RIGHTORDER     = 0x00002000,
        MFT_RIGHTJUSTIFY   = MF_RIGHTJUSTIFY,
        MFS_GRAYED         = 0x00000003,
        MFS_DISABLED       = MFS_GRAYED,
        MFS_CHECKED        = MF_CHECKED,
        MFS_HILITE         = MF_HILITE,
        MFS_ENABLED        = MF_ENABLED,
        MFS_UNCHECKED      = MF_UNCHECKED,
        MFS_UNHILITE       = MF_UNHILITE,
        MFS_DEFAULT        = MF_DEFAULT;
        #endregion MF*
        #region ## MIIM*
        public const int 
		MIIM_STATE      = 0x00000001,
        MIIM_ID         = 0x00000002,
        MIIM_SUBMENU    = 0x00000004,
        MIIM_CHECKMARKS = 0x00000008,
        MIIM_TYPE       = 0x00000010,
        MIIM_DATA       = 0x00000020,
        MIIM_STRING     = 0x00000040,
        MIIM_BITMAP     = 0x00000080,
        MIIM_FTYPE      = 0x00000100;
        #endregion MIIM*
        #region ## RDW*
        public const int
        RDW_INVALIDATE  = 0x0001,
        RDW_ERASE       = 0x0004,
        RDW_ALLCHILDREN = 0x0080,
        RDW_ERASENOW    = 0x0200,
        RDW_UPDATENOW   = 0x0100,
        RDW_FRAME       = 0x0400;
        #endregion RDW*
        #region ## S*
        public const int 
        S_OK    = 0x00000000,
        S_FALSE = 0x00000001;
        #endregion S*
        #region ## SC*
        public const int
        SC_SIZE         = 0xF000,
        SC_MOVE         = 0xF010,
        SC_MINIMIZE     = 0xF020,
        SC_MAXIMIZE     = 0xF030,
        SC_NEXTWINDOW   = 0xF040,
        SC_PREVWINDOW   = 0xF050,
        SC_CLOSE        = 0xF060,
        SC_VSCROLL      = 0xF070,
        SC_HSCROLL      = 0xF080,
        SC_MOUSEMENU    = 0xF090,
        SC_KEYMENU      = 0xF100,
        SC_ARRANGE      = 0xF110,
        SC_RESTORE      = 0xF120,
        SC_TASKLIST     = 0xF130,
        SC_SCREENSAVE   = 0xF140,
        SC_HOTKEY       = 0xF150,
        SC_DEFAULT      = 0xF160,
        SC_MONITORPOWER = 0xF170,
        SC_CONTEXTHELP  = 0xF180,
        SC_SEPARATOR    = 0xF00F;
        #endregion SC*
        #region ## SIZE*
        public const int
        SIZE_RESTORED  = 0,
        SIZE_MINIMIZED = 1,
        SIZE_MAXIMIZED = 2;
        #endregion SIZE*
        #region ## SPI*
        public const int
        SPI_GETBEEP = 0x0001,
        SPI_SETBEEP = 0x0002,
        SPI_GETMOUSE = 0x0003,
        SPI_SETMOUSE = 0x0004,
        SPI_GETBORDER = 0x0005,
        SPI_SETBORDER = 0x0006,
        SPI_GETKEYBOARDSPEED = 0x000A,
        SPI_SETKEYBOARDSPEED = 0x000B,
        SPI_LANGDRIVER = 0x000C,
        SPI_ICONHORIZONTALSPACING = 0x000D,
        SPI_GETSCREENSAVETIMEOUT = 0x000E,
        SPI_SETSCREENSAVETIMEOUT = 0x000F,
        SPI_GETSCREENSAVEACTIVE = 0x0010,
        SPI_SETSCREENSAVEACTIVE = 0x0011,
        SPI_GETGRIDGRANULARITY = 0x0012,
        SPI_SETGRIDGRANULARITY = 0x0013,
        SPI_SETDESKWALLPAPER = 0x0014,
        SPI_SETDESKPATTERN = 0x0015,
        SPI_GETKEYBOARDDELAY = 0x0016,
        SPI_SETKEYBOARDDELAY = 0x0017,
        SPI_ICONVERTICALSPACING = 0x0018,
        SPI_GETICONTITLEWRAP = 0x0019,
        SPI_SETICONTITLEWRAP = 0x001A,
        SPI_GETMENUDROPALIGNMENT = 0x001B,
        SPI_SETMENUDROPALIGNMENT = 0x001C,
        SPI_SETDOUBLECLKWIDTH = 0x001D,
        SPI_SETDOUBLECLKHEIGHT = 0x001E,
        SPI_GETICONTITLELOGFONT = 0x001F,
        SPI_SETDOUBLECLICKTIME = 0x0020,
        SPI_SETMOUSEBUTTONSWAP = 0x0021,
        SPI_SETICONTITLELOGFONT = 0x0022,
        SPI_GETFASTTASKSWITCH = 0x0023,
        SPI_SETFASTTASKSWITCH = 0x0024,
        SPI_SETDRAGFULLWINDOWS = 0x0025,
        SPI_GETDRAGFULLWINDOWS = 0x0026,
        SPI_GETNONCLIENTMETRICS = 0x0029,
        SPI_SETNONCLIENTMETRICS = 0x002A,
        SPI_GETMINIMIZEDMETRICS = 0x002B,
        SPI_SETMINIMIZEDMETRICS = 0x002C,
        SPI_GETICONMETRICS = 0x002D,
        SPI_SETICONMETRICS = 0x002E,
        SPI_SETWORKAREA = 0x002F,
        SPI_GETWORKAREA = 0x0030,
        SPI_SETPENWINDOWS = 0x0031,
        SPI_GETHIGHCONTRAST = 0x0042,
        SPI_SETHIGHCONTRAST = 0x0043,
        SPI_GETKEYBOARDPREF = 0x0044,
        SPI_SETKEYBOARDPREF = 0x0045,
        SPI_GETSCREENREADER = 0x0046,
        SPI_SETSCREENREADER = 0x0047,
        SPI_GETANIMATION = 0x0048,
        SPI_SETANIMATION = 0x0049,
        SPI_GETFONTSMOOTHING = 0x004A,
        SPI_SETFONTSMOOTHING = 0x004B,
        SPI_SETDRAGWIDTH = 0x004C,
        SPI_SETDRAGHEIGHT = 0x004D,
        SPI_SETHANDHELD = 0x004E,
        SPI_GETLOWPOWERTIMEOUT = 0x004F,
        SPI_GETPOWEROFFTIMEOUT = 0x0050,
        SPI_SETLOWPOWERTIMEOUT = 0x0051,
        SPI_SETPOWEROFFTIMEOUT = 0x0052,
        SPI_GETLOWPOWERACTIVE = 0x0053,
        SPI_GETPOWEROFFACTIVE = 0x0054,
        SPI_SETLOWPOWERACTIVE = 0x0055,
        SPI_SETPOWEROFFACTIVE = 0x0056,
        SPI_SETICONS = 0x0058,
        SPI_GETDEFAULTINPUTLANG = 0x0059,
        SPI_SETDEFAULTINPUTLANG = 0x005A,
        SPI_SETLANGTOGGLE = 0x005B,
        SPI_GETWINDOWSEXTENSION = 0x005C,
        SPI_SETMOUSETRAILS = 0x005D,
        SPI_GETMOUSETRAILS = 0x005E,
        SPI_SCREENSAVERRUNNING = 0x0061,
        SPI_GETFILTERKEYS = 0x0032,
        SPI_SETFILTERKEYS = 0x0033,
        SPI_GETTOGGLEKEYS = 0x0034,
        SPI_SETTOGGLEKEYS = 0x0035,
        SPI_GETMOUSEKEYS = 0x0036,
        SPI_SETMOUSEKEYS = 0x0037,
        SPI_GETSHOWSOUNDS = 0x0038,
        SPI_SETSHOWSOUNDS = 0x0039,
        SPI_GETSTICKYKEYS = 0x003A,
        SPI_SETSTICKYKEYS = 0x003B,
        SPI_GETACCESSTIMEOUT = 0x003C,
        SPI_SETACCESSTIMEOUT = 0x003D,
        SPI_GETSERIALKEYS = 0x003E,
        SPI_SETSERIALKEYS = 0x003F,
        SPI_GETSOUNDSENTRY = 0x0040,
        SPI_SETSOUNDSENTRY = 0x0041,
        SPI_GETSNAPTODEFBUTTON = 0x005F,
        SPI_SETSNAPTODEFBUTTON = 0x0060,
        SPI_GETMOUSEHOVERWIDTH = 0x0062,
        SPI_SETMOUSEHOVERWIDTH = 0x0063,
        SPI_GETMOUSEHOVERHEIGHT = 0x0064,
        SPI_SETMOUSEHOVERHEIGHT = 0x0065,
        SPI_GETMOUSEHOVERTIME = 0x0066,
        SPI_SETMOUSEHOVERTIME = 0x0067,
        SPI_GETWHEELSCROLLLINES = 0x0068,
        SPI_SETWHEELSCROLLLINES = 0x0069,
        SPI_GETMENUSHOWDELAY = 0x006A,
        SPI_SETMENUSHOWDELAY = 0x006B,
        SPI_GETSHOWIMEUI = 0x006E,
        SPI_SETSHOWIMEUI = 0x006F,
        SPI_GETMOUSESPEED = 0x0070,
        SPI_SETMOUSESPEED = 0x0071,
        SPI_GETSCREENSAVERRUNNING = 0x0072,
        SPI_GETDESKWALLPAPER = 0x0073,
        SPI_GETACTIVEWINDOWTRACKING = 0x1000,
        SPI_SETACTIVEWINDOWTRACKING = 0x1001,
        SPI_GETMENUANIMATION = 0x1002,
        SPI_SETMENUANIMATION = 0x1003,
        SPI_GETCOMBOBOXANIMATION = 0x1004,
        SPI_SETCOMBOBOXANIMATION = 0x1005,
        SPI_GETLISTBOXSMOOTHSCROLLING = 0x1006,
        SPI_SETLISTBOXSMOOTHSCROLLING = 0x1007,
        SPI_GETGRADIENTCAPTIONS = 0x1008,
        SPI_SETGRADIENTCAPTIONS = 0x1009,
        SPI_GETKEYBOARDCUES = 0x100A,
        SPI_SETKEYBOARDCUES = 0x100B,
        SPI_GETMENUUNDERLINES = SPI_GETKEYBOARDCUES,
        SPI_SETMENUUNDERLINES = SPI_SETKEYBOARDCUES,
        SPI_GETACTIVEWNDTRKZORDER = 0x100C,
        SPI_SETACTIVEWNDTRKZORDER = 0x100D,
        SPI_GETHOTTRACKING = 0x100E,
        SPI_SETHOTTRACKING = 0x100F,
        SPI_GETMENUFADE = 0x1012,
        SPI_SETMENUFADE = 0x1013,
        SPI_GETSELECTIONFADE = 0x1014,
        SPI_SETSELECTIONFADE = 0x1015,
        SPI_GETTOOLTIPANIMATION = 0x1016,
        SPI_SETTOOLTIPANIMATION = 0x1017,
        SPI_GETTOOLTIPFADE = 0x1018,
        SPI_SETTOOLTIPFADE = 0x1019,
        SPI_GETCURSORSHADOW = 0x101A,
        SPI_SETCURSORSHADOW = 0x101B,
        SPI_GETMOUSESONAR = 0x101C,
        SPI_SETMOUSESONAR = 0x101D,
        SPI_GETMOUSECLICKLOCK = 0x101E,
        SPI_SETMOUSECLICKLOCK = 0x101F,
        SPI_GETMOUSEVANISH = 0x1020,
        SPI_SETMOUSEVANISH = 0x1021,
        SPI_GETFLATMENU = 0x1022,
        SPI_SETFLATMENU = 0x1023,
        SPI_GETDROPSHADOW = 0x1024,
        SPI_SETDROPSHADOW = 0x1025,
        SPI_GETBLOCKSENDINPUTRESETS = 0x1026,
        SPI_SETBLOCKSENDINPUTRESETS = 0x1027,
        SPI_GETUIEFFECTS = 0x103E,
        SPI_SETUIEFFECTS = 0x103F,
        SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000,
        SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001,
        SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002,
        SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003,
        SPI_GETFOREGROUNDFLASHCOUNT = 0x2004,
        SPI_SETFOREGROUNDFLASHCOUNT = 0x2005,
        SPI_GETCARETWIDTH = 0x2006,
        SPI_SETCARETWIDTH = 0x2007,
        SPI_GETMOUSECLICKLOCKTIME = 0x2008,
        SPI_SETMOUSECLICKLOCKTIME = 0x2009,
        SPI_GETFONTSMOOTHINGTYPE = 0x200A,
        SPI_SETFONTSMOOTHINGTYPE = 0x200B,
        SPI_GETFONTSMOOTHINGCONTRAST = 0x200C,
        SPI_SETFONTSMOOTHINGCONTRAST = 0x200D,
        SPI_GETFOCUSBORDERWIDTH = 0x200E,
        SPI_SETFOCUSBORDERWIDTH = 0x200F,
        SPI_GETFOCUSBORDERHEIGHT = 0x2010,
        SPI_SETFOCUSBORDERHEIGHT = 0x2011,
        SPI_GETFONTSMOOTHINGORIENTATION = 0x2012,
        SPI_SETFONTSMOOTHINGORIENTATION = 0x2013;
        #endregion SPI*
        #region ## SW*
        public const int
        SW_HIDE            = 0,
        SW_NORMAL          = 1,
        SW_SHOWNORMAL      = 1,
        SW_SHOWMINIMIZED   = 2,
        SW_SHOWMAXIMIZED   = 3,
        SW_MAXIMIZE        = 3,
        SW_SHOWNOACTIVATE  = 4,
        SW_SHOW            = 5,
        SW_MINIMIZE        = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA          = 8,
        SW_RESTORE         = 9,
        SW_MAX             = 10;
        #endregion SW*
        #region ## SWP*
        public const int
        SWP_NOSIZE         = 0x0001,
        SWP_NOMOVE         = 0x0002,
        SWP_NOZORDER       = 0x0004,
        SWP_NOREDRAW       = 0x0008,
        SWP_NOACTIVATE     = 0x0010,
        SWP_FRAMECHANGED   = 0x0020,
        SWP_SHOWWINDOW     = 0x0040,
        SWP_HIDEWINDOW     = 0x0080,
        SWP_NOCOPYBITS     = 0x0100,
        SWP_NOREPOSITION   = 0x0200,
        SWP_NOOWNERZORDER  = 0x0200,
        SWP_NOSENDCHANGING = 0x0400,
        SWP_DEFERERASE     = 0x2000,
        SWP_ASYNCWINDOWPOS = 0x4000;
        #endregion SWP*
        #region ## TME*
        public const int
        TME_CANCEL    = unchecked((int)0x80000000),
        TME_HOVER     = 0x00000001,
        TME_LEAVE     = 0x00000002,
        TME_NONCLIENT = 0x00000010,
        TME_QUERY     = 0x40000000;
        #endregion TME*
        #region ## TPM*
        public const int
        TPM_LEFTBUTTON      = 0x0000,
        TPM_RIGHTBUTTON     = 0x0002,
        TPM_LEFTALIGN       = 0x0000,
        TPM_CENTERALIGN     = 0x0004,
        TPM_RIGHTALIGN      = 0x0008,
        TPM_TOPALIGN        = 0x0000,
        TPM_VCENTERALIGN    = 0x0010,
        TPM_BOTTOMALIGN     = 0x0020,
        TPM_HORIZONTAL      = 0x0000,
        TPM_VERTICAL        = 0x0040,
        TPM_NONOTIFY        = 0x0080,
        TPM_RETURNCMD       = 0x0100,
        TPM_RECURSE         = 0x0001,
        TPM_HORPOSANIMATION = 0x0400,
        TPM_HORNEGANIMATION = 0x0800,
        TPM_VERPOSANIMATION = 0x1000,
        TPM_VERNEGANIMATION = 0x2000,
        TPM_NOANIMATION     = 0x4000,
        TPM_LAYOUTRTL       = 0x8000,
        TPM_WORKAREA        = 0x10000;
        #endregion TPM*
        #region ## ULW*
        public const int 
        ULW_COLORKEY    = 0x00000001,
        ULW_ALPHA       = 0x00000002,
        ULW_OPAQUE      = 0x00000004,
        ULW_EX_NORESIZE = 0x00000008;
        #endregion ULW*
        #region ## WA*
        public const int
        WA_INACTIVE    = 0,
        WA_ACTIVE      = 1,
        WA_CLICKACTIVE = 2;
        #endregion WA*
        #region ## WM*
        public const int
        WM_NULL = 0x0000,
        WM_CREATE = 0x0001,
        WM_DELETEITEM = 0x002D,
        WM_DESTROY = 0x0002,
        WM_MOVE = 0x0003,
        WM_SIZE = 0x0005,
        WM_ACTIVATE = 0x0006,
        WM_SETFOCUS = 0x0007,
        WM_KILLFOCUS = 0x0008,
        WM_ENABLE = 0x000A,
        WM_SETREDRAW = 0x000B,
        WM_SETTEXT = 0x000C,
        WM_GETTEXT = 0x000D,
        WM_GETTEXTLENGTH = 0x000E,
        WM_PAINT = 0x000F,
        WM_CLOSE = 0x0010,
        WM_QUERYENDSESSION = 0x0011,
        WM_QUIT = 0x0012,
        WM_QUERYOPEN = 0x0013,
        WM_ERASEBKGND = 0x0014,
        WM_SYSCOLORCHANGE = 0x0015,
        WM_ENDSESSION = 0x0016,
        WM_SHOWWINDOW = 0x0018,
        WM_WININICHANGE = 0x001A,
        WM_SETTINGCHANGE = 0x001A,
        WM_DEVMODECHANGE = 0x001B,
        WM_ACTIVATEAPP = 0x001C,
        WM_FONTCHANGE = 0x001D,
        WM_TIMECHANGE = 0x001E,
        WM_CANCELMODE = 0x001F,
        WM_SETCURSOR = 0x0020,
        WM_MOUSEACTIVATE = 0x0021,
        WM_CHILDACTIVATE = 0x0022,
        WM_QUEUESYNC = 0x0023,
        WM_GETMINMAXINFO = 0x0024,
        WM_PAINTICON = 0x0026,
        WM_ICONERASEBKGND = 0x0027,
        WM_NEXTDLGCTL = 0x0028,
        WM_SPOOLERSTATUS = 0x002A,
        WM_DRAWITEM = 0x002B,
        WM_MEASUREITEM = 0x002C,
        WM_VKEYTOITEM = 0x002E,
        WM_CHARTOITEM = 0x002F,
        WM_SETFONT = 0x0030,
        WM_GETFONT = 0x0031,
        WM_SETHOTKEY = 0x0032,
        WM_GETHOTKEY = 0x0033,
        WM_QUERYDRAGICON = 0x0037,
        WM_COMPAREITEM = 0x0039,
        WM_GETOBJECT = 0x003D,
        WM_COMPACTING = 0x0041,
        WM_COMMNOTIFY = 0x0044,
        WM_WINDOWPOSCHANGING = 0x0046,
        WM_WINDOWPOSCHANGED = 0x0047,
        WM_POWER = 0x0048,
        WM_COPYDATA = 0x004A,
        WM_CANCELJOURNAL = 0x004B,
        WM_NOTIFY = 0x004E,
        WM_INPUTLANGCHANGEREQUEST = 0x0050,
        WM_INPUTLANGCHANGE = 0x0051,
        WM_TCARD = 0x0052,
        WM_HELP = 0x0053,
        WM_USERCHANGED = 0x0054,
        WM_NOTIFYFORMAT = 0x0055,
        WM_CONTEXTMENU = 0x007B,
        WM_STYLECHANGING = 0x007C,
        WM_STYLECHANGED = 0x007D,
        WM_DISPLAYCHANGE = 0x007E,
        WM_GETICON = 0x007F,
        WM_SETICON = 0x0080,
        WM_NCCREATE = 0x0081,
        WM_NCDESTROY = 0x0082,
        WM_NCCALCSIZE = 0x0083,
        WM_NCHITTEST = 0x0084,
        WM_NCPAINT = 0x0085,
        WM_NCACTIVATE = 0x0086,
        WM_GETDLGCODE = 0x0087,
        WM_NCMOUSEMOVE = 0x00A0,
        WM_NCMOUSEHOVER = 0x02A0,
        WM_NCMOUSELEAVE = 0x02A2,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_NCLBUTTONUP = 0x00A2,
        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_NCRBUTTONDOWN = 0x00A4,
        WM_NCRBUTTONUP = 0x00A5,
        WM_NCRBUTTONDBLCLK = 0x00A6,
        WM_NCMBUTTONDOWN = 0x00A7,
        WM_NCMBUTTONUP = 0x00A8,
        WM_NCMBUTTONDBLCLK = 0x00A9,
        WM_NCXBUTTONDOWN = 0x00AB,
        WM_NCXBUTTONUP = 0x00AC,
        WM_NCXBUTTONDBLCLK = 0x00AD,
        WM_KEYFIRST = 0x0100,
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_CHAR = 0x0102,
        WM_DEADCHAR = 0x0103,
        WM_CTLCOLOR = 0x0019,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
        WM_SYSCHAR = 0x0106,
        WM_SYSDEADCHAR = 0x0107,
        WM_KEYLAST = 0x0108,
        WM_IME_STARTCOMPOSITION = 0x010D,
        WM_IME_ENDCOMPOSITION = 0x010E,
        WM_IME_COMPOSITION = 0x010F,
        WM_IME_KEYLAST = 0x010F,
        WM_INITDIALOG = 0x0110,
        WM_COMMAND = 0x0111,
        WM_SYSCOMMAND = 0x0112,
        WM_TIMER = 0x0113,
        WM_HSCROLL = 0x0114,
        WM_VSCROLL = 0x0115,
        WM_INITMENU = 0x0116,
        WM_INITMENUPOPUP = 0x0117,
        WM_MENUSELECT = 0x011F,
        WM_MENUCHAR = 0x0120,
        WM_ENTERIDLE = 0x0121,
        WM_UNINITMENUPOPUP = 0x0125,
        WM_CHANGEUISTATE = 0x0127,
        WM_UPDATEUISTATE = 0x0128,
        WM_QUERYUISTATE = 0x0129,
        WM_CTLCOLORMSGBOX = 0x0132,
        WM_CTLCOLOREDIT = 0x0133,
        WM_CTLCOLORLISTBOX = 0x0134,
        WM_CTLCOLORBTN = 0x0135,
        WM_CTLCOLORDLG = 0x0136,
        WM_CTLCOLORSCROLLBAR = 0x0137,
        WM_CTLCOLORSTATIC = 0x0138,
        WM_MOUSEFIRST = 0x0200,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_XBUTTONDOWN = 0x020B,
        WM_XBUTTONUP = 0x020C,
        WM_XBUTTONDBLCLK = 0x020D,
        WM_MOUSEWHEEL = 0x020A,
        WM_MOUSELAST = 0x020A,
        WM_PARENTNOTIFY = 0x0210,
        WM_ENTERMENULOOP = 0x0211,
        WM_EXITMENULOOP = 0x0212,
        WM_NEXTMENU = 0x0213,
        WM_SIZING = 0x0214,
        WM_CAPTURECHANGED = 0x0215,
        WM_MOVING = 0x0216,
        WM_POWERBROADCAST = 0x0218,
        WM_DEVICECHANGE = 0x0219,
        WM_IME_SETCONTEXT = 0x0281,
        WM_IME_NOTIFY = 0x0282,
        WM_IME_CONTROL = 0x0283,
        WM_IME_COMPOSITIONFULL = 0x0284,
        WM_IME_SELECT = 0x0285,
        WM_IME_CHAR = 0x0286,
        WM_IME_KEYDOWN = 0x0290,
        WM_IME_KEYUP = 0x0291,
        WM_MDICREATE = 0x0220,
        WM_MDIDESTROY = 0x0221,
        WM_MDIACTIVATE = 0x0222,
        WM_MDIRESTORE = 0x0223,
        WM_MDINEXT = 0x0224,
        WM_MDIMAXIMIZE = 0x0225,
        WM_MDITILE = 0x0226,
        WM_MDICASCADE = 0x0227,
        WM_MDIICONARRANGE = 0x0228,
        WM_MDIGETACTIVE = 0x0229,
        WM_MDISETMENU = 0x0230,
        WM_ENTERSIZEMOVE = 0x0231,
        WM_EXITSIZEMOVE = 0x0232,
        WM_DROPFILES = 0x0233,
        WM_MDIREFRESHMENU = 0x0234,
        WM_MOUSEHOVER = 0x02A1,
        WM_MOUSELEAVE = 0x02A3,
        WM_DPICHANGED = 0x02E0,
        WM_GETDPISCALEDSIZE = 0x02e1,
        WM_DPICHANGED_BEFOREPARENT = 0x02E2,
        WM_DPICHANGED_AFTERPARENT = 0x02E3,
        WM_CUT = 0x0300,
        WM_COPY = 0x0301,
        WM_PASTE = 0x0302,
        WM_CLEAR = 0x0303,
        WM_UNDO = 0x0304,
        WM_RENDERFORMAT = 0x0305,
        WM_RENDERALLFORMATS = 0x0306,
        WM_DESTROYCLIPBOARD = 0x0307,
        WM_DRAWCLIPBOARD = 0x0308,
        WM_PAINTCLIPBOARD = 0x0309,
        WM_VSCROLLCLIPBOARD = 0x030A,
        WM_SIZECLIPBOARD = 0x030B,
        WM_ASKCBFORMATNAME = 0x030C,
        WM_CHANGECBCHAIN = 0x030D,
        WM_HSCROLLCLIPBOARD = 0x030E,
        WM_QUERYNEWPALETTE = 0x030F,
        WM_PALETTEISCHANGING = 0x0310,
        WM_PALETTECHANGED = 0x0311,
        WM_HOTKEY = 0x0312,
        WM_PRINT = 0x0317,
        WM_PRINTCLIENT = 0x0318,
        WM_THEMECHANGED = 0x031A,
        WM_DWMNCRENDERINGCHANGED = 0x031F,
        WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320,
        WM_HANDHELDFIRST = 0x0358,
        WM_HANDHELDLAST = 0x035F,
        WM_AFXFIRST = 0x0360,
        WM_AFXLAST = 0x037F,
        WM_PENWINFIRST = 0x0380,
        WM_PENWINLAST = 0x038F,
        WM_APP = unchecked(0x8000),
        WM_USER = 0x0400,
        WM_REFLECT = WM_USER + 0x1C00;
        #endregion WM*
        #region ## WS*
        public const int
        WS_OVERLAPPED       = 0x00000000,
        WS_POPUP            = unchecked((int)0x80000000),
        WS_CHILD            = 0x40000000,
        WS_MINIMIZE         = 0x20000000,
        WS_VISIBLE          = 0x10000000,
        WS_DISABLED         = 0x08000000,
        WS_CLIPSIBLINGS     = 0x04000000,
        WS_CLIPCHILDREN     = 0x02000000,
        WS_MAXIMIZE         = 0x01000000,
        WS_CAPTION          = 0x00C00000,
        WS_BORDER           = 0x00800000,
        WS_DLGFRAME         = 0x00400000,
        WS_VSCROLL          = 0x00200000,
        WS_HSCROLL          = 0x00100000,
        WS_SYSMENU          = 0x00080000,
        WS_THICKFRAME       = 0x00040000,
        WS_TABSTOP          = 0x00010000,
        WS_MINIMIZEBOX      = 0x00020000,
        WS_MAXIMIZEBOX      = 0x00010000,
        WS_TILED            = WS_OVERLAPPED,
        WS_ICONIC           = WS_MINIMIZE,
        WS_SIZEBOX          = WS_THICKFRAME,
        WS_TILEDWINDOW      = WS_OVERLAPPEDWINDOW,
        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
        WS_POPUPWINDOW      = WS_POPUP | WS_BORDER | WS_SYSMENU,
        WS_CHILDWINDOW      = WS_CHILD;
        #endregion WS*
        #region ## WS_EX*
        public const int
        WS_EX_DLGMODALFRAME       = 0x00000001,
        WS_EX_MDICHILD            = 0x00000040,
        WS_EX_TOOLWINDOW          = 0x00000080,
        WS_EX_WINDOWEDGE          = 0x00000100,
        WS_EX_CLIENTEDGE          = 0x00000200,
        WS_EX_CONTEXTHELP         = 0x00000400,
        WS_EX_RIGHT               = 0x00001000,
        WS_EX_LEFT                = 0x00000000,
        WS_EX_RTLREADING          = 0x00002000,
        WS_EX_LEFTSCROLLBAR       = 0x00004000,
        WS_EX_CONTROLPARENT       = 0x00010000,
        WS_EX_STATICEDGE          = 0x00020000,
        WS_EX_APPWINDOW           = 0x00040000,
        WS_EX_LAYERED             = 0x00080000,
        WS_EX_TOPMOST             = 0x00000008,
        WS_EX_NOREDIRECTIONBITMAP = 0x00200000,
        WS_EX_LAYOUTRTL           = 0x00400000,
        WS_EX_NOINHERITLAYOUT     = 0x00100000,
        WS_EX_NOACTIVATE          = 0x08000000;
        #endregion WS_EX*
        #region ## WVR*
        public const int
        WVR_ALIGNTOP    = 0x0010,
        WVR_ALIGNLEFT   = 0x0020,
        WVR_ALIGNBOTTOM = 0x0040,
        WVR_ALIGNRIGHT  = 0x0080,
        WVR_HREDRAW     = 0x0100,
        WVR_VREDRAW     = 0x0200,
        WVR_REDRAW      = WVR_HREDRAW | WVR_VREDRAW,
        WVR_VALIDRECTS  = 0x0400;
        #endregion WVR*
        #region ## XBUTTON*
        public const int
        XBUTTON1 = 0x0001,
        XBUTTON2 = 0x0002;
        #endregion XBUTTON?
    }
}
