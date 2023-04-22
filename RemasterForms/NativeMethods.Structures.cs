using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#pragma warning disable IDE1006 // Naming rule violation

namespace RemasterForms
{
    internal static partial class NativeMethods // Structures
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct APPBARDATA
        {
            public int    cbSize;
            public IntPtr hWnd;
            public int    uCallbackMessage;
            public int    uEdge;
            public RECT   rc;
            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;

            public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
            {
                BlendOp             = op;
                BlendFlags          = flags;
                SourceConstantAlpha = alpha;
                AlphaFormat         = format;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COLORREF
        {
            public byte R;
            public byte G;
            public byte B;

            public static implicit operator COLORREF(int color)
            {
                return new COLORREF
                {
                    R = (byte)color,
                    G = (byte)(color >> 8),
                    B = (byte)(color >> 16)
                };
            }

            public static implicit operator COLORREF(Color color)
            {
                return new COLORREF { R = color.R, G = color.G, B = color.B };
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth   ;
            public int cxRightWidth  ;
            public int cyTopHeight   ;
            public int cyBottomHeight;

            public MARGINS(int left, int right, int top, int bottom)
            {
                cxLeftWidth    = left  ;
                cxRightWidth   = right ;
                cyTopHeight    = top   ;
                cyBottomHeight = bottom;
            }

            public MARGINS(int all) : this(all, all, all, all) { }

            public static readonly MARGINS Empty = new MARGINS(0);

            public static implicit operator MARGINS(Padding p)
            {
                return new MARGINS()
                {
                    cxLeftWidth    = p.Left,
                    cxRightWidth   = p.Right,
                    cyTopHeight    = p.Top,
                    cyBottomHeight = p.Bottom
                };
            }

            public static implicit operator Padding(MARGINS m)
            {
                return new Padding(m.cxLeftWidth, m.cyTopHeight, m.cxRightWidth, m.cyBottomHeight);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MENUITEMINFO
        {
            public int    cbSize = Marshal.SizeOf(typeof(MENUITEMINFO));
            public int    fMask;
            public int    fType;
            public int    fState;
            public int    wID;
            public IntPtr hSubMenu;
            public IntPtr hbmpChecked;
            public IntPtr hbmpUnchecked;
            public IntPtr dwItemData;
            public string dwTypeData = null;
            public int    cch;
            public IntPtr hbmpItem;

            public MENUITEMINFO() { }

            public MENUITEMINFO(int pfMask)
            {
                fMask = pfMask;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;

            public static MINMAXINFO FromLParam(Message m)
            {
                return (MINMAXINFO)m.GetLParam(typeof(MINMAXINFO));
            }

            public void ToLParam(Message m)
            {
                Marshal.StructureToPtr(this, m.LParam, false);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public RECT[] rgrc;
            public WINDOWPOS wp;

            public static NCCALCSIZE_PARAMS FromLParam(Message m)
            {
                return (NCCALCSIZE_PARAMS)m.GetLParam(typeof(NCCALCSIZE_PARAMS));
            }

            public void ToLParam(Message m)
            {
                Marshal.StructureToPtr(this, m.LParam, false);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public static readonly POINT Empty = new POINT(0, 0);

            public static implicit operator Point(POINT p)
            {
                return new Point(p.x, p.y);
            }

            public static implicit operator POINT(Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTS
        {
            public short x;
            public short y;

            public POINTS(short x, short y)
            {
                this.x = x;
                this.y = y;
            }

            public POINTS(int x, int y)
            {
                this.x = (short)x;
                this.y = (short)y;
            }

            public static readonly POINTS Empty = new POINTS(0, 0);

            public static POINTS FromLParam(IntPtr lParam)
            {
                return new POINTS((short)SignedLoWord(lParam), (short)SignedHiWord(lParam));
            }

            public IntPtr ToLParam()
            {
                return MakeLParam(x, y);
            }

            public static implicit operator Point(POINTS p) => new Point(p.x, p.y);
            public static implicit operator POINTS(Point p) => new POINTS((short)p.X, (short)p.Y);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left   = left;
                this.top    = top;
                this.right  = right;
                this.bottom = bottom;
            }

            public static readonly RECT Empty = new RECT(0, 0, 0, 0);

            public int Height => bottom - top;
            public int Width  => right  - left;

            public void Offset(int x, int y)
            {
                left   += x;
                top    += y;
                right  += x;
                bottom += y;
            }

            public void Offset(Point pt)
            {
                Offset(pt.X, pt.Y);
            }

            public static RECT FromLParam(Message m)
            {
                return (RECT)m.GetLParam(typeof(RECT));
            }

            public void ToLParam(Message m)
            {
                Marshal.StructureToPtr(this, m.LParam, false);
            }

            public static implicit operator Rectangle(RECT r)
            {
                return new Rectangle(r.left, r.top, r.Width, r.Height);
            }

            public static implicit operator RECT(Rectangle r)
            {
                return new RECT(r.Left, r.Top, r.Right, r.Bottom);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public int cx;
            public int cy;

            public SIZE(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }

            public static implicit operator Size(SIZE s)
            {
                return new Size(s.cx, s.cy);
            }

            public static implicit operator SIZE(Size s)
            {
                return new SIZE(s.Width, s.Height);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STYLESTRUCT
        {
            public int styleOld;
            public int styleNew;

            public static STYLESTRUCT FromLParam(Message m)
            {
                return (STYLESTRUCT)m.GetLParam(typeof(STYLESTRUCT));
            }

            public void ToLParam(Message m)
            {
                Marshal.StructureToPtr(this, m.LParam, false);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TPMPARAMS
        {
            public int cbSize = Marshal.SizeOf(typeof(TPMPARAMS));
            // RECT rcExclude
            public int rcExclude_left;
            public int rcExclude_top;
            public int rcExclude_right;
            public int rcExclude_bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TRACKMOUSEEVENT
        {
            public int    cbSize;
            public int    dwFlags;
            public IntPtr hwndTrack;
            public int    dwHoverTime;

            public TRACKMOUSEEVENT(IntPtr hWnd, int flags, int time)
            {
                hwndTrack   = hWnd;
                dwFlags     = flags;
                dwHoverTime = time;
                cbSize      = Marshal.SizeOf(typeof(TRACKMOUSEEVENT));
            }

            public TRACKMOUSEEVENT(IntPtr hWnd, int flags)
            {
                int time = SystemInformation.MouseHoverTime;

                if (time <= 0)
                    time = 400;

                hwndTrack   = hWnd;
                dwFlags     = flags;
                dwHoverTime = time;
                cbSize      = Marshal.SizeOf(typeof(TRACKMOUSEEVENT));
            }

            public static TRACKMOUSEEVENT Empty => new TRACKMOUSEEVENT
            {
                cbSize = Marshal.SizeOf(typeof(TRACKMOUSEEVENT))
            };
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            // POINT ptMinPosition
            public int ptMinPosition_x;
            public int ptMinPosition_y;
            // POINT ptMaxPosition
            public int ptMaxPosition_x;
            public int ptMaxPosition_y;
            // RECT rcNormalPosition
            public int rcNormalPosition_left;
            public int rcNormalPosition_top;
            public int rcNormalPosition_right;
            public int rcNormalPosition_bottom;

            public POINT ptMinPosition
            {
                get => new POINT(ptMinPosition_x, ptMinPosition_y);
                set
                {
                    ptMinPosition_x = value.x;
                    ptMinPosition_y = value.y;
                }
            }

            public POINT ptMaxPosition
            {
                get => new POINT(ptMaxPosition_x, ptMaxPosition_y);
                set
                {
                    ptMaxPosition_x = value.x;
                    ptMaxPosition_y = value.y;
                }
            }

            public RECT rcNormalPosition
            {
                get => new RECT(rcNormalPosition_left, rcNormalPosition_top, rcNormalPosition_right, rcNormalPosition_bottom);
                set
                {
                    rcNormalPosition_left   = value.left;
                    rcNormalPosition_top    = value.top;
                    rcNormalPosition_right  = value.right;
                    rcNormalPosition_bottom = value.bottom;
                }
            }

            public static WINDOWPLACEMENT Empty => new WINDOWPLACEMENT()
            {
                length = Marshal.SizeOf(typeof(WINDOWPLACEMENT))
            };
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            /// <summary>
            /// The x-coordinate of the upper left corner of the new window. 
            /// If the window is a child window, coordinates are relative to the parent window. 
            /// Otherwise, the coordinates are relative to the screen origin.
            /// </summary>
            public int x;
            /// <summary>
            /// The y-coordinate of the upper left corner of the new window. 
            /// If the window is a child window, coordinates are relative to the parent window. 
            /// Otherwise, the coordinates are relative to the screen origin.
            /// </summary>
            public int y;
            public int cx;
            public int cy;
            public int flags;

            public static WINDOWPOS FromLParam(Message m)
            {
                return (WINDOWPOS)m.GetLParam(typeof(WINDOWPOS));
            }

            public void ToLParam(Message m)
            {
                Marshal.StructureToPtr(this, m.LParam, false);
            }
        }
    }
}
