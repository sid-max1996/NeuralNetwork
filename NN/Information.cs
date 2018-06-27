using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_Less3.NN
{
    class Information
    {
        static public int hiddenSize = -1;
        static public int inputSize = -1;
        static public List<int> countOnHiddenLayers = new List<int>();
        static public List<float> inputSignals = new List<float>();
        static public NeuralNetwork NN = null;
        static public Form1 form1;
        static public List<string> pattNames = new List<string>();
        static public List<string> answNames = new List<string>();
        static public bool isMnistLearn = false;
        static public bool isFormatLearn = false;
        static public string FormatFilePath = "";
        static public int indPic = 0;
        static public string[] mnistStrs = new string[60000];
    }
}
