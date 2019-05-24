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

        private void Runtime_Tick(object sender, EventArgs e)
        {
            try
            {
                TextLengthLabel.Text = "Length: " + notepadPages[TabbedNotepad.SelectedIndex].Text.Length;
                LineTotalLabel.Text = "Lines: " + notepadPages[TabbedNotepad.SelectedIndex].Lines.Length;
            }
            catch
            {
                TextLengthLabel.Text = "Length: 0";
                LineTotalLabel.Text = "Lines: 0";
            }
            
            if (notepadPages[TabbedNotepad.SelectedIndex].Saved) SavedLabel.Text = "Saved: True";
            else SavedLabel.Text = "Saved: False";
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
                    //Check if saved
                    this.notepadPages.RemoveAt(i);
                    this.TabbedNotepad.TabPages.RemoveAt(i);
                    break;
                }
            }
        }

        private void TabbedNotepad_TabIndexChanged(object sender, EventArgs e)
        {
            Runtime_Tick(sender, e);
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
        }

        public bool Save()
        {
            if (!String.IsNullOrEmpty(_filePath))
            {
                File.WriteAllLines(_filePath, _advandedTextBox.Lines);
                _advandedTextBox.isTextSaved = true;
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

                _advandedTextBox.isTextSaved = true;
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

                return true;
            }
            else return false;
        }
    }
}
