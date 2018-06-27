using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_Less3.NN
{
    class LoadFromFile
    {
        public int n;//количество образцов
        public int m;//размерность образцов
        public int[,] mas;//образцы

        public List<List<float>> ConvertIntoList()
        {
            List<List<float>> res = new List<List<float>>();
            for(int i = 0; i < n; i++)
            {
                List<float> pattList = new List<float>();
                for (int j = 0; j < m; j++)
                {
                    pattList.Add(mas[i, j]);
                }
                res.Add(pattList);
            }
            return res;
        }

        //конструктор задаем матрицу входных сигналов из файла
        public LoadFromFile(string[] fileNames)
        {
            if (fileNames == null)
                throw new Exception("Не задано ни одного образца");
            //читаем образцы в строки
            n = fileNames.Length;
            List<string[]> pattList = new List<string[]>();
            for (int i = 0; i < n; i++)
            {
                string[] strX = File.ReadAllLines(fileNames[i]);
                pattList.Add(strX);
            }
            //читаем количество элементов в образцах
            int[] sizeM = new int[n];
            for (int i = 0; i < n; i++)
            {
                string[] strX = pattList[i];
                string[] sizeX = strX[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (sizeX.Length == 2)
                    sizeM[i] = Convert.ToInt32(sizeX[0]) * Convert.ToInt32(sizeX[1]);
                else
                    throw new Exception("Неверно задан формат размера для образца");
            }
            //смотрим что размеры совпадают
            bool isSameSize = true;
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                {
                    if (sizeM[i] != sizeM[j])
                        isSameSize = false;
                }
            //если размеры совпали заносим данные
            if (!isSameSize)
                throw new Exception("Не совпадает длина образцов");
            else
            {
                m = sizeM[0];
                mas = new int[n, m];
                for (int i = 0; i < n; i++)
                {
                    List<int> l = new List<int>();
                    string[] strX = pattList[i];
                    for (int j = 1; j < strX.Length; j++)
                    {
                        string[] X = strX[j].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int k = 0; k < X.Length; k++)
                        {
                            l.Add(Convert.ToInt32(X[k]));
                        }
                    }
                    if (m != l.Count)
                        throw new Exception("Количество элементов в образце не равно заявленному");
                    for (int k = 0; k < m; k++)
                    {
                        mas[i, k] = l[k];
                    }
                }

            }
        }
    }
    }
