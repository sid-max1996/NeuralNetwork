using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_Less3.NN
{
    class FormConsole
    {
        public static System.Windows.Forms.RichTextBox console =
              (System.Windows.Forms.RichTextBox)Information.form1.Controls["console"];
        public static String printBuffer = "";

        public static void Refresh()
        {
            Information.form1.Refresh();
        }

        public static void Println(string text)
        {
            console.AppendText(text+"\r\n");
            Refresh();
        }

        public static void PrintlnAndScroll(string text)
        {
            console.AppendText(text + "\r\n");
            Scroll();
            Refresh();
        }

        public static void Scroll()
        {
            console.SelectionStart = console.TextLength;
            console.ScrollToCaret();
        }

        public static void ClearBuffer(string str)
        {
            printBuffer = "";
        }

        public static void AddBuffer(string str)
        {
            printBuffer += str;
        }

        public static void PrintBuffer()
        {
            console.AppendText(printBuffer);
            Refresh();
        }

        public static void PrintBufferAndScroll()
        {
            console.AppendText(printBuffer);
            Scroll();
            Refresh();
        }

        public static void AppendText(string str)
        {
            console.AppendText(str);
        }

    }
}
