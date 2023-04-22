namespace TestApp
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flpClient = new System.Windows.Forms.FlowLayoutPanel();
            this.flpProp2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flpCustomForm = new System.Windows.Forms.FlowLayoutPanel();
            this.tBaseForm = new System.Windows.Forms.Label();
            this.cbCloseBox = new System.Windows.Forms.CheckBox();
            this.cbDarkMode = new System.Windows.Forms.CheckBox();
            this.cbMaximizeBox = new System.Windows.Forms.CheckBox();
            this.cbMinimizeBox = new System.Windows.Forms.CheckBox();
            this.cbSizeBox = new System.Windows.Forms.CheckBox();
            this.cbToolWindow = new System.Windows.Forms.CheckBox();
            this.flpCornerStyle = new System.Windows.Forms.FlowLayoutPanel();
            this.tCornerStyle = new System.Windows.Forms.Label();
            this.rbCsDefault = new System.Windows.Forms.RadioButton();
            this.rbCsDoNotRound = new System.Windows.Forms.RadioButton();
            this.rbCsRound = new System.Windows.Forms.RadioButton();
            this.rbCsRoundSmall = new System.Windows.Forms.RadioButton();
            this.flpFormFrameStyle = new System.Windows.Forms.FlowLayoutPanel();
            this.tFormFrameStyle = new System.Windows.Forms.Label();
            this.rbFfsNone = new System.Windows.Forms.RadioButton();
            this.rbFfsDefault = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tSystemMenu = new System.Windows.Forms.Label();
            this.cbSmAltSpace = new System.Windows.Forms.CheckBox();
            this.cbSmCaption = new System.Windows.Forms.CheckBox();
            this.cbSmTaskbar = new System.Windows.Forms.CheckBox();
            this.flpClient.SuspendLayout();
            this.flpCustomForm.SuspendLayout();
            this.flpCornerStyle.SuspendLayout();
            this.flpFormFrameStyle.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpClient
            // 
            this.flpClient.Controls.Add(this.flpProp2);
            this.flpClient.Controls.Add(this.flpCustomForm);
            this.flpClient.Controls.Add(this.flpCornerStyle);
            this.flpClient.Controls.Add(this.flpFormFrameStyle);
            this.flpClient.Controls.Add(this.flowLayoutPanel1);
            this.flpClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpClient.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpClient.Location = new System.Drawing.Point(0, 0);
            this.flpClient.Margin = new System.Windows.Forms.Padding(2);
            this.flpClient.Name = "flpClient";
            this.flpClient.Padding = new System.Windows.Forms.Padding(5);
            this.flpClient.Size = new System.Drawing.Size(500, 300);
            this.flpClient.TabIndex = 9;
            // 
            // flpProp2
            // 
            this.flpProp2.AutoSize = true;
            this.flpProp2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpProp2.Location = new System.Drawing.Point(5, 5);
            this.flpProp2.Margin = new System.Windows.Forms.Padding(0);
            this.flpProp2.Name = "flpProp2";
            this.flpProp2.Size = new System.Drawing.Size(0, 0);
            this.flpProp2.TabIndex = 1;
            // 
            // flpCustomForm
            // 
            this.flpCustomForm.AutoSize = true;
            this.flpCustomForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpCustomForm.Controls.Add(this.tBaseForm);
            this.flpCustomForm.Controls.Add(this.cbCloseBox);
            this.flpCustomForm.Controls.Add(this.cbDarkMode);
            this.flpCustomForm.Controls.Add(this.cbMaximizeBox);
            this.flpCustomForm.Controls.Add(this.cbMinimizeBox);
            this.flpCustomForm.Controls.Add(this.cbSizeBox);
            this.flpCustomForm.Controls.Add(this.cbToolWindow);
            this.flpCustomForm.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCustomForm.Location = new System.Drawing.Point(7, 7);
            this.flpCustomForm.Margin = new System.Windows.Forms.Padding(2);
            this.flpCustomForm.Name = "flpCustomForm";
            this.flpCustomForm.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.flpCustomForm.Size = new System.Drawing.Size(119, 189);
            this.flpCustomForm.TabIndex = 20;
            // 
            // tBaseForm
            // 
            this.tBaseForm.AutoSize = true;
            this.tBaseForm.Location = new System.Drawing.Point(2, 2);
            this.tBaseForm.Margin = new System.Windows.Forms.Padding(2);
            this.tBaseForm.Name = "tBaseForm";
            this.tBaseForm.Size = new System.Drawing.Size(69, 19);
            this.tBaseForm.TabIndex = 0;
            this.tBaseForm.Text = "BaseForm";
            // 
            // cbCloseBox
            // 
            this.cbCloseBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCloseBox.AutoSize = true;
            this.cbCloseBox.Location = new System.Drawing.Point(5, 25);
            this.cbCloseBox.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.cbCloseBox.Name = "cbCloseBox";
            this.cbCloseBox.Size = new System.Drawing.Size(83, 23);
            this.cbCloseBox.TabIndex = 6;
            this.cbCloseBox.Text = "CloseBox";
            this.cbCloseBox.UseVisualStyleBackColor = false;
            // 
            // cbDarkMode
            // 
            this.cbDarkMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDarkMode.AutoSize = true;
            this.cbDarkMode.Location = new System.Drawing.Point(5, 52);
            this.cbDarkMode.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.cbDarkMode.Name = "cbDarkMode";
            this.cbDarkMode.Size = new System.Drawing.Size(93, 23);
            this.cbDarkMode.TabIndex = 21;
            this.cbDarkMode.Text = "DarkMode";
            this.cbDarkMode.UseVisualStyleBackColor = false;
            // 
            // cbMaximizeBox
            // 
            this.cbMaximizeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMaximizeBox.AutoSize = true;
            this.cbMaximizeBox.Location = new System.Drawing.Point(5, 79);
            this.cbMaximizeBox.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.cbMaximizeBox.Name = "cbMaximizeBox";
            this.cbMaximizeBox.Size = new System.Drawing.Size(107, 23);
            this.cbMaximizeBox.TabIndex = 7;
            this.cbMaximizeBox.Text = "MaximizeBox";
            this.cbMaximizeBox.UseVisualStyleBackColor = false;
            // 
            // cbMinimizeBox
            // 
            this.cbMinimizeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMinimizeBox.AutoSize = true;
            this.cbMinimizeBox.Location = new System.Drawing.Point(5, 106);
            this.cbMinimizeBox.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.cbMinimizeBox.Name = "cbMinimizeBox";
            this.cbMinimizeBox.Size = new System.Drawing.Size(105, 23);
            this.cbMinimizeBox.TabIndex = 8;
            this.cbMinimizeBox.Text = "MinimizeBox";
            this.cbMinimizeBox.UseVisualStyleBackColor = false;
            // 
            // cbSizeBox
            // 
            this.cbSizeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSizeBox.AutoSize = true;
            this.cbSizeBox.Location = new System.Drawing.Point(5, 133);
            this.cbSizeBox.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.cbSizeBox.Name = "cbSizeBox";
            this.cbSizeBox.Size = new System.Drawing.Size(73, 23);
            this.cbSizeBox.TabIndex = 23;
            this.cbSizeBox.Text = "SizeBox";
            this.cbSizeBox.UseVisualStyleBackColor = false;
            // 
            // cbToolWindow
            // 
            this.cbToolWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbToolWindow.AutoSize = true;
            this.cbToolWindow.Location = new System.Drawing.Point(5, 160);
            this.cbToolWindow.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.cbToolWindow.Name = "cbToolWindow";
            this.cbToolWindow.Size = new System.Drawing.Size(103, 23);
            this.cbToolWindow.TabIndex = 24;
            this.cbToolWindow.Text = "ToolWindow";
            this.cbToolWindow.UseVisualStyleBackColor = false;
            // 
            // flpCornerStyle
            // 
            this.flpCornerStyle.AutoSize = true;
            this.flpCornerStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpCornerStyle.Controls.Add(this.tCornerStyle);
            this.flpCornerStyle.Controls.Add(this.rbCsDefault);
            this.flpCornerStyle.Controls.Add(this.rbCsDoNotRound);
            this.flpCornerStyle.Controls.Add(this.rbCsRound);
            this.flpCornerStyle.Controls.Add(this.rbCsRoundSmall);
            this.flpCornerStyle.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCornerStyle.Location = new System.Drawing.Point(130, 7);
            this.flpCornerStyle.Margin = new System.Windows.Forms.Padding(2);
            this.flpCornerStyle.Name = "flpCornerStyle";
            this.flpCornerStyle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.flpCornerStyle.Size = new System.Drawing.Size(120, 135);
            this.flpCornerStyle.TabIndex = 21;
            // 
            // tCornerStyle
            // 
            this.tCornerStyle.AutoSize = true;
            this.tCornerStyle.Location = new System.Drawing.Point(2, 2);
            this.tCornerStyle.Margin = new System.Windows.Forms.Padding(2);
            this.tCornerStyle.Name = "tCornerStyle";
            this.tCornerStyle.Size = new System.Drawing.Size(80, 19);
            this.tCornerStyle.TabIndex = 0;
            this.tCornerStyle.Text = "CornerStyle";
            // 
            // rbCsDefault
            // 
            this.rbCsDefault.AutoSize = true;
            this.rbCsDefault.Location = new System.Drawing.Point(5, 25);
            this.rbCsDefault.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.rbCsDefault.Name = "rbCsDefault";
            this.rbCsDefault.Size = new System.Drawing.Size(71, 23);
            this.rbCsDefault.TabIndex = 1;
            this.rbCsDefault.TabStop = true;
            this.rbCsDefault.Text = "Default";
            this.rbCsDefault.UseVisualStyleBackColor = true;
            // 
            // rbCsDoNotRound
            // 
            this.rbCsDoNotRound.AutoSize = true;
            this.rbCsDoNotRound.Location = new System.Drawing.Point(5, 52);
            this.rbCsDoNotRound.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.rbCsDoNotRound.Name = "rbCsDoNotRound";
            this.rbCsDoNotRound.Size = new System.Drawing.Size(108, 23);
            this.rbCsDoNotRound.TabIndex = 2;
            this.rbCsDoNotRound.TabStop = true;
            this.rbCsDoNotRound.Text = "DoNotRound";
            this.rbCsDoNotRound.UseVisualStyleBackColor = true;
            // 
            // rbCsRound
            // 
            this.rbCsRound.AutoSize = true;
            this.rbCsRound.Location = new System.Drawing.Point(5, 79);
            this.rbCsRound.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.rbCsRound.Name = "rbCsRound";
            this.rbCsRound.Size = new System.Drawing.Size(67, 23);
            this.rbCsRound.TabIndex = 3;
            this.rbCsRound.TabStop = true;
            this.rbCsRound.Text = "Round";
            this.rbCsRound.UseVisualStyleBackColor = true;
            // 
            // rbCsRoundSmall
            // 
            this.rbCsRoundSmall.AutoSize = true;
            this.rbCsRoundSmall.Location = new System.Drawing.Point(5, 106);
            this.rbCsRoundSmall.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.rbCsRoundSmall.Name = "rbCsRoundSmall";
            this.rbCsRoundSmall.Size = new System.Drawing.Size(99, 23);
            this.rbCsRoundSmall.TabIndex = 4;
            this.rbCsRoundSmall.TabStop = true;
            this.rbCsRoundSmall.Text = "RoundSmall";
            this.rbCsRoundSmall.UseVisualStyleBackColor = true;
            // 
            // flpFormFrameStyle
            // 
            this.flpFormFrameStyle.AutoSize = true;
            this.flpFormFrameStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpFormFrameStyle.Controls.Add(this.tFormFrameStyle);
            this.flpFormFrameStyle.Controls.Add(this.rbFfsNone);
            this.flpFormFrameStyle.Controls.Add(this.rbFfsDefault);
            this.flpFormFrameStyle.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpFormFrameStyle.Location = new System.Drawing.Point(130, 146);
            this.flpFormFrameStyle.Margin = new System.Windows.Forms.Padding(2);
            this.flpFormFrameStyle.Name = "flpFormFrameStyle";
            this.flpFormFrameStyle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.flpFormFrameStyle.Size = new System.Drawing.Size(114, 81);
            this.flpFormFrameStyle.TabIndex = 23;
            // 
            // tFormFrameStyle
            // 
            this.tFormFrameStyle.AutoSize = true;
            this.tFormFrameStyle.Location = new System.Drawing.Point(2, 2);
            this.tFormFrameStyle.Margin = new System.Windows.Forms.Padding(2);
            this.tFormFrameStyle.Name = "tFormFrameStyle";
            this.tFormFrameStyle.Size = new System.Drawing.Size(108, 19);
            this.tFormFrameStyle.TabIndex = 0;
            this.tFormFrameStyle.Text = "FormFrameStyle";
            // 
            // rbFfsNone
            // 
            this.rbFfsNone.AutoSize = true;
            this.rbFfsNone.Location = new System.Drawing.Point(5, 25);
            this.rbFfsNone.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.rbFfsNone.Name = "rbFfsNone";
            this.rbFfsNone.Size = new System.Drawing.Size(60, 23);
            this.rbFfsNone.TabIndex = 1;
            this.rbFfsNone.TabStop = true;
            this.rbFfsNone.Text = "None";
            this.rbFfsNone.UseVisualStyleBackColor = true;
            // 
            // rbFfsDefault
            // 
            this.rbFfsDefault.AutoSize = true;
            this.rbFfsDefault.Location = new System.Drawing.Point(5, 52);
            this.rbFfsDefault.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.rbFfsDefault.Name = "rbFfsDefault";
            this.rbFfsDefault.Size = new System.Drawing.Size(71, 23);
            this.rbFfsDefault.TabIndex = 2;
            this.rbFfsDefault.TabStop = true;
            this.rbFfsDefault.Text = "Default";
            this.rbFfsDefault.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.tSystemMenu);
            this.flowLayoutPanel1.Controls.Add(this.cbSmAltSpace);
            this.flowLayoutPanel1.Controls.Add(this.cbSmCaption);
            this.flowLayoutPanel1.Controls.Add(this.cbSmTaskbar);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(254, 7);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(102, 108);
            this.flowLayoutPanel1.TabIndex = 22;
            // 
            // tSystemMenu
            // 
            this.tSystemMenu.AutoSize = true;
            this.tSystemMenu.Location = new System.Drawing.Point(2, 2);
            this.tSystemMenu.Margin = new System.Windows.Forms.Padding(2);
            this.tSystemMenu.Name = "tSystemMenu";
            this.tSystemMenu.Size = new System.Drawing.Size(92, 19);
            this.tSystemMenu.TabIndex = 0;
            this.tSystemMenu.Text = "System menu";
            // 
            // cbSmAltSpace
            // 
            this.cbSmAltSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSmAltSpace.AutoSize = true;
            this.cbSmAltSpace.Location = new System.Drawing.Point(5, 25);
            this.cbSmAltSpace.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.cbSmAltSpace.Name = "cbSmAltSpace";
            this.cbSmAltSpace.Size = new System.Drawing.Size(90, 23);
            this.cbSmAltSpace.TabIndex = 1;
            this.cbSmAltSpace.Text = "Alt+Space";
            this.cbSmAltSpace.UseVisualStyleBackColor = false;
            // 
            // cbSmCaption
            // 
            this.cbSmCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSmCaption.AutoSize = true;
            this.cbSmCaption.Location = new System.Drawing.Point(5, 52);
            this.cbSmCaption.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.cbSmCaption.Name = "cbSmCaption";
            this.cbSmCaption.Size = new System.Drawing.Size(76, 23);
            this.cbSmCaption.TabIndex = 2;
            this.cbSmCaption.Text = "Caption";
            this.cbSmCaption.UseVisualStyleBackColor = false;
            // 
            // cbSmTaskbar
            // 
            this.cbSmTaskbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSmTaskbar.AutoSize = true;
            this.cbSmTaskbar.Location = new System.Drawing.Point(5, 79);
            this.cbSmTaskbar.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.cbSmTaskbar.Name = "cbSmTaskbar";
            this.cbSmTaskbar.Size = new System.Drawing.Size(73, 23);
            this.cbSmTaskbar.TabIndex = 3;
            this.cbSmTaskbar.Text = "Taskbar";
            this.cbSmTaskbar.UseVisualStyleBackColor = false;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.flpClient);
            this.KeyPreview = true;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RemasterForms";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TestForm_KeyPress);
            this.flpClient.ResumeLayout(false);
            this.flpClient.PerformLayout();
            this.flpCustomForm.ResumeLayout(false);
            this.flpCustomForm.PerformLayout();
            this.flpCornerStyle.ResumeLayout(false);
            this.flpCornerStyle.PerformLayout();
            this.flpFormFrameStyle.ResumeLayout(false);
            this.flpFormFrameStyle.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpClient;
        private System.Windows.Forms.FlowLayoutPanel flpProp2;
        private System.Windows.Forms.FlowLayoutPanel flpCustomForm;
        private System.Windows.Forms.Label tBaseForm;
        private System.Windows.Forms.CheckBox cbCloseBox;
        private System.Windows.Forms.CheckBox cbDarkMode;
        private System.Windows.Forms.CheckBox cbMaximizeBox;
        private System.Windows.Forms.CheckBox cbMinimizeBox;
        private System.Windows.Forms.FlowLayoutPanel flpCornerStyle;
        private System.Windows.Forms.Label tCornerStyle;
        private System.Windows.Forms.RadioButton rbCsDefault;
        private System.Windows.Forms.RadioButton rbCsDoNotRound;
        private System.Windows.Forms.RadioButton rbCsRound;
        private System.Windows.Forms.RadioButton rbCsRoundSmall;
        private System.Windows.Forms.CheckBox cbSizeBox;
        private System.Windows.Forms.CheckBox cbToolWindow;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label tSystemMenu;
        private System.Windows.Forms.CheckBox cbSmAltSpace;
        private System.Windows.Forms.CheckBox cbSmCaption;
        private System.Windows.Forms.CheckBox cbSmTaskbar;
        private System.Windows.Forms.FlowLayoutPanel flpFormFrameStyle;
        private System.Windows.Forms.Label tFormFrameStyle;
        private System.Windows.Forms.RadioButton rbFfsNone;
        private System.Windows.Forms.RadioButton rbFfsDefault;
    }
}

