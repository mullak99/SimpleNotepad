using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleNotepad
{
    public partial class SimpleNotepad : Form
    {
        List<NotepadPage> notepadPages = new List<NotepadPage>();
        List<string> tabFileNames = new List<string>();

        Font _globalFont;
        Theme _globalTheme;

        Theme _defaultTheme = new Theme(SystemColors.WindowText, SystemColors.Window, SystemColors.ControlDarkDark, SystemColors.Control);

        public SimpleNotepad()
        {
            InitializeComponent();
            _globalFont = this.Font;
        }

        #region Methods

        private string GetNewFileName(Int64 newFileNameIntStart = -1)
        {
            Int64 newFileNameInt;
            if (newFileNameIntStart == -1)
                newFileNameInt = 1;
            else newFileNameInt = newFileNameIntStart + 1;

            string tabFileName = String.Format("New {0}", newFileNameInt);

            if (tabFileNames.Exists(s => s == tabFileName))
            {
                tabFileName = String.Format("New {0}", newFileNameInt + 1);
                return GetNewFileName(newFileNameInt);
            }
            else
            {
                tabFileNames.Add(tabFileName);
                return tabFileName;
            }
        }

        #endregion
        #region Tabs

        private void ForceRemoveTabAt(int index)
        {
            string fileName = notepadPages[index].FileName;

            notepadPages.RemoveAt(index);
            TabbedNotepad.TabPages.RemoveAt(index);
            tabFileNames.Remove(fileName);

            if (TabbedNotepad.TabCount > 0) TabbedNotepad.SelectedIndex = (TabbedNotepad.TabCount - 1);
        }

        private bool CloseTabAt(int index)
        {
            bool ret = false;
            try
            {
                if (notepadPages[index].Saved)
                {
                    if (TabbedNotepad.TabCount > 1 || !String.IsNullOrEmpty(notepadPages[index].Text))
                    {
                        ForceRemoveTabAt(index);
                        ret = true;
                    }
                }
                else
                {
                    DialogResult diagResult = MessageBox.Show(String.Format("Do you want to close '{0}' without saving?", notepadPages[index].FileName), "Close without saving?", MessageBoxButtons.YesNoCancel);

                    if (diagResult == DialogResult.Yes)
                    {
                        ForceRemoveTabAt(index);
                        ret = true;
                    }
                    else if (diagResult == DialogResult.No && notepadPages[index].Save())
                    {
                        ForceRemoveTabAt(index);
                        ret = true;
                    }

                    if (TabbedNotepad.TabPages.Count == 0) NewToolStripMenuItem_Click(null, null);
                }
            }
            catch { }

            return ret;
        }

        private bool CloseAllTabs()
        {
            bool didAllClose = true;
            NotepadPage[] tempPages = new NotepadPage[notepadPages.Count];
            notepadPages.CopyTo(tempPages);

            foreach (NotepadPage npPage in tempPages)
            {
                if (!CloseTabAt(notepadPages.IndexOf(npPage)) && !npPage.Saved)
                {
                    didAllClose = false;
                }
            }
            return didAllClose;
        }

        private void TabbedNotepad_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
            e.Graphics.DrawString(this.TabbedNotepad.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        private void TabbedNotepad_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.TabbedNotepad.TabPages.Count; i++)
            {
                Rectangle r = TabbedNotepad.GetTabRect(i);

                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                if (closeButton.Contains(e.Location))
                {
                    CloseTabAt(i);
                    break;
                }
            }
        }

        private void TabbedNotepad_TabIndexChanged(object sender, EventArgs e)
        {
            Runtime_Tick(sender, e);
        }

        private void TabbedNotepad_Click(object sender, EventArgs e)
        {
            foreach (NotepadPage npPage in notepadPages)
            {
                npPage.Focus();
            }
        }

        #endregion
        #region MenuToolStrip

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotepadPage notePadPage = new NotepadPage(ref TabbedNotepad, GetNewFileName(), _globalFont, _globalTheme);
            notepadPages.Add(notePadPage);
            notePadPage.Focus();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotepadPage notepadPage;
            string oldTabFileName = "";

            if (notepadPages.Count > 0 && !String.IsNullOrWhiteSpace(notepadPages[notepadPages.Count - 1].Text))
                notepadPage = new NotepadPage(ref TabbedNotepad, "", _globalFont, _globalTheme);
            else
            {
                oldTabFileName = notepadPages[notepadPages.Count - 1].FileName;
                notepadPage = notepadPages[notepadPages.Count - 1];
            }

            bool opened = notepadPage.Open();
            if (opened)
            {
                if (!String.IsNullOrWhiteSpace(oldTabFileName)) tabFileNames.Remove(oldTabFileName);
                notepadPages.Add(notepadPage);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepadPages[TabbedNotepad.SelectedIndex].Save();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepadPages[TabbedNotepad.SelectedIndex].SaveAs();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseTabAt(TabbedNotepad.SelectedIndex);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllTabs();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepadPages[TabbedNotepad.SelectedIndex].Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepadPages[TabbedNotepad.SelectedIndex].Redo();
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDiag = new FontDialog();
            fontDiag.ShowColor = false;
            fontDiag.Font = _globalFont;
            DialogResult result = fontDiag.ShowDialog();

            if (result != DialogResult.Cancel)
            {
                _globalFont = fontDiag.Font;

                foreach (NotepadPage npPage in notepadPages)
                {
                    npPage.Font = _globalFont;
                }
            }
        }

        private void SetDefaultThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color standardBackground = SystemColors.Window;
            Color standardForeground = SystemColors.WindowText;
            Color standardLineBackground = SystemColors.Control;
            Color standardLineForeground = SystemColors.ControlDarkDark;

            if (sender == DefaultToolStripMenuItem)
            {
                DefaultToolStripMenuItem.Checked = true;
                DarkModeToolStripMenuItem.Checked = false;
                PureBlackModeToolStripMenuItem.Checked = false;

                standardBackground = _defaultTheme.StandardBackgroundColor;
                standardForeground = _defaultTheme.StandardTextColor;
                standardLineBackground = _defaultTheme.AltBackgroundColor;
                standardLineForeground = _defaultTheme.AltTextColor;
            }
            else if (sender == DarkModeToolStripMenuItem)
            {
                DefaultToolStripMenuItem.Checked = false;
                DarkModeToolStripMenuItem.Checked = true;
                PureBlackModeToolStripMenuItem.Checked = false;

                standardBackground = Color.FromArgb(30, 30, 30);
                standardForeground = Color.White;
                standardLineBackground = Color.FromArgb(60, 60, 60);
                standardLineForeground = Color.LightGray;
            }
            else if (sender == PureBlackModeToolStripMenuItem)
            {
                DefaultToolStripMenuItem.Checked = false;
                DarkModeToolStripMenuItem.Checked = false;
                PureBlackModeToolStripMenuItem.Checked = true;

                standardBackground = Color.FromArgb(0, 0, 0);
                standardForeground = Color.White;
                standardLineBackground = Color.FromArgb(30, 30, 30);
                standardLineForeground = Color.LightGray;
            }

            _globalTheme = new Theme(standardForeground, standardBackground, standardLineForeground, standardLineBackground);

            foreach (NotepadPage npPage in notepadPages)
            {
                npPage.Theme = _globalTheme;
                npPage.ApplyTheme();
            }
        }

        private void SetEncodingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender == ANSIToolStripMenuItem)
            {
                notepadPages[TabbedNotepad.SelectedIndex].Encoding = Encoding.Default;
                ANSIToolStripMenuItem.Checked = true;
                ASCIIToolStripMenuItem.Checked = false;
                UTF8ToolStripMenuItem.Checked = false;
            }
            else if (sender == ASCIIToolStripMenuItem)
            {
                notepadPages[TabbedNotepad.SelectedIndex].Encoding = Encoding.ASCII;
                ANSIToolStripMenuItem.Checked = false;
                ASCIIToolStripMenuItem.Checked = true;
                UTF8ToolStripMenuItem.Checked = false;
            }
            else if (sender == UTF8ToolStripMenuItem)
            {
                notepadPages[TabbedNotepad.SelectedIndex].Encoding = Encoding.UTF8;
                ANSIToolStripMenuItem.Checked = false;
                ASCIIToolStripMenuItem.Checked = false;
                UTF8ToolStripMenuItem.Checked = true;
            }
        }

        #endregion
        #region SubToolStrip

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            NewToolStripMenuItem_Click(sender, e);
        }

        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            OpenToolStripMenuItem_Click(sender, e);
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveToolStripMenuItem_Click(sender, e);
        }

        private void SaveAsToolStripButton_Click(object sender, EventArgs e)
        {
            SaveAsToolStripMenuItem_Click(sender, e);
        }

        private void CloseToolStripButton_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
                CloseAllToolStripMenuItem_Click(sender, e);
            else
                CloseToolStripMenuItem_Click(sender, e);
        }

        private void CloseToolStripButton_MouseHover(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
                CloseToolStripButton.ToolTipText = "Close All";
            else
                CloseToolStripButton.ToolTipText = "Close";
        }

        private void FontToolStripButton_Click(object sender, EventArgs e)
        {
            FontToolStripMenuItem_Click(sender, e);
        }

        #endregion
        #region Misc UI

        private void SimpleNotepad_Load(object sender, EventArgs e)
        {
            if (notepadPages.Count == 0) NewToolStripMenuItem_Click(sender, e);
        }

        private void Runtime_Tick(object sender, EventArgs e)
        {
            try
            {
                if (TabbedNotepad.TabCount == 0) NewToolStripMenuItem_Click(sender, e);

                int textLen = notepadPages[TabbedNotepad.SelectedIndex].Text.Length;
                int lineLen = notepadPages[TabbedNotepad.SelectedIndex].Lines.Length;

                TextLengthLabel.Text = "Length: " + textLen;

                if (lineLen > 0) LineTotalLabel.Text = "Lines: " + lineLen;
                else LineTotalLabel.Text = "Lines: 1";

                string TabTitle, TabToolTip;
                if (notepadPages[TabbedNotepad.SelectedIndex].Saved)
                {
                    try
                    {
                        TabTitle = notepadPages[TabbedNotepad.SelectedIndex].FileName.Substring(0, 11).TrimEnd(' ', '.') + "...";
                    }
                    catch
                    {
                        TabTitle = notepadPages[TabbedNotepad.SelectedIndex].FileName;
                    }

                    TabToolTip = notepadPages[TabbedNotepad.SelectedIndex].FileName;
                    this.Text = String.Format("{0} - SimpleNotepad", notepadPages[TabbedNotepad.SelectedIndex].FileName);
                }
                else
                {
                    try
                    {
                        TabTitle = ("(*) " + notepadPages[TabbedNotepad.SelectedIndex].FileName).Substring(0, 11).TrimEnd(' ', '.') + "...";
                    }
                    catch
                    {
                        TabTitle = ("(*) " + notepadPages[TabbedNotepad.SelectedIndex].FileName);
                    }
                    TabToolTip = "(Unsaved) " + notepadPages[TabbedNotepad.SelectedIndex].FileName;
                    this.Text = String.Format("(*) {0} - SimpleNotepad", notepadPages[TabbedNotepad.SelectedIndex].FileName);
                }

                if (TabTitle != null && TabToolTip != null && notepadPages[TabbedNotepad.SelectedIndex].TabTitle != TabTitle || notepadPages[TabbedNotepad.SelectedIndex].TabToolTip != TabToolTip)
                {
                    notepadPages[TabbedNotepad.SelectedIndex].TabTitle = TabTitle;
                    notepadPages[TabbedNotepad.SelectedIndex].TabToolTip = TabToolTip;

                    TabbedNotepad.Refresh();
                    notepadPages[TabbedNotepad.SelectedIndex].Focus();
                }

                EncodingLabel.Text = notepadPages[TabbedNotepad.SelectedIndex].EncodingString.ToUpper();

                if (notepadPages[TabbedNotepad.SelectedIndex].Encoding == Encoding.Default && !ANSIToolStripMenuItem.Checked)
                    SetEncodingToolStripMenuItem_Click(ANSIToolStripMenuItem, e);
                else if (notepadPages[TabbedNotepad.SelectedIndex].Encoding == Encoding.ASCII && !ASCIIToolStripMenuItem.Checked)
                    SetEncodingToolStripMenuItem_Click(ASCIIToolStripMenuItem, e);
                else if (notepadPages[TabbedNotepad.SelectedIndex].Encoding == Encoding.UTF8 && !UTF8ToolStripMenuItem.Checked)
                    SetEncodingToolStripMenuItem_Click(UTF8ToolStripMenuItem, e);

            }
            catch { }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (!CloseAllTabs())
                e.Cancel = true;
        }

        #endregion
    }
}
