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
                notepadPage = new NotepadPage(ref TabbedNotepad, String.Format("New {0}", notepadPages.Count));
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
            NotepadPage notePadPage = new NotepadPage(ref TabbedNotepad, String.Format("New {0}", notepadPages.Count));
            notepadPages.Add(notePadPage);
            notePadPage.Focus();
        }

        private void CloseTabAt(int index)
        {
            try
            {
                if (notepadPages[index].Saved)
                {
                    notepadPages.RemoveAt(index);
                    TabbedNotepad.TabPages.RemoveAt(index);
                }
                else
                {
                    DialogResult diagResult = MessageBox.Show("Are you want to close this tab without saving?", "Close without saving?", MessageBoxButtons.YesNoCancel);

                    if (diagResult == DialogResult.Yes)
                    {
                        notepadPages.RemoveAt(index);
                        TabbedNotepad.TabPages.RemoveAt(index);
                    }
                    else if (diagResult == DialogResult.No && notepadPages[index].Save())
                    {
                        notepadPages.RemoveAt(index);
                        TabbedNotepad.TabPages.RemoveAt(index);
                    }
                }
            }
            catch { }
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
            
        }

        private void Runtime_Tick(object sender, EventArgs e)
        {
            try
            {
                TextLengthLabel.Text = "Length: " + notepadPages[TabbedNotepad.SelectedIndex].Text.Length;
                LineTotalLabel.Text = "Lines: " + notepadPages[TabbedNotepad.SelectedIndex].Lines.Length;

                string TabTitle, TabToolTip;
                if (notepadPages[TabbedNotepad.SelectedIndex].Saved)
                {
                    SavedLabel.Text = "Saved: True";

                    TabTitle = notepadPages[TabbedNotepad.SelectedIndex].GetFileName();
                    TabToolTip = notepadPages[TabbedNotepad.SelectedIndex].GetFileName();
                }
                else
                {
                    SavedLabel.Text = "Saved: False";

                    TabTitle = "(*) " + notepadPages[TabbedNotepad.SelectedIndex].GetFileName();
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
    }

    public class NotepadPage
    {
        private TabControl _tabControl;
        private TabPage _tabPage;
        private AdvancedTextBox _advandedTextBox;

        private string _fileName, _filePath;

        public NotepadPage(ref TabControl tabControl, string fileName, bool switchToNewTab = true, string filePath = null)
        {
            _tabControl = tabControl;
            _fileName = fileName;
            _filePath = filePath;

            _advandedTextBox = new AdvancedTextBox();
            _advandedTextBox.Dock = DockStyle.Fill;

            _tabPage = new TabPage();
            _tabPage.Text = _fileName;
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

        public string TabTitle
        {
            get
            {
                return _tabPage.Text;
            }
            set
            {
                _tabPage.Text = value;
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
                Saved = true;
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

                Saved = true;
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
                _tabPage.Text = _fileName;
                _tabPage.ToolTipText = _fileName;

                _tabControl.Refresh();

                _advandedTextBox.Lines = File.ReadAllLines(_filePath);
                _advandedTextBox.Select(_advandedTextBox.TextLength, 0);
                Focus();

                Saved = true;
                return true;
            }
            else return false;
        }
    }
}
