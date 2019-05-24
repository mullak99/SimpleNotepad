using SimpleNotepad.CustomControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        private void Save()
        {
            notepadPages[TabbedNotepad.SelectedIndex].Save();
        }

        private void SaveAs()
        {
            notepadPages[TabbedNotepad.SelectedIndex].SaveAs();
        }

        private void Open()
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

        private void New()
        {
            NotepadPage notePadPage = new NotepadPage(ref TabbedNotepad, String.Format("New {0}", notepadPages.Count + 1));
            notepadPages.Add(notePadPage);
            notePadPage.Focus();
        }

        private bool CloseTabAt(int index)
        {
            bool ret = false;
            try
            {
                if (notepadPages[index].Saved)
                {
                    if (!String.IsNullOrEmpty(notepadPages[index].Text))
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

                    if (TabbedNotepad.TabPages.Count == 0) New();
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

        private void ChangeColourScheme()
        {

        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseTabAt(TabbedNotepad.SelectedIndex);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllTabs();
        }

        private void Runtime_Tick(object sender, EventArgs e)
        {
            try
            {
                if (notepadPages.Count == 0) New();

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

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void SimpleNotepad_Load(object sender, EventArgs e)
        {
            if (notepadPages.Count == 0) New();
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

        private void DarkModeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DarkModeToolStripMenuItem.Checked = !DarkModeToolStripMenuItem.Checked;

            if (DarkModeToolStripMenuItem.Checked)
            {

            }
            else
            {

            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepadPages[TabbedNotepad.SelectedIndex].Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notepadPages[TabbedNotepad.SelectedIndex].Redo();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (!CloseAllTabs())
                e.Cancel = true;
        }
    }

    public class NotepadPage
    {
        private TabControl _tabControl;
        private TabPage _tabPage;
        private AdvancedTextBox _advandedTextBox;

        private string _fileName, _filePath;

        private const int MaxTabTitleLength = 11;

        public NotepadPage(ref TabControl tabControl, string fileName, bool switchToNewTab = true, string filePath = null)
        {
            _tabControl = tabControl;
            _fileName = fileName;
            _filePath = filePath;

            _advandedTextBox = new AdvancedTextBox();
            _advandedTextBox.Dock = DockStyle.Fill;

            _tabPage = new TabPage();

            if (_fileName.Length > (MaxTabTitleLength + 3))
            {
                _tabPage.Text = _fileName.Substring(0, MaxTabTitleLength).TrimEnd(' ') + "...";
            }
            else
            {
                _tabPage.Text = _fileName;
            }

            _tabPage.ToolTipText = _fileName;

            _tabPage.Controls.Add(_advandedTextBox);

            _tabControl.Controls.Add(_tabPage);
            _tabControl.SelectedTab = _tabPage;

            _tabControl.Refresh();

            Focus();
        }

        public string GetFileName()
        {
            return _fileName;
        }

        public string GetFilePath()
        {
            return _filePath;
        }

        public bool Focus()
        {
            return _advandedTextBox.Focus();
        }

        public void Undo()
        {
            _advandedTextBox.Undo();
        }

        public void Redo()
        {
            _advandedTextBox.Redo();
        }

        public string TabTitle
        {
            get
            {
                return _tabPage.Text;
            }
            set
            {
                if (value.Length > (MaxTabTitleLength + 3))
                {
                    _tabPage.Text = value.Substring(0, MaxTabTitleLength).TrimEnd(' ') + "...";
                }
                else
                {
                    _tabPage.Text = value;
                }
            }
        }

        public string TabToolTip
        {
            get
            {
                return _tabPage.ToolTipText;
            }
            set
            {
                _tabPage.ToolTipText = value;
            }
        }

        public string Text
        {
            get
            {
                return _advandedTextBox.Text;
            }
            set
            {
                _advandedTextBox.Text = value;
            }
        }

        public string[] Lines
        {
            get
            {
                return _advandedTextBox.Lines;
            }
            set
            {
                _advandedTextBox.Lines = value;
            }
        }

        public bool Saved
        {
            get
            {
                return _advandedTextBox.isTextSaved;
            }
            set
            {
                _advandedTextBox.isTextSaved = value;
            }
        }

        public bool Save()
        {
            if (!String.IsNullOrEmpty(_filePath))
            {
                File.WriteAllLines(_filePath, _advandedTextBox.Lines);

                _advandedTextBox.CreateSnapshotOfText();
                Saved = _advandedTextBox.DoesCurrentTextEqualSnapshot();

                return true;
            }
            else
            {
                return SaveAs();
            }
        }

        public bool SaveAs()
        {
            SaveFileDialog saveFileDiag = new SaveFileDialog();
            saveFileDiag.Title = "Save File As";
            saveFileDiag.Filter = "All types (*.*)|*.*|Text Files (*.txt)|*.txt";

            if (saveFileDiag.ShowDialog() == DialogResult.OK)
            {
                _fileName = Path.GetFileName(saveFileDiag.FileName);
                _filePath = Path.GetFullPath(saveFileDiag.FileName);

                _tabPage.Text = _fileName;
                _tabPage.ToolTipText = _fileName;

                File.WriteAllLines(_filePath, _advandedTextBox.Lines);

                _advandedTextBox.CreateSnapshotOfText();
                Saved = _advandedTextBox.DoesCurrentTextEqualSnapshot();

                return true;
            }
            else return false;
        }

        public bool Open()
        {
            OpenFileDialog openFileDiag = new OpenFileDialog();
            openFileDiag.Title = "Open File";
            openFileDiag.Filter = "All types (*.*)|*.*|Text Files (*.txt)|*.txt";

            if (openFileDiag.ShowDialog() == DialogResult.OK)
            {
                _fileName = openFileDiag.SafeFileName;
                _filePath = openFileDiag.FileName;
                TabTitle = _fileName;
                TabToolTip = _fileName;

                _tabControl.Refresh();

                _advandedTextBox.Lines = File.ReadAllLines(_filePath);
                _advandedTextBox.Select(_advandedTextBox.TextLength, 0);
                _advandedTextBox.CreateSnapshotOfText();
                Focus();

                Saved = _advandedTextBox.DoesCurrentTextEqualSnapshot();
                return true;
            }
            else return false;
        }
    }
}
