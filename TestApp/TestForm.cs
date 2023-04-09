using RemasterForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#pragma warning disable IDE1006 // Naming rule violation

namespace TestApp
{
    public partial class TestForm : CustomForm
    {
        private static readonly bool NoCornerStyle = Environment.OSVersion.Version.Build < 22000;
        private static readonly bool NoDarkMode    = Environment.OSVersion.Version.Build < 17763;

        private bool DarkMode = false;

        public TestForm()
        {
            InitializeComponent();
        }

        #region ## custom frame

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                WindowMenu.Show(e.Location);
                return;
            }

            base.OnMouseClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MaximizeBox)
            {
                TitleBar.PerformDoubleClick();
                return;
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.Space))
            {
                _ = DwmFrame.CornerStyle;
                WindowMenu.Show();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion custom frame

        #region ## interface

        private void UpdateColors()
        {
            if (NoDarkMode)
                return;

            if (DarkMode)
            {
                BackColor = Color.FromArgb(21, 21, 21);
                ForeColor = Color.White;
            }
            else
            {
                BackColor = Color.FromArgb(234, 234, 234);
                ForeColor = Color.Black;
            }
        }

        private void UpdatePadding()
        {
            var defStyles = new WindowStyles()
            {
                Caption = true,
                SizeBox = true
            };

            var defPadding = new WindowMetrics(defStyles).FramePadding; 

            Padding = WindowMetrics.FramePadding - NonClientPadding;
            flpClient.Padding = defPadding - Padding;
        }

        private void TestForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                Close();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            if (NoDarkMode)
            {
                cbDarkMode.Visible = false;
            }
            else
            {
                cbDarkMode.Checked = DarkMode;
                cbDarkMode.CheckedChanged += cbDarkMode_CheckedChanged;
            }

            cbShowCaption.Checked = false;
            cbShowCaption.CheckedChanged += cbShowCaption_CheckedChanged;

            cbCloseBox.Checked    = CloseBox;
            cbControlBox.Checked  = ControlBox;
            cbHelpButton.Checked  = HelpButton;
            cbMaximizeBox.Checked = MaximizeBox;
            cbMinimizeBox.Checked = MinimizeBox;
            cbSizeBox.Checked     = SizeBox;
            cbToolWindow.Checked  = ToolWindow;

            cbCloseBox.CheckedChanged    += (_s, _e) => CloseBox      = cbCloseBox.Checked;
            cbControlBox.CheckedChanged  += (_s, _e) => ControlBox    = cbControlBox.Checked;
            cbHelpButton.CheckedChanged  += (_s, _e) => HelpButton    = cbHelpButton.Checked;
            cbMaximizeBox.CheckedChanged += (_s, _e) => MaximizeBox   = cbMaximizeBox.Checked;
            cbMinimizeBox.CheckedChanged += (_s, _e) => MinimizeBox   = cbMinimizeBox.Checked;
            cbSizeBox.CheckedChanged     += (_s, _e) => SizeBox       = cbSizeBox.Checked;
            cbToolWindow.CheckedChanged  += (_s, _e) => ToolWindow    = cbToolWindow.Checked;

            switch (FrameStyle)
            {
                case FormFrameStyle.None:
                    rbFsNone.Checked = true;
                    break;
                case FormFrameStyle.Default:
                    rbFsDefault.Checked = true;
                    break;
                case FormFrameStyle.Embedded:
                    rbFsEmbedded.Checked = true;
                    break;
            }

            rbFsNone.CheckedChanged     += rbFrameStyle_CheckedChanged;
            rbFsDefault.CheckedChanged  += rbFrameStyle_CheckedChanged;
            rbFsEmbedded.CheckedChanged += rbFrameStyle_CheckedChanged;

            if (NoCornerStyle)
            {
                flpCornerStyle.Visible = false;
            }
            else
            {
                switch (DwmFrame.CornerStyle)
                {
                    case DwmCornerStyle.Default:
                        rbCsDefault.Checked = true;
                        break;
                    case DwmCornerStyle.DoNotRound:
                        rbCsDoNotRound.Checked = true;
                        break;
                    case DwmCornerStyle.Round:
                        rbCsRound.Checked = true;
                        break;
                    case DwmCornerStyle.RoundSmall:
                        rbCsRoundSmall.Checked = true;
                        break;
                }

                rbCsDefault.CheckedChanged    += rbCornerStyle_CheckedChanged;
                rbCsDoNotRound.CheckedChanged += rbCornerStyle_CheckedChanged;
                rbCsRound.CheckedChanged      += rbCornerStyle_CheckedChanged;
                rbCsRoundSmall.CheckedChanged += rbCornerStyle_CheckedChanged;
            }

            TitleBar.SystemCaption = cbShowCaption.Checked;
            TitleBar.SystemHeight  = true;

            UpdateColors();
            UpdatePadding();
        }

        private void TestForm_SizingBegin(object sender, EventArgs e)
        {
            if (FrameStyle == FormFrameStyle.Embedded)
                UpdatePadding();
        }

        private void TestForm_SizingEnd(object sender, EventArgs e)
        {
            if (FrameStyle == FormFrameStyle.Embedded)
                UpdatePadding();
        }

        private void TestForm_WindowStateChanged(object sender, EventArgs e)
        {
            UpdatePadding();
        }

        private void cbDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            DarkMode = cbDarkMode.Checked;
            DwmFrame.DarkMode = DarkMode;
            UpdateColors();
        }

        private void cbShowCaption_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();

            TitleBar.SystemCaption = cbShowCaption.Checked;

            UpdatePadding();
            ResumeLayout();
        }

        private void rbFrameStyle_CheckedChanged(object sender, EventArgs e)
        {
            SuspendLayout();

            if (rbFsNone.Checked)
                FrameStyle = FormFrameStyle.None;
            else if (rbFsDefault.Checked)
                FrameStyle = FormFrameStyle.Default;
            else if (rbFsEmbedded.Checked)
                FrameStyle = FormFrameStyle.Embedded;

            UpdatePadding();
            ResumeLayout();
        }

        private void rbCornerStyle_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCsDefault.Checked)
                DwmFrame.CornerStyle = DwmCornerStyle.Default;
            else if (rbCsDoNotRound.Checked)
                DwmFrame.CornerStyle = DwmCornerStyle.DoNotRound;
            else if (rbCsRound.Checked)
                DwmFrame.CornerStyle = DwmCornerStyle.Round;
            else if (rbCsRoundSmall.Checked)
                DwmFrame.CornerStyle = DwmCornerStyle.RoundSmall;
        }

        #endregion interface
    }
}
