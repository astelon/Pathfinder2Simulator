using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinUICombatSimulation
{
    public class Observer : PF2.IObserver
    {
        RichTextBox textBox;
        public Observer(RichTextBox tb)
        {
            textBox = tb;
        }
        public void Message(string message)
        {
            textBox.AppendText(message + Environment.NewLine);
        }
        public void Message(string message, Color color)
        {
            textBox.SelectionStart = textBox.TextLength;
            textBox.SelectionLength = 0;
            textBox.SelectionColor = color;
            textBox.AppendText(message);
            textBox.SelectionColor = textBox.ForeColor;
        }
    }
}