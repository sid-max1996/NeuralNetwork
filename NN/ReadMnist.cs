using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace NN_Less3.NN
{
    class ReadMnist
    {
        static public List<DigitImage> trainDigits;//на которых учимся
        static public List<DigitImage> testDigits;//на которых проверяем

        public List<DigitImage> TrainDigits { get { return trainDigits; }}
        public List<DigitImage> TestDigits { get { return testDigits; } }

        public ReadMnist(bool isNeedTrain, bool isNeedTest)
        {            
            FormConsole.PrintlnAndScroll("Read Mnist Start....");
            if (isNeedTrain)
            {
                string trainImagesPath = @"mnist\train-images.idx3-ubyte";
                string trainLabelsPath = @"mnist\train-labels.idx1-ubyte";
                int trainPattCount = 60000/*60000*/;
                trainDigits = ReadMnistFromFiles(trainImagesPath, trainLabelsPath, trainPattCount, 28, 28);
                FormConsole.PrintlnAndScroll("Read trainDigits Finish");
                Thread.Sleep(TimeSpan.FromMilliseconds(2000));
            }
            if (isNeedTest)
            {
                string testImagesPath = @"mnist\t10k-images.idx3-ubyte";
                string testLabelsPath = @"mnist\t10k-labels.idx1-ubyte";
                /*string testImagesPath = @"mnist\train-images.idx3-ubyte";
                string testLabelsPath = @"mnist\train-labels.idx1-ubyte";*/
                int testPattCount = 10000/*10000*/;
                testDigits = ReadMnistFromFiles(testImagesPath, testLabelsPath, testPattCount, 28, 28);
                FormConsole.PrintlnAndScroll("Read testDigits Finish");
                Thread.Sleep(TimeSpan.FromMilliseconds(2000));
            }
            FormConsole.PrintlnAndScroll("Read Mnist Finish");
            Thread.Sleep(TimeSpan.FromMilliseconds(2000));
        }

        static public List<DigitImage> ReadMnistFromFiles(string images, string labels,
            int pattCount, int imSize1, int imSize2)
        {
            List<DigitImage> res = new List<DigitImage>(pattCount);
            System.Windows.Forms.RichTextBox console =
              (System.Windows.Forms.RichTextBox)Information.form1.Controls["console"];
            try
            {
                FileStream ifsImages =
                    new FileStream(images, FileMode.Open); // images
                FileStream ifsLabels =
                    new FileStream(labels, FileMode.Open); // labels

                BinaryReader brLabels = new BinaryReader(ifsLabels);
                BinaryReader brImages = new BinaryReader(ifsImages);

                #region ReadInfoAboutFile
                int magic1 = brImages.ReadInt32(); // discard
                int numImages = brImages.ReadInt32();
                int numRows = brImages.ReadInt32();
                int numCols = brImages.ReadInt32();
                int magic2 = brLabels.ReadInt32();
                int numLabels = brLabels.ReadInt32();
                #endregion ReadInfoAboutFile

                byte[][] pixels = new byte[imSize1][];
                for (int i = 0; i < pixels.Length; ++i)
                    pixels[i] = new byte[imSize2];

                for (int di = 0; di < pattCount; ++di)
                {
                    for (int i = 0; i < imSize1; ++i)
                    {
                        for (int j = 0; j < imSize2; ++j)
                        {
                            byte b = brImages.ReadByte();
                            pixels[i][j] = b;
                        }
                    }

                    byte lbl = brLabels.ReadByte();

                    DigitImage dImage = new DigitImage(pixels, lbl, 
                        new Pair<int>(imSize1, imSize2));
                    res.Add(dImage);
                } // each image

                ifsImages.Close();
                brImages.Close();
                ifsLabels.Close();
                brLabels.Close();

            }
            catch (Exception ex)
            {
                console.AppendText(ex.Message);
                Information.form1.Refresh(); 
            }
            return res;
        }
    }

    public class DigitImage
    {
        public byte[] pixels;
        public byte label;
        int width;
        int height;

       public List<float> floatPixels { get{ return Array.ConvertAll(pixels, x => 
            (float)x).ToList<float>(); } }

        public DigitImage(byte[][] pixelsMatr,
          byte label, Pair<int> sizes)
        {
            /*width = sizes.first;
            height = sizes.second;
            this.pixels = new byte [width*height];
            for (int i = 0; i < height; ++i)
                for (int j = 0; j < width; ++j)
                    pixels[i*width+j] = pixelsMatr[i][j];*/
           
            width = 14; height = 14;
            this.pixels = ImageConvert.GetNormalizedImage(pixelsMatr, sizes.first, 
                sizes.second, width, height, label);
            this.label = label;
        }

        //вывод цифры ее представления
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if (this.pixels[i * width + j] == 0)
                        s += " "; // white
                    else if (this.pixels[i * width + j] == 255)
                        s += "O"; // black
                    else
                        s += "."; // gray
                }
                s += "\n";
            }
            s += "count = " + this.label.ToString() + "\r\n";
            return s;
        } // ToString
    }
}
