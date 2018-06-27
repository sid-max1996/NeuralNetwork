using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NN_Less3.NN;
using System.Collections.Generic;

namespace NN_Less3
{
    public partial class Form3 : Form
    {
        private string dirName = Directory.GetCurrentDirectory();
        static Bitmap fonImage;
        private Graphics g1;
        private OpenFileDialog openFileDialogFon;
        private bool needDrawing = false;
        private int width;
        private int height;

        public Form3(int width = 14, int height = 14)
        {
            InitializeComponent();
            this.width = width; this.height = height;
            fonImage = new Bitmap(Image.FromFile(dirName + @"\form3\clear.png"));
            fonImage = new Bitmap(fonImage, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = fonImage;
            g1 = Graphics.FromImage(pictureBox1.Image);
            pictureBox1.Invalidate();
        }

        //open
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialogFon = new OpenFileDialog();
            openFileDialogFon.Filter = "images|*.png;*.jpg,*.bmp";
            openFileDialogFon.InitialDirectory = Directory.GetCurrentDirectory() + @"\form3";
            if (openFileDialogFon.ShowDialog() == DialogResult.OK)
            {
                fonImage = new Bitmap(Image.FromFile(openFileDialogFon.FileName));
                fonImage = new Bitmap(fonImage, pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = fonImage;
                g1 = Graphics.FromImage(pictureBox1.Image);
                pictureBox1.Invalidate();
            }
        }

        //очистить
        private void button4_Click(object sender, EventArgs e)
        {
            fonImage = new Bitmap(Image.FromFile(dirName + @"\form3\clear.png"));
            fonImage = new Bitmap(fonImage, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = fonImage;
            g1 = Graphics.FromImage(pictureBox1.Image);
            pictureBox1.Invalidate();
        }

        private int borderWidth = 30;

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (g1 == null) return;
            if (!needDrawing) return;
            var x = e.X;
            var y = e.Y;
            g1.FillRectangle(Brushes.White, x, y, borderWidth, borderWidth);
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (null == g1) return;
            needDrawing = true;
            var x = e.X;
            var y = e.Y;
            g1.FillRectangle(Brushes.White, x, y, borderWidth, borderWidth);
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            needDrawing = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int fileNum = 0;
            string filePath;
            do
            {
                filePath = "drawIm" + fileNum + ".png";
                fileNum++;
            }
            while (File.Exists(dirName + "/form3/" + filePath) && fileNum != 100);
            if (fileNum == 100)
            {
                printErrorMessage("Сработало ограничение на количество файлов для записи = 100");
                return;
            }
            Image imSave = pictureBox1.Image;
            imSave.Save(dirName+"/form3/"+filePath);
            printMessage(filePath + " сохранено");
        }

        //всплывающее окно для ошибок
        private void printErrorMessage(string Text)
        {
            MessageBox.Show(Text, "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }

        //информационные сообщения
        private void printMessage(string Text)
        {
            MessageBox.Show(Text, "Информация",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int w =pictureBox1.Width;
            int h = pictureBox1.Height;
            if (Information.NN != null)
            {
                byte[][] pixels = new byte[h][];
                for (int i = 0; i < pixels.Length; ++i)
                    pixels[i] = new byte[w];
                Image im = pictureBox1.Image;
                Bitmap bm = new Bitmap(im, w, h);
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        Color c = bm.GetPixel(x, y);
                        pixels[y][x] = (byte)((c.R + c.G + c.B) / 3);                
                    }
                }
                byte[] res = ImageConvert.GetNormalizedImage(pixels, w, h, this.width, this.height);
                Information.inputSignals.Clear();
                Information.inputSignals = new List<float>(res.Length);

                string path = dirName + @"/form3/counts/семь/семь" + Information.indPic++ + ".txt";
                string strFileWrite = "";
                foreach (byte b in res)
                {
                    Information.inputSignals.Add(b);
                    strFileWrite += b + " ";
                }
                if (Information.NN.IsBiasNeuron)
                    Information.inputSignals.Add(1);
                //File.WriteAllLines(path, new string[] { strFileWrite });
                //Form1.consoleRunNN(false);
                //this.Close();
                List<float> output = Information.NN.Run(Information.inputSignals);
                string outputStr = "";
                float max = float.MinValue;
                int maxInd = -1;
                for (int i = 0; i < output.Count; i++)
                {
                    float signal = (float)Math.Round(output[i], 2);
                    outputStr += i + ") " + signal.ToString() + " ";
                    if (max < signal) { max = signal; maxInd = i - 1; }
                }
                this.textBoxOutput.Text = outputStr;
                this.textBoxAnswer.Text = (maxInd+1).ToString();
            }
            else
                printErrorMessage("NN не задана!!!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Information.NN != null && Information.inputSignals != null)
            {
                string rightAnswerStr = this.textBoxLearn.Text;
                int rightAnswer = -1;
                int.TryParse(rightAnswerStr, out rightAnswer);
                List<float> output = Information.NN.Run(Information.inputSignals);
                string pattStr = rightAnswer.ToString();
                foreach (float el in Information.inputSignals) { pattStr += ","+el;}
                string path = Directory.GetCurrentDirectory() + @"/pattCounts.txt";
                File.AppendAllLines(path, new string[] { pattStr });
                List<float> ideal = new List<float>(10);
                /*string filePath = Information.FormatFilePath;
                int posLastSlesh = filePath.LastIndexOf('\\');
                string answersPath = filePath.Substring(0, posLastSlesh);
                answersPath += "\\answers.txt";*/
                string answerPath = Directory.GetCurrentDirectory() 
                    + @"\formatData\counts\answers.txt";
                string[] lines = File.ReadAllLines(answerPath);
                string line = lines[rightAnswer].Substring(2);
                string[] signalsStr = line.Split(',');
                foreach(var s in signalsStr)
                    { float el; float.TryParse(s, out el); ideal.Add(el);}
                Information.NN.ChangeWeights(ideal, output);
                this.textBoxLearn.Text = "";
            }
            else printErrorMessage("NN или inputSignals не задана!!!");
        }
    }
}
