using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    internal partial class DisableDesigner { }

    public partial class BaseForm : Form // MsgHandlers
    {
        private void WmCreate(ref Message m)
        {
            _WindowStyles       = new WindowStyles(m.HWnd);
            _SystemFrameMetrics = new SystemFrameMetrics(WindowStyles);

            if (!Designtime)
            {
                if (!IsRestored)
                    RestoreBounds = GetNormalBounds();

                if (!IsMinimized)
                    UpdateNonClientPadding();

                int policy = (int)DWMNCRENDERINGPOLICY.DWMNCRP_ENABLED;

                _ = DwmSetWindowAttribute(
                    m.HWnd,
                    (int)DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_POLICY,
                    ref policy,
                    sizeof(int));

                DarkMode    = DarkMode;
                CornerStyle = CornerStyle;
            }

            base.WndProc(ref m);
        }

        private void WmDestroy(ref Message m)
        {
            if (IsMinimized)
                RestoreBounds = GetNormalBounds();

            base.WndProc(ref m);
        }

        private void WmEraseBkgnd(ref Message m)
        {
            UpdateWindowState();
            UpdateBounds();

            base.WndProc(ref m);
        }

        private void WmInitMenuPopup(ref Message m)
        {
            var hMenu = WindowStyles.SysMenu
                ? GetSystemMenu(m.HWnd, false)
                : IntPtr.Zero;

            if (hMenu != IntPtr.Zero && m.WParam == hMenu)
            {
                AdjustSystemMenu(hMenu, SC_DEFAULT);

                scRestoreToNormal = 0;

                if (IsMinimized && RestoreToMaximized)
                {
                    // get vacant id
                    for (int id = 0x10; id < 0xF000; id += 0x10)
                    {
                        var mii = new MENUITEMINFO();

                        if (!GetMenuItemInfo(hMenu, id, false, mii))
                        {
                            scRestoreToNormal = id;
                            break;
                        }
                    }
                }

                if (scRestoreToNormal != 0)
                    _ = SetMenuItemID(hMenu, SC_RESTORE, scRestoreToNormal);

                m.Result = IntPtr.Zero;
                return;
            }
        }

        private void WmNcActivate(ref Message m)
        {
            NonClientActive = m.WParam != IntPtr.Zero;

            if (Designtime)
                m.Result = (IntPtr)1;
            else
                DefWndProc(ref m);

            OnNonClientActiveChanged(EventArgs.Empty);
        }

        private void WmNcCalcSize(ref Message m)
        {
            if (m.WParam == IntPtr.Zero)
            {
                base.WndProc(ref m);
                return;
            }

            UpdateWindowState();

            if (IsMinimized)
            {
                _NonClientPadding = SystemFrameMetrics.DefaultWindow.FramePadding;

                base.WndProc(ref m);
                return;
            }

            SetNonClientPadding(IsRestored
                ? DefaultNonClientPadding
                : SystemFrameMetrics.BorderPadding);

            var nc = NCCALCSIZE_PARAMS.FromLParam(m);

            nc.rgrc[0] = Extensions.Grow(nc.rgrc[0], SystemFrameMetrics.FramePadding - NonClientPadding);

            nc.ToLParam(m);
            m.Result = (IntPtr)WVR_VALIDRECTS;

            DefWndProc(ref m);
        }

        private void WmNcHitTest(ref Message m)
        {
            if (Designtime)
            {
                base.WndProc(ref m);
                return;
            }

            var pt = POINTS.FromLParam(m.LParam);
            int ht = (int)NonClientHitTest(pt);

            if (ht == HTNOWHERE && ClientRectangle.Contains(PointToClient(pt)))
                ht = HTCLIENT;

            m.Result = (IntPtr)ht;
        }

        private void WmNcPaint(ref Message m)
        {
            if (Designtime)
            {
                UpdateNonClient();
                m.Result = (IntPtr)1;
            }
            else
                DefWndProc(ref m);
        }

        private void WmSettingChange(ref Message m)
        {
            var oldMetrics = SystemFrameMetrics;

            _SystemFrameMetrics = new SystemFrameMetrics(WindowStyles);

            base.WndProc(ref m);

            if (SystemFrameMetrics != oldMetrics)
                UpdateStyles();

            switch ((int)m.WParam)
            {
                case SPI_SETNONCLIENTMETRICS:
                    OnNonClientMetricsChanged(EventArgs.Empty);
                    break;
                case SPI_SETHIGHCONTRAST:
                    OnHighContrastChanged(EventArgs.Empty);
                    break;
            }
        }

        private void WmStyleChanged(ref Message m)
        {
            var ss = STYLESTRUCT.FromLParam(m);

            switch ((int)m.WParam)
            {
                case GWL_STYLE:
                    {
                        _WindowStyles = new WindowStyles(ss.styleNew, WindowStyles.ExStyle);
                    }
                    break;
                case GWL_EXSTYLE:
                    {
                        bool oldToolWindow = WindowStyles.ToolWindow;

                        _WindowStyles = new WindowStyles(WindowStyles.Style, ss.styleNew);

                        if (!Designtime && !Windows11 && WindowStyles.Border &&
                            (oldToolWindow || WindowStyles.ToolWindow))
                        {
                            // update tool border
                            _ = DefWndProc(WM_NCACTIVATE, (IntPtr)(NonClientActive ? 0 : 1), IntPtr.Zero);
                            _ = DefWndProc(WM_NCACTIVATE, (IntPtr)(NonClientActive ? 1 : 0), IntPtr.Zero);
                        }
                    }
                    break;
            }

            _SystemFrameMetrics = new SystemFrameMetrics(WindowStyles);

            CornerStyle = CornerStyle;

            base.WndProc(ref m);
        }

        private void WmSysCommand(ref Message m)
        {
            int cmd = (int)m.WParam & 0xFFF0;

            switch (cmd)
            {
                case SC_RESTORE:
                    {
                        if (IsMaximized && !MaximizeBox)
                        {
                            m.Result = IntPtr.Zero;
                            return;
                        }
                    }
                    break;
                case SC_MOVE:
                    {
                        if (IsMaximized && !MaximizeBox)
                        {
                            m.Result = IntPtr.Zero;
                            return;
                        }
                    }
                    break;
                case SC_SIZE:
                    {
                        if (!IsRestored)
                        {
                            m.Result = IntPtr.Zero;
                            return;
                        }
                    }
                    break;
                case SC_MINIMIZE:
                    {
                        if (!MinimizeBox || !WindowStyles.Border)
                        {
                            m.Result = IntPtr.Zero;
                            return;
                        }
                    }
                    break;
                case SC_MAXIMIZE:
                    {
                        switch (WindowState)
                        {
                            case FormWindowState.Minimized:
                                {
                                    if (!MaximizeBox && !RestoreToMaximized)
                                    {
                                        m.Result = IntPtr.Zero;
                                        return;
                                    }
                                }
                                break;
                            case FormWindowState.Normal:
                                {
                                    if (!MaximizeBox)
                                    {
                                        m.Result = IntPtr.Zero;
                                        return;
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case SC_CLOSE:
                    {
                        if (!CloseBox)
                        {
                            m.Result = IntPtr.Zero;
                            return;
                        }
                    }
                    break;
                default:
                    {
                        if (cmd == scRestoreToNormal)
                        {
                            WindowState = FormWindowState.Normal;

                            m.Result = IntPtr.Zero;
                            return;
                        }
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        private void WmUninitMenuPopup(ref Message m)
        {
            var hMenu = WindowStyles.SysMenu
                ? GetSystemMenu(m.HWnd, false)
                : IntPtr.Zero;

            if (hMenu != IntPtr.Zero && m.WParam == hMenu)
            {
                if (scRestoreToNormal != 0)
                    SetMenuItemID(hMenu, scRestoreToNormal, SC_RESTORE);

                m.Result = IntPtr.Zero;
                return;
            }
        }

        private void WmWindowPosChanged(ref Message m)
        {
            UpdateWindowState();

            base.WndProc(ref m);

            if (IsRestored)
                RestoreBounds = Bounds;

            if (WindowState != cachedWindowState)
            {
                if (IsMinimized)
                    RestoreToMaximized = cachedWindowState == FormWindowState.Maximized;

                cachedWindowState = WindowState;

                if (IsMaximized)
                    RestoreBounds = GetNormalBounds();

                OnWindowStateChanged(EventArgs.Empty);
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_CREATE:
                    WmCreate(ref m);
                    break;
                case WM_DESTROY:
                    WmDestroy(ref m);
                    break;
                case WM_ERASEBKGND:
                    WmEraseBkgnd(ref m);
                    break;
                case WM_INITMENUPOPUP:
                    WmInitMenuPopup(ref m);
                    break;
                case WM_NCACTIVATE:
                    WmNcActivate(ref m);
                    break;
                case WM_NCCALCSIZE:
                    WmNcCalcSize(ref m);
                    break;
                case WM_NCHITTEST:
                    WmNcHitTest(ref m);
                    break;
                case WM_NCPAINT:
                    WmNcPaint(ref m);
                    break;
                case WM_SETICON:
                case WM_SETTEXT:
                    {
                        int style = WindowStyles.Style;

                        _ = SetWindowLong(m.HWnd, GWL_STYLE, (IntPtr)(style & ~WS_VISIBLE));
                        DefWndProc(ref m);
                        _ = SetWindowLong(m.HWnd, GWL_STYLE, (IntPtr)style);
                    }
                    break;
                case WM_SETTINGCHANGE:
                    WmSettingChange(ref m);
                    break;
                case WM_STYLECHANGED:
                    WmStyleChanged(ref m);
                    break;
                case WM_SYSCOMMAND:
                    WmSysCommand(ref m);
                    break;
                case WM_UNINITMENUPOPUP:
                    WmUninitMenuPopup(ref m);
                    break;
                case WM_WINDOWPOSCHANGED:
                    WmWindowPosChanged(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
