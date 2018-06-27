using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NN_Less3.NN;

namespace NN_Less3
{
    public partial class Form2 : Form
    {
        private int comm;
        public Form2(int c)
        {
            InitializeComponent();
            comm = c;
            if (comm == 1)//ввод размеров скрытых слоев
            {
                int count = Information.hiddenSize;
                label2.Text += " " + count.ToString();
            }
            if (comm == 2)//ввод входных сигналов для работы
            {
                label1.Text = "Введите через пробел входные сигналы";
                label2.Text = "Их количество: " + Information.inputSize;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] strArr = richTextBox1.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (comm == 1)
            {
                Information.countOnHiddenLayers.Clear();
                foreach (string str in strArr)
                {
                    int size = 0;
                    if (!int.TryParse(str, out size))
                    {
                        MessageBox.Show("bad argument");
                        return;
                    }
                    Information.countOnHiddenLayers.Add(size);
                }
                if (Information.hiddenSize != Information.countOnHiddenLayers.Count)
                {
                    MessageBox.Show("bad hiddenSize");                   
                    return;
                }
                GroupBox mainGB = (GroupBox)Owner.Controls["mainConfig"];
                mainGB.Controls["runButton"].Visible = true;
            }
            if (comm == 2)
            {
                Information.inputSignals.Clear();
                foreach (string str in strArr)
                {
                    int size = 0;
                    if (!int.TryParse(str, out size))
                    {
                        MessageBox.Show("bad argument");
                        return;
                    }
                    Information.inputSignals.Add(size);
                }
                if (Information.inputSize != Information.inputSignals.Count)
                {
                    MessageBox.Show("bad inputSize");
                    return;
                }
                if (Information.NN.IsBiasNeuron) Information.inputSignals.Add(1);
                GroupBox mainGB = (GroupBox)Owner.Controls["mainConfig"];
                mainGB.Controls["runButton"].Visible = true;
            }
            this.Close();
        }
    }
}
