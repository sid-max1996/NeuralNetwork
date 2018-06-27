using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NN_Less3.NN;
using System.IO;

namespace NN_Less3
{
    using PatternsType = System.Collections.Generic.List<List<float>>;
    using AnswersType = System.Collections.Generic.Dictionary<string, List<float>>;
    using KeysType = System.Collections.Generic.List<string>;

    public partial class Form1 : Form
    {
        //конструктор формы
        public Form1()
        {
            InitializeComponent();
            settingGroupBox.Visible = false;
            mainConfig.Visible = false;
            learnConfig.Visible = false;
            learnConfig.Location = mainConfig.Location;
            settingButton.MouseClick += SettingsHiddenLayers;
            Information.form1 = this;
            //CreateAnswers.GreateAnswersLetters(@"\formatData\letters\");    
            //CreateData.CreateFile();
        }
        //печать НН
        private void NNPrint(NeuralNetwork NN, bool isShowNN)
        {
            console.AppendText(NN.AllInfo());
            if (isShowNN)
            {
                List<List<string>> textNN = NN.GetText();
                int numLayer = 1;
                foreach (var layerText in textNN)
                {
                    int numNeuron = 1;
                    console.AppendText("layer" + numLayer + ":\r\n");
                    foreach (var neuronText in layerText)
                    {
                        console.AppendText(numNeuron + ") " + neuronText);
                        numNeuron++;
                    }
                    console.AppendText("\r\n");
                    numLayer++;
                }
            }
            console.AppendText("\r\n");
        }
        //создание НН по умолчанию
        private void createNNDefault(object sender, EventArgs e)
        {
            string countStr = textBoxInCount.Text;
            int count = 0;
            if (!int.TryParse(countStr, out count))
            {
                MessageBox.Show("createNNDefault bad argument countNeuron");
                return;
            }
            if(count <= 0)
            {
                MessageBox.Show("createNNDefault bad argument countNeuron <= 0");
                return;
            }
            Information.inputSize = count;            
            bool isBias = (isBiasCheckBox.Checked == true);
            NeuralNetwork NN = new NeuralNetwork(count, ActivationFun.sigmoidFunction, isBias);
            Information.NN = NN;
            console.AppendText("NN by default create\r\n");
            console.AppendText("\r\n");
        }
        //создание с параметрами
        private void createNN(object sender, EventArgs e)
        {
            string countStr1 = textBoxInCount.Text;
            string countStr2 = textBoxOutCount.Text;
            int countIn = 0;
            int countOut = 0;
            if (!int.TryParse(countStr1, out countIn) || !int.TryParse(countStr2, out countOut))
            {
                MessageBox.Show("createNN bad argument countNeuron");
                return;
            }
            if (countIn <= 0 || countOut <= 0)
            {
                MessageBox.Show("createNN bad argument countNeuron <= 0");
                return;
            }
            Information.inputSize = countIn;
            bool isBias = (isBiasCheckBox.Checked == true);
            NeuralNetwork NN = new NeuralNetwork(countIn, Information.countOnHiddenLayers, countOut,
                ActivationFun.sigmoidFunction, isBias);
            Information.NN = NN;
            console.AppendText("NN by settings create\r\n");
            console.AppendText("\r\n");
        }
        //вывод в консоль выхода НН по входу
        public static void consoleRunNN(bool isPrintIn = true)
        {
            if (Information.inputSignals.Count == Information.inputSize + (Information.NN.IsBiasNeuron ? 1 : 0))
            {
                List<float> outputSignals = Information.NN.Run(Information.inputSignals, Information.NN.IsBiasNeuron);
                FormConsole.AppendText("Run Result:\r\n");
                int i = 1;
                if (isPrintIn)
                {                   
                    FormConsole.AppendText("input: ");
                    foreach (float signal in Information.inputSignals)
                    {
                        FormConsole.AppendText(i + ") " + signal.ToString() + " ");
                        i++;
                    }
                    FormConsole.AppendText("\r\n");
                }
                i = 1;
                FormConsole.AppendText("output: ");
                float max = float.MinValue;
                int maxInd = -1;
                foreach (float signal in outputSignals)
                {
                    if(max < signal)
                    {
                        max = signal; maxInd = i-1;
                    }
                    FormConsole.AppendText(i + ") " + Math.Round(signal, 4).ToString() + " ");
                    i++;
                }
                FormConsole.AppendText("\r\n");
                FormConsole.AppendText("Answer Pattern = " + maxInd);
                FormConsole.AppendText("\r\n");
                FormConsole.Scroll();
            }
            else
            {
                FormConsole.PrintlnAndScroll("Some Error In Run NN\r\n");
            }
        }
        //запуск из файла Run НН
        private void openRunNN(object sender, EventArgs e)
        {
            Information.inputSignals.Clear();
            openFileDialog1.FileName = "Файл Для Запуска";
            openFileDialog1.Filter = "*(*.txt)|*.txt";
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            //выбрать файл
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string str = "Выбран файл: ";
                str += openFileDialog1.FileName;
                MessageBox.Show(str);
            }
            string[] fileNames = new string[] { openFileDialog1.FileName };
            LoadFromFile lF = new LoadFromFile(fileNames);
            Information.inputSignals = lF.ConvertIntoList()[0];
            if (Information.NN.IsBiasNeuron) Information.inputSignals.Add(1);
            consoleRunNN();
        }
        //Run НН
        private void runNN(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(2);
            AddOwnedForm(form2);
            form2.ShowDialog();
            consoleRunNN();
        }
        //Настройка при создании скрытых слоев
        private void SettingsHiddenLayers(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(1);
            AddOwnedForm(form2);
            form2.ShowDialog();
        }
        //Изменение настроек НН
        private void changeNNSettings(object sender, EventArgs e)
        {
            float V;
            float.TryParse(textBoxLearnRate.Text, out V);
            Information.NN.LearnSpeed = V;
            string funStr = comboBoxFunAct.Text;
            if (funStr == "") funStr = "Linear";
            floatFun F = ActivationFun.sigmoidFunction;
            if (funStr == "Linear") F = ActivationFun.linearFunction;
            if (funStr == "Sigmoid") F = ActivationFun.sigmoidFunction;
            if (funStr == "Tangent") F = ActivationFun.tangentFunction;
            Information.NN.SetActFun(F);
            float M;
            float.TryParse(textBoxMoment.Text, out M);
            Information.NN.LearnMoment = M;
            console.AppendText("fun = " + funStr + " V = " + Information.NN.LearnSpeed);
            console.AppendText(" M = " + Information.NN.LearnMoment + "\r\n");
        }
        //Начать учить НН
        private async void startLearnNN(object sender, EventArgs e)
        {
            int MaxEp;
            if (int.TryParse(textBoxLearnRate.Text, out MaxEp))
                Information.NN.MaxEp = MaxEp;
            int DistPrint;
            if (int.TryParse(textBoxMoment.Text, out DistPrint))
                Information.NN.DistPrint = DistPrint;

            if (Information.pattNames.Count == Information.answNames.Count
                && Information.pattNames.Count != 0)
            {
                string[] fileNames1 = Information.pattNames.ToArray();
                LoadFromFile lF1 = new LoadFromFile(fileNames1);
                string[] fileNames2 = Information.answNames.ToArray();
                LoadFromFile lF2 = new LoadFromFile(fileNames2);
                List<List<float>> pattList = lF1.ConvertIntoList();
                List<List<float>> answList = lF2.ConvertIntoList();
                Information.NN.Learning(pattList.Count, pattList, answList);
            }
            else if (Information.isMnistLearn && Information.answNames.Count == 10)
            {
                ReadMnist readMnist = new ReadMnist(true, false);
                //File.WriteAllLines("mnist_data.txt", Information.mnistStrs);
                string[] fileNames2 = Information.answNames.ToArray();
                LoadFromFile lF2 = new LoadFromFile(fileNames2);
                List<List<float>> answList = lF2.ConvertIntoList();
                Information.NN.LearningMnist(readMnist, answList);
            }
            else if (Information.isFormatLearn)
            {
                if (Information.NN != null)
                {
                    openFileDialog1.FileName = "Выбор файла NN";
                    openFileDialog1.Filter = "*(*.txt)|*.txt";
                    openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory() + @"\formatData";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog1.FileName;
                        Information.FormatFilePath = filePath;
                        Tuple<PatternsType, KeysType, AnswersType> data = FormatLoad(filePath);
                        PatternsType pattList = data.Item1;
                        KeysType keys = data.Item2;
                        AnswersType answList = data.Item3;
                        await Information.NN.LearningFormat(pattList, keys, answList);
                    }
                    else FormConsole.PrintlnAndScroll("erroe open file dialog");
                }
                else FormConsole.PrintlnAndScroll("erroe NN is not define");
            }
            else
                printErrorMessage("Неверно заданы образцы и ответы");
        }
        //Первая группа чекбоксов
        private void checkBox_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            cB1.Checked = false;
            cB2.Checked = false;
            if (createCheckBox.Checked == true && checkBox == createCheckBox)
            {
                runCheckBox.Checked = false;
                learningCheckBox.Checked = false;
                createCheckBox.Checked = true;
                settingGroupBox.Visible = true;
                cB1.Visible = true;
                cB2.Visible = true;
                cB1.Text = "по умолчанию";
                cB2.Text = "с настройками";
            }
            else
            {
                if (runCheckBox.Checked == true && checkBox == runCheckBox)
                {
                    createCheckBox.Checked = false;
                    learningCheckBox.Checked = false;
                    runCheckBox.Checked = true;
                    settingGroupBox.Visible = true;
                    mainConfig.Visible = false;
                    cB1.Visible = true;
                    cB2.Visible = true;
                    cB1.Text = "запуск вводом";
                    cB2.Text = "запуск из файла";
                }
                else
                {
                    if (learningCheckBox.Checked == true && checkBox == learningCheckBox)
                    {
                        createCheckBox.Checked = false;
                        runCheckBox.Checked = false;
                        learningCheckBox.Checked = true;
                        settingGroupBox.Visible = true;
                        mainConfig.Visible = false;
                        cB1.Visible = true;
                        cB2.Visible = true;
                        cB1.Text = "настройки обучения";
                        cB2.Text = "запуск обучения";
                    }
                    else
                    {
                        settingGroupBox.Visible = false;
                        mainConfig.Visible = false;
                    }
                }
            }
        }
        //вторая группа чекбоксов
        private void checkBox_CheckedChanged2(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;           
            runButton.MouseClick -= createNNDefault;
            runButton.MouseClick -= createNN;
            runButton.MouseClick -= runNN;
            runButton.MouseClick -= openRunNN;
            if (createCheckBox.Checked == true)
            {
                label1.Text = "Количество Нейронов на входе";
                label2.Text = "Количество Нейронов на выходе";
                label3.Text = "Количество Скрытых Слоев";
                mainConfig.Visible = true;
                if (checkBox.Checked == true)
                {
                    if (checkBox == cB1)
                    {
                        cB2.Checked = false;
                        foreach (Control cnt in mainConfig.Controls)
                            cnt.Visible = false;
                        label1.Visible = true;
                        textBoxInCount.Visible = true;
                        runButton.Visible = true;
                        isBiasCheckBox.Visible = true;
                        runButton.MouseClick += createNNDefault;
                    }
                    if (checkBox == cB2)
                    {
                        cB1.Checked = false;
                        foreach (Control cnt in mainConfig.Controls)
                            cnt.Visible = true;
                        settingButton.Visible = false;
                        runButton.Text = "Создать";
                        runButton.MouseClick += createNN;
                    }
                }
                else if (cB2.Checked == false && cB1.Checked == false)
                    mainConfig.Visible = false;
            }

            if (runCheckBox.Checked == true)
            {
                mainConfig.Visible = true;
                if (checkBox.Checked == true)
                {
                    if (checkBox == cB1)
                    {
                        cB2.Checked = false;
                        foreach (Control cnt in mainConfig.Controls)
                            cnt.Visible = false;
                        runButton.Visible = true;
                        runButton.Text = "Ввод и Запуск";
                        runButton.MouseClick += runNN;
                        buttonDrawInput.Visible = true;
                    }
                    if (checkBox == cB2)
                    {
                        cB1.Checked = false;
                        foreach (Control cnt in mainConfig.Controls)
                            cnt.Visible = false;
                        runButton.Visible = true;
                        runButton.Text = "Открытие и Запуск";
                        runButton.MouseClick += openRunNN;
                    }
                }
                else if (cB2.Checked == false && cB1.Checked == false)
                    mainConfig.Visible = false;
            }

            if (learningCheckBox.Checked == true)
            {
                changeButton.MouseClick -= changeNNSettings;
                changeButton.MouseClick -= startLearnNN;
                learnConfig.Visible = true;
                if (checkBox.Checked == true)
                {
                    if (checkBox == cB1)
                    {
                        cB2.Checked = false;
                        foreach (Control cnt in learnConfig.Controls)
                            cnt.Visible = true;
                        changeButton.Text = "Изменить";
                        label5.Text = "Момент";
                        label6.Text = "Скорость обучения";
                        textBoxLearnRate.Text = "0,5";
                        textBoxMoment.Text = "0,5";
                        changeButton.MouseClick += changeNNSettings;
                    }
                    if (checkBox == cB2)
                    {
                        cB1.Checked = false;
                        foreach (Control cnt in learnConfig.Controls)
                            cnt.Visible = false;
                        changeButton.Visible = true;
                        changeButton.Text = "Начать Обучение";
                        label5.Visible = true;
                        label5.Text = "Интервал печати";
                        label6.Visible = true;
                        label6.Text = "Максимальная Эпоха";
                        textBoxLearnRate.Visible = true;
                        textBoxLearnRate.Text = "100";
                        textBoxMoment.Visible = true;
                        textBoxMoment.Text = "1";
                        changeButton.MouseClick += startLearnNN;
                    }
                }
                else if(cB2.Checked == false && cB1.Checked == false)
                    learnConfig.Visible = false;
            }
        }
        //запуск для количества скрытых слоёв
        private void textBoxHiddenLayers_TextChanged(object sender, EventArgs e)
        {
            if (createCheckBox.Checked == true)
            {
                TextBox textBox = (TextBox)sender;
                string countStr = textBox.Text;
                int count = 0;
                if (countStr != "")
                {
                    if (!int.TryParse(countStr, out count) || count < 0)
                    {
                        settingButton.Visible = false;
                        runButton.Visible = false;
                        MessageBox.Show("bad argument layersCount");
                        return;
                    }
                    else
                    {
                        Information.countOnHiddenLayers.Clear();
                        Information.hiddenSize = count;
                        settingButton.Visible = true;
                    }
                }
            }
        }
        //очистить консолль
        private void clearButton_Click(object sender, EventArgs e)
        {
            console.Clear();
        }
        //загрузка шаблонов группой файлов
        private void pattButton_Click(object sender, EventArgs e)
        {
            Information.pattNames.Clear();
            openFileDialog1.Multiselect = true;
            openFileDialog1.FileName = "Выбор Образцов";
            openFileDialog1.Filter = "(*.txt)|*.txt";
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            //выбрать файлы образцов
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string str = "Выбран файл:\r\n";
                foreach (string fileName in openFileDialog1.FileNames)
                {
                    Information.pattNames.Add(fileName);
                    str += fileName + "\r\n";
                }
                MessageBox.Show(str);
            }
            
        }
        //загрузка отвеетов группой файлов
        private void answButton_Click(object sender, EventArgs e)
        {
            Information.answNames.Clear();
            openFileDialog1.Multiselect = true;
            openFileDialog1.FileName = "Выбор Ответов";
            openFileDialog1.Filter = "*(*.txt)|*.txt";
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            //выбрать файлы ответов
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string str = "Выбран файл:\r\n";
                foreach (string fileName in openFileDialog1.FileNames)
                {
                    Information.answNames.Add(fileName);
                    str += fileName + "\r\n";
                }
                MessageBox.Show(str);
            }
        }
        //печать НН
        private void button1_Click(object sender, EventArgs e)
        {
            if (Information.NN != null)
            {
                bool show = isShowNNCheckBox.Checked;
                NNPrint(Information.NN, show);
            }
        }
        //установка флага mnist
        private void mnistButton_Click(object sender, EventArgs e)
        {
            Information.isMnistLearn = !Information.isMnistLearn;
            if (Information.isMnistLearn)
            {
                Information.isFormatLearn = false;
                Information.answNames.Clear();
                string dirName = Directory.GetCurrentDirectory() + "\\mnist\\mnistAnswers";
                string[] files = Directory.GetFiles(dirName);
                foreach (string fileName in files)
                    Information.answNames.Add(fileName);
            }
            MessageBox.Show("Mnist Learn " + (Information.isMnistLearn ? "Active" : "Not active"));
        }
        //запуск теста мниста
        private void mnistTestButton_Click(object sender, EventArgs e)
        {
            if(Information.NN == null)
            {
                printErrorMessage("NN не создана");
                return;
            }
            if (Information.isMnistLearn && Information.answNames.Count == 10)
            {
                ReadMnist readMnist = new ReadMnist(false, true);
                string[] fileNames2 = Information.answNames.ToArray();
                LoadFromFile lF2 = new LoadFromFile(fileNames2);
                List<List<float>> answList = lF2.ConvertIntoList();
                Information.NN.MnistTest(readMnist, answList);
            }
            else MessageBox.Show("Error Mnist Learn Not Active");
        }
        //всплывающее окно для ошибок
        private void printErrorMessage(string Text)
        {
            MessageBox.Show(Text, "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }
        //информационные сообщения
        private void printMessage(string Text)
        {
            MessageBox.Show(Text, "Информация",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
        }
        //сохранение НН
        private void saveNNButton_Click(object sender, EventArgs e)
        {
            int fileNum = 1;
            string filePath;
            do
            {
                filePath = @"NNfiles/NN" + fileNum + ".txt";
                fileNum++;
            }
            while (File.Exists(filePath) && fileNum != 100);
            if (fileNum == 100)
            {
                printErrorMessage("Сработало ограничение на количество файлов для записи = 100");
                return;
            }
            if (Information.NN != null)
            {
                File.WriteAllLines(filePath, new string[] { Information.NN.AllInfo() });              
                FileInfo fi = new FileInfo(Directory.GetCurrentDirectory()+"//"+filePath);
                StreamWriter sw = fi.AppendText();
                sw.WriteLine("Weights:");
                int numLayer = 1;
                List<List<string>> textNN = Information.NN.GetText();
                foreach (var layerText in textNN)
                {
                    sw.WriteLine("NumLayer = " + numLayer++);
                    foreach (var neuronText in layerText)
                        sw.Write(neuronText);
                    sw.WriteLine();
                }
                sw.Close();
                printMessage("NN сохранена в " + filePath);
            }
            else
                printErrorMessage("NN не создана!");
        }
        //загрузка НН
        private void loadNNButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "Выбор файла NN";
            openFileDialog1.Filter = "*(*.txt)|*.txt";
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory()+ @"\NNfiles";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string message = "NN загружена из ";
                string filePath = openFileDialog1.FileName;
                message += filePath + "\r\n";
                #region vars create NN
                Dictionary<string, string> infoDic = new Dictionary<string, string>();
                List<List<float>> allWeights = new List<List<float>>();
                List<int> hiddSizes = new List<int>();
                #endregion
                List<double> res = new List<double>();
                string[] readText = File.ReadAllLines(filePath);
                bool flagInfoRead = true;
                bool flagWeightsRead = false;
                #region read from file
                foreach (string s in readText)
                {
                    if (s.Contains("Weights:"))
                        { flagInfoRead = false; continue; }
                    if (flagInfoRead)
                    {
                        string[] keyVal = s.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (keyVal.Length == 2)
                        {
                            if (keyVal[0].Contains("hiddenLayerSizeNeurons"))
                            {
                                int layerNeuronSize = 0;
                                int.TryParse(keyVal[1], out layerNeuronSize);
                                hiddSizes.Add(layerNeuronSize);
                            } 
                            else
                            infoDic.Add(keyVal[0].Replace(" ", ""), keyVal[1].Replace(" ", ""));
                        }
                    }
                    else
                    {
                        if (!flagWeightsRead)
                            {flagWeightsRead = true; continue;}
                        else
                        {
                            flagWeightsRead = false;
                            string[] layerWeightsArr = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            List<float> layerWeights = new List<float>(layerWeightsArr.Length);
                            if (layerWeightsArr.Length != 0)
                            {
                                foreach(var w in layerWeightsArr)
                                {
                                    float floatW = 0;
                                    float.TryParse(w, out floatW);
                                    layerWeights.Add(floatW);
                                }                                    
                            }
                            allWeights.Add(layerWeights);
                        }
                    }
                }
                #endregion
                #region create NN
                int inSize = 0; int outSize = 0; bool isBias = false;
                int.TryParse(infoDic["inputLayerSizeNeurons"], out inSize);
                int.TryParse(infoDic["outputLayerSizeNeurons"], out outSize);
                bool.TryParse(infoDic["isBias"], out isBias);
                Information.NN = new NeuralNetwork(inSize, hiddSizes, outSize,
                    ActivationFun.sigmoidFunction, allWeights, isBias);
                #endregion
                #region settings NN
                float learnMoment = 0, learnSpeed = 0;
                int maxEp = 0, distPrint = 0;
                float.TryParse(infoDic["Moment"], out learnMoment);
                float.TryParse(infoDic["LearnRate"], out learnSpeed);
                int.TryParse(infoDic["MAX_EP"], out maxEp);
                int.TryParse(infoDic["DIST_PRINT"], out distPrint);
                Information.NN.LearnMoment = learnMoment;
                Information.NN.LearnSpeed = learnSpeed;
                Information.NN.MaxEp = maxEp;
                Information.NN.DistPrint = distPrint;
                #endregion
                Information.inputSize = isBias?inSize-1:inSize;
                MessageBox.Show(message);
            }
        }
        //Format Data установка флага
        private void buttonChoseData_Click(object sender, EventArgs e)
        {
            Information.isFormatLearn = !Information.isFormatLearn;
            if(Information.isFormatLearn) Information.isMnistLearn = false;
            MessageBox.Show("Format Learn " + (Information.isFormatLearn ? "Active" : "Not active"));
        }
        //загрузка Format Data из файла
        private Tuple<PatternsType, KeysType, AnswersType> FormatLoad(string filePath)
        {
            string message = "Данные загружены из ";
            message += filePath + "\r\n";
            PatternsType patts = new PatternsType();
            KeysType keys = new KeysType();
            AnswersType answs = new AnswersType();
            #region Read Answers
            int posLastSlesh = filePath.LastIndexOf('\\');
            string answersPath = filePath.Substring(0, posLastSlesh);
            answersPath += "\\answers.txt";
            answs = LoadVec.LoadFloatVecWithKey(answersPath);
            #endregion
            #region Read Patterns
            string[] readText = File.ReadAllLines(filePath);
            foreach (string s in readText)
            {
                if (s.Trim() != "")
                {
                    string[] elemsStr = s.Split(new char[] { ',' });
                    List<float> vec = new List<float>(elemsStr.Length - 1);
                    string key = elemsStr[0];
                    for (int i = 1; i < elemsStr.Length; i++)
                    {
                        float floatEl;
                        bool ok = float.TryParse(elemsStr[i], out floatEl);
                        if (ok) vec.Add(floatEl);
                        else
                        {
                            FormConsole.PrintlnAndScroll("error try parse");
                            return new Tuple<PatternsType, KeysType, AnswersType>(null, null, null);
                        }
                    }
                    patts.Add(vec);
                    keys.Add(key);
                }
            }
            #endregion
            FormConsole.PrintlnAndScroll(message);
            return new Tuple<PatternsType, KeysType, AnswersType>(patts, keys, answs);
        }
        //тест для Format Data
        private void buttonFormatTest_Click(object sender, EventArgs e)
        {
            if (Information.isFormatLearn)
            {
                openFileDialog1.FileName = "Выбор файла NN";
                openFileDialog1.Filter = "*(*.txt)|*.txt";
                openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory() + @"\formatData";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog1.FileName;
                    Information.FormatFilePath = filePath;
                }

                if (Information.FormatFilePath != "")
                {
                    Tuple<PatternsType, KeysType, AnswersType> data
                        = FormatLoad(Information.FormatFilePath);
                    PatternsType pattList = data.Item1;
                    KeysType keys = data.Item2;
                    AnswersType answList = data.Item3;
                    Information.NN.FormatTest(pattList, keys, answList);
                }
            }
            else
                printErrorMessage("Format Learn Nor Active!");
        }

        private void buttonDrawInput_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            AddOwnedForm(form3);
            form3.ShowDialog();
        }
    }
}
