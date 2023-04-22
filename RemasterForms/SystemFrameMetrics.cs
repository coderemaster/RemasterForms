using System;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    public struct SystemFrameMetrics
    {
        public static SystemFrameMetrics DefaultWindow
        {
            get => new SystemFrameMetrics(
                WS_CAPTION | WS_THICKFRAME,
                0);
        }

        public static SystemFrameMetrics ToolWindow
        {
            get => new SystemFrameMetrics(
                WS_CAPTION | WS_THICKFRAME,
                WS_EX_TOOLWINDOW);
        }

        public Padding BorderPadding;
        public Padding FramePadding ;

        internal SystemFrameMetrics(int style, int exStyle)
        {
            var rc = new RECT();

            _ = AdjustWindowRectEx(ref rc, style, false, exStyle & ~WS_EX_CLIENTEDGE);

            FramePadding = new Padding(-rc.left, -rc.top, rc.right, rc.bottom);

            int captionHeight = ((style & WS_CAPTION) == WS_CAPTION)
                ? (((exStyle & WS_EX_TOOLWINDOW) == WS_EX_TOOLWINDOW)
                    ? SystemInformation.ToolWindowCaptionHeight
                    : SystemInformation.CaptionHeight)
                : 0;

            BorderPadding = new Padding(
                FramePadding.Left,
                FramePadding.Top - captionHeight,
                FramePadding.Right,
                FramePadding.Bottom);
        }

        internal SystemFrameMetrics(CreateParams cp)
            : this(cp.Style, cp.ExStyle) { }

        internal SystemFrameMetrics(WindowStyles ws)
            : this(ws.Style, ws.ExStyle) { }

        internal SystemFrameMetrics(IntPtr hWnd)
            : this((int)GetWindowLong(hWnd, GWL_STYLE), (int)GetWindowLong(hWnd, GWL_EXSTYLE)) { }

        public override bool Equals(object obj)
        {
            return 
                obj != null && 
                obj is SystemFrameMetrics other && 
                this.Equals(other);
        }

        public bool Equals(SystemFrameMetrics other)
        {
            return
                BorderPadding == other.BorderPadding &&
                FramePadding  == other.FramePadding  ;
        }

        public override int GetHashCode()
        {
            return (BorderPadding, FramePadding).GetHashCode();
        }

        public static bool operator ==(SystemFrameMetrics left, SystemFrameMetrics right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SystemFrameMetrics left, SystemFrameMetrics right)
        {
            return !(left == right);
        }
    }
}
