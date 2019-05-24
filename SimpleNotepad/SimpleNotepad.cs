using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleNotepad
{
    public partial class SimpleNotepad : Form
    {

        List<NotepadPage> notepadPages = new List<NotepadPage>();

        public SimpleNotepad()
        {
            InitializeComponent();
        }

        #region Tabs

        private bool CloseTabAt(int index)
        {
            bool ret = false;
            try
            {
                if (notepadPages[index].Saved)
                {
                    if (TabbedNotepad.TabCount > 1 || !String.IsNullOrEmpty(notepadPages[index].Text))
                    {
                        notepadPages.RemoveAt(index);
                        TabbedNotepad.TabPages.RemoveAt(index);
                        ret = true;
                    }
                }
                else
                {
                    DialogResult diagResult = MessageBox.Show(String.Format("Are you want to close '{0}' without saving?", notepadPages[index].GetFileName()), "Close without saving?", MessageBoxButtons.YesNoCancel);

                    if (diagResult == DialogResult.Yes)
                    {
                        notepadPages.RemoveAt(index);
                        TabbedNotepad.TabPages.RemoveAt(index);
                        ret = true;
                    }
                    else if (diagResult == DialogResult.No && notepadPages[index].Save())
                    {
                        notepadPages.RemoveAt(index);
                        TabbedNotepad.TabPages.RemoveAt(index);
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

        #endregion
        #region MenuToolStrip

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotepadPage notePadPage = new NotepadPage(ref TabbedNotepad, String.Format("New {0}", notepadPages.Count + 1));
            notepadPages.Add(notePadPage);
            notePadPage.Focus();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotepadPage notepadPage;

            if (notepadPages.Count > 0 && !String.IsNullOrWhiteSpace(notepadPages[notepadPages.Count - 1].Text))
                notepadPage = new NotepadPage(ref TabbedNotepad, String.Format("New {0}", notepadPages.Count + 1));
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
                    SavedLabel.Text = "Saved: True";

                    try
                    {
                        TabTitle = notepadPages[TabbedNotepad.SelectedIndex].GetFileName().Substring(0, 11).TrimEnd(' ') + "...";
                    }
                    catch
                    {
                        TabTitle = notepadPages[TabbedNotepad.SelectedIndex].GetFileName();
                    }

                    TabToolTip = notepadPages[TabbedNotepad.SelectedIndex].GetFileName();
                    this.Text = String.Format("{0} - SimpleNotepad", notepadPages[TabbedNotepad.SelectedIndex].GetFileName());
                }
                else
                {
                    SavedLabel.Text = "Saved: False";

                    try
                    {
                        TabTitle = ("(*) " + notepadPages[TabbedNotepad.SelectedIndex].GetFileName()).Substring(0, 11).TrimEnd(' ') + "...";
                    }
                    catch
                    {
                        TabTitle = ("(*) " + notepadPages[TabbedNotepad.SelectedIndex].GetFileName());
                    }
                    TabToolTip = "(Unsaved) " + notepadPages[TabbedNotepad.SelectedIndex].GetFileName();
                    this.Text = String.Format("(*) {0} - SimpleNotepad", notepadPages[TabbedNotepad.SelectedIndex].GetFileName());
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
