using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_Less3.NN
{
    class CreateData
    {
        public static void GreateAnswersLetters(string dirPath)
        {
            int onePos = 0;
            string[] saveArr = new string[26];
            for (char c = 'A'; c <= 'Z'; c++)
            {
                int[] arr = new int[26];
                arr[onePos] = 1;
                string answerStr = ""+c;
                foreach (var el in arr)
                    answerStr += "," + el;
                saveArr[onePos] = answerStr;
                onePos++;
            }
            File.WriteAllLines(Directory.GetCurrentDirectory() + dirPath
                   + "answers.txt", saveArr);
        }

        public static void CreateFile()
        {
            string dirName = Directory.GetCurrentDirectory();
            /*List<string> digitNames = new List<string>{ "ноль","один","два","три","четыре","пять",
                "шесть","семь","восемь","девять" };*/
            List<string> digitNames = new List<string> { "три", "пять" };
            //List<string> digitNames = new List<string> { "квадрат", "треугольник", "круг" };
            List <List<string>> allData = new List<List<string>>();
            int[] names = new int[] { 3, 5 };
            for(int i=0; i < digitNames.Count; i++)
            {
                List<string> dataDigit = new List<string>();
                var fileArr = Directory.GetFiles(dirName+@"/form3/counts/"+digitNames[i]);
                for (int j = 0; j < fileArr.Length; j++) {
                    string line = File.ReadAllText(fileArr[j]);
                    line = names[i] + " " + line.Trim();
                    line = line.Replace(" ", ",");
                    dataDigit.Add(line);
                }
                allData.Add(dataDigit);
            }

            List<string[]> saveParts = new List<string[]>();
            int pattLen = 30;
            for (int j = 0; j < pattLen; j++)
            {
                string[] strArr = new string[allData.Count];
                for (int i = 0; i < allData.Count; i++)
                {
                    var curCountVectors = allData[i];
                    string str = curCountVectors[j];
                    strArr[i] = str;
                }
                saveParts.Add(strArr);
            }

            string[] FinalWriteArr = new string[pattLen*allData.Count];
            int ind = 0;
            foreach(var a in saveParts)
                foreach(var s in a)
                {
                    FinalWriteArr[ind++] = s;
                }
            File.WriteAllLines(dirName+"/my_tree_five.txt", FinalWriteArr);
        }

    }
}
