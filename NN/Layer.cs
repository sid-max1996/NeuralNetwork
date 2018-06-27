using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NN_Less3.NN
{
    delegate float floatFun(float arg);
    class Layer
    {
        private List<float> weights = new List<float>();
        //private List<float> deltaWeights = new List<float>();
        private List<float> outputs = new List<float>();
        private floatFun activationFun;
        private int sizeNeurons;
        private int sizeWeights;

        //конструктор слоя
        public Layer(int size, int sizePrev, floatFun actFun, float a = -0.8f, float b = 0.8f)
        {
            sizeNeurons = size;
            outputs = new List<float>(sizeNeurons);
            activationFun = actFun;
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            Random rand = new Random(DateTime.Now.Millisecond);
            if (sizePrev > 0)
            {
                sizeWeights = size*sizePrev;
                weights = new List<float>(sizeWeights);
                //deltaWeights = new List<float>(sizeWeights);
                for (int i = 0; i < sizeWeights; i++)
                {
                    weights.Add(a + (float)rand.NextDouble() * (b - a));
                    //deltaWeights.Add(0);
                }
            }
        }

        public string GetActivationFunctionText()
        {
            string s = "non";
            if (activationFun == ActivationFun.linearFunction) s = "linear";
            if (activationFun == ActivationFun.sigmoidFunction) s = "sigmoid";
            if (activationFun == ActivationFun.tangentFunction) s = "tangent";
            return s;
        }

        public List<string> GetText(int linksCount = -1)
        {
            List<string> layerText = new List<string>(sizeNeurons);
            int ind = 0;
            for (int i = 0; i < sizeNeurons; i++)
            {
                string curText = "";
                if (linksCount != -1)
                {
                    for (int j = 0; j < linksCount; j++)
                        curText += weights[ind++] + " ";
                }          
                layerText.Add(curText);
            }
            return layerText;
        }

        public int SizeNeurons
        {
            get { return sizeNeurons; }
        }

        public int SizeWeights
        {
            get { return sizeWeights; }
        }

        //public List<float> DeltaWeights { get { return deltaWeights; } set { deltaWeights = value; } }
        public List<float> Weights { get { return weights; } set { weights = value; } }
        public List<float> Outputs { get { return outputs; } set { outputs = value; } }

        public float ActivationFunction(float arg)
        {
            return activationFun.Invoke(arg);
        }

        public void setActivationFunction(floatFun fun)
        {
            activationFun = fun;
        }

    }
}
