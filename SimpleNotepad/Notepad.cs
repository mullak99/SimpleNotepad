using SimpleNotepad.CustomControls;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SimpleNotepad
{
    public class NotepadPage
    {
        private TabControl _tabControl;
        private TabPage _tabPage;
        private AdvancedTextBox _advandedTextBox;
        private Encoding _textEncoding = Encoding.UTF8;
        private Theme _theme = null;

        private string _fileName, _filePath;
        private bool _switchToNewTab;

        private const int MaxTabTitleLength = 11;

        public NotepadPage(ref TabControl tabControl, string fileName, Font font = null, Theme theme = null, bool switchToNewTab = true, string filePath = null)
        {
            _tabControl = tabControl;
            _fileName = fileName;
            _filePath = filePath;
            _switchToNewTab = switchToNewTab;
            _theme = theme;

            _advandedTextBox = new AdvancedTextBox();
            _advandedTextBox.Dock = DockStyle.Fill;

            if (font != null) _advandedTextBox.Font = font;

            _tabPage = new TabPage();

            if (_fileName.Length > (MaxTabTitleLength + 3))
                _tabPage.Text = _fileName.Substring(0, MaxTabTitleLength).TrimEnd(' ', '.') + "...";
            else
                _tabPage.Text = _fileName;

            if (_filePath != null)
                _tabPage.ToolTipText = _filePath;
            else
                _tabPage.ToolTipText = _fileName;

            if (theme != null) ApplyTheme();

            _tabPage.Controls.Add(_advandedTextBox);

            _tabControl.Controls.Add(_tabPage);
            _tabControl.SelectedTab = _tabPage;
            _tabControl.Refresh();

            Focus();
        }

        public void ApplyTheme()
        {
            if (_theme != null)
            {
                _tabPage.ForeColor = _theme.StandardTextColor;
                _tabPage.BackColor = _theme.StandardBackgroundColor;

                _advandedTextBox.ForeColor = _theme.StandardTextColor;
                _advandedTextBox.BackColor = _theme.StandardBackgroundColor;

                _advandedTextBox.LineNumberForeColor = _theme.AltTextColor;
                _advandedTextBox.LineNumberBackColor = _theme.AltBackgroundColor;
            }
            else ResetTheme();
        }

        public void ResetTheme()
        {
            _tabPage.ForeColor = SystemColors.WindowText;
            _tabPage.BackColor = SystemColors.Window;

            _advandedTextBox.ForeColor = SystemColors.WindowText;
            _advandedTextBox.BackColor = SystemColors.Window;

            _advandedTextBox.LineNumberForeColor = SystemColors.ControlDarkDark;
            _advandedTextBox.LineNumberBackColor = SystemColors.Control;
        }

        public bool Focus()
        {
            if (_switchToNewTab) return _advandedTextBox.Focus();

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

        public Theme Theme
        {
            get { return _theme; }
            set { _theme = value; }
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public string FilePath
        {
            get { return _filePath; }
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
                    _tabPage.Text = value.Substring(0, MaxTabTitleLength).TrimEnd(' ', '.') + "...";
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

        public Font Font
        {
            get { return _advandedTextBox.GlobalFont; }
            set { _advandedTextBox.GlobalFont = value; }
        }

        public string Text
        {
            get { return _advandedTextBox.Text; }
            set { _advandedTextBox.Text = value; }
        }

        public string[] Lines
        {
            get { return _advandedTextBox.Lines; }
            set { _advandedTextBox.Lines = value; }
        }

        public Encoding Encoding
        {
            get { return _textEncoding; }
            set { _textEncoding = value; }
        }

        public string EncodingString
        {
            get
            {
                if (Encoding == Encoding.Default) return "ANSI";
                else if (Encoding == Encoding.ASCII) return "ASCII";
                else if (Encoding == Encoding.UTF8) return "UTF-8";
                else if (Encoding == Encoding.Unicode) return "UTF-16 (LE)";
                else if (Encoding == Encoding.BigEndianUnicode) return "UTF-16 (BE)";
                else if (Encoding == Encoding.UTF7) return "UTF-7";
                else if (Encoding == Encoding.UTF32) return "UTF-32";
                else return Encoding.WebName;
            }
        }

        public bool Saved
        {
            get { return _advandedTextBox.Saved; }
            set { _advandedTextBox.Saved = value; }
        }

        public bool Save()
        {
            if (!String.IsNullOrEmpty(_filePath))
            {
                File.WriteAllLines(_filePath, _advandedTextBox.Lines, _textEncoding);

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

                File.WriteAllLines(_filePath, _advandedTextBox.Lines, _textEncoding);

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
                TabToolTip = _filePath;

                _tabControl.Refresh();

                _advandedTextBox.Lines = File.ReadAllLines(_filePath, _textEncoding);
                _advandedTextBox.Select(_advandedTextBox.TextLength, 0);
                _advandedTextBox.CreateSnapshot();
                Focus();

                Saved = _advandedTextBox.DoesCurrentTextEqualSnapshot();
                return true;
            }
            return false;
        }
    }

    public class Theme
    {
        private Color _standardText, _standardBack, _altText, _altBack;

        public Theme(Color StandardTextColor, Color StandardBackgroundColor, Color AltTextColor, Color AltBackgroundColor)
        {
            _standardText = StandardTextColor;
            _standardBack = StandardBackgroundColor;
            _altText = AltTextColor;
            _altBack = AltBackgroundColor;
        }

        public Color StandardTextColor
        {
            get { return _standardText; }
            set { _standardText = value; }
        }

        public Color StandardBackgroundColor
        {
            get { return _standardBack; }
            set { _standardBack = value; }
        }

        public Color AltTextColor
        {
            get { return _altText; }
            set { _altText = value; }
        }

        public Color AltBackgroundColor
        {
            get { return _altBack; }
            set { _altBack = value; }
        }
    }
}
