using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_Less3.NN
{
    class ErrorSet
    {
        static public float MSE(List<float> ideal, List<float> cur)
        {
            float err = 0;
            for(int i = 0; i < ideal.Count; i++)
                err += (float)Math.Pow((ideal[i] - cur[i]), 2);
            int n = ideal.Count == 1 ? 2 : ideal.Count;
            err = err / n;
            return err;
        }


        static public float RootMSE(List<float> ideal, List<float> cur)
        {
            int n = ideal.Count;
            return (float)Math.Sqrt(MSE(ideal, cur));
        }

    }
}
