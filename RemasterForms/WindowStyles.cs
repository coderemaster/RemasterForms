using System;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    internal struct WindowStyles
    {
        #region ## fields

        public int Style  ;
        public int ExStyle;

        #endregion fields

        #region ## constructors

        public WindowStyles(int style, int exStyle)
        {
            Style   = style;
            ExStyle = exStyle;
        }

        public WindowStyles(CreateParams cp)
            : this(cp.Style, cp.ExStyle) { }

        public WindowStyles(IntPtr hWnd)
            : this((int)GetWindowLong(hWnd, GWL_STYLE), (int)GetWindowLong(hWnd, GWL_EXSTYLE)) { }

        #endregion constructors

        #region ## macros

        public bool Border 
            => (Style & WS_BORDER) == WS_BORDER;

        public bool Caption 
            => (Style & WS_CAPTION) == WS_CAPTION;

        public bool LayoutRtl
            => (ExStyle & WS_EX_LAYOUTRTL) == WS_EX_LAYOUTRTL;

        public bool MaximizeBox 
            => (Style & WS_MAXIMIZEBOX) == WS_MAXIMIZEBOX;

        public bool MinimizeBox 
            => (Style & WS_MINIMIZEBOX) == WS_MINIMIZEBOX;

        public bool SizeBox 
            => (Style & WS_SIZEBOX) == WS_SIZEBOX;

        public bool SysMenu 
            => (Style & WS_SYSMENU) == WS_SYSMENU;

        public bool ToolWindow 
            => (ExStyle & WS_EX_TOOLWINDOW) == WS_EX_TOOLWINDOW;

        #endregion macros

        #region ## methods

        public override bool Equals(object obj)
        {
            return
                obj != null &&
                obj is WindowStyles other &&
                Equals(other);
        }

        public bool Equals(WindowStyles other)
        {
            return
                Style == other.Style &&
                ExStyle == other.ExStyle;
        }

        public override int GetHashCode()
        {
            return (Style, ExStyle).GetHashCode();
        }

        #endregion methods

        #region ## operators

        public static bool operator ==(WindowStyles left, WindowStyles right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WindowStyles left, WindowStyles right)
        {
            return !(left == right);
        }

        #endregion operators
    }
}
