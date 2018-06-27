using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_Less3.NN
{
    using PatternsType = System.Collections.Generic.List<List<float>>;
    using AnswersType = System.Collections.Generic.Dictionary<string, List<float>>;
    using KeysType = System.Collections.Generic.List<string>;
    class NeuralNetwork
    {
        private Layer inputLayer;
        private List<Layer> hiddenLayers = new List<Layer>();
        private Layer outputLayer;
        private int layerSize;
        private bool isBias = false;
        private float E = 0.5f;// скорость обучуния
        private float A = 0.5f;// момент
        private int MAX_EP = 100;//эпохи
        private int DIST_PRINT = 3;//печать через
        private Pair<float> range = new Pair<float>(0, 1);

        //Вся инфа о НН
        public string AllInfo()
        {
            string allInfo = "";
            allInfo += "isBias = " + (isBias?"true":"false") + "\r\n";
            allInfo += "LearnRate = " + E + "\r\n";
            allInfo += "Moment = " + A + "\r\n";
            allInfo += "MAX_EP = " + MAX_EP + "\r\n";
            allInfo += "DIST_PRINT = " + DIST_PRINT + "\r\n";
            allInfo += "layerSize = " + layerSize + "\r\n";
            allInfo += "inputLayerSizeNeurons = " + inputLayer.SizeNeurons + "\r\n";
            //allInfo += "Act Fun = " + inputLayer.GetActivationFunctionText() + "\r\n";
            allInfo += "outputLayerSizeNeurons = " + outputLayer.SizeNeurons + "\r\n";
            //allInfo += "Act Fun = " + outputLayer.GetActivationFunctionText() + "\r\n";
            for (int i = 0; i < layerSize - 2; i++)
            {
                allInfo += "hiddenLayerSizeNeurons"+(i + 1)+" = " + hiddenLayers[i].SizeNeurons + "\r\n";
                //allInfo += "Act Fun = " + hiddenLayers[i].GetActivationFunctionText() /*+ "\r\n"*/;
            }
            //allInfo += "\r\n";
            return allInfo;
        }

        //по умолчанию
        public NeuralNetwork(int size, floatFun actFun, bool isB = false, Pair<float> r = null)
        {
            int sizeOut = size;
            if (isB) size++;
            inputLayer = new Layer(size, 0, ActivationFun.linearFunction);
            hiddenLayers.Add(new Layer(size, size, actFun));
            outputLayer = new Layer(sizeOut, size, actFun);
            if (r != null) range = r;
            layerSize = 3;
            isBias = isB;
        }

        //описание инфы НН
        public List<List<string>> GetText()
        {
            List<List<string>> textNN = new List<List<string>>(layerSize);
            textNN.Add(inputLayer.GetText());
            int linkCount = inputLayer.SizeNeurons;
            foreach (var layer in hiddenLayers) {
                textNN.Add(layer.GetText(linkCount));
                linkCount = layer.SizeNeurons;
            }
            textNN.Add(outputLayer.GetText(linkCount));
            return textNN;
        }

        //основной конструктор, размер входного слоя, размеры скрытых слоев и тд
        public NeuralNetwork(int inSize, List<int> hiddSizes,
           int outSize, floatFun actFun, bool isB = false, Pair<float> r = null)
        {
            if (isB) inSize++;
            inputLayer = new Layer(inSize, 0, ActivationFun.linearFunction);
            int prevSize = inSize;
            foreach (int size in hiddSizes)
            {
                int sizeLayer = size;
                if (isB) sizeLayer++; 
                hiddenLayers.Add(new Layer(sizeLayer, prevSize, actFun));               
                prevSize = sizeLayer;
            }
            outputLayer = new Layer(outSize, prevSize, actFun);
            if (r != null) range = r;
            layerSize = 2 + hiddenLayers.Count;
            isBias = isB;
        }

        //конструктор с инициализацией весов(выгрузка из файла)
        public NeuralNetwork(int inSize, List<int> hiddSizes, int outSize,
            floatFun actFun, List<List<float>> weights, bool isB = false)
        {
            isBias = isB;
            inputLayer = new Layer(inSize, 0, ActivationFun.linearFunction);
            int weightIndex = 1;
            int prevSize = inSize;
            foreach (int sizeLayer in hiddSizes)
            {
                hiddenLayers.Add(new Layer(sizeLayer, prevSize, actFun));
                prevSize = sizeLayer;
                hiddenLayers[weightIndex - 1].Weights = weights[weightIndex];
                weightIndex++;
            }
            outputLayer = new Layer(outSize, prevSize, actFun);
            outputLayer.Weights = weights[weightIndex];
            layerSize = 2 + hiddenLayers.Count;            
        }

        //выходной сигнал слоя next при входном сигнале input
        private List<float> TransferNextLayer(List<float> input, Layer next, bool isBiasNeuron)
        {
            List<float> output = new List<float>(next.SizeNeurons);
            int addInd = input.Count;
            for(int i = 0; i < next.SizeNeurons; i++)
            {
                float sum = 0;
                int lastInputInd = input.Count - 1;
                for (int j = 0; j < lastInputInd; j++)
                {
                    float signal = input[j];
                    sum += signal * next.Weights[i*addInd+j];
                }
                if (!isBiasNeuron)
                    sum += input[lastInputInd] * next.Weights[i*addInd + lastInputInd];
                else
                    sum += 1 * next.Weights[i*addInd + lastInputInd];
                output.Add(next.ActivationFunction(sum));
            }
           return output;

        }

        //Работа Нейронной сети, возвращает выход по входу
        public List<float> Run(List<float> input, bool isBiasNeuron = false)
        {
            inputLayer.Outputs = input;
            List<float> curInput = input;
            if (input.Count != inputLayer.SizeNeurons)
                throw new Exception("input.Count != inputLayer.SizeNeurons");
            foreach(var layer in hiddenLayers)
            {
                curInput = TransferNextLayer(curInput, layer, isBiasNeuron);
                if (IsBiasNeuron) curInput[curInput.Count - 1] = 1;
                layer.Outputs = curInput;
            }
            curInput = TransferNextLayer(curInput, outputLayer, isBiasNeuron);
            outputLayer.Outputs = curInput;
            return curInput;
        }

        //изменение весов алгоритм МОР
        public void ChangeWeights(List<float> ideal, List<float> output)
        {
            Layer nextLayer = outputLayer;
            List<float> sigmaNext = new List<float>(ideal.Count);
            float sigmaLeft = 0;
            float fin = 0;
            for(int i = 0; i < ideal.Count; i++)
            {
                sigmaLeft = (ideal[i] - output[i]);
                fin = (1 - output[i]) * output[i];
                sigmaNext.Add(sigmaLeft * fin);
            }
            for (int i = hiddenLayers.Count - 1; i >= -1; i--) {
                Layer prevLayer;
                prevLayer = i != -1 ? hiddenLayers[i] : inputLayer;
                List<float> prevOutput = prevLayer.Outputs;
                float[] newWeights = new float[nextLayer.Weights.Count];
                //List<float> deltaWeights = new List<float>(nextLayer.Weights.Count);

                int ind = 0;
                for (int k = 0; k < nextLayer.SizeNeurons; k++)
                {
                    for (int j = 0; j < prevLayer.SizeNeurons; j++)
                    {
                        float deltaWeight = (sigmaNext[k] * prevOutput[j] * E)
                           /* + (A * nextLayer.DeltaWeights[ind])*/;
                        //deltaWeights.Add(deltaWeight);
                        //здесь была ошибка выражение слева(веса присваивались в неправильном порядке)
                        newWeights[ind] = 
                            nextLayer.Weights[ind] + deltaWeight;
                        ind++;
                    }
                }
                //nextLayer.DeltaWeights = deltaWeights;                              
            
                List<float> sigmaPrev = new List<float>(sigmaNext);
                sigmaNext.Clear();
                for (int m = 0; m < prevLayer.SizeNeurons; m++)
                {
                    float sum = 0;
                    for (int k = 0; k < nextLayer.SizeNeurons; k++)
                    {
                        sum += nextLayer.Weights[prevLayer.SizeNeurons * k + m] * sigmaPrev[k];
                    }
                    
                    fin = (1 - prevLayer.Outputs[m]) * prevLayer.Outputs[m]; 
                    sigmaNext.Add(fin * sum);
                }
                for (int wI = 0; wI < nextLayer.SizeWeights; wI++)
                    nextLayer.Weights[wI] = newWeights[wI];
                //nextLayer.Weights = newWeights.ToList<float>();
                nextLayer = prevLayer;
            }
            
        }

        //обучение МОР, количество образцов равно количеству ответов для них
        //(плохо что ответы повторяются, лучше использовать метки)
        public void Learning(int setCount, List<List<float>> patterns, List<List<float>> answers)
        {
            System.Windows.Forms.RichTextBox console = FormConsole.console;
            if (!(setCount == patterns.Count && setCount == answers.Count))
            {
                console.AppendText("learning sizes error\r\n");
                return;
            }
            if (IsBiasNeuron)
                for(int i=0; i < patterns.Count; i++)
                    patterns[i].Add(1);
            console.AppendText("Learning Start\r\n");
            Information.form1.Refresh();
            float epsEpErr = 0.0001f;
            float[] lastErrors = new float[3];
            for (int i = 0; i < lastErrors.Length; i++)
                lastErrors[i] = i;
            for (int ep = 0; ep < MAX_EP; ep++)
            {
                bool isStopLearning = true;
                float sumEpErr = 0;
                for (int setIter = 0; setIter < setCount; setIter++)
                {
                    List<float> input = patterns[setIter];
                    List<float> output = Run(input, IsBiasNeuron);
                    float err = ErrorSet.MSE(answers[setIter], output);
                    sumEpErr += err;
                    if (err > 0.03) isStopLearning = false;
                        
                    if (ep % DIST_PRINT == 0)
                    {
                        console.AppendText("ep = " + ep + " setIter = " + setIter
                            + " err = " + Math.Round(err, 4) + "\r\n");
                        console.SelectionStart = console.TextLength;
                        console.ScrollToCaret();
                        Information.form1.Refresh();
                    }
                    ChangeWeights(answers[setIter], output);
                }
                lastErrors[0] = lastErrors[1];
                lastErrors[1] = lastErrors[2];
                lastErrors[2] = sumEpErr / setCount;
                if (Math.Abs(lastErrors[0] - lastErrors[1]) < epsEpErr ||
                    Math.Abs(lastErrors[1] - lastErrors[2]) < epsEpErr)
                {
                    console.AppendText("exit epsEpErr\r\n");
                    console.AppendText(lastErrors[0] + "\r\n");
                    console.AppendText(lastErrors[1] + "\r\n");
                    console.AppendText(lastErrors[2] + "\r\n");
                    Information.form1.Refresh();
                    return;
                }
                if (isStopLearning)
                {
                    console.AppendText("exit isStopLearning\r\n");
                    Information.form1.Refresh();
                    return;
                }
            }
            console.AppendText("exit MAX_EP\r\n");
            Information.form1.Refresh();
        }

        public void SetActFun(floatFun dF)
        {
            foreach(var l in hiddenLayers)
            {
                l.setActivationFunction(dF);
            }
            outputLayer.setActivationFunction(dF);
        }

        public float LearnSpeed { get { return E; } set { E = value; } }
        public float LearnMoment { get { return A; } set { A = value; } }
        public int MaxEp { get { return MAX_EP; } set { MAX_EP = value; } }
        public int DistPrint { get { return DIST_PRINT; } set { DIST_PRINT = value; } }
        public bool IsBiasNeuron { get { return isBias; } }
        public int SizeLayers { get { return layerSize; } }

        //обучение для мниста такое же как и раньше только у здесь есть метки
        public void LearningMnist( ReadMnist readMnist, List<List<float>> answers)
        {
            int setCount = readMnist.TrainDigits.Count;
            System.Windows.Forms.RichTextBox console = FormConsole.console;
            console.AppendText("Learning Start\r\n");
            Information.form1.Refresh();
            float epsEpErr = 0.0000001f;
            float[] lastErrors = new float[3];
            for (int i = 0; i < lastErrors.Length; i++)
                lastErrors[i] = i;
            for (int ep = 0; ep < MAX_EP; ep++)
            {
                DateTime startEpTime = DateTime.Now;
                bool isStopLearning = true;
                float sumEpErr = 0;
                for (int setIter = 0; setIter < setCount; setIter++)
                {
                    List<float> input = readMnist.TrainDigits[setIter].floatPixels;
                    if (IsBiasNeuron)
                        input.Add(1);
                    /*string inStr = "";
                    foreach (var el in input)
                        inStr += el + " ";
                    File.WriteAllLines("input"+setIter+"-"+ep, new string[] { inStr });*/
                    int label = readMnist.TrainDigits[setIter].label;
                    List<float> output = Run(input, IsBiasNeuron);
                    float err = ErrorSet.MSE(answers[label], output);
                    sumEpErr += err;
                    if (err > 0.03) isStopLearning = false;
                    string outStr = "";
                    string answStr = "";
                    foreach (var el in output)
                        outStr += Math.Round(el, 4) + " ";
                    foreach (var el in answers[label])
                        answStr += Math.Round(el, 4) + " ";
                    ChangeWeights(answers[label], output);
                    if (ep % DIST_PRINT == 0 && setIter % 1000 == 0)
                    {
                        console.AppendText("ep = " + ep + " setIter = " + setIter
                            + " err = " + Math.Round(err, 4) + "\r\n");
                        console.AppendText("out: "+outStr +"\r\n");
                        console.AppendText("answ: "+answStr + "\r\n");
                        console.AppendText("label = " + label + "\r\n");
                        console.AppendText(readMnist.TrainDigits[setIter].ToString());
                        TimeSpan epTime = DateTime.Now - startEpTime;
                        console.AppendText("timeEp = " + epTime.ToString("h'h 'm'm 's's'") + "\r\n");
                        console.SelectionStart = console.TextLength;
                        console.ScrollToCaret();
                        //Information.form1.Refresh();
                    }
                }
                lastErrors[0] = lastErrors[1];
                lastErrors[1] = lastErrors[2];
                lastErrors[2] = sumEpErr / setCount;
                if (Math.Abs(lastErrors[0] - lastErrors[1]) < epsEpErr ||
                    Math.Abs(lastErrors[1] - lastErrors[2]) < epsEpErr)
                {
                    console.AppendText("exit epsEpErr\r\n");
                    console.AppendText(lastErrors[0]+"\r\n");
                    console.AppendText(lastErrors[1]+"\r\n");
                    console.AppendText(lastErrors[2]+"\r\n");
                    Information.form1.Refresh();
                    return;
                }
                if (isStopLearning)
                {
                    console.AppendText("exit isStopLearning\r\n");
                    Information.form1.Refresh();
                    return;
                }
            }
            console.AppendText("exit MAX_EP\r\n");
            Information.form1.Refresh();
        }

        //Проверка корректности ответа с выходом для задач распознавания
        private bool IsCorrectAnswer(List<float> output, List<float> answer)
        {
            int pattNum = -1;
            for(int i=0; i < answer.Count; i++)
            {
                if (answer[i] == 1)
                {
                    pattNum = i;
                    break;
                }
            }
            float maxOutSignal = float.MinValue;
            int maxOutSignalPos = -1;
            for (int i = 0; i < output.Count; i++)
            {
                if (maxOutSignal < output[i])
                {
                    maxOutSignal = output[i];
                    maxOutSignalPos = i;
                }
            }
            return maxOutSignalPos == pattNum;
        }

        //Тест количество верных ответов для mnist TestDigits
        public void MnistTest(ReadMnist readMnist, List<List<float>> answers)
        {
            FormConsole.PrintlnAndScroll("\r\nstart Mnist Test");
            int testCount = readMnist.TestDigits.Count;
            int goodCount = 0;
            for (int i=0; i < testCount; i++)
            {
                List<float> input = readMnist.TestDigits[i].floatPixels;
                if (IsBiasNeuron)
                    input.Add(1);
                int label = readMnist.TestDigits[i].label;
                List<float> output = Run(input, IsBiasNeuron);
                if (IsCorrectAnswer(output, answers[label]))
                    goodCount++;
                if(i % 100 == 0)
                    FormConsole.PrintlnAndScroll("i = " + i + " goodCount = " + goodCount);
            }
            FormConsole.PrintlnAndScroll("goodCount = " + goodCount
                + "\r\nfinish Mnist Test");
        }

        //обучение с наилучгим форматом
        public Task LearningFormat(PatternsType patterns, KeysType keys, AnswersType answers)
        {
            int setCount = patterns.Count;
            System.Windows.Forms.RichTextBox console = FormConsole.console;
            console.AppendText("Learning Start\r\n");
            Information.form1.Refresh();
            if (IsBiasNeuron)
                for (int i = 0; i < patterns.Count; i++)
                {
                    if(patterns[i].Count != this.inputLayer.SizeNeurons)
                        patterns[i].Add(1);
                }
            float epsEpErr = 0.0000001f;
            float[] lastErrors = new float[3];
            for (int i = 0; i < lastErrors.Length; i++)
                lastErrors[i] = i;
            for (int ep = 0; ep < MAX_EP; ep++)
            {
                DateTime startEpTime = DateTime.Now;
                bool isStopLearning = true;
                float sumEpErr = 0;
                for (int setIter = 0; setIter < setCount; setIter++)
                {
                    List<float> input = patterns[setIter];
                    string label = keys[setIter];
                    List<float> output = Run(input, IsBiasNeuron);                   
                    float err = ErrorSet.MSE(answers[label], output);
                    sumEpErr += err;
                    if (err > 0.03) isStopLearning = false;
                    string outStr = "";
                    string answStr = "";
                    foreach (var el in output)
                        outStr += Math.Round(el, 4) + " ";
                    foreach (var el in answers[label])
                        answStr += Math.Round(el, 4) + " ";
                    ChangeWeights(answers[label], output);
                    if (ep % DIST_PRINT == 0 && setIter % 100 == 0)
                    {
                        console.AppendText("ep = " + ep + " setIter = " + setIter
                            + " err = " + Math.Round(err, 4) + "\r\n");
                        console.AppendText("out: " + outStr + "\r\n");
                        console.AppendText("answ: " + answStr + "\r\n");
                        console.AppendText("label = " + label + "\r\n");
                        TimeSpan epTime = DateTime.Now - startEpTime;
                        console.AppendText("timeEp = " + epTime.ToString("h'h 'm'm 's's'") + "\r\n");
                        console.SelectionStart = console.TextLength;
                        console.ScrollToCaret();
                        Information.form1.Refresh();
                    }
                }
                lastErrors[0] = lastErrors[1];
                lastErrors[1] = lastErrors[2];
                lastErrors[2] = sumEpErr / setCount;
                if (Math.Abs(lastErrors[0] - lastErrors[1]) < epsEpErr ||
                    Math.Abs(lastErrors[1] - lastErrors[2]) < epsEpErr)
                {
                    console.AppendText("exit epsEpErr\r\n");
                    console.AppendText(lastErrors[0] + "\r\n");
                    console.AppendText(lastErrors[1] + "\r\n");
                    console.AppendText(lastErrors[2] + "\r\n");
                    Information.form1.Refresh();
                    return Task.CompletedTask;
                }
                if (isStopLearning)
                {
                    console.AppendText("exit isStopLearning\r\n");
                    Information.form1.Refresh();
                    return Task.CompletedTask;
                }
            }
            console.AppendText("exit MAX_EP\r\n");
            Information.form1.Refresh();
            return Task.CompletedTask;
        }

        //Тест количество верных ответов для mnist TestDigits
        public void FormatTest(PatternsType patterns, KeysType keys, AnswersType answers)
        {
            FormConsole.PrintlnAndScroll("\r\nstart Format Test");
            int testCount = patterns.Count;
            int goodCount = 0;
            int[] pattSuccess = new int[answers.Count];
            for (int i = 0; i < testCount; i++)
            {
                List<float> input = patterns[i];
                if (IsBiasNeuron && input.Count != this.inputLayer.SizeNeurons)
                    input.Add(1);
                string label = keys[i];
                List<float> output = Run(input, IsBiasNeuron);
                if (IsCorrectAnswer(output, answers[label]))
                {
                    goodCount++;
                    int num = 0;
                    if(int.TryParse(label, out num))
                    {
                        pattSuccess[num]++;
                    }
                }
                if (i % 1000 == 0)
                    FormConsole.PrintlnAndScroll("i = " + i + " goodCount = " + goodCount);
            }
            FormConsole.PrintlnAndScroll("goodCount = " + goodCount
                + "\r\nfinish Mnist Test");
            for(int i = 0; i < pattSuccess.Length; i++)
                FormConsole.AppendText(i + ": " + pattSuccess[i].ToString() + "\r\n");
        }

    }
}
