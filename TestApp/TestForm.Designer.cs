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
            this.flpTestForm = new System.Windows.Forms.FlowLayoutPanel();
            this.tTestForm = new System.Windows.Forms.Label();
            this.cbDarkMode = new System.Windows.Forms.CheckBox();
            this.cbShowCaption = new System.Windows.Forms.CheckBox();
            this.flpCustomForm = new System.Windows.Forms.FlowLayoutPanel();
            this.tCustomForm = new System.Windows.Forms.Label();
            this.cbCloseBox = new System.Windows.Forms.CheckBox();
            this.cbControlBox = new System.Windows.Forms.CheckBox();
            this.cbHelpButton = new System.Windows.Forms.CheckBox();
            this.cbMaximizeBox = new System.Windows.Forms.CheckBox();
            this.cbMinimizeBox = new System.Windows.Forms.CheckBox();
            this.cbToolWindow = new System.Windows.Forms.CheckBox();
            this.cbSizeBox = new System.Windows.Forms.CheckBox();
            this.flpFrameStyle = new System.Windows.Forms.FlowLayoutPanel();
            this.tFrameStyle = new System.Windows.Forms.Label();
            this.rbFsNone = new System.Windows.Forms.RadioButton();
            this.rbFsDefault = new System.Windows.Forms.RadioButton();
            this.rbFsEmbedded = new System.Windows.Forms.RadioButton();
            this.flpCornerStyle = new System.Windows.Forms.FlowLayoutPanel();
            this.tCornerStyle = new System.Windows.Forms.Label();
            this.rbCsDefault = new System.Windows.Forms.RadioButton();
            this.rbCsDoNotRound = new System.Windows.Forms.RadioButton();
            this.rbCsRound = new System.Windows.Forms.RadioButton();
            this.rbCsRoundSmall = new System.Windows.Forms.RadioButton();
            this.flpClient.SuspendLayout();
            this.flpTestForm.SuspendLayout();
            this.flpCustomForm.SuspendLayout();
            this.flpFrameStyle.SuspendLayout();
            this.flpCornerStyle.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpClient
            // 
            this.flpClient.BackColor = System.Drawing.Color.Transparent;
            this.flpClient.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpClient.Controls.Add(this.flpProp2);
            this.flpClient.Controls.Add(this.flpTestForm);
            this.flpClient.Controls.Add(this.flpCustomForm);
            this.flpClient.Controls.Add(this.flpFrameStyle);
            this.flpClient.Controls.Add(this.flpCornerStyle);
            this.flpClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpClient.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpClient.Location = new System.Drawing.Point(0, 0);
            this.flpClient.Name = "flpClient";
            this.flpClient.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.flpClient.Size = new System.Drawing.Size(800, 443);
            this.flpClient.TabIndex = 1;
            // 
            // flpProp2
            // 
            this.flpProp2.AutoSize = true;
            this.flpProp2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpProp2.Location = new System.Drawing.Point(8, 8);
            this.flpProp2.Margin = new System.Windows.Forms.Padding(0);
            this.flpProp2.Name = "flpProp2";
            this.flpProp2.Size = new System.Drawing.Size(0, 0);
            this.flpProp2.TabIndex = 1;
            // 
            // flpTestForm
            // 
            this.flpTestForm.AutoSize = true;
            this.flpTestForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTestForm.Controls.Add(this.tTestForm);
            this.flpTestForm.Controls.Add(this.cbDarkMode);
            this.flpTestForm.Controls.Add(this.cbShowCaption);
            this.flpTestForm.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTestForm.Location = new System.Drawing.Point(11, 11);
            this.flpTestForm.Name = "flpTestForm";
            this.flpTestForm.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.flpTestForm.Size = new System.Drawing.Size(179, 121);
            this.flpTestForm.TabIndex = 21;
            // 
            // tTestForm
            // 
            this.tTestForm.AutoSize = true;
            this.tTestForm.Location = new System.Drawing.Point(3, 3);
            this.tTestForm.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tTestForm.Name = "tTestForm";
            this.tTestForm.Size = new System.Drawing.Size(96, 30);
            this.tTestForm.TabIndex = 0;
            this.tTestForm.Text = "TestForm";
            // 
            // cbDarkMode
            // 
            this.cbDarkMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDarkMode.AutoSize = true;
            this.cbDarkMode.Location = new System.Drawing.Point(8, 39);
            this.cbDarkMode.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.cbDarkMode.Name = "cbDarkMode";
            this.cbDarkMode.Size = new System.Drawing.Size(136, 34);
            this.cbDarkMode.TabIndex = 2;
            this.cbDarkMode.Text = "DarkMode";
            this.cbDarkMode.UseVisualStyleBackColor = false;
            // 
            // cbShowCaption
            // 
            this.cbShowCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbShowCaption.AutoSize = true;
            this.cbShowCaption.Location = new System.Drawing.Point(8, 79);
            this.cbShowCaption.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.cbShowCaption.Name = "cbShowCaption";
            this.cbShowCaption.Size = new System.Drawing.Size(161, 34);
            this.cbShowCaption.TabIndex = 5;
            this.cbShowCaption.Text = "ShowCaption";
            this.cbShowCaption.UseVisualStyleBackColor = false;
            // 
            // flpCustomForm
            // 
            this.flpCustomForm.AutoSize = true;
            this.flpCustomForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpCustomForm.Controls.Add(this.tCustomForm);
            this.flpCustomForm.Controls.Add(this.cbCloseBox);
            this.flpCustomForm.Controls.Add(this.cbControlBox);
            this.flpCustomForm.Controls.Add(this.cbHelpButton);
            this.flpCustomForm.Controls.Add(this.cbMaximizeBox);
            this.flpCustomForm.Controls.Add(this.cbMinimizeBox);
            this.flpCustomForm.Controls.Add(this.cbToolWindow);
            this.flpCustomForm.Controls.Add(this.cbSizeBox);
            this.flpCustomForm.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCustomForm.Location = new System.Drawing.Point(196, 11);
            this.flpCustomForm.Name = "flpCustomForm";
            this.flpCustomForm.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.flpCustomForm.Size = new System.Drawing.Size(180, 321);
            this.flpCustomForm.TabIndex = 20;
            // 
            // tCustomForm
            // 
            this.tCustomForm.AutoSize = true;
            this.tCustomForm.Location = new System.Drawing.Point(3, 3);
            this.tCustomForm.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tCustomForm.Name = "tCustomForm";
            this.tCustomForm.Size = new System.Drawing.Size(131, 30);
            this.tCustomForm.TabIndex = 0;
            this.tCustomForm.Text = "CustomForm";
            // 
            // cbCloseBox
            // 
            this.cbCloseBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbCloseBox.AutoSize = true;
            this.cbCloseBox.Location = new System.Drawing.Point(8, 39);
            this.cbCloseBox.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.cbCloseBox.Name = "cbCloseBox";
            this.cbCloseBox.Size = new System.Drawing.Size(123, 34);
            this.cbCloseBox.TabIndex = 6;
            this.cbCloseBox.Text = "CloseBox";
            this.cbCloseBox.UseVisualStyleBackColor = false;
            // 
            // cbControlBox
            // 
            this.cbControlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbControlBox.AutoSize = true;
            this.cbControlBox.Location = new System.Drawing.Point(8, 79);
            this.cbControlBox.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.cbControlBox.Name = "cbControlBox";
            this.cbControlBox.Size = new System.Drawing.Size(141, 34);
            this.cbControlBox.TabIndex = 17;
            this.cbControlBox.Text = "ControlBox";
            this.cbControlBox.UseVisualStyleBackColor = false;
            // 
            // cbHelpButton
            // 
            this.cbHelpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbHelpButton.AutoSize = true;
            this.cbHelpButton.Location = new System.Drawing.Point(8, 119);
            this.cbHelpButton.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.cbHelpButton.Name = "cbHelpButton";
            this.cbHelpButton.Size = new System.Drawing.Size(144, 34);
            this.cbHelpButton.TabIndex = 18;
            this.cbHelpButton.Text = "HelpButton";
            this.cbHelpButton.UseVisualStyleBackColor = false;
            // 
            // cbMaximizeBox
            // 
            this.cbMaximizeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMaximizeBox.AutoSize = true;
            this.cbMaximizeBox.Location = new System.Drawing.Point(8, 159);
            this.cbMaximizeBox.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.cbMaximizeBox.Name = "cbMaximizeBox";
            this.cbMaximizeBox.Size = new System.Drawing.Size(162, 34);
            this.cbMaximizeBox.TabIndex = 7;
            this.cbMaximizeBox.Text = "MaximizeBox";
            this.cbMaximizeBox.UseVisualStyleBackColor = false;
            // 
            // cbMinimizeBox
            // 
            this.cbMinimizeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMinimizeBox.AutoSize = true;
            this.cbMinimizeBox.Location = new System.Drawing.Point(8, 199);
            this.cbMinimizeBox.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.cbMinimizeBox.Name = "cbMinimizeBox";
            this.cbMinimizeBox.Size = new System.Drawing.Size(158, 34);
            this.cbMinimizeBox.TabIndex = 8;
            this.cbMinimizeBox.Text = "MinimizeBox";
            this.cbMinimizeBox.UseVisualStyleBackColor = false;
            // 
            // cbToolWindow
            // 
            this.cbToolWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbToolWindow.AutoSize = true;
            this.cbToolWindow.Location = new System.Drawing.Point(8, 239);
            this.cbToolWindow.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.cbToolWindow.Name = "cbToolWindow";
            this.cbToolWindow.Size = new System.Drawing.Size(153, 34);
            this.cbToolWindow.TabIndex = 13;
            this.cbToolWindow.Text = "ToolWindow";
            this.cbToolWindow.UseVisualStyleBackColor = false;
            // 
            // cbSizeBox
            // 
            this.cbSizeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSizeBox.AutoSize = true;
            this.cbSizeBox.Location = new System.Drawing.Point(8, 279);
            this.cbSizeBox.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.cbSizeBox.Name = "cbSizeBox";
            this.cbSizeBox.Size = new System.Drawing.Size(110, 34);
            this.cbSizeBox.TabIndex = 9;
            this.cbSizeBox.Text = "SizeBox";
            this.cbSizeBox.UseVisualStyleBackColor = false;
            // 
            // flpFrameStyle
            // 
            this.flpFrameStyle.AutoSize = true;
            this.flpFrameStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpFrameStyle.Controls.Add(this.tFrameStyle);
            this.flpFrameStyle.Controls.Add(this.rbFsNone);
            this.flpFrameStyle.Controls.Add(this.rbFsDefault);
            this.flpFrameStyle.Controls.Add(this.rbFsEmbedded);
            this.flpFrameStyle.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpFrameStyle.Location = new System.Drawing.Point(382, 11);
            this.flpFrameStyle.Name = "flpFrameStyle";
            this.flpFrameStyle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.flpFrameStyle.Size = new System.Drawing.Size(155, 161);
            this.flpFrameStyle.TabIndex = 14;
            // 
            // tFrameStyle
            // 
            this.tFrameStyle.AutoSize = true;
            this.tFrameStyle.Location = new System.Drawing.Point(3, 3);
            this.tFrameStyle.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tFrameStyle.Name = "tFrameStyle";
            this.tFrameStyle.Size = new System.Drawing.Size(113, 30);
            this.tFrameStyle.TabIndex = 0;
            this.tFrameStyle.Text = "FrameStyle";
            // 
            // rbFsNone
            // 
            this.rbFsNone.AutoSize = true;
            this.rbFsNone.Location = new System.Drawing.Point(8, 39);
            this.rbFsNone.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.rbFsNone.Name = "rbFsNone";
            this.rbFsNone.Size = new System.Drawing.Size(89, 34);
            this.rbFsNone.TabIndex = 1;
            this.rbFsNone.TabStop = true;
            this.rbFsNone.Text = "None";
            this.rbFsNone.UseVisualStyleBackColor = true;
            // 
            // rbFsDefault
            // 
            this.rbFsDefault.AutoSize = true;
            this.rbFsDefault.Location = new System.Drawing.Point(8, 79);
            this.rbFsDefault.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.rbFsDefault.Name = "rbFsDefault";
            this.rbFsDefault.Size = new System.Drawing.Size(106, 34);
            this.rbFsDefault.TabIndex = 2;
            this.rbFsDefault.TabStop = true;
            this.rbFsDefault.Text = "Default";
            this.rbFsDefault.UseVisualStyleBackColor = true;
            // 
            // rbFsEmbedded
            // 
            this.rbFsEmbedded.AutoSize = true;
            this.rbFsEmbedded.Location = new System.Drawing.Point(8, 119);
            this.rbFsEmbedded.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.rbFsEmbedded.Name = "rbFsEmbedded";
            this.rbFsEmbedded.Size = new System.Drawing.Size(137, 34);
            this.rbFsEmbedded.TabIndex = 3;
            this.rbFsEmbedded.TabStop = true;
            this.rbFsEmbedded.Text = "Embedded";
            this.rbFsEmbedded.UseVisualStyleBackColor = true;
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
            this.flpCornerStyle.Location = new System.Drawing.Point(382, 178);
            this.flpCornerStyle.Name = "flpCornerStyle";
            this.flpCornerStyle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.flpCornerStyle.Size = new System.Drawing.Size(178, 201);
            this.flpCornerStyle.TabIndex = 5;
            // 
            // tCornerStyle
            // 
            this.tCornerStyle.AutoSize = true;
            this.tCornerStyle.Location = new System.Drawing.Point(3, 3);
            this.tCornerStyle.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tCornerStyle.Name = "tCornerStyle";
            this.tCornerStyle.Size = new System.Drawing.Size(118, 30);
            this.tCornerStyle.TabIndex = 0;
            this.tCornerStyle.Text = "CornerStyle";
            // 
            // rbCsDefault
            // 
            this.rbCsDefault.AutoSize = true;
            this.rbCsDefault.Location = new System.Drawing.Point(8, 39);
            this.rbCsDefault.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.rbCsDefault.Name = "rbCsDefault";
            this.rbCsDefault.Size = new System.Drawing.Size(106, 34);
            this.rbCsDefault.TabIndex = 1;
            this.rbCsDefault.TabStop = true;
            this.rbCsDefault.Text = "Default";
            this.rbCsDefault.UseVisualStyleBackColor = true;
            // 
            // rbCsDoNotRound
            // 
            this.rbCsDoNotRound.AutoSize = true;
            this.rbCsDoNotRound.Location = new System.Drawing.Point(8, 79);
            this.rbCsDoNotRound.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.rbCsDoNotRound.Name = "rbCsDoNotRound";
            this.rbCsDoNotRound.Size = new System.Drawing.Size(160, 34);
            this.rbCsDoNotRound.TabIndex = 2;
            this.rbCsDoNotRound.TabStop = true;
            this.rbCsDoNotRound.Text = "DoNotRound";
            this.rbCsDoNotRound.UseVisualStyleBackColor = true;
            // 
            // rbCsRound
            // 
            this.rbCsRound.AutoSize = true;
            this.rbCsRound.Location = new System.Drawing.Point(8, 119);
            this.rbCsRound.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.rbCsRound.Name = "rbCsRound";
            this.rbCsRound.Size = new System.Drawing.Size(98, 34);
            this.rbCsRound.TabIndex = 3;
            this.rbCsRound.TabStop = true;
            this.rbCsRound.Text = "Round";
            this.rbCsRound.UseVisualStyleBackColor = true;
            // 
            // rbCsRoundSmall
            // 
            this.rbCsRoundSmall.AutoSize = true;
            this.rbCsRoundSmall.Location = new System.Drawing.Point(8, 159);
            this.rbCsRoundSmall.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.rbCsRoundSmall.Name = "rbCsRoundSmall";
            this.rbCsRoundSmall.Size = new System.Drawing.Size(148, 34);
            this.rbCsRoundSmall.TabIndex = 4;
            this.rbCsRoundSmall.TabStop = true;
            this.rbCsRoundSmall.Text = "RoundSmall";
            this.rbCsRoundSmall.UseVisualStyleBackColor = true;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 443);
            this.Controls.Add(this.flpClient);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RemasterForms";
            this.SizingBegin += new System.EventHandler(this.TestForm_SizingBegin);
            this.SizingEnd += new System.EventHandler(this.TestForm_SizingEnd);
            this.WindowStateChanged += new System.EventHandler(this.TestForm_WindowStateChanged);
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TestForm_KeyPress);
            this.flpClient.ResumeLayout(false);
            this.flpClient.PerformLayout();
            this.flpTestForm.ResumeLayout(false);
            this.flpTestForm.PerformLayout();
            this.flpCustomForm.ResumeLayout(false);
            this.flpCustomForm.PerformLayout();
            this.flpFrameStyle.ResumeLayout(false);
            this.flpFrameStyle.PerformLayout();
            this.flpCornerStyle.ResumeLayout(false);
            this.flpCornerStyle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpClient;
        private System.Windows.Forms.CheckBox cbCloseBox;
        private System.Windows.Forms.CheckBox cbControlBox;
        private System.Windows.Forms.CheckBox cbHelpButton;
        private System.Windows.Forms.CheckBox cbMaximizeBox;
        private System.Windows.Forms.CheckBox cbMinimizeBox;
        private System.Windows.Forms.CheckBox cbSizeBox;
        private System.Windows.Forms.CheckBox cbToolWindow;
        private System.Windows.Forms.FlowLayoutPanel flpFrameStyle;
        private System.Windows.Forms.Label tFrameStyle;
        private System.Windows.Forms.RadioButton rbFsNone;
        private System.Windows.Forms.RadioButton rbFsDefault;
        private System.Windows.Forms.RadioButton rbFsEmbedded;
        private System.Windows.Forms.CheckBox cbDarkMode;
        private System.Windows.Forms.FlowLayoutPanel flpCornerStyle;
        private System.Windows.Forms.Label tCornerStyle;
        private System.Windows.Forms.RadioButton rbCsDefault;
        private System.Windows.Forms.RadioButton rbCsDoNotRound;
        private System.Windows.Forms.RadioButton rbCsRound;
        private System.Windows.Forms.RadioButton rbCsRoundSmall;
        private System.Windows.Forms.FlowLayoutPanel flpProp2;
        private System.Windows.Forms.FlowLayoutPanel flpTestForm;
        private System.Windows.Forms.Label tTestForm;
        private System.Windows.Forms.FlowLayoutPanel flpCustomForm;
        private System.Windows.Forms.Label tCustomForm;
        private System.Windows.Forms.CheckBox cbShowCaption;
    }
}

