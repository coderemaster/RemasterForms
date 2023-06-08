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

namespace RemasterFormsTest
{
    public partial class TestForm : BaseForm
    {
        // constructor
        public TestForm()
        {
            InitializeComponent();

            flpClientArea.RightToLeft = RightToLeft.No;
            RightToLeft               = RightToLeft.Yes;

            //MaximizedBounds = new Rectangle(50, 50, 660, 450);
        }

        #region ## appearance

        private void UpdateColors()
        {
            ColorMode = cbDarkMode.Checked
                ? ColorMode.Dark
                : ColorMode.Light;

            switch (ColorMode)
            {
                case ColorMode.Light:
                    BackColor = Color.White;
                    ForeColor = Color.Black;
                    break;
                case ColorMode.Dark:
                    BackColor = Color.FromArgb(43, 43, 43);
                    ForeColor = Color.White;
                    break;
                case ColorMode.HighContrast:
                    BackColor = SystemColors.Control;
                    ForeColor = SystemColors.ControlText;
                    break;
            }
        }

        protected override void OnColorModeChanged(EventArgs e)
        {
            UpdateColors();
        }

        #endregion appearance

        #region ## layout

        private void UpdatePadding()
        {
            Padding = DefaultFrameMetrics;
        }

        protected override void OnNonClientMetricsChanged(EventArgs e)
        {
            UpdatePadding();
        }

        #endregion layout

        #region ## behavior

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;

                if (ControlBox)
                    cp.Style |= NativeMethods.WS_SYSMENU;

                return cp;
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left
                && Borderless
                && e.Y < Padding.Top)
            {
                SendSystemCommand(DefaultSystemCommand);
            }

            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right
                && Borderless
                && e.Y < Padding.Top)
            {
                ShowSystemMenu(this, e.Location);
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left
                && Borderless
                && e.Y < Padding.Top)
            {
                StartMoving();
            }

            base.OnMouseMove(e);
        }

        private void TestForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                Close();
        }

        #endregion behavior

        #region ## interface

        private void TestForm_Load(object sender, EventArgs e)
        {
            cbDarkMode.Checked = false;
            cbDarkMode.CheckedChanged += (_s, _e) => UpdateColors();

            BF_MaximizeBox      .Checked = MaximizeBox;
            BF_MinimizeBox      .Checked = MinimizeBox;
            BF_RightToLeftLayout.Checked = RightToLeftLayout;
            BF_ShowInTaskbar    .Checked = ShowInTaskbar;

            BF_MaximizeBox      .CheckedChanged += (_s, _e) => MaximizeBox       = BF_MaximizeBox.Checked;
            BF_MinimizeBox      .CheckedChanged += (_s, _e) => MinimizeBox       = BF_MinimizeBox.Checked;
            BF_RightToLeftLayout.CheckedChanged += (_s, _e) => RightToLeftLayout = BF_RightToLeftLayout.Checked;
            BF_ShowInTaskbar    .CheckedChanged += (_s, _e) => ShowInTaskbar     = BF_ShowInTaskbar.Checked;

            if (_windows11)
            {
                switch (DwmFrame.CornerStyle)
                {
                    case CornerStyle.Default:
                        CS_Default.Checked      = true;
                        break;
                    case CornerStyle.DoNotRound:
                        CS_DoNotRound.Checked   = true;
                        break;
                    case CornerStyle.Round:
                        CS_Round.Checked        = true;
                        break;
                    case CornerStyle.RoundSmall:
                        CS_RoundSmall.Checked   = true;
                        break;
                }

                CS_Default.CheckedChanged    += rbCornerStyle_CheckedChanged;
                CS_DoNotRound.CheckedChanged += rbCornerStyle_CheckedChanged;
                CS_Round.CheckedChanged      += rbCornerStyle_CheckedChanged;
                CS_RoundSmall.CheckedChanged += rbCornerStyle_CheckedChanged;
            }
            else
            {
                flpCornerStyle.Visible = false;
            }

            switch (FormBorderStyle)
            {
                case FormBorderStyle.None:
                    BS_None.Checked = true;
                    break;
                case FormBorderStyle.FixedSingle:
                    BS_FixedSingle.Checked = true;
                    break;
                case FormBorderStyle.Sizable:
                    BS_Sizable.Checked = true;
                    break;
                case FormBorderStyle.FixedToolWindow:
                    BS_FixedToolWindow.Checked = true;
                    break;
                case FormBorderStyle.SizableToolWindow:
                    BS_SizableToolWindow.Checked = true;
                    break;
            }

            BS_None             .CheckedChanged += rbFormBorderStyle_CheckedChanged;
            BS_FixedSingle      .CheckedChanged += rbFormBorderStyle_CheckedChanged;
            BS_Sizable          .CheckedChanged += rbFormBorderStyle_CheckedChanged;
            BS_FixedToolWindow  .CheckedChanged += rbFormBorderStyle_CheckedChanged;
            BS_SizableToolWindow.CheckedChanged += rbFormBorderStyle_CheckedChanged;

            UpdatePadding();
            UpdateColors();
        }

        private void rbCornerStyle_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender as RadioButton).Checked)
                return;

            if (CS_Default.Checked)
                DwmFrame.CornerStyle = CornerStyle.Default;
            else if (CS_DoNotRound.Checked)
                DwmFrame.CornerStyle = CornerStyle.DoNotRound;
            else if (CS_Round.Checked)
                DwmFrame.CornerStyle = CornerStyle.Round;
            else if (CS_RoundSmall.Checked)
                DwmFrame.CornerStyle = CornerStyle.RoundSmall;
        }

        private void rbFormBorderStyle_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender as RadioButton).Checked)
                return;

            if (BS_None.Checked)
                FormBorderStyle = FormBorderStyle.None;
            else if (BS_FixedSingle.Checked)
                FormBorderStyle = FormBorderStyle.FixedSingle;
            else if (BS_Sizable.Checked)
                FormBorderStyle = FormBorderStyle.Sizable;
            else if (BS_FixedToolWindow.Checked)
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
            else if (BS_SizableToolWindow.Checked)
                FormBorderStyle = FormBorderStyle.SizableToolWindow;
        }

        #endregion interface
    }
}
