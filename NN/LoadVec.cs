using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_Less3.NN
{
    class LoadVec
    {
        public static Dictionary<string, List<float>> LoadFloatVecWithKey
            (string path, char sep = ',')
        {
            Dictionary<string,List<float>> res = new Dictionary<string, List<float>>();
            string[] readText = File.ReadAllLines(path);
            foreach (string s in readText)
            {
                List<float> curVec = new List<float>();
                string[] elemsStr = s.Split(new char[] { sep });
                string key = elemsStr[0];
                for (int i = 1; i < elemsStr.Length; i++)
                {
                    float floatEl;
                    bool ok = float.TryParse(elemsStr[i], out floatEl);
                    if (ok) curVec.Add(floatEl);
                }
                res.Add(key, curVec);
            }
            return res;
        }

    }
}
