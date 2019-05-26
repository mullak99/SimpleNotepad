using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SimpleNotepad.CustomControls
{
    public partial class AdvancedTextBox : UserControl
    {
        private string _snapshotMD5 = "";

        public AdvancedTextBox()
        {
            InitializeComponent();

            LineNumbers.Font = MainTextBox.Font;
            MainTextBox.Select();
            UpdateLineNumbers();
            CreateSnapshot();
        }

        #region Designer Vars

        [Description("The lines of text"), Category("Appearance"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        public string[] Lines
        {
            get { return MainTextBox.Lines; }
            set { MainTextBox.Lines = value; }
        }

        [Description("The text of the textbox"), Category("Appearance"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return MainTextBox.Text; }
            set { MainTextBox.Text = value; }
        }

        [Description("The color of the text"), Category("Appearance"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        public override Color ForeColor
        {
            get { return MainTextBox.ForeColor; }
            set { MainTextBox.ForeColor = value; }
        }

        [Description("The background color"), Category("Appearance"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        public override Color BackColor
        {
            get { return MainTextBox.BackColor; }
            set { MainTextBox.BackColor = value; }
        }

        [Description("The color of the line number text"), Category("Appearance"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        public Color LineNumberForeColor
        {
            get { return LineNumbers.ForeColor; }
            set { LineNumbers.ForeColor = value; }
        }

        [Description("The background color of the line number textbox"), Category("Appearance"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        public Color LineNumberBackColor
        {
            get { return LineNumbers.BackColor; }
            set { LineNumbers.BackColor = value; }
        }

        [Description("The font used to display text in the control"), Category("Appearance"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        public Font GlobalFont
        {
            get { return MainTextBox.Font; }
            set
            {
                MainTextBox.Font = value;
                LineNumbers.Font = value;
            }
        }

        #endregion
        #region Public Methods
        public void CreateSnapshot()
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(MainTextBox.Text));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                _snapshotMD5 = builder.ToString();
            }
        }

        public bool DoesCurrentTextEqualSnapshot()
        {
            string current_MD5 = "";
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(MainTextBox.Text));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                current_MD5 = builder.ToString();
            }

            return (current_MD5 == _snapshotMD5);
        }

        public bool Saved { get; set; } = true;

        public void Select(int start, int length)
        {
            MainTextBox.Select(start, length);
        }

        public void Undo()
        {
            MainTextBox.Undo();
        }

        public void Redo()
        {
            MainTextBox.Redo();
        }

        public int TextLength
        {
            get
            {
                return MainTextBox.TextLength;
            }
        }

        #endregion
        #region Private Methods

        private int GetWidth()
        {
            int w = 25;
            int line = MainTextBox.Lines.Length;

            if (line <= 99)
                w = 20 + (int)MainTextBox.Font.Size;
            else if (line <= 999)
                w = 30 + (int)MainTextBox.Font.Size;
            else
                w = 50 + (int)MainTextBox.Font.Size;

            return w;
        }

        private void UpdateLineNumbers()
        {
            Point pt = new Point(0, 0);

            int firstIndex = MainTextBox.GetCharIndexFromPosition(pt);
            int firstLine = MainTextBox.GetLineFromCharIndex(firstIndex);

            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;

            int lastIndex = MainTextBox.GetCharIndexFromPosition(pt);
            int lastLine = MainTextBox.GetLineFromCharIndex(lastIndex);

            LineNumbers.SelectionAlignment = HorizontalAlignment.Center;
            LineNumbers.Text = "";
            LineNumbers.Width = GetWidth();

            if (MainTextBox.Lines.Length > 1)
            {
                for (int i = firstLine; i <= lastLine + 1; i++)
                {
                    LineNumbers.Text += (i + 1) + "\n";
                }
            }
            else LineNumbers.Text += 1 + "\n";
        }

        #endregion
        #region Events

        private void LineNumbers_MouseEnter(object sender, EventArgs e)
        {
            ActiveControl = MainTextBox;
        }

        private void LineNumbers_Enter(object sender, EventArgs e)
        {
            ActiveControl = MainTextBox;
        }

        private void AdvancedTextBox_Resize(object sender, EventArgs e)
        {
            UpdateLineNumbers();
        }

        private void MainTextBox_VScroll(object sender, EventArgs e)
        {
            LineNumbers.Text = "";
            UpdateLineNumbers();
            LineNumbers.Invalidate();
        }

        private void MainTextBox_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = MainTextBox.GetPositionFromCharIndex(MainTextBox.SelectionStart);

            if (pt.X == 1) UpdateLineNumbers();
        }

        private void MainTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateLineNumbers();

            Saved = DoesCurrentTextEqualSnapshot();
        }

        private void MainTextBox_FontChanged(object sender, EventArgs e)
        {
            LineNumbers.Font = MainTextBox.Font;
            MainTextBox.Select();
            UpdateLineNumbers();
        }

        private void MainTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            MainTextBox.Select();
            LineNumbers.DeselectAll();
        }

        private void MainTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Tab)
            {
                this.SuspendLayout();
                rtb.Undo();
                rtb.Redo();
                this.ResumeLayout();
            }
        }

        #endregion
    }
}
