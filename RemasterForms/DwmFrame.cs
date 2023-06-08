using System;
using System.Drawing;
using System.Windows.Forms;

namespace RemasterForms
{
    using static NativeMethods;

    /// <summary>
    /// Desktop Window Manager (DWM) Frame.
    /// </summary>
    /// <remarks>
    /// https://learn.microsoft.com/en-us/windows/win32/dwm/dwm-overview
    /// </remarks>
    internal class DwmFrame
    {
        #region ## fields

        private readonly bool _noCornerStyle = Environment.OSVersion.Version.Build < 22000;
        private readonly bool _noDarkMode    = Environment.OSVersion.Version.Build < 17763;

        private readonly BaseForm _owner;

        #endregion fields

        #region ## constructors

        internal DwmFrame(BaseForm owner)
        {
            _owner = owner;
        }

        #endregion constructors

        #region ## properties

        // AllowTransitions
        private bool _allowTransitions = true;
        /// <summary>
        /// Enables or forcibly disables DWM transitions.
        /// </summary>
        internal bool AllowTransitions
        {
            get => _allowTransitions;
            set
            {
                _allowTransitions = value;

                if (_owner.IsHandleCreated)
                {
                    var attr = (int)DWMWINDOWATTRIBUTE.DWMWA_TRANSITIONS_FORCEDISABLED;
                    int attrValue = value ? 0 : 1;

                    _ = DwmSetWindowAttribute(_owner.Handle, attr, ref attrValue, sizeof(int));
                }
            }
        }

        // CornerStyle
        private CornerStyle _cornerStyle = CornerStyle.Default;
        /// <summary>
        /// Sets the rounded corner preference for a window.
        /// Supported starting with Windows 11 Build 22000.
        /// </summary>
        public CornerStyle CornerStyle
        {
            get => _cornerStyle;
            set
            {
                if (!Enum.IsDefined(typeof(CornerStyle), value))
                    return;

                if (_noCornerStyle)
                    return;

                _cornerStyle = value;

                if (_owner.IsHandleCreated)
                {
                    int attr = (int)DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
                    int attrValue = (int)value;

                    _ = DwmSetWindowAttribute(_owner.Handle, attr, ref attrValue, sizeof(int));
                }
            }
        }

        // DarkMode
        private bool _darkMode = false;
        /// <summary>
        /// Allows the window frame to be drawn in dark mode colors.
        /// Supported starting with Windows 10 Build 17763.
        /// </summary>
        public bool DarkMode
        {
            get => _darkMode;
            set
            {
                if (_noDarkMode)
                    return;

                _darkMode = value;

                if (_owner.IsHandleCreated)
                {
                    var attr = (Environment.OSVersion.Version.Build < 18985)
                        ? (int)DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1
                        : (int)DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE;

                    int attrValue = value ? 1 : 0;

                    _ = DwmSetWindowAttribute(_owner.Handle, attr, ref attrValue, sizeof(int));
                }
            }
        }

        // GlassInsets
        private Padding _glassInsets = Padding.Empty;
        /// <summary>
        /// Extends the DWM frame into the client area.
        /// </summary>
        internal Padding GlassInsets
        {
            get => _glassInsets;
            set
            {
                _glassInsets = value;

                if (_owner.IsHandleCreated)
                {
                    MARGINS margins = value;
                    _ = DwmExtendFrameIntoClientArea(_owner.Handle, ref margins);
                }
            }
        }

        #endregion properties

        #region ## methods

        // Initialize
        internal void Initialize()
        {
            int policy = (int)DWMNCRENDERINGPOLICY.DWMNCRP_ENABLED;

            _ = DwmSetWindowAttribute(
                _owner.Handle,
                (int)DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_POLICY,
                ref policy,
                sizeof(int));

            AllowTransitions = AllowTransitions;
            CornerStyle      = CornerStyle;
            DarkMode         = DarkMode;
            GlassInsets      = GlassInsets;
        }

        #endregion methods
    }
}
