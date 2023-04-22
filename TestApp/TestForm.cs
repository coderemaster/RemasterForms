using RemasterForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#pragma warning disable IDE1006 // Naming rule violation

namespace TestApp
{
    public partial class TestForm : BaseForm
    {
        private readonly bool Windows11 = 22000 <= Environment.OSVersion.Version.Build;

        public TestForm()
        {
            InitializeComponent();
        }

        #region ## custom border

        int BorderHeight = 0;

        private void UpdateBorder()
        {
            if (WindowState == FormWindowState.Minimized)
                return;

            int newBorderHeight = 0;
            int newGlassHeight  = 0;

            if (FormBorderStyle != FormBorderStyle.None && WindowState != FormWindowState.Maximized)
            {
                if (SystemInformation.HighContrast)
                {
                    newBorderHeight = 2;
                    newGlassHeight  = 1;
                }
                else if (!Windows11 && !(ToolWindow && ControlBox))
                {
                    newBorderHeight = 1;
                    newGlassHeight  = 2;
                }
            }

            int maxHeight = Math.Max(
                Math.Max(BorderHeight,    newBorderHeight),
                Math.Max(GlassInsets.Top, newGlassHeight ));

            BorderHeight = newBorderHeight;
            GlassInsets  = new Padding(0, newGlassHeight, 0, 0);

            if (IsHandleCreated)
            {
                Invalidate(new Rectangle(0, 0, ClientSize.Width, maxHeight));
                Update();
            }
        }

        protected override void OnHighContrastChanged(EventArgs e)
        {
            if (FormFrameStyle == FormFrameStyle.Default)
                UpdateBorder();
        }

        protected override void OnNonClientActiveChanged(EventArgs e)
        {
            int maxHeight = Math.Max(BorderHeight, GlassInsets.Top);

            if (0 < maxHeight)
                Invalidate(new Rectangle(0, 0, ClientSize.Width, maxHeight));
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (0 < GlassInsets.Top)
            {
                var eraseColor = DoubleBuffered
                    ? Color.Transparent
                    : Color.Black;

                if (0 < BorderHeight)
                {
                    using (var brush = new SolidBrush(eraseColor))
                    {
                        e.Graphics.FillRectangle(
                            brush,
                            new Rectangle(0, 0, ClientSize.Width, 1));
                    }
                }

                if (1 < BorderHeight) // high contrast
                {
                    var captionColor = NonClientActive
                        ? SystemColors.ActiveCaption
                        : SystemColors.InactiveCaption;

                    using (var pen = new Pen(captionColor))
                        e.Graphics.DrawLine(pen, 0, 1, ClientSize.Width - 1, 1);
                }
                else
                {
                    using (var pen = new Pen(BackColor))
                        e.Graphics.DrawLine(pen, 0, BorderHeight, ClientSize.Width - 1, BorderHeight);
                }

                e.Graphics.ExcludeClip(
                    new Rectangle(0, 0, ClientSize.Width, Math.Max(BorderHeight, GlassInsets.Top)));
            }

            using (var brush = new SolidBrush(BackColor))
                e.Graphics.FillRectangle(brush, ClientRectangle);
        }

        protected override void OnStyleChanged(EventArgs e)
        {
            base.OnStyleChanged(e);
            UpdateBorder();
        }

        protected override void OnWindowStateChanged(EventArgs e)
        {
            if (FormFrameStyle == FormFrameStyle.Default)
                UpdateBorder();
        }

        #endregion custom border

        #region ## system menu

        bool SizeMoveMode;

        protected override NonClientCode NonClientHitTest(Point pt)
        {
            var code = base.NonClientHitTest(pt);

            return (!SizeMoveMode && code == NonClientCode.Caption)
                ? NonClientCode.Nowhere
                : code;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (cbSmCaption.Checked)
                {
                    ControlBox = true;
                    ShowSystemMenu(PointToScreen(e.Location));
                    ControlBox = cbSmTaskbar.Checked;
                }

                return;
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (WindowState)
                {
                    case FormWindowState.Normal:
                        {
                            var cmd = (Bounds == NormalBounds)
                                ? SystemCommand.Maximize
                                : SystemCommand.Restore;

                            SendSystemCommand(cmd);
                            return;
                        }
                    case FormWindowState.Maximized:
                        {
                            SendSystemCommand(SystemCommand.Restore);
                            return;
                        }
                }
            }

            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DragMove();
                return;
            }

            base.OnMouseMove(e);
        }

        protected override void OnResizeBegin(EventArgs e)
        {
            base.OnResizeBegin(e);
            SizeMoveMode = true;
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            SizeMoveMode = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.Space))
            {
                if (cbSmAltSpace.Checked)
                {
                    ControlBox = true;
                    ShowSystemMenu();
                    ControlBox = cbSmTaskbar.Checked;
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion system menu

        #region ## interface

        protected override void OnNonClientMetricsChanged(EventArgs e)
        {
            Font = SystemFonts.MessageBoxFont;
        }

        private void UpdateColors()
        {
            if (SystemInformation.HighContrast)
            {
                BackColor = SystemColors.Control;
                ForeColor = SystemColors.ControlText;
            }
            else if (cbDarkMode.Checked)
            {
                BackColor = Color.FromArgb(43, 43, 43);
                ForeColor = Color.White;

                DarkMode = true;
            }
            else
            {
                BackColor = Color.White;
                ForeColor = Color.Black;

                DarkMode = false;
            }
        }

        private void TestForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                Close();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            Padding = SystemFrameMetrics.DefaultWindow.FramePadding;

            cbCloseBox.Checked    = CloseBox;
            cbDarkMode.Checked    = false;
            cbMaximizeBox.Checked = MaximizeBox;
            cbMinimizeBox.Checked = MinimizeBox;
            cbSizeBox.Checked     = SizeBox;
            cbToolWindow.Checked  = ToolWindow;

            cbCloseBox.CheckedChanged    += (_s, _e) => CloseBox      = cbCloseBox.Checked;
            cbDarkMode.CheckedChanged    += (_s, _e) => UpdateColors();
            cbMaximizeBox.CheckedChanged += (_s, _e) => MaximizeBox   = cbMaximizeBox.Checked;
            cbMinimizeBox.CheckedChanged += (_s, _e) => MinimizeBox   = cbMinimizeBox.Checked;
            cbSizeBox.CheckedChanged     += (_s, _e) => SizeBox       = cbSizeBox.Checked;
            cbToolWindow.CheckedChanged  += (_s, _e) => ToolWindow    = cbToolWindow.Checked;

            if (Windows11)
            {
                switch (CornerStyle)
                {
                    case CornerStyle.Default:
                        rbCsDefault.Checked = true;
                        break;
                    case CornerStyle.DoNotRound:
                        rbCsDoNotRound.Checked = true;
                        break;
                    case CornerStyle.Round:
                        rbCsRound.Checked = true;
                        break;
                    case CornerStyle.RoundSmall:
                        rbCsRoundSmall.Checked = true;
                        break;
                }

                rbCsDefault.CheckedChanged    += rbCornerStyle_CheckedChanged;
                rbCsDoNotRound.CheckedChanged += rbCornerStyle_CheckedChanged;
                rbCsRound.CheckedChanged      += rbCornerStyle_CheckedChanged;
                rbCsRoundSmall.CheckedChanged += rbCornerStyle_CheckedChanged;
            }
            else
            {
                flpCornerStyle.Visible = false;
            }

            switch (FormFrameStyle)
            {
                case FormFrameStyle.None:
                    rbFfsNone.Checked = true;
                    break;
                case FormFrameStyle.Default:
                    rbFfsDefault.Checked = true;
                    break;
            }

            rbFfsNone.CheckedChanged    += rbFormFrameStyle_CheckedChanged;
            rbFfsDefault.CheckedChanged += rbFormFrameStyle_CheckedChanged;

            cbSmAltSpace.Checked = true;
            cbSmCaption.Checked  = true;
            cbSmTaskbar.Checked  = ControlBox;

            cbSmTaskbar.CheckedChanged += (_s, _e) => ControlBox = cbSmTaskbar.Checked;

            UpdateColors();
            SystemColorsChanged += (_s, _e) => UpdateColors();
        }

        private void rbCornerStyle_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCsDefault.Checked)
                CornerStyle = CornerStyle.Default;
            else if (rbCsDoNotRound.Checked)
                CornerStyle = CornerStyle.DoNotRound;
            else if (rbCsRound.Checked)
                CornerStyle = CornerStyle.Round;
            else if (rbCsRoundSmall.Checked)
                CornerStyle = CornerStyle.RoundSmall;
        }

        private void rbFormFrameStyle_CheckedChanged(object sender, EventArgs e)
        {
            var clientRect = NormalBounds.Shrink(DefaultNonClientPadding);

            LockBounds();
            SuspendLayout();

            if (rbFfsNone.Checked)
                FormFrameStyle = FormFrameStyle.None;
            else if (rbFfsDefault.Checked)
                FormFrameStyle = FormFrameStyle.Default;

            UnlockBounds();

            Bounds = clientRect.Grow(DefaultNonClientPadding);

            ResumeLayout();
        }

        #endregion interface
    }
}
