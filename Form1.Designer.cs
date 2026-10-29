namespace winform_进阶联合编程
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.topLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.trainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.selectImageBtn = new System.Windows.Forms.Button();
            this.btnAddCaliper = new System.Windows.Forms.Button();
            this.btnRemoveCaliper = new System.Windows.Forms.Button();
            this.TrainBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.runLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblMode = new System.Windows.Forms.Label();
            this.ModeSelectionBox = new System.Windows.Forms.ComboBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.CountTxtBox = new System.Windows.Forms.TextBox();
            this.RunBtn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pipelineLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblInterval = new System.Windows.Forms.Label();
            this.numInterval = new System.Windows.Forms.NumericUpDown();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblRunCount = new System.Windows.Forms.Label();
            this.numRunCount = new System.Windows.Forms.NumericUpDown();
            this.lblTimes = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRunOne = new System.Windows.Forms.Button();
            this.lblPlcIp = new System.Windows.Forms.Label();
            this.txtPlcIp = new System.Windows.Forms.TextBox();
            this.btnPlcConnect = new System.Windows.Forms.Button();
            this.lblPlcStatus = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.statsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblStats = new System.Windows.Forms.Label();
            this.resultLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.BlobResultLabel = new System.Windows.Forms.Label();
            this.CaliperResultLabel = new System.Windows.Forms.Label();
            this.mainLayoutPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.trainLayoutPanel.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.runLayoutPanel.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.pipelineLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRunCount)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.statsLayoutPanel.SuspendLayout();
            this.resultLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            this.mainLayoutPanel.ColumnCount = 1;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayoutPanel.Controls.Add(this.groupBox1, 0, 0);
            this.mainLayoutPanel.Controls.Add(this.groupBox2, 0, 1);
            this.mainLayoutPanel.Controls.Add(this.groupBox4, 0, 2);
            this.mainLayoutPanel.Controls.Add(this.groupBox5, 0, 3);
            this.mainLayoutPanel.Controls.Add(this.groupBox3, 0, 4);
            this.mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 5;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.mainLayoutPanel.Size = new System.Drawing.Size(982, 953);
            this.mainLayoutPanel.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.topLayoutPanel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(976, 486);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图像显示区域";
            // 
            // topLayoutPanel
            // 
            this.topLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.topLayoutPanel.ColumnCount = 2;
            this.topLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.topLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.topLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topLayoutPanel.Location = new System.Drawing.Point(3, 21);
            this.topLayoutPanel.Name = "topLayoutPanel";
            this.topLayoutPanel.RowCount = 1;
            this.topLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topLayoutPanel.Size = new System.Drawing.Size(970, 462);
            this.topLayoutPanel.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trainLayoutPanel);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(3, 495);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(976, 64);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "模板训练";
            // 
            // trainLayoutPanel
            // 
            this.trainLayoutPanel.ColumnCount = 6;
            this.trainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.trainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.trainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.trainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.trainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.trainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.trainLayoutPanel.Controls.Add(this.selectImageBtn, 0, 0);
            this.trainLayoutPanel.Controls.Add(this.btnAddCaliper, 1, 0);
            this.trainLayoutPanel.Controls.Add(this.btnRemoveCaliper, 2, 0);
            this.trainLayoutPanel.Controls.Add(this.TrainBtn, 4, 0);
            this.trainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trainLayoutPanel.Location = new System.Drawing.Point(3, 21);
            this.trainLayoutPanel.Name = "trainLayoutPanel";
            this.trainLayoutPanel.RowCount = 1;
            this.trainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.trainLayoutPanel.Size = new System.Drawing.Size(970, 40);
            this.trainLayoutPanel.TabIndex = 0;
            // 
            // selectImageBtn
            // 
            this.selectImageBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectImageBtn.Location = new System.Drawing.Point(3, 3);
            this.selectImageBtn.Name = "selectImageBtn";
            this.selectImageBtn.Size = new System.Drawing.Size(120, 34);
            this.selectImageBtn.TabIndex = 0;
            this.selectImageBtn.Text = "选择图像";
            this.selectImageBtn.UseVisualStyleBackColor = true;
            this.selectImageBtn.Click += new System.EventHandler(this.selectImageBtn_Click);
            // 
            // btnAddCaliper
            // 
            this.btnAddCaliper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddCaliper.Location = new System.Drawing.Point(129, 3);
            this.btnAddCaliper.Name = "btnAddCaliper";
            this.btnAddCaliper.Size = new System.Drawing.Size(100, 34);
            this.btnAddCaliper.TabIndex = 1;
            this.btnAddCaliper.Text = "+ 增加卡尺";
            this.btnAddCaliper.UseVisualStyleBackColor = true;
            this.btnAddCaliper.Click += new System.EventHandler(this.btnAddCaliper_Click);
            // 
            // btnRemoveCaliper
            // 
            this.btnRemoveCaliper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemoveCaliper.Location = new System.Drawing.Point(235, 3);
            this.btnRemoveCaliper.Name = "btnRemoveCaliper";
            this.btnRemoveCaliper.Size = new System.Drawing.Size(102, 34);
            this.btnRemoveCaliper.TabIndex = 2;
            this.btnRemoveCaliper.Text = "- 删除卡尺";
            this.btnRemoveCaliper.UseVisualStyleBackColor = true;
            this.btnRemoveCaliper.Click += new System.EventHandler(this.btnRemoveCaliper_Click);
            // 
            // TrainBtn
            // 
            this.TrainBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrainBtn.Location = new System.Drawing.Point(367, 3);
            this.TrainBtn.Name = "TrainBtn";
            this.TrainBtn.Size = new System.Drawing.Size(146, 34);
            this.TrainBtn.TabIndex = 3;
            this.TrainBtn.Text = "🔧 训练模板";
            this.TrainBtn.UseVisualStyleBackColor = true;
            this.TrainBtn.Click += new System.EventHandler(this.TrainBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.runLayoutPanel);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.ForeColor = System.Drawing.Color.DarkGreen;
            this.groupBox4.Location = new System.Drawing.Point(3, 565);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(976, 64);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "运行检测";
            // 
            // runLayoutPanel
            // 
            this.runLayoutPanel.ColumnCount = 6;
            this.runLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.runLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 147F));
            this.runLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.runLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.runLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.runLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.runLayoutPanel.Controls.Add(this.lblMode, 0, 0);
            this.runLayoutPanel.Controls.Add(this.ModeSelectionBox, 1, 0);
            this.runLayoutPanel.Controls.Add(this.lblCount, 2, 0);
            this.runLayoutPanel.Controls.Add(this.CountTxtBox, 3, 0);
            this.runLayoutPanel.Controls.Add(this.RunBtn, 5, 0);
            this.runLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runLayoutPanel.Location = new System.Drawing.Point(3, 21);
            this.runLayoutPanel.Name = "runLayoutPanel";
            this.runLayoutPanel.RowCount = 1;
            this.runLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.runLayoutPanel.Size = new System.Drawing.Size(970, 40);
            this.runLayoutPanel.TabIndex = 0;
            // 
            // lblMode
            // 
            this.lblMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(3, 12);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(67, 15);
            this.lblMode.TabIndex = 0;
            this.lblMode.Text = "检测模式";
            // 
            // ModeSelectionBox
            // 
            this.ModeSelectionBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ModeSelectionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModeSelectionBox.FormattingEnabled = true;
            this.ModeSelectionBox.Location = new System.Drawing.Point(86, 8);
            this.ModeSelectionBox.Name = "ModeSelectionBox";
            this.ModeSelectionBox.Size = new System.Drawing.Size(124, 23);
            this.ModeSelectionBox.TabIndex = 1;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(233, 12);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(97, 15);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "期望圆孔数量";
            // 
            // CountTxtBox
            // 
            this.CountTxtBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CountTxtBox.Location = new System.Drawing.Point(342, 7);
            this.CountTxtBox.Name = "CountTxtBox";
            this.CountTxtBox.Size = new System.Drawing.Size(111, 25);
            this.CountTxtBox.TabIndex = 3;
            // 
            // RunBtn
            // 
            this.RunBtn.BackColor = System.Drawing.Color.LightGreen;
            this.RunBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RunBtn.Location = new System.Drawing.Point(503, 3);
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(464, 34);
            this.RunBtn.TabIndex = 4;
            this.RunBtn.Text = "▶ 运行检测";
            this.RunBtn.UseVisualStyleBackColor = false;
            this.RunBtn.Click += new System.EventHandler(this.RunBtn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.pipelineLayoutPanel);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.ForeColor = System.Drawing.Color.DarkOrange;
            this.groupBox5.Location = new System.Drawing.Point(3, 635);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(976, 104);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "流水线模拟";
            // 
            // pipelineLayoutPanel
            // 
            this.pipelineLayoutPanel.ColumnCount = 10;
            this.pipelineLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.pipelineLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 189F));
            this.pipelineLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.pipelineLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.pipelineLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.pipelineLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.pipelineLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.pipelineLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.pipelineLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.pipelineLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pipelineLayoutPanel.Controls.Add(this.lblFolder, 0, 0);
            this.pipelineLayoutPanel.Controls.Add(this.txtFolder, 1, 0);
            this.pipelineLayoutPanel.Controls.Add(this.btnBrowse, 2, 0);
            this.pipelineLayoutPanel.Controls.Add(this.lblInterval, 4, 0);
            this.pipelineLayoutPanel.Controls.Add(this.numInterval, 5, 0);
            this.pipelineLayoutPanel.Controls.Add(this.lblUnit, 6, 0);
            this.pipelineLayoutPanel.Controls.Add(this.lblRunCount, 7, 0);
            this.pipelineLayoutPanel.Controls.Add(this.numRunCount, 8, 0);
            this.pipelineLayoutPanel.Controls.Add(this.lblTimes, 9, 0);
            this.pipelineLayoutPanel.Controls.Add(this.btnStart, 0, 1);
            this.pipelineLayoutPanel.Controls.Add(this.btnStop, 1, 1);
            this.pipelineLayoutPanel.Controls.Add(this.btnRunOne, 6, 1);
            this.pipelineLayoutPanel.SetColumnSpan(this.btnRunOne, 2);
            this.pipelineLayoutPanel.Controls.Add(this.lblPlcIp, 2, 1);
            this.pipelineLayoutPanel.Controls.Add(this.txtPlcIp, 3, 1);
            this.pipelineLayoutPanel.Controls.Add(this.btnPlcConnect, 4, 1);
            this.pipelineLayoutPanel.Controls.Add(this.lblPlcStatus, 5, 1);
            this.pipelineLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pipelineLayoutPanel.Location = new System.Drawing.Point(3, 21);
            this.pipelineLayoutPanel.Name = "pipelineLayoutPanel";
            this.pipelineLayoutPanel.RowCount = 2;
            this.pipelineLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.pipelineLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.pipelineLayoutPanel.Size = new System.Drawing.Size(970, 80);
            this.pipelineLayoutPanel.TabIndex = 0;
            // 
            // lblFolder
            // 
            this.lblFolder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(3, 2);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(67, 30);
            this.lblFolder.TabIndex = 0;
            this.lblFolder.Text = "照片文件夹";
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtFolder.Location = new System.Drawing.Point(83, 5);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(183, 25);
            this.txtFolder.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(272, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(64, 29);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "浏览...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblInterval
            // 
            this.lblInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(477, 10);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(67, 15);
            this.lblInterval.TabIndex = 3;
            this.lblInterval.Text = "运行间隔";
            // 
            // numInterval
            // 
            this.numInterval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numInterval.Location = new System.Drawing.Point(555, 5);
            this.numInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new System.Drawing.Size(64, 25);
            this.numInterval.TabIndex = 4;
            this.numInterval.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblUnit
            // 
            this.lblUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(641, 10);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(22, 15);
            this.lblUnit.TabIndex = 5;
            this.lblUnit.Text = "秒";
            // 
            // lblRunCount
            // 
            this.lblRunCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRunCount.AutoSize = true;
            this.lblRunCount.Location = new System.Drawing.Point(704, 10);
            this.lblRunCount.Name = "lblRunCount";
            this.lblRunCount.Size = new System.Drawing.Size(82, 15);
            this.lblRunCount.TabIndex = 6;
            this.lblRunCount.Text = "连续运行数";
            // 
            // numRunCount
            // 
            this.numRunCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numRunCount.Location = new System.Drawing.Point(794, 5);
            this.numRunCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRunCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRunCount.Name = "numRunCount";
            this.numRunCount.Size = new System.Drawing.Size(94, 25);
            this.numRunCount.TabIndex = 7;
            this.numRunCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblTimes
            // 
            this.lblTimes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTimes.AutoSize = true;
            this.lblTimes.Location = new System.Drawing.Point(896, 10);
            this.lblTimes.Name = "lblTimes";
            this.lblTimes.Size = new System.Drawing.Size(22, 15);
            this.lblTimes.TabIndex = 8;
            this.lblTimes.Text = "次";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.LightGreen;
            this.btnStart.Location = new System.Drawing.Point(3, 38);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(74, 29);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "▶ 开始";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.LightCoral;
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(83, 38);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(74, 29);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "■ 停止";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRunOne
            // 
            this.btnRunOne.BackColor = System.Drawing.Color.Khaki;
            this.btnRunOne.Location = new System.Drawing.Point(516, 38);
            this.btnRunOne.Name = "btnRunOne";
            this.btnRunOne.Size = new System.Drawing.Size(150, 29);
            this.btnRunOne.TabIndex = 11;
            this.btnRunOne.Text = "▶ 单张运行";
            this.btnRunOne.UseVisualStyleBackColor = false;
            this.btnRunOne.Click += new System.EventHandler(this.btnRunOne_Click);
            // 
            // lblPlcIp
            // 
            this.lblPlcIp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPlcIp.AutoSize = true;
            this.lblPlcIp.Location = new System.Drawing.Point(272, 50);
            this.lblPlcIp.Name = "lblPlcIp";
            this.lblPlcIp.Size = new System.Drawing.Size(63, 15);
            this.lblPlcIp.TabIndex = 11;
            this.lblPlcIp.Text = "PLC IP:";
            // 
            // txtPlcIp
            // 
            this.txtPlcIp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPlcIp.Location = new System.Drawing.Point(344, 45);
            this.txtPlcIp.Name = "txtPlcIp";
            this.txtPlcIp.Size = new System.Drawing.Size(124, 25);
            this.txtPlcIp.TabIndex = 12;
            this.txtPlcIp.Text = "192.168.0.201";
            // 
            // btnPlcConnect
            // 
            this.btnPlcConnect.Location = new System.Drawing.Point(477, 38);
            this.btnPlcConnect.Name = "btnPlcConnect";
            this.btnPlcConnect.Size = new System.Drawing.Size(64, 29);
            this.btnPlcConnect.TabIndex = 13;
            this.btnPlcConnect.Text = "连接";
            this.btnPlcConnect.UseVisualStyleBackColor = true;
            this.btnPlcConnect.Click += new System.EventHandler(this.btnPlcConnect_Click);
            // 
            // lblPlcStatus
            // 
            this.lblPlcStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPlcStatus.AutoSize = true;
            this.lblPlcStatus.ForeColor = System.Drawing.Color.Red;
            this.lblPlcStatus.Location = new System.Drawing.Point(555, 50);
            this.lblPlcStatus.Name = "lblPlcStatus";
            this.lblPlcStatus.Size = new System.Drawing.Size(75, 15);
            this.lblPlcStatus.TabIndex = 14;
            this.lblPlcStatus.Text = "● 未连接";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.statsLayoutPanel);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 745);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(976, 205);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "检测结果";
            // 
            // statsLayoutPanel
            // 
            this.statsLayoutPanel.ColumnCount = 1;
            this.statsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statsLayoutPanel.Controls.Add(this.lblStats, 0, 0);
            this.statsLayoutPanel.Controls.Add(this.resultLayoutPanel, 0, 1);
            this.statsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statsLayoutPanel.Location = new System.Drawing.Point(3, 21);
            this.statsLayoutPanel.Name = "statsLayoutPanel";
            this.statsLayoutPanel.RowCount = 2;
            this.statsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.statsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statsLayoutPanel.Size = new System.Drawing.Size(970, 181);
            this.statsLayoutPanel.TabIndex = 0;
            // 
            // lblStats
            // 
            this.lblStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStats.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblStats.Location = new System.Drawing.Point(3, 0);
            this.lblStats.Name = "lblStats";
            this.lblStats.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblStats.Size = new System.Drawing.Size(964, 30);
            this.lblStats.TabIndex = 0;
            this.lblStats.Text = "统计: 总数=0  OK=0  NG=0  良率=0%";
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // resultLayoutPanel
            // 
            this.resultLayoutPanel.ColumnCount = 2;
            this.resultLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.resultLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.resultLayoutPanel.Controls.Add(this.BlobResultLabel, 0, 0);
            this.resultLayoutPanel.Controls.Add(this.CaliperResultLabel, 1, 0);
            this.resultLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultLayoutPanel.Location = new System.Drawing.Point(3, 33);
            this.resultLayoutPanel.Name = "resultLayoutPanel";
            this.resultLayoutPanel.RowCount = 1;
            this.resultLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.resultLayoutPanel.Size = new System.Drawing.Size(964, 145);
            this.resultLayoutPanel.TabIndex = 1;
            // 
            // BlobResultLabel
            // 
            this.BlobResultLabel.AutoSize = true;
            this.BlobResultLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BlobResultLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BlobResultLabel.Location = new System.Drawing.Point(3, 0);
            this.BlobResultLabel.Name = "BlobResultLabel";
            this.BlobResultLabel.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.BlobResultLabel.Size = new System.Drawing.Size(476, 145);
            this.BlobResultLabel.TabIndex = 0;
            this.BlobResultLabel.Text = "【圆孔检测】\r\n";
            // 
            // CaliperResultLabel
            // 
            this.CaliperResultLabel.AutoSize = true;
            this.CaliperResultLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CaliperResultLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CaliperResultLabel.Location = new System.Drawing.Point(485, 0);
            this.CaliperResultLabel.Name = "CaliperResultLabel";
            this.CaliperResultLabel.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.CaliperResultLabel.Size = new System.Drawing.Size(476, 145);
            this.CaliperResultLabel.TabIndex = 1;
            this.CaliperResultLabel.Text = "【边缘检测】\r\n";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 953);
            this.Controls.Add(this.mainLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(1000, 900);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视觉检测系统 - 进阶联合编程";
            this.mainLayoutPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.trainLayoutPanel.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.runLayoutPanel.ResumeLayout(false);
            this.runLayoutPanel.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.pipelineLayoutPanel.ResumeLayout(false);
            this.pipelineLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRunCount)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.statsLayoutPanel.ResumeLayout(false);
            this.resultLayoutPanel.ResumeLayout(false);
            this.resultLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel topLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel trainLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel runLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel pipelineLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel resultLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel statsLayoutPanel;
        private System.Windows.Forms.Button selectImageBtn;
        private System.Windows.Forms.Button btnAddCaliper;
        private System.Windows.Forms.Button btnRemoveCaliper;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox ModeSelectionBox;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.TextBox CountTxtBox;
        private System.Windows.Forms.Button RunBtn;
        private System.Windows.Forms.Button TrainBtn;
        private System.Windows.Forms.Label BlobResultLabel;
        private System.Windows.Forms.Label CaliperResultLabel;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblRunCount;
        private System.Windows.Forms.NumericUpDown numRunCount;
        private System.Windows.Forms.Label lblTimes;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRunOne;
        private System.Windows.Forms.Label lblPlcIp;
        private System.Windows.Forms.TextBox txtPlcIp;
        private System.Windows.Forms.Button btnPlcConnect;
        private System.Windows.Forms.Label lblPlcStatus;
        private System.Windows.Forms.Label lblStats;
    }
}
