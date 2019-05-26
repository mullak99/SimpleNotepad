using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleNotepad
{
    public partial class SimpleNotepad : Form
    {
        List<NotepadPage> notepadPages = new List<NotepadPage>();

        Font _globalFont;

        public SimpleNotepad()
        {
            InitializeComponent();
            _globalFont = this.Font;
        }

        #region Tabs

        private void ForceRemoveTabAt(int index)
        {
            notepadPages.RemoveAt(index);
            TabbedNotepad.TabPages.RemoveAt(index);

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
            NotepadPage notePadPage = new NotepadPage(ref TabbedNotepad, String.Format("New {0}", notepadPages.Count + 1), _globalFont);
            notepadPages.Add(notePadPage);
            notePadPage.Focus();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotepadPage notepadPage;

            if (notepadPages.Count > 0 && !String.IsNullOrWhiteSpace(notepadPages[notepadPages.Count - 1].Text))
                notepadPage = new NotepadPage(ref TabbedNotepad, String.Format("New {0}", notepadPages.Count + 1), _globalFont);
            else
                notepadPage = notepadPages[notepadPages.Count - 1];

            bool opened = notepadPage.Open();
            if (opened)
            {
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

        private void DarkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DarkModeToolStripMenuItem.Checked = !DarkModeToolStripMenuItem.Checked;

            if (DarkModeToolStripMenuItem.Checked)
            {
                foreach (NotepadPage npPage in notepadPages)
                {
                    npPage.SetTabColours(Color.White, Color.FromArgb(30, 30, 30));
                    npPage.SetNotepadColours(Color.White, Color.FromArgb(30, 30, 30), Color.LightGray, Color.FromArgb(60, 60, 60));
                }
                /*
                MenuStrip.BackColor = Color.FromArgb(45, 45, 45);
                MenuStrip.ForeColor = Color.White;

                TopToolStrip.BackColor = Color.FromArgb(45, 45, 45);
                TopToolStrip.ForeColor = Color.White;

                BottomToolStrip.BackColor = Color.FromArgb(45, 45, 45);
                BottomToolStrip.ForeColor = Color.White;*/
            }
            else
            {
                foreach (NotepadPage npPage in notepadPages)
                {
                    npPage.ResetTabColours();
                    npPage.ResetNotepadColours();
                }
                /*
                MenuStrip.BackColor = Color.FromArgb(240, 240, 240);
                MenuStrip.ForeColor = SystemColors.WindowText;

                TopToolStrip.BackColor = Color.FromArgb(240, 240, 240);
                TopToolStrip.ForeColor = SystemColors.WindowText;

                BottomToolStrip.BackColor = Color.FromArgb(240, 240, 240);
                BottomToolStrip.ForeColor = SystemColors.WindowText;*/
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
            CloseToolStripMenuItem_Click(sender, e);
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

                TextLengthLabel.Text = "Length: " + notepadPages[TabbedNotepad.SelectedIndex].Text.Length;
                LineTotalLabel.Text = "Lines: " + notepadPages[TabbedNotepad.SelectedIndex].Lines.Length;

                string TabTitle, TabToolTip;
                if (notepadPages[TabbedNotepad.SelectedIndex].Saved)
                {
                    try
                    {
                        TabTitle = notepadPages[TabbedNotepad.SelectedIndex].FileName.Substring(0, 11).TrimEnd(' ') + "...";
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
                        TabTitle = ("(*) " + notepadPages[TabbedNotepad.SelectedIndex].FileName).Substring(0, 11).TrimEnd(' ') + "...";
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
