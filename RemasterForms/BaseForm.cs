using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    internal partial class DisableDesigner { }

    public partial class BaseForm : Form
    {
        #region ## constructors

        public BaseForm()
        {
            base.SizeGripStyle = SizeGripStyle.Hide;

            Font = SystemFonts.MessageBoxFont;
        }

        #endregion constructors

        #region ## fields

        private int             boundsLockCount;
        private FormWindowState cachedWindowState;
        private int             scRestoreToNormal;

        internal readonly bool Designtime = LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        internal readonly bool Windows11 = 22000 <= Environment.OSVersion.Version.Build;

        #endregion fields

        #region ## macros

        internal bool IsMaximized => !Designtime &&  IsHandleCreated && WindowState == FormWindowState.Maximized;
        internal bool IsMinimized => !Designtime &&  IsHandleCreated && WindowState == FormWindowState.Minimized;
        internal bool IsRestored  =>  Designtime || !IsHandleCreated || WindowState == FormWindowState.Normal;

        #endregion macros

        #region ## properties

        // BoundsLocked
        internal bool BoundsLocked
        {
            get
            {
                if (0 < boundsLockCount)
                    return true;

                if (!Designtime &&
                    IsHandleCreated &&
                    WindowState == FormWindowState.Normal &&
                    cachedWindowState != FormWindowState.Normal)
                {
                    return true;
                }

                return false;
            }
        }

        // ClientSize
        // show in designer
        [Browsable(true), Category("Layout")]
        public new Size ClientSize
        {
            get => base.ClientSize;
            set => base.ClientSize = value;
        }

        // CloseBox
        private bool _CloseBox = true;
        [Category("Behavior")]
        public bool CloseBox
        {
            get => _CloseBox;
            set
            {
                if (_CloseBox != value)
                {
                    _CloseBox = value;
                    OnStyleChanged(EventArgs.Empty);
                }
            }
        }

        // ControlBox
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ControlBox
        {
            get => base.ControlBox;
            set => base.ControlBox = value;
        }

        // CornerStyle
        private int _CornerStyle = (int)CornerStyle.Default;
        /// <summary>
        /// Supported starting with Windows 11 Build 22000.
        /// </summary>
        [Browsable(false)]
        public CornerStyle CornerStyle
        {
            get => (CornerStyle)_CornerStyle;
            set
            {
                if (!Enum.IsDefined(typeof(CornerStyle), value))
                    return;

                if (!Windows11)
                    return;

                if (!Designtime && IsHandleCreated)
                {
                    if (value == CornerStyle.Default)
                    {
                        _CornerStyle = (int)(WindowStyles.Border
                            ? (WindowStyles.ToolWindow
                                ? CornerStyle.RoundSmall
                                : CornerStyle.Round)
                            : CornerStyle.DoNotRound);
                    }
                    else
                    {
                        _CornerStyle = (int)value;
                    }

                    _ = DwmSetWindowAttribute(
                        Handle,
                        (int)DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE,
                        ref _CornerStyle,
                        sizeof(int));
                }

                _CornerStyle = (int)value;
            }
        }

        // CreateParams
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;

                if (FormBorderStyle == FormBorderStyle.None)
                {
                    if (ToolWindow)
                        cp.ExStyle |= WS_EX_TOOLWINDOW;
                }
                else
                {
                    cp.Style |= WS_CAPTION;
                }

                return cp;
            }
        }

        // DarkMode
        private int _DarkMode = 0;
        /// <summary>
        /// Supported starting with Windows 10 Build 17763.
        /// </summary>
        [Browsable(false)]
        public bool DarkMode
        {
            get => _DarkMode != 0;
            set
            {
                if (Environment.OSVersion.Version.Build < 17763)
                    return;

                _DarkMode = value ? 1 : 0;

                if (!Designtime && IsHandleCreated)
                {
                    var attr = (Environment.OSVersion.Version.Build < 18985)
                        ? DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1
                        : DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE;

                    _ = DwmSetWindowAttribute(Handle, (int)attr, ref _DarkMode, sizeof(int));
                }
            }
        }

        // DefaultNonClientPadding
        protected virtual Padding DefaultNonClientPadding
        {
            get
            {
                var padding = (FormFrameStyle == FormFrameStyle.Default)
                    ? (ToolWindow
                        ? SystemFrameMetrics.ToolWindow.FramePadding
                        : SystemFrameMetrics.DefaultWindow.FramePadding)
                    : Padding.Empty;

                padding.Top = 0;

                return padding;
            }
        }

        // Font
        public override void ResetFont() => base.Font = SystemFonts.MessageBoxFont;

        // FormBorderStyle
        /// <summary>
        /// Replaced by FormFrameStyle, SizeBox and ToolWindow properties.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FormBorderStyle FormBorderStyle
        {
            get => base.FormBorderStyle;
            private set => base.FormBorderStyle = value;
        }

        // FormFrameStyle
        private FormFrameStyle _FormFrameStyle = FormFrameStyle.Default;
        [Category("WindowStyle")]
        public FormFrameStyle FormFrameStyle
        {
            get => _FormFrameStyle;
            set
            {
                if (!Enum.IsDefined(typeof(FormFrameStyle), value))
                    return;

                if (_FormFrameStyle == value)
                    return;

                _FormFrameStyle = value;

                if (_FormFrameStyle == FormFrameStyle.Default)
                {
                    FormBorderStyle = SizeBox
                        ? (ToolWindow
                            ? FormBorderStyle.SizableToolWindow
                            : FormBorderStyle.Sizable)
                        : (ToolWindow
                            ? FormBorderStyle.FixedToolWindow
                            : FormBorderStyle.FixedSingle);
                }
                else
                {
                    FormBorderStyle = FormBorderStyle.None;
                }
            }
        }

        // GlassInsets
        private MARGINS _GlassInsets = MARGINS.Empty;
        /// <summary>
        /// Extends the DWM frame into the client area.
        /// </summary>
        protected Padding GlassInsets
        {
            get => _GlassInsets;
            set
            {
                SetGlassInsets(value);

                if (!Designtime && IsHandleCreated)
                    _ = DwmExtendFrameIntoClientArea(Handle, ref _GlassInsets);
            }
        }

        // HelpButton
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool HelpButton
        {
            get => base.HelpButton;
            set => base.HelpButton = value;
        }

        // MaximizedBounds
        protected new Rectangle MaximizedBounds
        {
            get => base.MaximizedBounds;
            set
            {
                base.MaximizedBounds = value;

                if (IsMaximized)
                    UpdateMaximizedBounds();
            }
        }

        // NonClientActive
        protected bool NonClientActive { get; private set; }

        // NonClientPadding
        private Padding? _NonClientPadding;
        protected Padding NonClientPadding
        {
            get
            {
                if (_NonClientPadding == null)
                    _NonClientPadding = DefaultNonClientPadding;

                return (Padding)_NonClientPadding;
            }
            set
            {
                SetNonClientPadding(value);

                if (IsHandleCreated && !IsMinimized)
                    UpdateNonClientPadding();
            }
        }

        // NormalBounds
        /// <summary>
        /// Returns bounds in a restored state, which is not a snap layout.
        /// </summary>
        protected Rectangle NormalBounds
        {
            get => GetNormalBounds();
        }

        // RestoreBounds
        public new Rectangle RestoreBounds { get; private set; } = Rectangle.Empty;

        // RestoreToMaximized
        /// <summary>
        /// Gets the state of a window before being either minimized.
        /// </summary>
        protected bool RestoreToMaximized { get; private set; }

        // ShowInTaskbar
        private bool _ShowInTaskbar = true;
        public new bool ShowInTaskbar
        {
            get => _ShowInTaskbar;
            set
            {
                _ShowInTaskbar = value;

                // prevent the handle from being re-created when it is not necessary
                if (!Designtime)
                    base.ShowInTaskbar = value;
            }
        }

        // Size
        // hide in designer
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size Size
        {
            get => base.Size;
            set => base.Size = value;
        }

        // SizeBox
        private bool _SizeBox = true;
        [Category("WindowStyle")]
        public bool SizeBox
        {
            get => _SizeBox;
            set
            {
                if (_SizeBox == value)
                    return;

                _SizeBox = value;

                switch (base.FormBorderStyle)
                {
                    case FormBorderStyle.FixedSingle:
                        if (_SizeBox)
                            base.FormBorderStyle = FormBorderStyle.Sizable;
                        break;
                    case FormBorderStyle.Sizable:
                        if (!_SizeBox)
                            base.FormBorderStyle = FormBorderStyle.FixedSingle;
                        break;
                    case FormBorderStyle.FixedToolWindow:
                        if (_SizeBox)
                            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                        break;
                    case FormBorderStyle.SizableToolWindow:
                        if (!_SizeBox)
                            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                        break;
                }
            }
        }

        // SystemFrameMetrics
        private SystemFrameMetrics? _SystemFrameMetrics = null;
        protected SystemFrameMetrics SystemFrameMetrics
        {
            get => (_SystemFrameMetrics == null)
                ? new SystemFrameMetrics(CreateParams)
                : (SystemFrameMetrics)_SystemFrameMetrics;
        }

        // ToolWindow
        private bool _ToolWindow = false;
        [Category("WindowStyle")]
        public bool ToolWindow
        {
            get => _ToolWindow;
            set
            {
                if (_ToolWindow == value)
                    return;

                _ToolWindow = value;

                switch (base.FormBorderStyle)
                {
                    case FormBorderStyle.None:
                        if (!Designtime)
                            UpdateStyles();
                        break;
                    case FormBorderStyle.FixedSingle:
                        if (_ToolWindow)
                            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                        break;
                    case FormBorderStyle.Sizable:
                        if (_ToolWindow)
                            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                        break;
                    case FormBorderStyle.FixedToolWindow:
                        if (!_ToolWindow)
                            base.FormBorderStyle = FormBorderStyle.FixedSingle;
                        break;
                    case FormBorderStyle.SizableToolWindow:
                        if (!_ToolWindow)
                            base.FormBorderStyle = FormBorderStyle.Sizable;
                        break;
                }
            }
        }

        // WindowState
        private FormWindowState _WindowState = FormWindowState.Normal;
        public new FormWindowState WindowState
        {
            get => _WindowState;
            set
            {
                _WindowState = value;
                base.WindowState = value;
            }
        }

        // WindowStyles
        private WindowStyles? _WindowStyles = null;
        internal WindowStyles WindowStyles
        {
            get => (_WindowStyles == null)
                ? new WindowStyles(CreateParams)
                : (WindowStyles)_WindowStyles;
        }

        #endregion properties

        #region ## methods

        // AdjustSystemMenu
        internal virtual void AdjustSystemMenu(IntPtr hMenu, int defaultItem)
        {
            switch (WindowState)
            {
                case FormWindowState.Minimized:
                    {
                        if (RestoreToMaximized)
                        {
                            _ = SetMenuItemEnabled(hMenu, SC_RESTORE, MaximizeBox);
                            _ = SetMenuItemEnabled(hMenu, SC_MAXIMIZE, true);

                            if (defaultItem == SC_DEFAULT)
                                defaultItem = SC_MAXIMIZE;
                        }
                        else
                        {
                            _ = SetMenuItemEnabled(hMenu, SC_RESTORE, true);
                            _ = SetMenuItemEnabled(hMenu, SC_MAXIMIZE, MaximizeBox);

                            if (defaultItem == SC_DEFAULT)
                                defaultItem = SC_RESTORE;
                        }

                        _ = SetMenuItemEnabled(hMenu, SC_MOVE, !ShowInTaskbar);
                        _ = SetMenuItemEnabled(hMenu, SC_SIZE, false);
                        _ = SetMenuItemEnabled(hMenu, SC_MINIMIZE, false);
                    }
                    break;
                case FormWindowState.Normal:
                    {
                        bool snapLayout = Bounds != GetNormalBounds();

                        _ = SetMenuItemEnabled(hMenu, SC_RESTORE, snapLayout);
                        _ = SetMenuItemEnabled(hMenu, SC_MOVE, WindowStyles.Caption);
                        _ = SetMenuItemEnabled(hMenu, SC_SIZE, WindowStyles.SizeBox);
                        _ = SetMenuItemEnabled(hMenu, SC_MINIMIZE, MinimizeBox && WindowStyles.Border);
                        _ = SetMenuItemEnabled(hMenu, SC_MAXIMIZE, MaximizeBox);

                        if (defaultItem == SC_DEFAULT)
                        {
                            defaultItem = snapLayout
                                ? SC_RESTORE
                                : (MaximizeBox ? SC_MAXIMIZE : -1);
                        }
                    }
                    break;
                case FormWindowState.Maximized:
                    {
                        _ = SetMenuItemEnabled(hMenu, SC_RESTORE, MaximizeBox);
                        _ = SetMenuItemEnabled(hMenu, SC_MOVE, false);
                        _ = SetMenuItemEnabled(hMenu, SC_SIZE, false);
                        _ = SetMenuItemEnabled(hMenu, SC_MINIMIZE, MinimizeBox && WindowStyles.Border);
                        _ = SetMenuItemEnabled(hMenu, SC_MAXIMIZE, false);

                        if (defaultItem == SC_DEFAULT)
                            defaultItem = MaximizeBox ? SC_RESTORE : -1;
                    }
                    break;
            }

            _ = SetMenuItemEnabled(hMenu, SC_CLOSE, CloseBox);

            if (defaultItem != -1 && !IsMenuItemEnabled(hMenu, defaultItem))
                defaultItem = -1;

            _ = SetMenuDefaultItem(hMenu, defaultItem, false);
        }

        // BorderHitTest
        internal virtual int BorderHitTest(Point pt)
        {
            if (!IsRestored || !WindowStyles.SizeBox)
            {
                return Bounds.Shrink(NonClientPadding).Contains(pt)
                    ? HTNOWHERE
                    : HTBORDER;
            }

            var border = SystemFrameMetrics.BorderPadding;
            var corner = border.Multiply(3).Divide(2);

            int[,] values = {
                { HTTOPLEFT,    HTTOP,     HTTOPRIGHT    },
                { HTLEFT,       HTNOWHERE, HTRIGHT       },
                { HTBOTTOMLEFT, HTBOTTOM,  HTBOTTOMRIGHT } };

            int h = 1, v = 1;

            if (Left <= pt.X && pt.X < (Left + corner.Left))
                h = 0;
            else if ((Right - corner.Right) <= pt.X && pt.X < Right)
                h = 2;

            if (Top <= pt.Y && pt.Y < (Top + corner.Top))
                v = 0;
            else if ((Bottom - corner.Bottom) <= pt.Y && pt.Y < Bottom)
                v = 2;

            return Bounds.Shrink(border).Contains(pt)
                ? HTNOWHERE
                : values[v, h];
        }

        // CaptionHitTest
        internal virtual int CaptionHitTest(Point pt)
        {
            var rect = ClientRectangle;

            rect.Height = SystemFrameMetrics.FramePadding.Top;

            return rect.Contains(PointToClient(pt))
                ? HTCAPTION
                : HTNOWHERE;
        }

        // CreateHandle
        protected override void CreateHandle()
        {
            if (!RecreatingHandle)
                cachedWindowState = WindowState;

            base.CreateHandle();

            if (!Designtime && !RecreatingHandle)
            {
                var screen = Screen.FromHandle(Handle);
                var bounds = IsRestored ? Bounds : GetNormalBounds();

                switch (StartPosition)
                {
                    case FormStartPosition.CenterScreen:
                        {
                            Location = new Point(
                                (screen.Bounds.Width - bounds.Width) / 2,
                                (screen.Bounds.Height - bounds.Height) / 2);
                        }
                        break;
                    case FormStartPosition.CenterParent:
                        {
                            var ownerBounds = (Owner != null)
                                ? Owner.Bounds
                                : screen.WorkingArea;

                            Location = new Point(
                                ownerBounds.X + (ownerBounds.Width - bounds.Width) / 2,
                                ownerBounds.Y + (ownerBounds.Height - bounds.Height) / 2);
                        }
                        break;
                }
            }
        }

        // DefWndProc
        internal IntPtr DefWndProc(int msg, IntPtr wParam, IntPtr lParam)
        {
            var m = Message.Create(Handle, msg, wParam, lParam);

            DefWndProc(ref m);

            return m.Result;
        }

        // DestroyHandle
        protected override void DestroyHandle()
        {
            _NonClientPadding   = null;
            _SystemFrameMetrics = null;
            _WindowStyles       = null;

            base.DestroyHandle();
        }

        // DragMove
        /// <summary>
        /// Allows a form to be dragged by a mouse with it's left button down.
        /// </summary>
        protected internal void DragMove()
        {
            if (!IsHandleCreated)
                return;

            var button = SystemInformation.MouseButtonsSwapped
                ? Keys.RButton
                : Keys.LButton;

            if (!IsKeyDown((int)button))
                return;

            var pt = MousePosition;

            _ = ReleaseCapture();
            _ = DefWndProc(WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, MakeLParam(pt.X, pt.Y));
        }

        // GetNormalBounds
        private Rectangle GetNormalBounds()
        {
            var wp = WINDOWPLACEMENT.Empty;

            _ = GetWindowPlacement(Handle, ref wp);

            if (WindowStyles.ToolWindow)
            {
                return wp.rcNormalPosition;
            }
            else
            {
                var rect = Screen.FromHandle(Handle).WorkingArea;

                return new Rectangle(
                    rect.X + wp.rcNormalPosition_left,
                    rect.Y + wp.rcNormalPosition_top,
                    wp.rcNormalPosition.Width,
                    wp.rcNormalPosition.Height);
            }
        }

        // LockBounds
        protected void LockBounds()
        {
            boundsLockCount += 1;
        }

        // NonClientHitTest
        protected virtual NonClientCode NonClientHitTest(Point pt)
        {
            var caption = (NonClientCode)CaptionHitTest(pt);

            switch (caption)
            {
                case NonClientCode.Nowhere:
                    {
                        return (NonClientCode)BorderHitTest(pt);
                    }
                case NonClientCode.Caption:
                    {
                        var border = (NonClientCode)BorderHitTest(pt);

                        return (border == HTNOWHERE)
                            ? caption
                            : border;
                    }
                default:
                    {
                        return caption;
                    }
            }

        }

        // SendSystemCommand
        protected void SendSystemCommand(SystemCommand cmd)
        {
            if (IsHandleCreated)
            {
                _ = ReleaseCapture();
                _ = SendMessage(Handle, WM_SYSCOMMAND, (int)cmd, 0);
            }
        }

        // SetBoundsCore
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (BoundsLocked)
                return;

            if (!IsHandleCreated)
            {
                base.SetBoundsCore(x, y, width, height, specified);
                return;
            }

            var rect = GetNormalBounds();

            if ((specified & BoundsSpecified.X) != 0)
                rect.X = x;

            if ((specified & BoundsSpecified.Y) != 0)
                rect.Y = y;

            if ((specified & BoundsSpecified.Width) != 0)
                rect.Width = width;

            if ((specified & BoundsSpecified.Height) != 0)
                rect.Height = height;

            SetNormalBounds(rect);
        }

        // SetClientSizeCore
        protected override void SetClientSizeCore(int x, int y)
        {
            if (BoundsLocked || !IsRestored)
                return;

            var oldClientSize = ClientSize;

            Size = SizeFromClientSize(new Size(x, y));

            if (IsRestored && ClientSize != oldClientSize)
                OnClientSizeChanged(EventArgs.Empty);
        }

        // SetGlassInsets
        protected virtual void SetGlassInsets(Padding value)
        {
            if (value.Left < 0 ||
                value.Top < 0 ||
                value.Right < 0 ||
                value.Bottom < 0)
            {
                _GlassInsets = new Padding(-1);
            }
            else
                _GlassInsets = value;
        }

        // SetNonClientPadding
        protected virtual void SetNonClientPadding(Padding value)
        {
            _NonClientPadding = value;
        }

        // SetNormalBounds
        private void SetNormalBounds(Rectangle rect)
        {
            var wp = WINDOWPLACEMENT.Empty;

            _ = GetWindowPlacement(Handle, ref wp);

            if (WindowStyles.ToolWindow)
            {
                wp.rcNormalPosition = rect;
            }
            else
            {
                var area = Screen.FromHandle(Handle).WorkingArea;

                wp.rcNormalPosition = new Rectangle(
                    area.X + rect.X,
                    area.Y + rect.Y,
                    rect.Width,
                    rect.Height);
            }

            SetWindowPlacement(Handle, ref wp);
        }

        // ShowSystemMenu
        /// <summary>
        /// Displays the system menu at the specified location in screen coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        internal void ShowSystemMenu(int x, int y)
        {
            if (!IsHandleCreated || !WindowStyles.SysMenu)
                return;

            var hMenu = GetSystemMenu(Handle, false);

            if (hMenu != IntPtr.Zero)
            {
                int cmd = TrackPopupMenuEx(hMenu, TPM_RETURNCMD, x, y, Handle, null);

                if (cmd != 0)
                    _ = SendMessage(Handle, WM_SYSCOMMAND, cmd, 0);
            }
        }
        /// <summary>
        /// Displays the system menu at the specified location.
        /// </summary>
        /// <param name="screenLocation"></param>
        protected internal void ShowSystemMenu(Point screenLocation)
        {
            ShowSystemMenu(screenLocation.X, screenLocation.Y);
        }
        /// <summary>
        /// Displays the system menu at the default location.
        /// </summary>
        protected internal void ShowSystemMenu()
        {
            if (!IsHandleCreated || !WindowStyles.SysMenu)
                return;

            _ = DefWndProc(WM_SYSCOMMAND, (IntPtr)SC_KEYMENU, (IntPtr)Keys.Space);
        }

        // SizeFromClientSize
        protected override Size SizeFromClientSize(Size clientSize)
        {
            return clientSize + DefaultNonClientPadding.Size;
        }

        // UnlockBounds
        protected void UnlockBounds()
        {
            boundsLockCount -= 1;
        }

        // UpdateMaximizedBounds
        private void UpdateMaximizedBounds()
        {
            Rectangle rect;

            if (MaximizedBounds.IsEmpty)
            {
                var screen = Screen.FromHandle(Handle);

                rect = (WindowStyles.Caption && WindowStyles.MaximizeBox && !WindowStyles.ToolWindow)
                    ? screen.WorkingArea
                    : screen.Bounds;

                rect = rect.Grow(SystemFrameMetrics.BorderPadding);
            }
            else
            {
                rect = MaximizedBounds;
            }

            if (Bounds != rect)
            {
                int flags =
                    SWP_NOZORDER |
                    SWP_NOACTIVATE |
                    SWP_FRAMECHANGED |
                    SWP_NOOWNERZORDER |
                    SWP_FRAMECHANGED;

                _ = SetWindowPos(Handle, IntPtr.Zero, rect.X, rect.Y, rect.Width, rect.Height, flags);
            }
        }

        // UpdateNonClient
        /// <summary>
        /// Used at design-time.
        /// </summary>
        internal void UpdateNonClient()
        {
            if (NonClientPadding.IsEmpty())
                return;

            var clipRect = new Rectangle(0, 0, Width, Height);
            var windowDC = GetWindowDC(Handle);

            try
            {
                if (DoubleBuffered)
                {
                    var compatibleDC     = CreateCompatibleDC(windowDC);
                    var compatibleBitmap = CreateCompatibleBitmap(windowDC, clipRect.Width, clipRect.Height);

                    try
                    {
                        _ = SelectObject(compatibleDC, compatibleBitmap);
                        _ = BitBlt(compatibleDC, 0, 0, clipRect.Width, clipRect.Height, windowDC, 0, 0, SRCCOPY);

                        using (Graphics graphics = Graphics.FromHdc(compatibleDC))
                            OnPaintNonClient(new PaintEventArgs(graphics, clipRect));

                        _ = BitBlt(windowDC, 0, 0, clipRect.Width, clipRect.Height, compatibleDC, 0, 0, SRCCOPY);
                    }
                    finally
                    {
                        _ = DeleteObject(compatibleBitmap);
                        _ = DeleteDC(compatibleDC);
                    }
                }
                else
                {
                    using (Graphics graphics = Graphics.FromHdc(windowDC))
                        OnPaintNonClient(new PaintEventArgs(graphics, clipRect));
                }
            }
            finally
            {
                _ = ReleaseDC(Handle, windowDC);
            }
        }

        // UpdateNonClientPadding
        private void UpdateNonClientPadding()
        {
            int flags =
                SWP_NOZORDER |
                SWP_NOACTIVATE |
                SWP_NOOWNERZORDER |
                SWP_FRAMECHANGED |
                SWP_NOMOVE |
                SWP_NOSIZE;

            _ = SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, flags);
        }

        // UpdateWindowState
        private void UpdateWindowState()
        {
            var wp = WINDOWPLACEMENT.Empty;

            _ = GetWindowPlacement(Handle, ref wp);

            switch (wp.showCmd)
            {
                case SW_SHOWNORMAL:
                case SW_SHOWNOACTIVATE:
                case SW_SHOW:
                case SW_SHOWNA:
                case SW_RESTORE:
                    _WindowState = FormWindowState.Normal;
                    break;
                case SW_SHOWMINIMIZED:
                case SW_MINIMIZE:
                case SW_SHOWMINNOACTIVE:
                    _WindowState = FormWindowState.Minimized;
                    break;
                case SW_SHOWMAXIMIZED:
                    _WindowState = FormWindowState.Maximized;
                    break;
            }
        }

        #endregion methods

        #region ## events

        // TextChanged
        public new event EventHandler TextChanged = delegate { };

        #endregion events
    }
}
