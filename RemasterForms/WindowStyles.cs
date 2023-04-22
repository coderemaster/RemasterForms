using System;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    internal struct WindowStyles
    {
        public int ExStyle;
        public int Style  ;

        internal WindowStyles(int style, int exStyle)
        {
            Style   = style;
            ExStyle = exStyle;
        }

        internal WindowStyles(CreateParams cp)
            : this(cp.Style, cp.ExStyle) { }

        internal WindowStyles(IntPtr hWnd)
            : this((int)GetWindowLong(hWnd, GWL_STYLE), (int)GetWindowLong(hWnd, GWL_EXSTYLE)) { }

        public static readonly WindowStyles Default = new WindowStyles(
            WS_CAPTION | WS_SIZEBOX | WS_MAXIMIZEBOX | WS_MINIMIZEBOX | WS_SYSMENU, 
            0);

        public bool Border
        {
            get => (Style & WS_BORDER) == WS_BORDER;
            set => Style = value ? (Style | WS_BORDER) : (Style & ~WS_BORDER);
        }

        public bool Caption
        {
            get => (Style & WS_CAPTION) == WS_CAPTION;
            set => Style = value ? (Style | WS_CAPTION) : (Style & ~WS_DLGFRAME);
        }

        public bool MaximizeBox
        {
            get => (Style & WS_MAXIMIZEBOX) == WS_MAXIMIZEBOX;
            set => Style = value ? (Style | WS_MAXIMIZEBOX) : (Style & ~WS_MAXIMIZEBOX);
        }

        public bool MinimizeBox
        {
            get => (Style & WS_MINIMIZEBOX) == WS_MINIMIZEBOX;
            set => Style = value ? (Style | WS_MINIMIZEBOX) : (Style & ~WS_MINIMIZEBOX);
        }

        public bool SizeBox
        {
            get => (Style & WS_SIZEBOX) == WS_SIZEBOX;
            set => Style = value ? (Style | WS_SIZEBOX) : (Style & ~WS_SIZEBOX);
        }

        public bool SysMenu
        {
            get => (Style & WS_SYSMENU) == WS_SYSMENU;
            set => Style = value ? (Style | WS_SYSMENU) : (Style & ~WS_SYSMENU);
        }

        public bool ToolWindow
        {
            get => (ExStyle & WS_EX_TOOLWINDOW) == WS_EX_TOOLWINDOW;
            set => ExStyle = value ? (ExStyle | WS_EX_TOOLWINDOW) : (ExStyle & ~WS_EX_TOOLWINDOW);
        }

        public override bool Equals(object obj)
        {
            return
                obj != null &&
                obj is WindowStyles other &&
                this.Equals(other);
        }

        public bool Equals(WindowStyles other)
        {
            return
                Style   == other.Style  &&
                ExStyle == other.ExStyle;
        }

        public override int GetHashCode()
        {
            return (Style, ExStyle).GetHashCode();
        }

        public static bool operator ==(WindowStyles left, WindowStyles right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WindowStyles left, WindowStyles right)
        {
            return !(left == right);
        }
    }
}
