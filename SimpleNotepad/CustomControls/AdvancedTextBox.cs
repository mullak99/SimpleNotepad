using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleNotepad.CustomControls
{
    public partial class AdvancedTextBox : UserControl
    {
        public AdvancedTextBox()
        {
            InitializeComponent();

            LineNumbers.Font = MainTextBox.Font;
            MainTextBox.Select();
            UpdateLineNumbers();
        }

        public bool isTextSaved = false;

        [Description("The lines of text"), Category("Appearance"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        public string[] Lines
        {
            get
            {
                return MainTextBox.Lines;
            }
            set
            {
                MainTextBox.Lines = value;
            }
        }

        [Description("The text of the textbox"), Category("Appearance"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get
            {
                return MainTextBox.Text;
            }
            set
            {
                MainTextBox.Text = value;
            }
        }

        public void Select(int start, int length)
        {
            MainTextBox.Select(start, length);
        }

        public int TextLength
        {
            get
            {
                return MainTextBox.TextLength;
            }
        }

        public int GetWidth()
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

        public void UpdateLineNumbers()
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
            if (MainTextBox.Text == "") UpdateLineNumbers();

            isTextSaved = false;
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

    }
}
