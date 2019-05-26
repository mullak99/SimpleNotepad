namespace SimpleNotepad
{
    partial class SimpleNotepad
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleNotepad));
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EncodingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ANSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UTF8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DarkModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PureBlackModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BottomToolStrip = new System.Windows.Forms.ToolStrip();
            this.TextLengthLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.LineTotalLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TopToolStrip = new System.Windows.Forms.ToolStrip();
            this.NewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.OpenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveAsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CloseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.FontToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Runtime = new System.Windows.Forms.Timer(this.components);
            this.TabbedNotepad = new System.Windows.Forms.TabControl();
            this.EncodingLabel = new System.Windows.Forms.ToolStripLabel();
            this.ASCIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip.SuspendLayout();
            this.BottomToolStrip.SuspendLayout();
            this.TopToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.formatToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(800, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.SaveAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.CloseToolStripMenuItem,
            this.CloseAllToolStripMenuItem,
            this.toolStripSeparator4,
            this.ExitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.NewToolStripMenuItem.Text = "New";
            this.NewToolStripMenuItem.ToolTipText = "New";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.OpenToolStripMenuItem.Text = "Open...";
            this.OpenToolStripMenuItem.ToolTipText = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.SaveToolStripMenuItem.Text = "Save";
            this.SaveToolStripMenuItem.ToolTipText = "Save";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.SaveAsToolStripMenuItem.Text = "Save As...";
            this.SaveAsToolStripMenuItem.ToolTipText = "Save As";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(194, 6);
            // 
            // CloseToolStripMenuItem
            // 
            this.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem";
            this.CloseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.CloseToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.CloseToolStripMenuItem.Text = "Close";
            this.CloseToolStripMenuItem.ToolTipText = "Close";
            this.CloseToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // CloseAllToolStripMenuItem
            // 
            this.CloseAllToolStripMenuItem.Name = "CloseAllToolStripMenuItem";
            this.CloseAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
            this.CloseAllToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.CloseAllToolStripMenuItem.Text = "Close All";
            this.CloseAllToolStripMenuItem.ToolTipText = "Close All";
            this.CloseAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(194, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItem_Click);
            // 
            // formatToolStripMenuItem
            // 
            this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FontToolStripMenuItem,
            this.EncodingToolStripMenuItem});
            this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            this.formatToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.formatToolStripMenuItem.Text = "Format";
            // 
            // FontToolStripMenuItem
            // 
            this.FontToolStripMenuItem.Name = "FontToolStripMenuItem";
            this.FontToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.FontToolStripMenuItem.Text = "Font...";
            this.FontToolStripMenuItem.Click += new System.EventHandler(this.FontToolStripMenuItem_Click);
            // 
            // EncodingToolStripMenuItem
            // 
            this.EncodingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ANSIToolStripMenuItem,
            this.ASCIIToolStripMenuItem,
            this.UTF8ToolStripMenuItem});
            this.EncodingToolStripMenuItem.Name = "EncodingToolStripMenuItem";
            this.EncodingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.EncodingToolStripMenuItem.Text = "Encoding";
            // 
            // ANSIToolStripMenuItem
            // 
            this.ANSIToolStripMenuItem.Name = "ANSIToolStripMenuItem";
            this.ANSIToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ANSIToolStripMenuItem.Text = "ANSI";
            this.ANSIToolStripMenuItem.Click += new System.EventHandler(this.SetEncodingToolStripMenuItem_Click);
            // 
            // UTF8ToolStripMenuItem
            // 
            this.UTF8ToolStripMenuItem.Checked = true;
            this.UTF8ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UTF8ToolStripMenuItem.Name = "UTF8ToolStripMenuItem";
            this.UTF8ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.UTF8ToolStripMenuItem.Text = "UTF-8";
            this.UTF8ToolStripMenuItem.Click += new System.EventHandler(this.SetEncodingToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DefaultToolStripMenuItem,
            this.DarkModeToolStripMenuItem,
            this.PureBlackModeToolStripMenuItem});
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.themeToolStripMenuItem.Text = "Theme";
            // 
            // DefaultToolStripMenuItem
            // 
            this.DefaultToolStripMenuItem.Checked = true;
            this.DefaultToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DefaultToolStripMenuItem.Name = "DefaultToolStripMenuItem";
            this.DefaultToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.DefaultToolStripMenuItem.Text = "Default";
            this.DefaultToolStripMenuItem.Click += new System.EventHandler(this.SetDefaultThemeToolStripMenuItem_Click);
            // 
            // DarkModeToolStripMenuItem
            // 
            this.DarkModeToolStripMenuItem.Name = "DarkModeToolStripMenuItem";
            this.DarkModeToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.DarkModeToolStripMenuItem.Text = "Dark Mode";
            this.DarkModeToolStripMenuItem.Click += new System.EventHandler(this.SetDefaultThemeToolStripMenuItem_Click);
            // 
            // PureBlackModeToolStripMenuItem
            // 
            this.PureBlackModeToolStripMenuItem.Name = "PureBlackModeToolStripMenuItem";
            this.PureBlackModeToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.PureBlackModeToolStripMenuItem.Text = "Pure-Black Mode";
            this.PureBlackModeToolStripMenuItem.Click += new System.EventHandler(this.SetDefaultThemeToolStripMenuItem_Click);
            // 
            // BottomToolStrip
            // 
            this.BottomToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.BottomToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.BottomToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TextLengthLabel,
            this.toolStripSeparator1,
            this.LineTotalLabel,
            this.toolStripSeparator2,
            this.EncodingLabel});
            this.BottomToolStrip.Location = new System.Drawing.Point(0, 425);
            this.BottomToolStrip.Name = "BottomToolStrip";
            this.BottomToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BottomToolStrip.Size = new System.Drawing.Size(800, 25);
            this.BottomToolStrip.TabIndex = 3;
            this.BottomToolStrip.Text = "toolStrip2";
            // 
            // TextLengthLabel
            // 
            this.TextLengthLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TextLengthLabel.Name = "TextLengthLabel";
            this.TextLengthLabel.Size = new System.Drawing.Size(56, 22);
            this.TextLengthLabel.Text = "Length: 0";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // LineTotalLabel
            // 
            this.LineTotalLabel.Name = "LineTotalLabel";
            this.LineTotalLabel.Size = new System.Drawing.Size(46, 22);
            this.LineTotalLabel.Text = "Lines: 0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // TopToolStrip
            // 
            this.TopToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.TopToolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.TopToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripButton,
            this.OpenToolStripButton,
            this.SaveToolStripButton,
            this.SaveAsToolStripButton,
            this.CloseToolStripButton,
            this.toolStripSeparator5,
            this.FontToolStripButton});
            this.TopToolStrip.Location = new System.Drawing.Point(0, 24);
            this.TopToolStrip.Name = "TopToolStrip";
            this.TopToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.TopToolStrip.Size = new System.Drawing.Size(800, 25);
            this.TopToolStrip.TabIndex = 5;
            this.TopToolStrip.Text = "toolStrip2";
            // 
            // NewToolStripButton
            // 
            this.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("NewToolStripButton.Image")));
            this.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewToolStripButton.Name = "NewToolStripButton";
            this.NewToolStripButton.Size = new System.Drawing.Size(35, 22);
            this.NewToolStripButton.Text = "New";
            this.NewToolStripButton.Click += new System.EventHandler(this.NewToolStripButton_Click);
            // 
            // OpenToolStripButton
            // 
            this.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.OpenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenToolStripButton.Image")));
            this.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenToolStripButton.Name = "OpenToolStripButton";
            this.OpenToolStripButton.Size = new System.Drawing.Size(49, 22);
            this.OpenToolStripButton.Text = "Open...";
            this.OpenToolStripButton.Click += new System.EventHandler(this.OpenToolStripButton_Click);
            // 
            // SaveToolStripButton
            // 
            this.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SaveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveToolStripButton.Image")));
            this.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripButton.Name = "SaveToolStripButton";
            this.SaveToolStripButton.Size = new System.Drawing.Size(35, 22);
            this.SaveToolStripButton.Text = "Save";
            this.SaveToolStripButton.Click += new System.EventHandler(this.SaveToolStripButton_Click);
            // 
            // SaveAsToolStripButton
            // 
            this.SaveAsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SaveAsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveAsToolStripButton.Image")));
            this.SaveAsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveAsToolStripButton.Name = "SaveAsToolStripButton";
            this.SaveAsToolStripButton.Size = new System.Drawing.Size(60, 22);
            this.SaveAsToolStripButton.Text = "Save As...";
            this.SaveAsToolStripButton.Click += new System.EventHandler(this.SaveAsToolStripButton_Click);
            // 
            // CloseToolStripButton
            // 
            this.CloseToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CloseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CloseToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseToolStripButton.Image")));
            this.CloseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseToolStripButton.Name = "CloseToolStripButton";
            this.CloseToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.CloseToolStripButton.Text = "X";
            this.CloseToolStripButton.ToolTipText = "Close";
            this.CloseToolStripButton.Click += new System.EventHandler(this.CloseToolStripButton_Click);
            this.CloseToolStripButton.MouseHover += new System.EventHandler(this.CloseToolStripButton_MouseHover);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // FontToolStripButton
            // 
            this.FontToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FontToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("FontToolStripButton.Image")));
            this.FontToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FontToolStripButton.Name = "FontToolStripButton";
            this.FontToolStripButton.Size = new System.Drawing.Size(44, 22);
            this.FontToolStripButton.Text = "Font...";
            this.FontToolStripButton.Click += new System.EventHandler(this.FontToolStripButton_Click);
            // 
            // Runtime
            // 
            this.Runtime.Enabled = true;
            this.Runtime.Tick += new System.EventHandler(this.Runtime_Tick);
            // 
            // TabbedNotepad
            // 
            this.TabbedNotepad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabbedNotepad.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.TabbedNotepad.Location = new System.Drawing.Point(0, 49);
            this.TabbedNotepad.Name = "TabbedNotepad";
            this.TabbedNotepad.Padding = new System.Drawing.Point(0, 0);
            this.TabbedNotepad.SelectedIndex = 0;
            this.TabbedNotepad.ShowToolTips = true;
            this.TabbedNotepad.Size = new System.Drawing.Size(800, 376);
            this.TabbedNotepad.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabbedNotepad.TabIndex = 6;
            this.TabbedNotepad.TabStop = false;
            this.TabbedNotepad.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabbedNotepad_DrawItem);
            this.TabbedNotepad.TabIndexChanged += new System.EventHandler(this.TabbedNotepad_TabIndexChanged);
            this.TabbedNotepad.Click += new System.EventHandler(this.TabbedNotepad_Click);
            this.TabbedNotepad.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TabbedNotepad_MouseDown);
            // 
            // EncodingLabel
            // 
            this.EncodingLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.EncodingLabel.Name = "EncodingLabel";
            this.EncodingLabel.Size = new System.Drawing.Size(38, 22);
            this.EncodingLabel.Text = "UTF-8";
            // 
            // ASCIIToolStripMenuItem
            // 
            this.ASCIIToolStripMenuItem.Name = "ASCIIToolStripMenuItem";
            this.ASCIIToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ASCIIToolStripMenuItem.Text = "ASCII";
            this.ASCIIToolStripMenuItem.Click += new System.EventHandler(this.SetEncodingToolStripMenuItem_Click);
            // 
            // SimpleNotepad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TabbedNotepad);
            this.Controls.Add(this.TopToolStrip);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.BottomToolStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "SimpleNotepad";
            this.Text = "SimpleNotepad";
            this.Load += new System.EventHandler(this.SimpleNotepad_Load);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.BottomToolStrip.ResumeLayout(false);
            this.BottomToolStrip.PerformLayout();
            this.TopToolStrip.ResumeLayout(false);
            this.TopToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip BottomToolStrip;
        private System.Windows.Forms.ToolStripLabel TextLengthLabel;
        private System.Windows.Forms.ToolStrip TopToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel LineTotalLabel;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton NewToolStripButton;
        private System.Windows.Forms.ToolStripButton OpenToolStripButton;
        private System.Windows.Forms.ToolStripButton SaveToolStripButton;
        private System.Windows.Forms.ToolStripButton SaveAsToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer Runtime;
        private System.Windows.Forms.TabControl TabbedNotepad;
        private System.Windows.Forms.ToolStripButton CloseToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FontToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton FontToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DarkModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PureBlackModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DefaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EncodingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ANSIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UTF8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel EncodingLabel;
        private System.Windows.Forms.ToolStripMenuItem ASCIIToolStripMenuItem;
    }
}

