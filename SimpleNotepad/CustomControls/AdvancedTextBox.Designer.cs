namespace SimpleNotepad.CustomControls
{
    partial class AdvancedTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FullAreaPanel = new System.Windows.Forms.Panel();
            this.TextBoxPanel = new System.Windows.Forms.Panel();
            this.MainTextBox = new System.Windows.Forms.RichTextBox();
            this.LineNumbers = new System.Windows.Forms.RichTextBox();
            this.FullAreaPanel.SuspendLayout();
            this.TextBoxPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FullAreaPanel
            // 
            this.FullAreaPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FullAreaPanel.Controls.Add(this.TextBoxPanel);
            this.FullAreaPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FullAreaPanel.Location = new System.Drawing.Point(0, 0);
            this.FullAreaPanel.Name = "FullAreaPanel";
            this.FullAreaPanel.Size = new System.Drawing.Size(289, 164);
            this.FullAreaPanel.TabIndex = 5;
            // 
            // TextBoxPanel
            // 
            this.TextBoxPanel.Controls.Add(this.MainTextBox);
            this.TextBoxPanel.Controls.Add(this.LineNumbers);
            this.TextBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxPanel.Location = new System.Drawing.Point(0, 0);
            this.TextBoxPanel.Name = "TextBoxPanel";
            this.TextBoxPanel.Size = new System.Drawing.Size(285, 160);
            this.TextBoxPanel.TabIndex = 2;
            this.TextBoxPanel.Resize += new System.EventHandler(this.AdvancedTextBox_Resize);
            // 
            // MainTextBox
            // 
            this.MainTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MainTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTextBox.Location = new System.Drawing.Point(27, 0);
            this.MainTextBox.Name = "MainTextBox";
            this.MainTextBox.Size = new System.Drawing.Size(258, 160);
            this.MainTextBox.TabIndex = 0;
            this.MainTextBox.Text = "";
            this.MainTextBox.SelectionChanged += new System.EventHandler(this.MainTextBox_SelectionChanged);
            this.MainTextBox.VScroll += new System.EventHandler(this.MainTextBox_VScroll);
            this.MainTextBox.FontChanged += new System.EventHandler(this.MainTextBox_FontChanged);
            this.MainTextBox.TextChanged += new System.EventHandler(this.MainTextBox_TextChanged);
            this.MainTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainTextBox_MouseDown);
            // 
            // LineNumbers
            // 
            this.LineNumbers.BackColor = System.Drawing.SystemColors.Control;
            this.LineNumbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LineNumbers.Cursor = System.Windows.Forms.Cursors.Default;
            this.LineNumbers.Dock = System.Windows.Forms.DockStyle.Left;
            this.LineNumbers.Enabled = false;
            this.LineNumbers.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.LineNumbers.Location = new System.Drawing.Point(0, 0);
            this.LineNumbers.Name = "LineNumbers";
            this.LineNumbers.ReadOnly = true;
            this.LineNumbers.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.LineNumbers.Size = new System.Drawing.Size(27, 160);
            this.LineNumbers.TabIndex = 1;
            this.LineNumbers.TabStop = false;
            this.LineNumbers.Text = "";
            // 
            // AdvancedTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FullAreaPanel);
            this.Name = "AdvancedTextBox";
            this.Size = new System.Drawing.Size(289, 164);
            this.FullAreaPanel.ResumeLayout(false);
            this.TextBoxPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel FullAreaPanel;
        private System.Windows.Forms.Panel TextBoxPanel;
        private System.Windows.Forms.RichTextBox MainTextBox;
        private System.Windows.Forms.RichTextBox LineNumbers;
    }
}
