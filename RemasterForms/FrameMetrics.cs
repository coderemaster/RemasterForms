using System;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    public struct FrameMetrics
    {
        #region ## fields

        internal static readonly FrameMetrics Empty = new FrameMetrics(Padding.Empty, 0);

        public  Padding BorderPadding;
        public  int     CaptionHeight;

        #endregion fields

        #region ## constructors

        internal FrameMetrics(Padding borderPadding, int captionHeight) : this()
        {
            BorderPadding = borderPadding;
            CaptionHeight = captionHeight;
        }

        internal FrameMetrics(int style, int exStyle)
        {
            var rect = new RECT();

            _ = AdjustWindowRectEx(ref rect, style, false, exStyle & ~WS_EX_CLIENTEDGE);

            var padding = new Padding(-rect.left, -rect.top, rect.right, rect.bottom);

            CaptionHeight = ((style & WS_CAPTION) == WS_CAPTION)
                ? (((exStyle & WS_EX_TOOLWINDOW) == WS_EX_TOOLWINDOW)
                    ? SystemInformation.ToolWindowCaptionHeight
                    : SystemInformation.CaptionHeight)
                : 0;

            BorderPadding = new Padding(
                padding.Left,
                padding.Top - CaptionHeight,
                padding.Right,
                padding.Bottom);
        }

        internal FrameMetrics(CreateParams cp)
            : this(cp.Style, cp.ExStyle) { }

        internal FrameMetrics(WindowStyles ws)
            : this(ws.Style, ws.ExStyle) { }

        internal FrameMetrics(IntPtr hWnd)
            : this((int)GetWindowLong(hWnd, GWL_STYLE), (int)GetWindowLong(hWnd, GWL_EXSTYLE)) { }

        #endregion constructors

        #region ## methods

        internal static FrameMetrics Default(bool toolWindow)
        {
            return new FrameMetrics(
                WS_CAPTION | WS_THICKFRAME,
                toolWindow ? WS_EX_TOOLWINDOW : 0);
        }

        public override bool Equals(object obj)
        {
            return 
                obj != null && 
                obj is FrameMetrics other && 
                Equals(other);
        }

        public bool Equals(FrameMetrics other)
        {
            return
                BorderPadding == other.BorderPadding &&
                CaptionHeight == other.CaptionHeight;
        }

        public override int GetHashCode()
        {
            return (BorderPadding, CaptionHeight).GetHashCode();
        }

        #endregion methods

        #region ## operators

        public static bool operator ==(FrameMetrics left, FrameMetrics right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FrameMetrics left, FrameMetrics right)
        {
            return !(left == right);
        }

        public static implicit operator Padding(FrameMetrics metrics)
        {
            return new Padding(
                metrics.BorderPadding.Left,
                metrics.BorderPadding.Top + metrics.CaptionHeight,
                metrics.BorderPadding.Right,
                metrics.BorderPadding.Bottom);
        }

        #endregion operators
    }
}
