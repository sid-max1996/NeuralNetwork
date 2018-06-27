using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_Less3.NN
{
    //в классе MemoryStream нет полей WriteTimeout ReadTimeout, поэтому выдаёт исключение
    public class MStream : MemoryStream
    {
        public override int WriteTimeout { get; set; }
        public override int ReadTimeout { get; set; }

        public MStream(byte[] b) : base(b) { }

        public MStream() : base() { }
    }

    public class ImageConvert
    {
        //конвертирует массив байт в Bitmap
        private static Bitmap ConvertToBitmap(byte[] imagesSource)
        {
            var imageConverter = new ImageConverter();
            var image = (Image)imageConverter.ConvertFrom(imagesSource);
            return new Bitmap(image);
        }

        //конвертирует массив байт в изображение(Image)
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MStream ms = new MStream(byteArrayIn);
            Bitmap b = new Bitmap(ms);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        //конвертирует Image в массив байт
        private static byte[] ImageToByteArray(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        //конвертирует Bitmap в Image
        private static Image BitmapToImage(Bitmap map)
        {
            Stream imageStream = new MStream();
            map.Save(imageStream, ImageFormat.Bmp);
            return Image.FromStream(imageStream);
        }

        //изменяет размер bitmap
        private static Bitmap ResizeBitmap(Bitmap bitmap, int width, int height)
        {
            Bitmap resizedBitmap = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(resizedBitmap))
            {
                gfx.DrawImage(bitmap, new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
            }
            return resizedBitmap;
        }//конец ResizeBitmap

        //меняет размер изображения представленного byte[]
        public static byte[] GetNormalizedImage(byte[][] img, int oldWidth, int oldHeight, int newWidth, int newHeight, int label = 0)
        {

            Bitmap oldBitmap = new Bitmap(oldWidth, oldHeight);
            //byte[] oldVec = new byte[28*28];          
            for (int y = 0; y < oldHeight; y++)
            {
                for (int x = 0; x < oldWidth; x++)
                {
                    //oldVec[y * 28 + x] = img[y][x];
                    int avg = img[y][x];
                    oldBitmap.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                }
            }
            /*Image im28 = BitmapToImage(oldBitmap);
            im28.Save("im28//" + label + "image" + Information.indPic + ".bmp");*/
            /*string oldVecStr = label.ToString();
            foreach (var el in oldVec)
                oldVecStr += "," + el;
            Information.mnistStrs[Information.indPic++] = oldVecStr;*/
            //Bitmap newBitmap16 = ResizeBitmap(oldBitmap, 16, 16);


            /*Image im = BitmapToImage(newBitmap);
            im.Save("im//"+label+"image"+Information.indPic + ".bmp");*/
            //string strFileWrite = "1 64\r\n";

            Bitmap newBitmap = ResizeBitmap(oldBitmap, newWidth, newHeight);
            byte[] res = new byte[newWidth*newHeight];
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    Color c = newBitmap.GetPixel(x, y);
                    res[y * newWidth + x] = (byte)((c.R + c.G + c.B) / 3);
                }
            }
            /*string path = "vec//" + label + "vec" + Information.indPic++ + ".txt";
            foreach(var el in res)
                strFileWrite += el + " ";
            File.WriteAllLines(path, new string[] { strFileWrite });*/
            return res;
        }//конец GetNormalizedImage

    }
}
