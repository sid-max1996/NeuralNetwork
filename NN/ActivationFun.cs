using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_Less3.NN
{
    class ActivationFun
    {
        public static float linearFunction(float arg)
        {
            return arg;
        }

        public static float sigmoidFunction(float arg)
        {
            return (float)Math.Pow(1 + Math.Exp(-1*arg), -1);
        }

        public static float tangentFunction(float arg)
        {
            return ((float)Math.Exp(2 * arg) - 1) / (float)(Math.Exp(2 * arg) + 1);
        }
    }
}
