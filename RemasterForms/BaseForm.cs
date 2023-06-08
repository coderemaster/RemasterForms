using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

[assembly: InternalsVisibleTo("RemasterFormsPro")]
[assembly: InternalsVisibleTo("RemasterFormsTest")]
namespace RemasterForms
{
    using static NativeMethods;

    internal partial class DisableDesigner { }

    public partial class BaseForm : Form
    {
        #region ## fields

        private bool            _activeMoving;
        private bool            _activeSizing;
        private bool?           _defaultFont;
        private int             _htRightButton;
        private bool            _keepClientSize;
        private Rectangle       _normalBounds;
        private Point?          _normalLocation;
        private Padding         _oldNormalBorder;
        private FormWindowState _oldWindowState;
        private FormWindowState _prevWindowState;
        private bool            _restoreToNormal;
        private int             _scDefault;
        private int             _scNormal;

        internal readonly bool _designTime = LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        internal readonly bool _windows11  = 22000 <= Environment.OSVersion.Version.Build;

        #endregion fields

        #region ## constructors

        internal BaseForm()
        {
            var styles =
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint;

            SetStyle(styles, true);

            WindowStyles = new WindowStyles(CreateParams);

            if (!_designTime)
            {
                DefaultFrameMetrics = FrameMetrics.Default(WindowStyles.ToolWindow);
                SystemFrameMetrics  = new FrameMetrics(WindowStyles);
            }

            _oldNormalBorder = NormalBorderMetrics;

            RestoreClientArea();
        }

        #endregion constructors

        #region ## macros

        internal bool Borderless => FormBorderStyle == FormBorderStyle.None;
        internal bool Sizable    => FormBorderStyle == FormBorderStyle.Sizable         || FormBorderStyle == FormBorderStyle.SizableToolWindow;
        internal bool ToolWindow => FormBorderStyle == FormBorderStyle.FixedToolWindow || FormBorderStyle == FormBorderStyle.SizableToolWindow;

        internal bool IsMaximized => !_designTime && IsHandleCreated  && WindowState == FormWindowState.Maximized;
        internal bool IsMinimized => !_designTime && IsHandleCreated  && WindowState == FormWindowState.Minimized;
        internal bool IsRestored  =>  _designTime || !IsHandleCreated || WindowState == FormWindowState.Normal   ;

        internal bool MaximizedLayout    => IsMaximized && MaximizedBounds.IsEmpty;
        internal bool RestoreToMaximized => _prevWindowState == FormWindowState.Maximized;
        internal bool SnapAvailable      => !_designTime && IsHandleCreated && !IsMinimized && WindowStyles.SizeBox;
        internal bool SnapLayout         => SnapAvailable && Bounds != NormalBounds;
        internal bool SnapMenuAvailable  => _windows11 && SnapAvailable;

        #endregion macros

        #region ## properties

        // BorderMetrics
        [Browsable(false)]
        public BorderMetrics BorderMetrics
        {
            get
            {
                if (Borderless)
                    return BorderMetrics.Empty;

                if (IsMinimized)
                    return new BorderMetrics(Padding.Empty, SystemFrameMetrics.BorderPadding);

                return IsMaximized
                    ? MaximizedBorderMetrics
                    : NormalBorderMetrics;
            }
        }

        // ClientArea
        /// <summary>
        /// A client rectangle excluding client edges.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ClientArea
        {
            get
            {
                return new Rectangle(
                    BorderMetrics.ClientEdges.Left,
                    BorderMetrics.ClientEdges.Top,
                    Width - ((Padding)BorderMetrics).Horizontal,
                    Height - ((Padding)BorderMetrics).Vertical);
            }
        }

        // ClientCoordinates
        /// <summary>
        /// Client area in screen coordinates.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle ClientCoordinates
        {
            get => Bounds.Deflate(IsMinimized ? (Padding)SystemFrameMetrics : BorderMetrics);
            set => Bounds = value.Inflate(BorderMetrics);
        }

        // ClientRectangle
        public new Rectangle ClientRectangle
        {
            get
            {
                return new Rectangle(
                    0,
                    0,
                    Math.Min(ClientSize.Width, Width),
                    Math.Min(ClientSize.Height, Height));
            }
        }

        // ClientSize
        [Browsable(true), Category("Layout")]
        public new Size ClientSize
        {
            get
            {
                var clientSize = base.ClientSize;

                if (Height < clientSize.Height)
                    clientSize.Height = Height;

                return clientSize;
            }
            set => base.ClientSize = value;
        }

        // ColorMode
        private ColorMode _colorMode = ColorMode.Light;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ColorMode ColorMode
        {
            get
            {
                return HighContrastMode
                    ? ColorMode.HighContrast
                    : _colorMode;
            }
            set
            {
                if (value != ColorMode.Light && value != ColorMode.Dark)
                    return;

                var oldColorMode = ColorMode;

                _colorMode = value;

                if (DwmFrame != null)
                {
                    switch (value)
                    {
                        case ColorMode.Light:
                            DwmFrame.DarkMode = false;
                            break;
                        case ColorMode.Dark:
                            DwmFrame.DarkMode = true;
                            break;
                    }
                }

                if (ColorMode != oldColorMode)
                    OnColorModeChanged(EventArgs.Empty);
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
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CornerStyle CornerStyle
        {
            get
            {
                return (DwmFrame != null)
                    ? DwmFrame.CornerStyle
                    : CornerStyle.Default;
            }
            set
            {
                if (DwmFrame != null)
                    DwmFrame.CornerStyle = value;
            }
        }

        // CreateParams
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;

                if (!Borderless)
                    cp.Style |= WS_CAPTION;

                if (_designTime)
                    cp.Style &= ~WS_SYSMENU;

                return cp;
            }
        }

        // DefaultFont
        [Browsable(false)]
        public new virtual Font DefaultFont
        {
            get => SystemFonts.MessageBoxFont;
        }

        // DefaultFrameMetrics
        private FrameMetrics? _defaultFrameMetrics;
        /// <summary>
        /// Gets system frame metrics for the window with caption and resize border.
        /// Cached if handle is created.
        /// </summary>
        [Browsable(false)]
        public FrameMetrics DefaultFrameMetrics
        {
            get
            {
                return (_defaultFrameMetrics != null)
                   ? (FrameMetrics)_defaultFrameMetrics
                   : FrameMetrics.Default(WindowStyles.ToolWindow);
            }
            private set => _defaultFrameMetrics = value;
        }

        // DefaultSystemCommand
        /// <summary>
        /// The command that will be sent to the window when by double-clicking on the caption.
        /// </summary>
        [Browsable(false)]
        public SystemCommandID DefaultSystemCommand
        {
            get
            {
                switch (WindowState)
                {
                    case FormWindowState.Normal:
                        {
                            if (SnapLayout)
                                return SystemCommandID.Restore;

                            if (MaximizeBox)
                                return SystemCommandID.Maximize;
                        }
                        break;
                    case FormWindowState.Minimized:
                        {
                            return (_prevWindowState == FormWindowState.Maximized)
                                ? SystemCommandID.Maximize
                                : SystemCommandID.Restore;
                        }
                    case FormWindowState.Maximized:
                        {
                            if (MaximizeBox)
                                return SystemCommandID.Restore;
                        }
                        break;
                }

                return SystemCommandID.None;
            }
        }

        // DisplayRectangle
        public override Rectangle DisplayRectangle
        {
            get => ClientArea.Deflate(Padding);
        }

        // DwmFrame
        private DwmFrame _dwmFrame;
        internal DwmFrame DwmFrame
        {
            get
            {
                if (_dwmFrame == null && !_designTime)
                    _dwmFrame = new DwmFrame(this);

                return _dwmFrame;
            }
        }

        // Font
        public override Font Font
        {
            get
            {
                if (_defaultFont == null)
                {
                    _defaultFont = true;
                    base.Font = DefaultFont;
                }

                return base.Font;
            }
            set
            {
                _defaultFont = value == null;
                base.Font = value ?? DefaultFont;
            }
        }
        public override void ResetFont() => Font = null;
        private bool ShouldSerializeFont() => _defaultFont == false;

        // FormBorderStyle
        private FormBorderStyle _formBorderStyle = FormBorderStyle.Sizable;
        public new FormBorderStyle FormBorderStyle
        {
            get => _formBorderStyle;
            set
            {
                _formBorderStyle = value;

                _keepClientSize = true;
                SuspendLayout();

                switch (value)
                {
                    case FormBorderStyle.Fixed3D:
                    case FormBorderStyle.FixedDialog:
                        base.FormBorderStyle = FormBorderStyle.FixedSingle;
                        break;
                    default:
                        base.FormBorderStyle = value;
                        break;
                }

                _keepClientSize = false;
                ResumeLayout(false);
                PerformLayout();
            }
        }

        // FrameMetrics
        [Browsable(false)]
        public FrameMetrics FrameMetrics
        {
            get
            {
                if (Borderless)
                    return FrameMetrics.Empty;

                if (IsMinimized)
                    return SystemFrameMetrics;

                return IsMaximized
                    ? MaximizedFrameMetrics
                    : NormalFrameMetrics;
            }
        }

        // HighContrastLayout
        private bool? _highContrastLayout;
        internal bool HighContrastLayout
        {
            get => _highContrastLayout == true;
            private set => _highContrastLayout = value;
        }

        // HighContrastMode
        private bool? _highContrastMode;
        internal bool HighContrastMode
        {
            get
            {
                return (_highContrastMode != null)
                    ? (bool)_highContrastMode
                    : SystemInformation.HighContrast;
            }
            private set => _highContrastMode = value;
        }

        // MaximizedBorderMetrics
        internal virtual BorderMetrics MaximizedBorderMetrics
        {
            get => new BorderMetrics(
                Padding.Empty, 
                MaximizedBounds.IsEmpty ? SystemFrameMetrics.BorderPadding : Padding.Empty);
        }

        // MaximizedBounds
        protected new Rectangle MaximizedBounds
        {
            get => base.MaximizedBounds;
            set
            {
                base.MaximizedBounds = value;
                UpdateMaximizedBounds();
            }
        }

        // MaximizedFrameMetrics
        internal virtual FrameMetrics MaximizedFrameMetrics
        {
            get => MaximizedBounds.IsEmpty 
                ? SystemFrameMetrics
                : new FrameMetrics(Padding.Empty, SystemFrameMetrics.CaptionHeight);
        }

        // NonClientActive
        internal bool NonClientActive { get; private set; }

        // NormalBorderMetrics
        private BorderMetrics? _normalBorderMetrics;
        internal BorderMetrics NormalBorderMetrics
        {
            get
            {
                if (_normalBorderMetrics == null)
                {
                    var metrics = Borderless
                        ? BorderMetrics.Empty
                        : AdjustBorderMetrics();

                    if (!IsHandleCreated)
                        return metrics;

                    _normalBorderMetrics = metrics;
                }

                return (BorderMetrics)_normalBorderMetrics;
            }
        }

        // NormalBounds
        internal Rectangle NormalBounds
        {
            get
            {
                if (!IsHandleCreated)
                    return Bounds;

                var wp = WINDOWPLACEMENT.Empty;

                _ = GetWindowPlacement(Handle, ref wp);

                if (!WindowStyles.ToolWindow)
                    wp.rcNormalPosition.Offset(Screen.FromHandle(Handle).WorkingArea.Location);

                return wp.rcNormalPosition;
            }
            set
            {
                if (!IsHandleCreated)
                {
                    Bounds = value;
                    return;
                }

                var wp = WINDOWPLACEMENT.Empty;

                _ = GetWindowPlacement(Handle, ref wp);

                if (!WindowStyles.ToolWindow)
                    value.Offset(Screen.FromHandle(Handle).WorkingArea.Location);

                wp.rcNormalPosition = value;

                _ = SetWindowPlacement(Handle, ref wp);
            }
        }

        // NormalFrameMetrics
        private FrameMetrics? _normalFrameMetrics;
        internal FrameMetrics NormalFrameMetrics
        {
            get
            {
                if (_normalFrameMetrics == null)
                {
                    var metrics = Borderless
                        ? FrameMetrics.Empty
                        : AdjustFrameMetrics();

                    if (!IsHandleCreated)
                        return metrics;

                    _normalFrameMetrics = metrics;
                }

                return (FrameMetrics)_normalFrameMetrics;
            }
        }

        // Padding
        public new Padding Padding
        {
            get => base.Padding;
            set => base.Padding = value;
        }
        private void ResetPadding()
        {
            // BUGFIX_2
            // - The layout is not updated when reset.

            Padding = Padding.Empty;
        }

        // RestoreBounds
        private Rectangle? _restoreBounds;
        public new Rectangle RestoreBounds
        {
            get
            {
                return (_restoreBounds != null)
                    ? (Rectangle)_restoreBounds
                    : Bounds;
            }
            private set => _restoreBounds = value;
        }

        // ShowIcon
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ShowIcon
        {
            get => base.ShowIcon;
            set => base.ShowIcon = value;
        }

        // ShowInTaskbar
        private bool _showInTaskbar = true;
        public new bool ShowInTaskbar
        {
            get => _showInTaskbar;
            set
            {
                _showInTaskbar = value;

                // avoid re-creating the handle
                if (!_designTime)
                    base.ShowInTaskbar = value;
            }
        }

        // Size
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size Size
        {
            get => base.Size;
            set => base.Size = value;
        }

        // StartPosition
        private FormStartPosition _startPosition = FormStartPosition.WindowsDefaultLocation;
        public new FormStartPosition StartPosition
        {
            get => _startPosition;
            set
            {
                _startPosition = value;
                base.StartPosition = value;
            }
        }

        // SystemFrameMetrics
        private FrameMetrics? _systemFrameMetrics;
        /// <summary>
        /// Gets system frame metrics of the window.
        /// Cached if handle is created.
        /// </summary>
        internal FrameMetrics SystemFrameMetrics
        {
            get
            {
                var metrics = (_systemFrameMetrics != null)
                   ? (FrameMetrics)_systemFrameMetrics
                   : new FrameMetrics(WindowStyles);

                return IsMinimized
                    ? new FrameMetrics(metrics.BorderPadding, Height - metrics.BorderPadding.Vertical)
                    : metrics;
            }
            private set => _systemFrameMetrics = value;
        }

        // WindowState
        private FormWindowState _windowState = FormWindowState.Normal;
        public new FormWindowState WindowState
        {
            // UPGRADE
            // + WindowState is updated on WM_NCCALCSIZE.

            get => _windowState;
            set
            {
                _windowState = value;
                base.WindowState = value;
            }
        }

        // WindowStyles
        /// <summary>
        /// Gets styles of the window. 
        /// Cached if handle is created.
        /// </summary>
        internal WindowStyles WindowStyles { get; private set; }

        #endregion properties

        #region ## methods

        // AdjustBorderMetrics
        internal virtual BorderMetrics AdjustBorderMetrics()
        {
            var clientEdges = Padding.Empty;
            var windowEdges = Padding.Empty;

            if (HighContrastLayout)
            {
                clientEdges = NormalFrameMetrics.BorderPadding;
                clientEdges.Top = 2;

                windowEdges = Padding.Empty;                
            }
            else if (!_designTime && WindowStyles.ToolWindow)
            {
                // Make the tool window border the same in all versions.

                if (_windows11)
                    windowEdges = new Padding(1, 0, 1, 1);
                else
                    clientEdges = new Padding(1);
            }
            else if (_designTime || _windows11)
            {
                clientEdges = Padding.Empty;

                windowEdges = NormalFrameMetrics.BorderPadding;
                windowEdges.Top = 0;
            }
            else
            {
                clientEdges = new Padding(0, 1, 0, 0);

                windowEdges = NormalFrameMetrics.BorderPadding;
                windowEdges.Top = 0;
            }

            return new BorderMetrics(clientEdges, windowEdges);
        }

        // AdjustFrameMetrics
        internal virtual FrameMetrics AdjustFrameMetrics()
        {
            return DefaultFrameMetrics;
        }

        // AdjustGlassInsets
        internal virtual Padding AdjustGlassInsets()
        {
            if (_windows11 || Borderless || HighContrastLayout)
                return Padding.Empty;

            return WindowStyles.ToolWindow
                ? new Padding(1, 2, 1, 1)
                : new Padding(0, 2, 0, 0);
        }

        // BorderHitTest
        internal virtual int BorderHitTest(Point pt)
        {
            if (IsRestored && WindowStyles.SizeBox)
            {
                var border = FrameMetrics.BorderPadding;
                var corner = border + new Padding(border.Left, border.Top / 2, border.Right, border.Bottom / 2);

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

                return !Bounds.Deflate(border).Contains(pt)
                    ? values[v, h]
                    : HTNOWHERE;
            }
            else
            {
                return ClientCoordinates.Contains(pt)
                    ? HTNOWHERE
                    : HTBORDER;
            }

        }

        // CaptionHitTest
        internal virtual int CaptionHitTest(Point pt)
        {
            if (WindowStyles.Caption)
            {
                var rect = new Rectangle(0, 0, ClientSize.Width, ((Padding)FrameMetrics).Top);

                if (rect.Contains(PointToClient(pt)))
                    return HTCAPTION;
            }

            return HTNOWHERE;
        }

        // CreateHandle
        protected override void CreateHandle()
        {
            _oldWindowState = WindowState;

            if (!RecreatingHandle)
                _prevWindowState = WindowState;

            if (!_designTime && !RecreatingHandle)
            {
                // BUGFIX_1.0
                // - The window is not centered if WindowState is Maximized at startup.
                switch (StartPosition)
                {
                    case FormStartPosition.CenterScreen:
                        {
                            // BUGFIX_1.1
                            // - The window is centered relative to the desktop area instead of the entire screen.

                            base.StartPosition = FormStartPosition.Manual;

                            var screenBounds = Screen.PrimaryScreen.Bounds;

                            Location = new Point(
                                (screenBounds.Width  - Width ) / 2,
                                (screenBounds.Height - Height) / 2);
                        }
                        break;
                    case FormStartPosition.CenterParent:
                        {
                            // UPGRADE_1.2
                            // + Center relative to the Owner, if there is one.
                            //   Otherwise, center relative to the desktop area.

                            var ownerBounds = (Owner != null)
                                ? Owner.Bounds
                                : Screen.PrimaryScreen.WorkingArea;

                            base.StartPosition = FormStartPosition.Manual;

                            Location = new Point(
                                ownerBounds.X + (ownerBounds.Width  - Width ) / 2,
                                ownerBounds.Y + (ownerBounds.Height - Height) / 2);
                        }
                        break;
                }
            }

            base.CreateHandle();

            if (!RecreatingHandle)
            {
                NormalBounds  = _normalBounds;
                RestoreBounds = _normalBounds;
            }

            RestoreClientArea();
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
            if (!_designTime)
            {
                switch (WindowState)
                {
                    case FormWindowState.Normal:
                        // restore if snapped
                        _ = ReleaseCapture();
                        _ = SendMessage(Handle, WM_SYSCOMMAND, SC_RESTORE, 0);
                        break;
                    case FormWindowState.Minimized:
                        RestoreBounds = NormalBounds;
                        break;
                }
            }

            base.DestroyHandle();
        }

        // DrawNonClient
        /// <summary>
        /// Used only at design-time.
        /// </summary>
        internal void DrawNonClient()
        {
            var rcWindow = (Rectangle)GetWindowRect(Handle);
            var rcClient = RectangleToScreen(GetClientRect(Handle));

            rcClient.X -= rcWindow.X;
            rcClient.Y -= rcWindow.Y;

            rcWindow.X = 0;
            rcWindow.Y = 0;

            if (rcWindow.Exclude(rcClient).IsEmpty())
                return;

            var windowDC = GetWindowDC(Handle);
            try
            {
                using (var graphics = Graphics.FromHdc(windowDC))
                {
                    if (WindowStyles.LayoutRtl)
                        rcClient.Offset(-1, 0);

                    graphics.ExcludeClip(rcClient);

                    OnPaintNonClient(new PaintEventArgs(graphics, rcWindow));
                }
            }
            finally
            {
                _ = ReleaseDC(Handle, windowDC);
            }
        }

        // InitSystemMenuPopup
        internal virtual void InitSystemMenuPopup(IntPtr hMenu)
        {
            int defCmd = (int)DefaultSystemCommand;

            switch (WindowState)
            {
                case FormWindowState.Normal:
                    {
                        bool snapped = defCmd == SC_RESTORE;

                        SetMenuItemEnabled(hMenu, SC_RESTORE, snapped);
                        SetMenuItemEnabled(hMenu, SC_MOVE, !Borderless && !snapped);
                        SetMenuItemEnabled(hMenu, SC_SIZE, Sizable);
                        SetMenuItemEnabled(hMenu, SC_MINIMIZE, MinimizeBox);
                        SetMenuItemEnabled(hMenu, SC_MAXIMIZE, MaximizeBox);
                    }
                    break;
                case FormWindowState.Minimized:
                    {
                        if (_prevWindowState == FormWindowState.Maximized)
                        {
                            SetMenuItemEnabled(hMenu, SC_RESTORE, MaximizeBox);
                            SetMenuItemEnabled(hMenu, SC_MAXIMIZE, true);
                        }
                        else
                        {
                            SetMenuItemEnabled(hMenu, SC_RESTORE, true);
                            SetMenuItemEnabled(hMenu, SC_MAXIMIZE, MaximizeBox);
                        }

                        SetMenuItemEnabled(hMenu, SC_MOVE, !ShowInTaskbar);
                        SetMenuItemEnabled(hMenu, SC_SIZE, false);
                        SetMenuItemEnabled(hMenu, SC_MINIMIZE, false);
                    }
                    break;
                case FormWindowState.Maximized:
                    {
                        SetMenuItemEnabled(hMenu, SC_RESTORE, MaximizeBox);
                        SetMenuItemEnabled(hMenu, SC_MOVE, false);
                        SetMenuItemEnabled(hMenu, SC_SIZE, false);
                        SetMenuItemEnabled(hMenu, SC_MINIMIZE, MinimizeBox);
                        SetMenuItemEnabled(hMenu, SC_MAXIMIZE, false);
                    }
                    break;
            }

            int defItem = -1;

            if (_scDefault == 0)
            {
                defItem = defCmd;
            }
            else if (
                _scDefault != -1 
                && MenuItemExists(hMenu, _scDefault)
                && IsMenuItemEnabled(hMenu, _scDefault))
            {
                defItem = _scDefault;
            }

            if (defItem == -1)
            {
                SetMenuDefaultItem(hMenu, -1);
            }
            else
            {
                SetMenuDefaultItem(hMenu, defItem);

                // update metrics
                {
                    var text = GetMenuItemText(hMenu, defItem);

                    if (!string.IsNullOrEmpty(text))
                        SetMenuItemText(hMenu, defItem, text);
                }
            }

            _scNormal = 0;

            if (IsMinimized && _prevWindowState == FormWindowState.Maximized)
            {
                _scNormal = GetMenuVacantID(hMenu);

                if (_scNormal != 0)
                    SetMenuItemID(hMenu, SC_RESTORE, _scNormal);
            }
        }

        // InvalidateBorderRegion
        internal virtual void InvalidateBorderRegion()
        {
            if (!IsHandleCreated || IsMinimized)
                return;

            if (!BorderMetrics.ClientEdges.IsEmpty())
            {
                var rect = ClientRectangle.Translate(this);

                using (var region = new Region(rect))
                {
                    region.Exclude(rect.Deflate(BorderMetrics.ClientEdges));
                    Invalidate(region, true);
                }
            }
        }

        // ProcessNcMouse
        internal virtual bool ProcessNcMouse(ref Message m)
        {
            int ht = (int)m.WParam;
            var pt = POINTS.FromLParam(m.LParam);

            switch (m.Msg)
            {
                case WM_NCLBUTTONDBLCLK:
                    {
                        if (ht == HTCAPTION)
                        {
                            var cmd = DefaultSystemCommand;

                            if (cmd != SystemCommandID.None)
                            {
                                _ = ReleaseCapture();
                                _ = SendMessage(Handle, WM_SYSCOMMAND, (int)cmd, 0);
                            }

                            return true;
                        }
                    }
                    break;
                case WM_NCRBUTTONDOWN:
                    {
                        _htRightButton = ht;
                        return true;
                    }
                case WM_NCRBUTTONUP:
                    {
                        int oldHt = _htRightButton;

                        _htRightButton = HTNOWHERE;

                        if (ht == HTCAPTION
                            && oldHt == HTCAPTION
                            && WindowStyles.SysMenu)
                        {
                            ShowSystemMenu(pt.x, pt.y);
                        }

                        return true;
                    }
                case WM_NCMOUSELEAVE:
                    {
                        _htRightButton = HTNOWHERE;
                    }
                    break;
                case WM_NCMOUSEMOVE:
                    {
                        if (ht != _htRightButton)
                            _htRightButton = HTNOWHERE;
                    }
                    break;
            }

            return false;
        }

        // RestoreClientArea
        /// <summary>
        /// Restores the client area for a window in the normal state.
        /// </summary>
        internal void RestoreClientArea()
        {
            var oldPadding = _oldNormalBorder;
            var newPadding = (Padding)NormalBorderMetrics;

            _oldNormalBorder = newPadding;

            if (oldPadding != newPadding)
            {
                var rect = NormalBounds.Deflate(oldPadding).Inflate(newPadding);

                var specified = _designTime
                    ? BoundsSpecified.Size
                    : BoundsSpecified.All;

                SetBounds(rect.X, rect.Y, rect.Width, rect.Height, specified);
            }
        }

        // SendSystemCommand
        public void SendSystemCommand(SystemCommandID cmd)
        {
            if (!Enum.IsDefined(typeof(SystemCommandID), cmd))
                return;

            if (!IsHandleCreated)
                return;

            if (cmd == SystemCommandID.None)
                return;

            _ = ReleaseCapture();
            _ = SendMessage(Handle, WM_SYSCOMMAND, (int)cmd, 0);
        }

        // SetBoundsCore
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (!_designTime
                && IsHandleCreated
                && IsRestored
                && _oldWindowState != FormWindowState.Normal)
            {
                // prevent restoring bounds after restoring the window
                return;
            }

            if (_designTime || !IsHandleCreated)
            {
                base.SetBoundsCore(x, y, width, height, specified);
                return;
            }

            var rect = NormalBounds;

            if (IsRestored)
            {
                // Restore if snapped, otherwise the corners will remain not rounded.
                if (Bounds != rect)
                {
                    _ = ReleaseCapture();
                    _ = SendMessage(Handle, WM_SYSCOMMAND, SC_RESTORE, 0);
                }

                base.SetBoundsCore(x, y, width, height, specified);
                return;
            }

            if ((specified & BoundsSpecified.X) != 0)
                rect.X = x;

            if ((specified & BoundsSpecified.Y) != 0)
                rect.Y = y;

            if ((specified & BoundsSpecified.Width) != 0)
                rect.Width = width;

            if ((specified & BoundsSpecified.Height) != 0)
                rect.Height = height;

            NormalBounds  = rect;
            RestoreBounds = rect;

            _restoreToNormal = IsMinimized;
        }

        // SetClientSizeCore
        protected override void SetClientSizeCore(int x, int y)
        {
            if (_keepClientSize)
                return;

            if (IsRestored)
            {
                var oldClientSize = ClientSize;

                Size = SizeFromClientSize(new Size(x, y));

                if (ClientSize != oldClientSize)
                    OnClientSizeChanged(EventArgs.Empty);
            }
        }

        // ShowSystemMenu
        /// <summary>
        /// Displays the system menu at the specified location in screen coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scDefault"></param>
        internal void ShowSystemMenu(int x, int y, int scDefault = 0)
        {
            if (IsHandleCreated && WindowStyles.SysMenu)
            {
                var hMenu = GetSystemMenu(Handle, false);

                if (hMenu != IntPtr.Zero)
                {
                    _scDefault = scDefault;

                    int cmd = TrackPopupMenuEx(hMenu, TPM_RETURNCMD, x, y, Handle, null);

                    if (cmd != 0)
                        _ = SendMessage(Handle, WM_SYSCOMMAND, cmd, 0);

                    _scDefault = 0;
                }
            }
        }
        /// <summary>
        /// Displays the system menu at the specified location in client coordinates of the control.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="location"></param>
        /// <param name="scDefault"></param>
        internal void ShowSystemMenu(Control control, Point location, int scDefault = 0)
        {
            if (control != null && control.IsHandleCreated)
            {
                var pt = control.PointToScreen(location);

                ShowSystemMenu(pt.X, pt.Y, scDefault);
            }
        }
        /// <summary>
        /// Displays the system menu at the default location.
        /// </summary>
        internal void ShowSystemMenu()
        {
            if (IsHandleCreated && WindowStyles.SysMenu)
            {
                _scDefault = SC_CLOSE;
                _ = DefWndProc(WM_SYSCOMMAND, (IntPtr)SC_KEYMENU, (IntPtr)Keys.Space);
                _scDefault = 0;
            }
        }

        // SizeFromClientSize
        protected override Size SizeFromClientSize(Size clientSize)
        {
            return Borderless
                ? clientSize
                : clientSize + ((Padding)NormalBorderMetrics).Size;
        }

        // StartMoving
        /// <summary>
        /// Allows a form to be moved by a mouse with it's left button down.
        /// </summary>
        public void StartMoving()
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

        // UninitSystemMenuPopup
        internal virtual void UninitSystemMenuPopup(IntPtr hMenu)
        {
            if (_scNormal != 0)
                SetMenuItemID(hMenu, _scNormal, SC_RESTORE);
        }

        // UpdateMaximizedBounds
        /// <summary>
        /// Updates the window rectangle for the maximized window when the border metrics change.
        /// </summary>
        internal void UpdateMaximizedBounds()
        {
            if (!IsMaximized)
                return;

            Rectangle rect;

            if (MaximizedBounds.IsEmpty)
            {
                var screen = Screen.FromHandle(Handle);

                rect = (WindowStyles.Caption && WindowStyles.MaximizeBox && !WindowStyles.ToolWindow)
                    ? screen.WorkingArea
                    : screen.Bounds;

                rect = rect.Inflate(SystemFrameMetrics.BorderPadding);
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

        // UpdateStyles
        protected internal new void UpdateStyles()
        {
            _normalBorderMetrics = null;
            _normalFrameMetrics  = null;

            base.UpdateStyles();
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
                    _windowState = FormWindowState.Normal;
                    break;
                case SW_SHOWMINIMIZED:
                case SW_MINIMIZE:
                case SW_SHOWMINNOACTIVE:
                    _windowState = FormWindowState.Minimized;
                    break;
                case SW_SHOWMAXIMIZED:
                    _windowState = FormWindowState.Maximized;
                    break;
            }
        }

        #endregion methods

        #region ## events

        // TextChanged
        public new event EventHandler TextChanged;

        #endregion events
    }
}
