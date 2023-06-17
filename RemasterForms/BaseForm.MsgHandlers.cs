using System;
using System.Drawing;
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
            _normalBounds = NormalBounds;

            _normalBorderMetrics = null;
            _normalFrameMetrics  = null;

            WindowStyles = new WindowStyles(m.HWnd);

            if (!_designTime)
            {
                SystemFrameMetrics  = new FrameMetrics(WindowStyles);
                DefaultFrameMetrics = FrameMetrics.Default(WindowStyles.ToolWindow);
                HighContrastMode    = SystemInformation.HighContrast;
                HighContrastLayout  = HighContrastMode;
            }

            if (!Borderless && !IsMinimized)
            {
                // notify the window about the frame change

                int flags =
                SWP_NOZORDER |
                SWP_NOACTIVATE |
                SWP_NOOWNERZORDER |
                SWP_FRAMECHANGED |
                SWP_NOMOVE |
                SWP_NOSIZE;

                _ = SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, flags);
            }

            DwmFrame?.Initialize();

            base.WndProc(ref m);
        }

        private void WmDestroy(ref Message m)
        {
            _highContrastMode    = null;
            _normalBorderMetrics = null;
            _normalFrameMetrics  = null;

            base.WndProc(ref m);
        }

        private void WmEnterSizeMove(ref Message m)
        {
            if (_activeMoving && SnapAvailable)
            {
                // BUGFIX_3
                // - Wrong location by restoring from snap layout.

                var normalBounds = NormalBounds;

                if (Bounds != normalBounds)
                    _normalLocation = normalBounds.Location;
            }

            base.WndProc(ref m);
        }

        private void WmExitSizeMove(ref Message m)
        {
            _activeMoving = false;
            _activeSizing = false;

            base.WndProc(ref m);
        }

        private void WmGetMinMaxInfo(ref Message m)
        {
            // BUGFIX
            // - The caption buttons are drawn outside the caption area.

            if (!IsMinimized)
            {
                int buttonHeight = ((Padding)DefaultFrameMetrics).Top;
                int buttonWidth = buttonHeight * 3 / 2;

                int minHeight = ((Padding)SystemFrameMetrics).Vertical;
                int minWidth = 3 * buttonWidth + SystemFrameMetrics.BorderPadding.Horizontal;

                var info = MINMAXINFO.FromLParam(m);

                info.ptMinTrackSize = new POINT(
                    Math.Max(minWidth, MinimumSize.Width),
                    Math.Max(minHeight, MinimumSize.Height));

                info.ToLParam(m);

                DefWndProc(ref m);
                return;
            }

            base.WndProc(ref m);
        }

        private void WmInitMenuPopup(ref Message m)
        {
            var hMenu = WindowStyles.SysMenu
                ? GetSystemMenu(m.HWnd, false)
                : IntPtr.Zero;

            if (hMenu != IntPtr.Zero || m.WParam == hMenu)
            {
                InitSystemMenuPopup(hMenu);

                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }

        private void WmNcActivate(ref Message m)
        {
            bool oldActive = NonClientActive;

            NonClientActive = m.WParam != IntPtr.Zero;

            if (_designTime)
                m.Result = IntPtr.Zero;
            else
                base.WndProc(ref m);

            if (oldActive != NonClientActive)
                OnNonClientActiveChanged(EventArgs.Empty);
        }

        private void WmNcCalcSize(ref Message m)
        {
            if (m.WParam == IntPtr.Zero)
            {
                base.WndProc(ref m);
                return;
            }

            if (!_designTime && !_activeSizing)
                UpdateWindowState();

            if (!_activeSizing && DwmFrame != null)
            {
                DwmFrame.GlassInsets = IsRestored
                    ? AdjustGlassInsets()
                    : Padding.Empty;
            }

            if (Borderless || IsMinimized)
            {
                base.WndProc(ref m);
                return;
            }

            var ncPadding = BorderMetrics.WindowEdges;

            if (!_designTime && ncPadding.IsEmpty() && WindowStyles.SizeBox)
            {
                // reduce resize flicker
                ncPadding = new Padding(0, 0, 0, -1);
            }

            var ncParams = NCCALCSIZE_PARAMS.FromLParam(m);

            ncParams.rgrc[0] = Extensions.Inflate(
                ncParams.rgrc[0],
                SystemFrameMetrics - ncPadding);

            ncParams.ToLParam(m);
            m.Result = (IntPtr)WVR_VALIDRECTS;

            DefWndProc(ref m);
        }

        private void WmNcHitTest(ref Message m)
        {
            if (_designTime)
            {
                base.WndProc(ref m);
                return;
            }

            if (_normalLocation != null && SnapAvailable && Bounds == NormalBounds)
            {
                // BUGFIX_3
                // - Wrong location by restoring from snap layout.

                _normalLocation = null;
            }

            var pt = POINTS.FromLParam(m.LParam);

            if (!Bounds.Contains(pt))
            {
                m.Result = (IntPtr)HTNOWHERE;
                return;
            }

            if (IsMinimized)
            {
                m.Result = (IntPtr)HTCAPTION;
                return;
            }

            int htBorder  = BorderHitTest (pt);
            int htCaption = CaptionHitTest(pt);

            switch (htCaption)
            {
                case HTNOWHERE:
                    m.Result = (IntPtr)htBorder;
                    break;
                case HTCAPTION:
                    m.Result = (IntPtr)((htBorder != HTNOWHERE) ? htBorder : htCaption);
                    break;
                default:
                    m.Result = (IntPtr)htCaption;
                    break;
            }

            if (m.Result == (IntPtr)HTNOWHERE && ClientRectangle.Contains(PointToClient(pt)))
            {
                m.Result = (IntPtr)HTCLIENT;
            }
        }

        private void WmNcPaint(ref Message m)
        {
            if (_designTime)
            {
                DrawNonClient();

                m.Result = IntPtr.Zero;
                return;
            }

            DefWndProc(ref m);
        }

        private void WmSettingChange(ref Message m)
        {
            bool highContrastModeChanged = false;
            bool nonClientMetricsChanged = false;

            switch ((int)m.WParam)
            {
                case SPI_SETHIGHCONTRAST:
                    {
                        if (HighContrastMode != SystemInformation.HighContrast)
                        {
                            highContrastModeChanged = true;

                            HighContrastMode   = !HighContrastMode;
                            HighContrastLayout = HighContrastMode;

                            _normalBorderMetrics = null;
                            _normalFrameMetrics  = null;
                        }
                    }
                    break;
                case SPI_SETNONCLIENTMETRICS:
                    {
                        nonClientMetricsChanged = true;

                        DefaultFrameMetrics = FrameMetrics.Default(WindowStyles.ToolWindow);
                        SystemFrameMetrics  = new FrameMetrics(WindowStyles);

                        if (_defaultFont == true)
                            _defaultFont = null;

                        _normalBorderMetrics = null;
                        _normalFrameMetrics  = null;
                    }
                    break;
            }

            base.WndProc(ref m);

            if (highContrastModeChanged)
                OnColorModeChanged(EventArgs.Empty);

            if (nonClientMetricsChanged)
                OnNonClientMetricsChanged(EventArgs.Empty);

            if (highContrastModeChanged || nonClientMetricsChanged)
            {
                UpdateStyles();
                RestoreClientArea();
                UpdateMaximizedBounds();
            }
        }

        private void WmStyleChanged(ref Message m)
        {
            var styles = STYLESTRUCT.FromLParam(m);

            switch ((int)m.WParam)
            {
                case GWL_STYLE:
                    WindowStyles = new WindowStyles(styles.styleNew, WindowStyles.ExStyle);
                    break;
                case GWL_EXSTYLE:
                    WindowStyles = new WindowStyles(WindowStyles.Style, styles.styleNew);
                    break;
            }

            DefaultFrameMetrics = FrameMetrics.Default(WindowStyles.ToolWindow);
            SystemFrameMetrics  = new FrameMetrics(WindowStyles);

            _normalBorderMetrics = null;
            _normalFrameMetrics  = null;

            base.WndProc(ref m);

            UpdateMaximizedBounds();
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

                        if (SnapLayout && _normalLocation != null)
                        {
                            // BUGFIX_3
                            // - Wrong location by restoring from snap layout.

                            DefWndProc(ref m);

                            Location = (Point)_normalLocation;
                            _normalLocation = null;

                            return;
                        }

                        if (_restoreToNormal)
                        {
                            _ = ShowWindow(m.HWnd, SW_SHOWNORMAL);

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

                        _activeMoving = true;
                    }
                    break;
                case SC_SIZE:
                    {
                        if (!IsRestored)
                        {
                            m.Result = IntPtr.Zero;
                            return;
                        }

                        _activeSizing = true;
                    }
                    break;
                case SC_MINIMIZE:
                    {
                        if (!MinimizeBox)
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
                                    if (!MaximizeBox && _prevWindowState != FormWindowState.Maximized)
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
                case SC_KEYMENU:
                    {
                        if ((int)m.LParam == (int)Keys.Space)
                            goto case SC_MOUSEMENU;
                    }
                    break;
                case SC_MOUSEMENU:
                    {
                        if (WindowStyles.SysMenu)
                        {
                            _scDefault = SC_CLOSE;
                            _ = DefWndProc(m.Msg, m.WParam, m.LParam);
                            _scDefault = 0;
                        }

                        m.Result = IntPtr.Zero;
                        return;
                    }
                default:
                    {
                        if (cmd == _scNormal)
                        {
                            _ = ShowWindow(m.HWnd, SW_SHOWNORMAL);

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

            if (hMenu != IntPtr.Zero || m.WParam == hMenu)
            {
                UninitSystemMenuPopup(hMenu);

                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }

        private void WmWindowPosChanged(ref Message m)
        {
            base.WndProc(ref m);

            if (IsRestored)
                RestoreBounds = Bounds;

            if (_oldWindowState != WindowState)
            {
                _restoreToNormal = false;
                _prevWindowState = _oldWindowState;
                _oldWindowState  = WindowState;

                if (IsMaximized)
                    RestoreBounds = NormalBounds;

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
                case WM_ENTERSIZEMOVE:
                    WmEnterSizeMove(ref m);
                    break;
                case WM_EXITSIZEMOVE:
                    WmExitSizeMove(ref m);
                    break;
                case WM_GETMINMAXINFO:
                    WmGetMinMaxInfo(ref m);
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
                case WM_SETICON:
                case WM_SETTEXT:
                    {
                        int style = WindowStyles.Style;

                        _ = SetWindowLong(m.HWnd, GWL_STYLE, (IntPtr)(style & ~WS_VISIBLE));
                        DefWndProc(ref m);
                        _ = SetWindowLong(m.HWnd, GWL_STYLE, (IntPtr)style);
                    }
                    break;
                case WM_NCLBUTTONDBLCLK:
                case WM_NCLBUTTONDOWN:
                case WM_NCLBUTTONUP:
                case WM_NCMBUTTONDBLCLK:
                case WM_NCMBUTTONDOWN:
                case WM_NCMBUTTONUP:
                case WM_NCMOUSELEAVE:
                case WM_NCMOUSEMOVE:
                case WM_NCRBUTTONDBLCLK:
                case WM_NCRBUTTONDOWN:
                case WM_NCRBUTTONUP:
                case WM_NCXBUTTONDBLCLK:
                case WM_NCXBUTTONDOWN:
                case WM_NCXBUTTONUP:
                    {
                        if (!_designTime && ProcessNcMouse(ref m))
                        {
                            m.Result = IntPtr.Zero;
                            return;
                        }

                        base.WndProc(ref m);
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
