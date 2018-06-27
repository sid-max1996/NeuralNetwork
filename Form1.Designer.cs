namespace NN_Less3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.console = new System.Windows.Forms.RichTextBox();
            this.consoleLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.mainGroupBox = new System.Windows.Forms.GroupBox();
            this.learningCheckBox = new System.Windows.Forms.CheckBox();
            this.runCheckBox = new System.Windows.Forms.CheckBox();
            this.createCheckBox = new System.Windows.Forms.CheckBox();
            this.settingGroupBox = new System.Windows.Forms.GroupBox();
            this.cB2 = new System.Windows.Forms.CheckBox();
            this.cB1 = new System.Windows.Forms.CheckBox();
            this.textBoxInCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOutCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxHiddenLayers = new System.Windows.Forms.TextBox();
            this.settingButton = new System.Windows.Forms.Button();
            this.mainConfig = new System.Windows.Forms.GroupBox();
            this.isBiasCheckBox = new System.Windows.Forms.CheckBox();
            this.answButton = new System.Windows.Forms.Button();
            this.pattButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.learnConfig = new System.Windows.Forms.GroupBox();
            this.buttonChoseData = new System.Windows.Forms.Button();
            this.mnistButton = new System.Windows.Forms.Button();
            this.comboBoxFunAct = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.changeButton = new System.Windows.Forms.Button();
            this.textBoxMoment = new System.Windows.Forms.TextBox();
            this.textBoxLearnRate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.isShowNNCheckBox = new System.Windows.Forms.CheckBox();
            this.mnistTestButton = new System.Windows.Forms.Button();
            this.saveNNButton = new System.Windows.Forms.Button();
            this.loadNNButton = new System.Windows.Forms.Button();
            this.buttonFormatTest = new System.Windows.Forms.Button();
            this.buttonDrawInput = new System.Windows.Forms.Button();
            this.mainGroupBox.SuspendLayout();
            this.settingGroupBox.SuspendLayout();
            this.mainConfig.SuspendLayout();
            this.learnConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(68, 221);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(694, 330);
            this.console.TabIndex = 0;
            this.console.Text = "";
            // 
            // consoleLabel
            // 
            this.consoleLabel.AutoSize = true;
            this.consoleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.consoleLabel.Location = new System.Drawing.Point(328, 165);
            this.consoleLabel.Name = "consoleLabel";
            this.consoleLabel.Size = new System.Drawing.Size(156, 42);
            this.consoleLabel.TabIndex = 1;
            this.consoleLabel.Text = "Console";
            // 
            // runButton
            // 
            this.runButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.runButton.Location = new System.Drawing.Point(9, 93);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(92, 45);
            this.runButton.TabIndex = 2;
            this.runButton.Text = "Создать";
            this.runButton.UseVisualStyleBackColor = false;
            // 
            // mainGroupBox
            // 
            this.mainGroupBox.Controls.Add(this.learningCheckBox);
            this.mainGroupBox.Controls.Add(this.runCheckBox);
            this.mainGroupBox.Controls.Add(this.createCheckBox);
            this.mainGroupBox.Location = new System.Drawing.Point(27, 24);
            this.mainGroupBox.Name = "mainGroupBox";
            this.mainGroupBox.Size = new System.Drawing.Size(112, 140);
            this.mainGroupBox.TabIndex = 3;
            this.mainGroupBox.TabStop = false;
            this.mainGroupBox.Text = "Режимы Работы";
            // 
            // learningCheckBox
            // 
            this.learningCheckBox.AutoSize = true;
            this.learningCheckBox.Location = new System.Drawing.Point(6, 73);
            this.learningCheckBox.Name = "learningCheckBox";
            this.learningCheckBox.Size = new System.Drawing.Size(74, 17);
            this.learningCheckBox.TabIndex = 6;
            this.learningCheckBox.Text = "Обучение";
            this.learningCheckBox.UseVisualStyleBackColor = true;
            this.learningCheckBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged1);
            // 
            // runCheckBox
            // 
            this.runCheckBox.AutoSize = true;
            this.runCheckBox.Location = new System.Drawing.Point(6, 48);
            this.runCheckBox.Name = "runCheckBox";
            this.runCheckBox.Size = new System.Drawing.Size(89, 17);
            this.runCheckBox.TabIndex = 5;
            this.runCheckBox.Text = "Работа Сети";
            this.runCheckBox.UseVisualStyleBackColor = true;
            this.runCheckBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged1);
            // 
            // createCheckBox
            // 
            this.createCheckBox.AutoSize = true;
            this.createCheckBox.Location = new System.Drawing.Point(6, 22);
            this.createCheckBox.Name = "createCheckBox";
            this.createCheckBox.Size = new System.Drawing.Size(102, 17);
            this.createCheckBox.TabIndex = 4;
            this.createCheckBox.Text = "Создание Сети";
            this.createCheckBox.UseVisualStyleBackColor = true;
            this.createCheckBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged1);
            // 
            // settingGroupBox
            // 
            this.settingGroupBox.Controls.Add(this.cB2);
            this.settingGroupBox.Controls.Add(this.cB1);
            this.settingGroupBox.Location = new System.Drawing.Point(160, 24);
            this.settingGroupBox.Name = "settingGroupBox";
            this.settingGroupBox.Size = new System.Drawing.Size(143, 140);
            this.settingGroupBox.TabIndex = 4;
            this.settingGroupBox.TabStop = false;
            // 
            // cB2
            // 
            this.cB2.AutoSize = true;
            this.cB2.Location = new System.Drawing.Point(6, 50);
            this.cB2.Name = "cB2";
            this.cB2.Size = new System.Drawing.Size(102, 17);
            this.cB2.TabIndex = 5;
            this.cB2.Text = "с настройками";
            this.cB2.UseVisualStyleBackColor = true;
            this.cB2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged2);
            // 
            // cB1
            // 
            this.cB1.AutoSize = true;
            this.cB1.Location = new System.Drawing.Point(6, 22);
            this.cB1.Name = "cB1";
            this.cB1.Size = new System.Drawing.Size(97, 17);
            this.cB1.TabIndex = 4;
            this.cB1.Text = "по умолчанию";
            this.cB1.UseVisualStyleBackColor = true;
            this.cB1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged2);
            // 
            // textBoxInCount
            // 
            this.textBoxInCount.Location = new System.Drawing.Point(186, 8);
            this.textBoxInCount.Name = "textBoxInCount";
            this.textBoxInCount.Size = new System.Drawing.Size(44, 20);
            this.textBoxInCount.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Количество Нейронов на входе";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Количество Нейронов на выходе";
            // 
            // textBoxOutCount
            // 
            this.textBoxOutCount.Location = new System.Drawing.Point(186, 34);
            this.textBoxOutCount.Name = "textBoxOutCount";
            this.textBoxOutCount.Size = new System.Drawing.Size(44, 20);
            this.textBoxOutCount.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Количество Скрытых Слоев";
            // 
            // textBoxHiddenLayers
            // 
            this.textBoxHiddenLayers.Location = new System.Drawing.Point(186, 64);
            this.textBoxHiddenLayers.Name = "textBoxHiddenLayers";
            this.textBoxHiddenLayers.Size = new System.Drawing.Size(44, 20);
            this.textBoxHiddenLayers.TabIndex = 10;
            this.textBoxHiddenLayers.TextChanged += new System.EventHandler(this.textBoxHiddenLayers_TextChanged);
            // 
            // settingButton
            // 
            this.settingButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.settingButton.Location = new System.Drawing.Point(249, 58);
            this.settingButton.Name = "settingButton";
            this.settingButton.Size = new System.Drawing.Size(86, 30);
            this.settingButton.TabIndex = 11;
            this.settingButton.Text = "Настройка";
            this.settingButton.UseVisualStyleBackColor = false;
            // 
            // mainConfig
            // 
            this.mainConfig.Controls.Add(this.buttonDrawInput);
            this.mainConfig.Controls.Add(this.isBiasCheckBox);
            this.mainConfig.Controls.Add(this.label2);
            this.mainConfig.Controls.Add(this.settingButton);
            this.mainConfig.Controls.Add(this.runButton);
            this.mainConfig.Controls.Add(this.textBoxHiddenLayers);
            this.mainConfig.Controls.Add(this.textBoxInCount);
            this.mainConfig.Controls.Add(this.label3);
            this.mainConfig.Controls.Add(this.label1);
            this.mainConfig.Controls.Add(this.textBoxOutCount);
            this.mainConfig.Location = new System.Drawing.Point(324, 14);
            this.mainConfig.Name = "mainConfig";
            this.mainConfig.Size = new System.Drawing.Size(428, 150);
            this.mainConfig.TabIndex = 12;
            this.mainConfig.TabStop = false;
            // 
            // isBiasCheckBox
            // 
            this.isBiasCheckBox.AutoSize = true;
            this.isBiasCheckBox.Location = new System.Drawing.Point(249, 10);
            this.isBiasCheckBox.Name = "isBiasCheckBox";
            this.isBiasCheckBox.Size = new System.Drawing.Size(168, 17);
            this.isBiasCheckBox.TabIndex = 12;
            this.isBiasCheckBox.Text = "добавить нейрон смещения";
            this.isBiasCheckBox.UseVisualStyleBackColor = true;
            // 
            // answButton
            // 
            this.answButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.answButton.Location = new System.Drawing.Point(308, 83);
            this.answButton.Name = "answButton";
            this.answButton.Size = new System.Drawing.Size(92, 46);
            this.answButton.TabIndex = 15;
            this.answButton.Text = "answButton";
            this.answButton.UseVisualStyleBackColor = false;
            this.answButton.Click += new System.EventHandler(this.answButton_Click);
            // 
            // pattButton
            // 
            this.pattButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pattButton.Location = new System.Drawing.Point(285, 27);
            this.pattButton.Name = "pattButton";
            this.pattButton.Size = new System.Drawing.Size(67, 50);
            this.pattButton.TabIndex = 14;
            this.pattButton.Text = "pattButton";
            this.pattButton.UseVisualStyleBackColor = false;
            this.pattButton.Click += new System.EventHandler(this.pattButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.clearButton.Location = new System.Drawing.Point(649, 569);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(113, 28);
            this.clearButton.TabIndex = 13;
            this.clearButton.Text = "Очистить";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // learnConfig
            // 
            this.learnConfig.Controls.Add(this.buttonChoseData);
            this.learnConfig.Controls.Add(this.mnistButton);
            this.learnConfig.Controls.Add(this.answButton);
            this.learnConfig.Controls.Add(this.comboBoxFunAct);
            this.learnConfig.Controls.Add(this.pattButton);
            this.learnConfig.Controls.Add(this.label4);
            this.learnConfig.Controls.Add(this.changeButton);
            this.learnConfig.Controls.Add(this.textBoxMoment);
            this.learnConfig.Controls.Add(this.textBoxLearnRate);
            this.learnConfig.Controls.Add(this.label5);
            this.learnConfig.Controls.Add(this.label6);
            this.learnConfig.Location = new System.Drawing.Point(324, 165);
            this.learnConfig.Name = "learnConfig";
            this.learnConfig.Size = new System.Drawing.Size(428, 150);
            this.learnConfig.TabIndex = 14;
            this.learnConfig.TabStop = false;
            this.learnConfig.Visible = false;
            // 
            // buttonChoseData
            // 
            this.buttonChoseData.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonChoseData.Location = new System.Drawing.Point(155, 101);
            this.buttonChoseData.Name = "buttonChoseData";
            this.buttonChoseData.Size = new System.Drawing.Size(88, 37);
            this.buttonChoseData.TabIndex = 20;
            this.buttonChoseData.Text = "ChoseData";
            this.buttonChoseData.UseVisualStyleBackColor = false;
            this.buttonChoseData.Click += new System.EventHandler(this.buttonChoseData_Click);
            // 
            // mnistButton
            // 
            this.mnistButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mnistButton.Location = new System.Drawing.Point(353, 27);
            this.mnistButton.Name = "mnistButton";
            this.mnistButton.Size = new System.Drawing.Size(69, 50);
            this.mnistButton.TabIndex = 19;
            this.mnistButton.Text = "MnistLearn";
            this.mnistButton.UseVisualStyleBackColor = false;
            this.mnistButton.Click += new System.EventHandler(this.mnistButton_Click);
            // 
            // comboBoxFunAct
            // 
            this.comboBoxFunAct.FormattingEnabled = true;
            this.comboBoxFunAct.Items.AddRange(new object[] {
            "Linear",
            "Sigmoid",
            "Tangent"});
            this.comboBoxFunAct.Location = new System.Drawing.Point(186, 64);
            this.comboBoxFunAct.Name = "comboBoxFunAct";
            this.comboBoxFunAct.Size = new System.Drawing.Size(57, 21);
            this.comboBoxFunAct.TabIndex = 13;
            this.comboBoxFunAct.Text = "Sigmoid";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Функция Активации";
            // 
            // changeButton
            // 
            this.changeButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.changeButton.Location = new System.Drawing.Point(9, 93);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(92, 45);
            this.changeButton.TabIndex = 2;
            this.changeButton.Text = "Изменить";
            this.changeButton.UseVisualStyleBackColor = false;
            // 
            // textBoxMoment
            // 
            this.textBoxMoment.Location = new System.Drawing.Point(186, 37);
            this.textBoxMoment.Name = "textBoxMoment";
            this.textBoxMoment.Size = new System.Drawing.Size(57, 20);
            this.textBoxMoment.TabIndex = 10;
            this.textBoxMoment.Text = "0,4";
            // 
            // textBoxLearnRate
            // 
            this.textBoxLearnRate.Location = new System.Drawing.Point(186, 8);
            this.textBoxLearnRate.Name = "textBoxLearnRate";
            this.textBoxLearnRate.Size = new System.Drawing.Size(57, 20);
            this.textBoxLearnRate.TabIndex = 5;
            this.textBoxLearnRate.Text = "0,3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Момент";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Скорость обучения";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Location = new System.Drawing.Point(68, 169);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 46);
            this.button1.TabIndex = 16;
            this.button1.Text = "PrintNN";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // isShowNNCheckBox
            // 
            this.isShowNNCheckBox.AutoSize = true;
            this.isShowNNCheckBox.Location = new System.Drawing.Point(166, 185);
            this.isShowNNCheckBox.Name = "isShowNNCheckBox";
            this.isShowNNCheckBox.Size = new System.Drawing.Size(102, 17);
            this.isShowNNCheckBox.TabIndex = 17;
            this.isShowNNCheckBox.Text = "Показать веса";
            this.isShowNNCheckBox.UseVisualStyleBackColor = true;
            // 
            // mnistTestButton
            // 
            this.mnistTestButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.mnistTestButton.Location = new System.Drawing.Point(313, 562);
            this.mnistTestButton.Name = "mnistTestButton";
            this.mnistTestButton.Size = new System.Drawing.Size(112, 35);
            this.mnistTestButton.TabIndex = 18;
            this.mnistTestButton.Text = "Mnist Test";
            this.mnistTestButton.UseVisualStyleBackColor = false;
            this.mnistTestButton.Click += new System.EventHandler(this.mnistTestButton_Click);
            // 
            // saveNNButton
            // 
            this.saveNNButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.saveNNButton.Location = new System.Drawing.Point(68, 562);
            this.saveNNButton.Name = "saveNNButton";
            this.saveNNButton.Size = new System.Drawing.Size(112, 35);
            this.saveNNButton.TabIndex = 19;
            this.saveNNButton.Text = "Save NN";
            this.saveNNButton.UseVisualStyleBackColor = false;
            this.saveNNButton.Click += new System.EventHandler(this.saveNNButton_Click);
            // 
            // loadNNButton
            // 
            this.loadNNButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.loadNNButton.Location = new System.Drawing.Point(191, 562);
            this.loadNNButton.Name = "loadNNButton";
            this.loadNNButton.Size = new System.Drawing.Size(112, 35);
            this.loadNNButton.TabIndex = 20;
            this.loadNNButton.Text = "Load NN";
            this.loadNNButton.UseVisualStyleBackColor = false;
            this.loadNNButton.Click += new System.EventHandler(this.loadNNButton_Click);
            // 
            // buttonFormatTest
            // 
            this.buttonFormatTest.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.buttonFormatTest.Location = new System.Drawing.Point(442, 562);
            this.buttonFormatTest.Name = "buttonFormatTest";
            this.buttonFormatTest.Size = new System.Drawing.Size(112, 35);
            this.buttonFormatTest.TabIndex = 21;
            this.buttonFormatTest.Text = "Format Test";
            this.buttonFormatTest.UseVisualStyleBackColor = false;
            this.buttonFormatTest.Click += new System.EventHandler(this.buttonFormatTest_Click);
            // 
            // buttonDrawInput
            // 
            this.buttonDrawInput.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonDrawInput.Location = new System.Drawing.Point(326, 95);
            this.buttonDrawInput.Name = "buttonDrawInput";
            this.buttonDrawInput.Size = new System.Drawing.Size(91, 40);
            this.buttonDrawInput.TabIndex = 13;
            this.buttonDrawInput.Text = "Draw Input";
            this.buttonDrawInput.UseVisualStyleBackColor = false;
            this.buttonDrawInput.Click += new System.EventHandler(this.buttonDrawInput_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 609);
            this.Controls.Add(this.buttonFormatTest);
            this.Controls.Add(this.loadNNButton);
            this.Controls.Add(this.saveNNButton);
            this.Controls.Add(this.mnistTestButton);
            this.Controls.Add(this.isShowNNCheckBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.learnConfig);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.mainConfig);
            this.Controls.Add(this.settingGroupBox);
            this.Controls.Add(this.mainGroupBox);
            this.Controls.Add(this.consoleLabel);
            this.Controls.Add(this.console);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "NN";
            this.mainGroupBox.ResumeLayout(false);
            this.mainGroupBox.PerformLayout();
            this.settingGroupBox.ResumeLayout(false);
            this.settingGroupBox.PerformLayout();
            this.mainConfig.ResumeLayout(false);
            this.mainConfig.PerformLayout();
            this.learnConfig.ResumeLayout(false);
            this.learnConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.Label consoleLabel;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.GroupBox mainGroupBox;
        private System.Windows.Forms.CheckBox createCheckBox;
        private System.Windows.Forms.GroupBox settingGroupBox;
        private System.Windows.Forms.CheckBox cB1;
        private System.Windows.Forms.TextBox textBoxInCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxOutCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxHiddenLayers;
        private System.Windows.Forms.Button settingButton;
        private System.Windows.Forms.GroupBox mainConfig;
        private System.Windows.Forms.CheckBox cB2;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.CheckBox runCheckBox;
        private System.Windows.Forms.CheckBox isBiasCheckBox;
        private System.Windows.Forms.CheckBox learningCheckBox;
        private System.Windows.Forms.Button answButton;
        private System.Windows.Forms.Button pattButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox learnConfig;
        private System.Windows.Forms.ComboBox comboBoxFunAct;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.TextBox textBoxMoment;
        private System.Windows.Forms.TextBox textBoxLearnRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox isShowNNCheckBox;
        private System.Windows.Forms.Button mnistButton;
        private System.Windows.Forms.Button mnistTestButton;
        private System.Windows.Forms.Button saveNNButton;
        private System.Windows.Forms.Button loadNNButton;
        private System.Windows.Forms.Button buttonChoseData;
        private System.Windows.Forms.Button buttonFormatTest;
        private System.Windows.Forms.Button buttonDrawInput;
    }
}

