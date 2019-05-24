using SimpleNotepad.CustomControls;
using System;
using System.IO;
using System.Windows.Forms;

namespace SimpleNotepad
{
    public class NotepadPage
    {
        private TabControl _tabControl;
        private TabPage _tabPage;
        private AdvancedTextBox _advandedTextBox;

        private string _fileName, _filePath;
        private bool _switchToNewTab;

        private const int MaxTabTitleLength = 11;

        public NotepadPage(ref TabControl tabControl, string fileName, bool switchToNewTab = true, string filePath = null)
        {
            _tabControl = tabControl;
            _fileName = fileName;
            _filePath = filePath;
            _switchToNewTab = switchToNewTab;

            _advandedTextBox = new AdvancedTextBox();
            _advandedTextBox.Dock = DockStyle.Fill;

            _tabPage = new TabPage();

            if (_fileName.Length > (MaxTabTitleLength + 3))
                _tabPage.Text = _fileName.Substring(0, MaxTabTitleLength).TrimEnd(' ') + "...";
            else
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
            if (_switchToNewTab)
                return _advandedTextBox.Focus();

            return false;
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
                    _tabPage.Text = value.Substring(0, MaxTabTitleLength).TrimEnd(' ') + "...";
                else
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
                return _advandedTextBox.Saved;
            }
            set
            {
                _advandedTextBox.Saved = value;
            }
        }

        public bool Save()
        {
            if (!String.IsNullOrEmpty(_filePath))
            {
                File.WriteAllLines(_filePath, _advandedTextBox.Lines);

                _advandedTextBox.CreateSnapshot();
                Saved = _advandedTextBox.DoesCurrentTextEqualSnapshot();

                return true;
            }
            else return SaveAs();
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

                _advandedTextBox.CreateSnapshot();
                Saved = _advandedTextBox.DoesCurrentTextEqualSnapshot();

                return true;
            }
            return false;
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
                _advandedTextBox.CreateSnapshot();
                Focus();

                Saved = _advandedTextBox.DoesCurrentTextEqualSnapshot();
                return true;
            }
            return false;
        }
    }
}
