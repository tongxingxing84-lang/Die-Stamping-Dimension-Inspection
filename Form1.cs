using _9._14INI;
using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.CalibFix;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace winform_进阶联合编程
{
    public partial class Form1 : Form
    {
        public enum Mode
        {
            All = 0,        //全部检测
            CheckCircle = 1,//圆孔检测
            CheckSide = 2   //检查尺寸
        }
        private Mode _currentMode = Mode.All;
        private CogRecordDisplay _leftDisplay = new CogRecordDisplay();
        private CogRecordDisplay _rightDisplay = new CogRecordDisplay();
        private CogImageFileTool _imageFileTool = new CogImageFileTool();
        // 统一转换为 8 位灰度图供视觉工具使用（PMAlign/Blob/Caliper 不支持彩色图）
        private CogImage8Grey _greyImage;
        //private CogRectangleAffine _pmAlignRegion = new CogRectangleAffine();   // PMAlign训练区域（蓝色）
        //private CogCircle _circleRegion = new CogCircle();
        
        // 卡尺区域列表（支持多个）
        private List<CogRectangleAffine> _caliperRegions = new List<CogRectangleAffine>();
        private int _caliperCounter = 0;  // 卡尺编号计数器
        private CogBlobTool _cogBlobTool = new CogBlobTool();
        private CogCaliperTool _cogCaliperTool = new CogCaliperTool();
        private CogPMAlignTool _cogPMAlignTool = new CogPMAlignTool();
        private CogFixtureTool _cogFixtureTool = new CogFixtureTool();
        
        // 训练时保存的标准值
        private double _caliperTolerance = 5.0;    // 卡尺公差（像素）
        private List<double> _caliperStandardWidths = new List<double>();  // 每个卡尺的标准宽度
        
        // 模板与待检图状态
        private ICogImage _templateImage;        // 训练时锁定的模板图（左图固定显示）
        private bool _isTrained = false;         // 是否已完成训练
        private bool _hasPendingImage = false;   // 训练后是否已选择待检图
        private bool _calipersChanged = false;   // 训练后是否修改过卡尺
        
        // PLC通信
        private PlcClient _plcClient;
        
        // 流水线模拟
        private List<string> _imageFiles = new List<string>();
        private int _currentImageIndex = 0;
        private int _pipelineProcessed = 0;      // 本轮流水线已处理张数
        private int _runLimit = 0;               // 本轮流水线计划处理张数
        private bool _isRunning = false;
        private System.Windows.Forms.Timer _pipelineTimer;
        
        // 统计数据
        private int _totalCount = 0;
        private int _okCount = 0;
        private int _ngCount = 0;
        private bool _isProcessing = false;      // 防止检测重入

        //存放区域的字典，方便后续使用
        public Dictionary<string, ICogRegion> regionDictionary = new Dictionary<string, ICogRegion>();
        private Dictionary<string, Mode> _modeMap = new Dictionary<string, Mode>
        {
            { "全部检测", Mode.All },
            { "圆孔检测", Mode.CheckCircle },
            { "边缘检测", Mode.CheckSide },
        };

        public Form1()
        {
            InitializeComponent();

            this.Text = "基于机器视觉的冲压件尺寸检测系统";

            LoadControls();
            CountTxtBox.Text = ApplicationConfig.Counts.ToString();
            ModeSelectionBox.SelectedValueChanged += ModeSelectionBox_SelectedValueChanged;
            
            // 初始化PLC客户端
            _plcClient = new PlcClient();
            _plcClient.ConnectionStateChanged += OnPlcConnectionChanged;
            
            // 初始化定时器
            _pipelineTimer = new System.Windows.Forms.Timer();
            _pipelineTimer.Tick += PipelineTimer_Tick;
        }
        
        // PLC连接状态变化
        private void OnPlcConnectionChanged(bool connected)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<bool>(OnPlcConnectionChanged), connected);
                return;
            }
            
            if (connected)
            {
                lblPlcStatus.Text = "● 已连接";
                lblPlcStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblPlcStatus.Text = "● 未连接";
                lblPlcStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        
        // 浏览文件夹
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "选择照片文件夹";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolder.Text = dialog.SelectedPath;
                    LoadImageFiles(dialog.SelectedPath);
                }
            }
        }
        
        // 加载文件夹中的图片
        private void LoadImageFiles(string folderPath)
        {
            _imageFiles.Clear();
            _currentImageIndex = 0;
            
            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show("文件夹不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var extensions = new[] { ".bmp", ".jpg", ".jpeg", ".png", ".tif", ".tiff" };
            var files = Directory.GetFiles(folderPath)
                .Where(f => extensions.Contains(Path.GetExtension(f).ToLower()))
                .OrderBy(f => f)
                .ToList();
            
            if (files.Count == 0)
            {
                MessageBox.Show("文件夹中没有找到图片文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            _imageFiles = files;
            _pipelineProcessed = 0;
            
            // 未训练时清空显示，提示用户自己挑一张合格样件作模板（不自动采用第一张）
            if (!_isTrained)
            {
                _greyImage = null;
                _leftDisplay.Image = null;
                _rightDisplay.Image = null;
                _rightDisplay.StaticGraphics.Clear();
                MessageBox.Show($"已加载 {files.Count} 张图片。\n请点「选择图像」挑选一张合格样件作为模板，添加卡尺后点「训练模板」。",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"已加载 {files.Count} 张图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        // 将任意图像转换为 8 位灰度图（VisionPro 视觉工具只支持灰度图）
        private CogImage8Grey ToGrey(ICogImage image)
        {
            if (image == null) return null;
            
            // 本身已是灰度图，直接返回
            if (image is CogImage8Grey grey)
                return grey;
            
            CogImageConvertTool convertTool = new CogImageConvertTool();
            convertTool.InputImage = image;
            convertTool.RunParams.RunMode = CogImageConvertRunModeConstants.Intensity;
            convertTool.Run();
            
            return convertTool.OutputImage as CogImage8Grey;
        }
        
        // PLC连接按钮
        private async void btnPlcConnect_Click(object sender, EventArgs e)
        {
            string ip = txtPlcIp.Text.Trim();
            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("请输入PLC IP地址！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            try
            {
                btnPlcConnect.Enabled = false;
                btnPlcConnect.Text = "连接中...";
                
                bool success = await _plcClient.ConnectAsync(ip);
                
                if (success)
                {
                    MessageBox.Show("PLC连接成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"连接失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnPlcConnect.Enabled = true;
                btnPlcConnect.Text = _plcClient.IsConnected ? "已连接" : "连接";
            }
        }
        
        // 开始连续运行
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_imageFiles.Count == 0)
            {
                MessageBox.Show("请先选择照片文件夹！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (!_isTrained)
            {
                MessageBox.Show("请先训练模板！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (_calipersChanged)
            {
                MessageBox.Show("卡尺已修改，请重新训练模板！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (_currentMode != Mode.CheckCircle && _caliperRegions.Count == 0)
            {
                MessageBox.Show("请先添加至少一个卡尺！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (_isRunning || _isProcessing) return;
            
            int expectedCount;
            if (!int.TryParse(CountTxtBox.Text, out expectedCount) || expectedCount <= 0)
            {
                MessageBox.Show("请输入有效的期望孔数！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // 重置统计（仅开始连续运行时清零）
            _totalCount = 0;
            _okCount = 0;
            _ngCount = 0;
            _currentImageIndex = 0;
            _pipelineProcessed = 0;
            
            // 本轮运行张数：取“连续运行数”与文件夹图片数的较小值
            _runLimit = Math.Min(_imageFiles.Count, (int)numRunCount.Value);
            
            // 设置定时器间隔
            int interval = (int)(numInterval.Value * 1000);
            _pipelineTimer.Interval = interval;
            
            // 更新UI状态：运行期间锁住会改变模板/图集/模式的控件
            _isRunning = true;
            SetUiRunning(true);
            
            // 启动定时器并立即执行第一次
            _pipelineTimer.Start();
            PipelineTimer_Tick(null, EventArgs.Empty);
        }
        
        // 停止连续运行
        private void btnStop_Click(object sender, EventArgs e)
        {
            _isRunning = false;
            _pipelineTimer.Stop();
            SetUiRunning(false);
        }
        
        // 流水线运行期间统一锁定/恢复相关控件
        private void SetUiRunning(bool running)
        {
            btnBrowse.Enabled = !running;
            txtFolder.Enabled = !running;
            selectImageBtn.Enabled = !running;
            TrainBtn.Enabled = !running;
            btnAddCaliper.Enabled = !running;
            btnRemoveCaliper.Enabled = !running;
            ModeSelectionBox.Enabled = !running;
            CountTxtBox.Enabled = !running;
            numInterval.Enabled = !running;
            numRunCount.Enabled = !running;
            RunBtn.Enabled = !running;
            btnRunOne.Enabled = !running;
            btnStart.Enabled = !running;
            btnStop.Enabled = running;
        }
        
        // 单张运行：检测当前张，然后前进一张（末尾回到第一张）
        private async void btnRunOne_Click(object sender, EventArgs e)
        {
            if (_imageFiles.Count == 0)
            {
                MessageBox.Show("请先选择照片文件夹！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (!_isTrained)
            {
                MessageBox.Show("请先训练模板！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (_calipersChanged)
            {
                MessageBox.Show("卡尺已修改，请重新训练模板！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (_currentMode != Mode.CheckCircle && _caliperRegions.Count == 0)
            {
                MessageBox.Show("请先添加至少一个卡尺！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (_isRunning)
            {
                MessageBox.Show("连续运行中，请先停止！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (_isProcessing) return;
            
            _isProcessing = true;
            try
            {
                await ProcessCurrentFolderImageAsync();
            }
            finally
            {
                _isProcessing = false;
            }
        }
        
        // 与 PLC 完成一次完整握手：
        // ① 写入 Trigger + Result → ② 等待 PLC 置位 Completed
        // → ③ 清除 Trigger → ④ 等待 PLC 清除 Completed
        private async Task<bool> HandshakeWithPlcAsync(bool isQualified)
        {
            if (!_plcClient.IsConnected) return false;

            try
            {
                // ① 写入检测结果（Trigger=1 + Result）
                await _plcClient.WriteResultAsync(isQualified);

                // ② 等待 PLC 处理完成（最多 2 秒）
                int waited = 0;
                bool completed = false;
                while (waited < 2000)
                {
                    if (await _plcClient.ReadCompletedSignalAsync())
                    {
                        completed = true;
                        break;
                    }
                    await Task.Delay(20);
                    waited += 20;
                }

                if (!completed)
                {
                    // 超时：PLC 没应答，把触发信号清掉，避免卡死
                    await _plcClient.ClearTriggerSignalAsync();
                    return false;
                }

                // ③ 清除 Trigger，通知 PLC 进入下一轮
                await _plcClient.ClearTriggerSignalAsync();

                // ④ 等待 PLC 清除 Completed（最多 1 秒）
                waited = 0;
                while (waited < 1000)
                {
                    if (!await _plcClient.ReadCompletedSignalAsync())
                        break;
                    await Task.Delay(20);
                    waited += 20;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PLC握手异常：{ex.Message}");
                return false;
            }
        }

        // 处理当前索引对应的一张图片（含统计与PLC握手），完成后索引前进
        private async Task ProcessCurrentFolderImageAsync()
        {
            if (_imageFiles.Count == 0) return;
            
            _currentImageIndex %= _imageFiles.Count;
            string imagePath = _imageFiles[_currentImageIndex];
            bool isQualified = false;
            
            try
            {
                _imageFileTool.Operator.Open(imagePath, CogImageFileModeConstants.Read);
                _imageFileTool.Run();
                _greyImage = ToGrey(_imageFileTool.OutputImage);
                
                if (_greyImage == null)
                {
                    throw new InvalidOperationException("图像转换为灰度图失败");
                }
                
                isQualified = RunDetection();
            }
            catch (Exception ex)
            {
                isQualified = false;
                System.Diagnostics.Debug.WriteLine($"处理图片失败：{ex.Message}");
                _rightDisplay.Image = _imageFileTool.OutputImage;
                _rightDisplay.StaticGraphics.Clear();
                BlobResultLabel.Text = $"【检测异常】{Path.GetFileName(imagePath)}\n{ex.Message}";
                CaliperResultLabel.Text = "";
                DrawVerdict(false, "检测异常");
                _rightDisplay.Fit();
                _rightDisplay.Refresh();
            }
            
            // 更新统计
            _totalCount++;
            if (isQualified)
                _okCount++;
            else
                _ngCount++;
            UpdateStats();
            
            // 与PLC握手（写结果 → 等完成 → 清触发）
            if (_plcClient.IsConnected)
            {
                await HandshakeWithPlcAsync(isQualified);
            }
            
            // 下一张图片（循环）
            _currentImageIndex = (_currentImageIndex + 1) % _imageFiles.Count;
        }
        
        // 结束连续运行
        private void FinishPipeline()
        {
            _isRunning = false;
            _pipelineTimer.Stop();
            SetUiRunning(false);
            
            MessageBox.Show($"流水线模拟完成！\n已处理: {_totalCount}\nOK: {_okCount}\nNG: {_ngCount}\n良率: {(_totalCount > 0 ? (double)_okCount / _totalCount * 100 : 0):F1}%", 
                "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        // 定时器触发 - 执行一次检测
        private async void PipelineTimer_Tick(object sender, EventArgs e)
        {
            if (!_isRunning || _isProcessing) return;
            
            int limit = (_runLimit > 0) ? _runLimit : _imageFiles.Count;
            if (_pipelineProcessed >= limit)
            {
                FinishPipeline();
                return;
            }
            
            _isProcessing = true;
            _pipelineTimer.Stop();
            try
            {
                await ProcessCurrentFolderImageAsync();
                _pipelineProcessed++;
            }
            finally
            {
                _isProcessing = false;
                
                if (_isRunning)
                {
                    if (_pipelineProcessed >= limit)
                        FinishPipeline();
                    else
                        _pipelineTimer.Start();
                }
            }
        }
        
        // 更新统计显示
        private void UpdateStats()
        {
            double yield = _totalCount > 0 ? (double)_okCount / _totalCount * 100 : 0;
            string current = "";
            if (_imageFiles.Count > 0)
            {
                if (_isRunning)
                {
                    int limit = (_runLimit > 0) ? _runLimit : _imageFiles.Count;
                    int shown = Math.Min(_pipelineProcessed + (_isProcessing ? 1 : 0), limit);
                    current = $"  进度={shown}/{limit}";
                }
                else
                {
                    current = $"  待检=#{Math.Min(_currentImageIndex + 1, _imageFiles.Count)}/{_imageFiles.Count}";
                }
            }
            lblStats.Text = $"统计: 总数={_totalCount}  OK={_okCount}  NG={_ngCount}  良率={yield:F1}%{current}";
        }

        //模式选择框
        private void ModeSelectionBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string text = ModeSelectionBox.SelectedItem?.ToString();
            if (text == null) return;

            if (_modeMap.TryGetValue(text, out Mode mode))
            {
                _currentMode = mode;
            }
        }

        //加载控件
        private void LoadControls()
        {
            _leftDisplay.Dock = DockStyle.Fill;
            topLayoutPanel.Controls.Add(_leftDisplay, 0, 0);


            _rightDisplay.Dock = DockStyle.Fill;
            topLayoutPanel.Controls.Add(_rightDisplay, 1, 0);

            string[] ModeSelection = { "全部检测", "圆孔检测", "边缘检测" };
            ModeSelectionBox.Items.AddRange(ModeSelection);
            ModeSelectionBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ModeSelectionBox.SelectedIndex = 0;
        }

        //图像资源加载
        private void selectImageBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"E:\机器视觉\知识库\Vp知识\快学visionpro 素材_202204";
                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    _imageFileTool.Operator.Open(filePath, CogImageFileModeConstants.Read);
                    _imageFileTool.Run();
                    _greyImage = ToGrey(_imageFileTool.OutputImage);

                    _rightDisplay.StaticGraphics.Clear();
                    BlobResultLabel.Text = "";
                    CaliperResultLabel.Text = "";

                    if (_isTrained)
                    {
                        // 已训练：新图只作为待检图显示在右图，左图保持模板
                        _hasPendingImage = true;
                        _rightDisplay.Image = _imageFileTool.OutputImage;
                        _rightDisplay.Fit();
                    }
                    else
                    {
                        // 未训练：显示在左图，用于添加卡尺和训练模板
                        _rightDisplay.Image = null;
                        _leftDisplay.Image = _imageFileTool.OutputImage;
                        _leftDisplay.Fit();
                    }
                }
            }
        }

        //增加卡尺
        private void btnAddCaliper_Click(object sender, EventArgs e)
        {
            ICogImage referenceImage = _templateImage ?? _imageFileTool.OutputImage;
            if (referenceImage == null)
            {
                MessageBox.Show("请先加载图像！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            _caliperCounter++;
            var caliperRegion = new CogRectangleAffine();
            caliperRegion.CenterX = referenceImage.Width / 2.0;
            caliperRegion.CenterY = referenceImage.Height / 2.0;
            caliperRegion.SideXLength = 50;
            caliperRegion.SideYLength = 50;
            caliperRegion.Color = CogColorConstants.Green;
            caliperRegion.LineWidthInScreenPixels = 2;
            caliperRegion.Interactive = true;
            caliperRegion.GraphicDOFEnable = CogRectangleAffineDOFConstants.All;
            
            _caliperRegions.Add(caliperRegion);
            _leftDisplay.InteractiveGraphics.Add(caliperRegion, $"Caliper{_caliperCounter}", false);
            _leftDisplay.Refresh();
            
            if (_isTrained)
            {
                _calipersChanged = true;
            }
            
            MessageBox.Show($"已添加卡尺{_caliperCounter}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        //删除卡尺
        private void btnRemoveCaliper_Click(object sender, EventArgs e)
        {
            if (_caliperRegions.Count == 0)
            {
                MessageBox.Show("没有可删除的卡尺！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // 删除最后一个卡尺
            string caliperName = $"Caliper{_caliperCounter}";
            _caliperRegions.RemoveAt(_caliperRegions.Count - 1);
            
            // 从显示中移除（使用分组名称）
            _leftDisplay.InteractiveGraphics.Remove(caliperName);
            _caliperCounter--;
            
            _leftDisplay.Refresh();
            
            if (_isTrained)
            {
                _calipersChanged = true;
            }
            
            MessageBox.Show($"已删除卡尺{_caliperCounter + 1}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //运行按钮（检测「选择图像」选中的待检图）
        private async void RunBtn_Click(object sender, EventArgs e)
        {
            if (!_isTrained)
            {
                MessageBox.Show("请先训练模板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (!_hasPendingImage || _imageFileTool.OutputImage == null || _greyImage == null)
            {
                MessageBox.Show("请先选择待检图像！\n训练完成后再点「选择图像」，新图会显示在右侧。",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (_calipersChanged)
            {
                MessageBox.Show("卡尺已修改，请重新训练模板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (_isRunning)
            {
                MessageBox.Show("流水线正在运行，请先停止！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (_isProcessing) return;
            
            _isProcessing = true;
            try
            {
                bool isQualified = RunDetection();
                _hasPendingImage = false;
                
                // 更新统计
                _totalCount++;
                if (isQualified) _okCount++;
                else _ngCount++;
                UpdateStats();
                
                // 与PLC握手：写结果 → 等完成 → 清触发
                if (_plcClient.IsConnected)
                {
                    await HandshakeWithPlcAsync(isQualified);
                }
            }
            finally
            {
                _isProcessing = false;
            }
        }

        //执行一次检测（右图显示待检图与结果，左图保持模板）
        private bool RunDetection()
        {
            _rightDisplay.Image = _imageFileTool.OutputImage;
            _rightDisplay.StaticGraphics.Clear();
            BlobResultLabel.Text = "";
            CaliperResultLabel.Text = "";

            bool isQualified = false;

            // 先运行 PMAlign 获取产品位姿
            if (!RunPMAlignForFixture())
            {
                BlobResultLabel.Text = "【检测失败】未找到匹配的产品，请检查产品是否在视野内。";
                DrawVerdict(false, "未找到产品");
                _rightDisplay.Fit();
                _rightDisplay.Refresh();
                return false;
            }

            switch (_currentMode)
            {
                case Mode.All:
                    ApplyExpectedCount();
                    // 分开调用，避免 && 短路导致卡尺不执行
                    {
                        bool blobOk = CheckCircle();
                        bool sideOk = CheckSide();
                        isQualified = blobOk && sideOk;
                    }
                    break;
                case Mode.CheckCircle:
                    ApplyExpectedCount();
                    isQualified = CheckCircle();
                    break;
                case Mode.CheckSide:
                    isQualified = CheckSide();
                    break;
            }

            DrawVerdict(isQualified);
            _rightDisplay.Fit();
            _rightDisplay.Refresh();

            return isQualified;
        }

        // 读取并保存期望孔数
        private void ApplyExpectedCount()
        {
            int count;
            if (int.TryParse(CountTxtBox.Text, out count) && count > 0)
            {
                try
                {
                    ApplicationConfig.WriteConfig(count);
                    ApplicationConfig.ReadConfig();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"读写配置文件失败：{ex.Message}");
                }
            }
        }

        //运行 PMAlign 获取位姿并建立随动坐标系
        private bool RunPMAlignForFixture()
        {
            try
            {
                if (_greyImage == null)
                {
                    System.Diagnostics.Debug.WriteLine("图像转换为灰度图失败！");
                    return false;
                }
                
                // 运行 PMAlign 搜索匹配（必须使用 8 位灰度图）
                _cogPMAlignTool.InputImage = _greyImage;
                _cogPMAlignTool.Run();

                // 检查是否有匹配结果
                if (_cogPMAlignTool.Results == null || _cogPMAlignTool.Results.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("未找到匹配的产品！");
                    return false;
                }

                // 获取位姿并建立随动坐标系
                _cogFixtureTool.InputImage = _greyImage;
                _cogFixtureTool.RunParams.UnfixturedFromFixturedTransform = _cogPMAlignTool.Results[0].GetPose();
                _cogFixtureTool.RunParams.Action = CogFixtureActionConstants.EstablishNewFixture;
                _cogFixtureTool.RunParams.FixturedSpaceName = "fixture";
                _cogFixtureTool.Run();

                // 在右图上显示匹配位置
                var pose = _cogPMAlignTool.Results[0].GetPose();
                CogPointMarker marker = new CogPointMarker();
                marker.X = pose.TranslationX;
                marker.Y = pose.TranslationY;
                marker.Color = CogColorConstants.Yellow;
                _rightDisplay.StaticGraphics.Add(marker, "MatchPosition");

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"建立随动坐标系失败：{ex.Message}");
                return false;
            }
        }

        //圆孔检测
        private bool CheckCircle()
        {
            //如果没有加载图像，直接返回
            if (_imageFileTool.OutputImage == null) return false;
            
            _cogBlobTool.RunParams.RunTimeMeasures.Clear();
            //设置的是图像分割模式为 硬阈值
            _cogBlobTool.RunParams.SegmentationParams.Mode = CogBlobSegmentationModeConstants.HardDynamicThreshold;
            //设置的是斑点的极性 找的是什么颜色的斑点
            _cogBlobTool.RunParams.SegmentationParams.Polarity = CogBlobSegmentationPolarityConstants.DarkBlobs;
            //斑点工具使用全图检测（Region = null）
            _cogBlobTool.Region = null;

            //添加"非圆度"筛选：0.9~1.1 之间才算合格圆孔
            CogBlobMeasure measure = new CogBlobMeasure();
            measure.Mode = CogBlobMeasureModeConstants.Filter;              // 筛选模式
            measure.FilterMode = CogBlobFilterModeConstants.IncludeBlobsInRange;  // 范围内才保留
            measure.Measure = CogBlobMeasureConstants.AcircularityRms;      // 非圆度 RMS
            measure.FilterRangeLow = 0.9;
            measure.FilterRangeHigh = 1.1;
            _cogBlobTool.RunParams.RunTimeMeasures.Add(measure);

            // 使用 CogFixtureTool 包装 BlobTool 实现随动坐标系
            _cogFixtureTool.InputImage = _greyImage;
            _cogFixtureTool.RunParams.UnfixturedFromFixturedTransform = _cogPMAlignTool.Results[0].GetPose();
            _cogFixtureTool.RunParams.Action = CogFixtureActionConstants.EstablishNewFixture;
            _cogFixtureTool.RunParams.FixturedSpaceName = "fixture";
            
            _cogBlobTool.InputImage = _cogFixtureTool.OutputImage;
            _cogBlobTool.Run();

            return ShowBlobResultOnRight(ApplicationConfig.Counts);
        }

        //圆孔检测结果显示
        private bool ShowBlobResultOnRight(int Counts)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("【圆孔检测】");

            bool isQualified = true;  // 是否合格
            int foundCount = 0;

            if (_cogBlobTool.Results != null)
            {
                int index = 0;
                foreach (CogBlobResult blob in _cogBlobTool.Results.GetBlobs())
                {
                    index++;
                    if (index > Counts) break;
                    foundCount++;
                    
                    // 根据是否合格选择颜色
                    ICogGraphicInteractive graphics = blob.CreateResultGraphics(CogBlobResultGraphicConstants.Boundary);
                    graphics.Color = CogColorConstants.Green;  // 合格：绿色
                    _rightDisplay.StaticGraphics.Add(graphics, $"Blob{index}");

                    double cx = blob.CenterOfMassX;
                    double cy = blob.CenterOfMassY;
                    double area = blob.Area;

                    sb.AppendLine($"  圆孔{index}: 质心=({cx:F1}, {cy:F1})  面积={area:F0}");
                }
                
                // 判定：孔数是否足够
                if (foundCount < Counts)
                {
                    isQualified = false;
                    sb.AppendLine($"  ❌ 孔数不足！期望{Counts}个，实际{foundCount}个");
                }
                else
                {
                    sb.AppendLine($"  ✓ 孔数合格：{foundCount}/{Counts}");
                }
            }
            else
            {
                isQualified = false;
                sb.AppendLine("  ❌ 未找到圆孔");
            }

            BlobResultLabel.Text = sb.ToString();
            
            return isQualified;
        }

        //边缘检测（遍历所有卡尺区域）
        private bool CheckSide()
        {
            if (_imageFileTool.OutputImage == null) return false;
            if (_caliperRegions.Count == 0) return false;
            
            // 使用 CogFixtureTool 建立随动坐标系
            _cogFixtureTool.InputImage = _greyImage;
            _cogFixtureTool.RunParams.UnfixturedFromFixturedTransform = _cogPMAlignTool.Results[0].GetPose();
            _cogFixtureTool.RunParams.Action = CogFixtureActionConstants.EstablishNewFixture;
            _cogFixtureTool.RunParams.FixturedSpaceName = "fixture";
            _cogFixtureTool.Run();
            
            bool allQualified = true;
            
            // 遍历所有卡尺区域
            for (int i = 0; i < _caliperRegions.Count; i++)
            {
                var region = _caliperRegions[i];
                
                //设置输入图片为随动坐标系的输出
                _cogCaliperTool.InputImage = (CogImage8Grey)_cogFixtureTool.OutputImage;
                //设置搜索区域为当前卡尺区域
                _cogCaliperTool.Region = region;
                //把边缘模式改为"边缘对"
                _cogCaliperTool.RunParams.EdgeMode = CogCaliperEdgeModeConstants.Pair;
                //设置第一条边和第二条边的极性
                _cogCaliperTool.RunParams.Edge0Polarity = CogCaliperPolarityConstants.DarkToLight;
                _cogCaliperTool.RunParams.Edge1Polarity = CogCaliperPolarityConstants.LightToDark;
                //对比度阈值
                _cogCaliperTool.RunParams.ContrastThreshold = 10;
                //边缘对宽度
                _cogCaliperTool.RunParams.Edge0Position = -600;
                _cogCaliperTool.RunParams.Edge1Position = 600;

                _cogCaliperTool.Run();
                
                // 获取当前卡尺的标准宽度
                double standardWidth = (i < _caliperStandardWidths.Count) ? _caliperStandardWidths[i] : 0;
                
                // 显示当前卡尺的结果
                if (!ShowCaliperResultForRegion(i + 1, standardWidth))
                {
                    allQualified = false;
                }
            }
            
            return allQualified;
        }

        //显示单个卡尺的检测结果
        private bool ShowCaliperResultForRegion(int caliperIndex, double standardWidth)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"【卡尺{caliperIndex}】");

            bool isQualified = true;

            if (_cogCaliperTool.Results != null)
            {
                foreach (CogCaliperResult result in _cogCaliperTool.Results)
                {
                    CogCompositeShape graphics = result.CreateResultGraphics(
                        CogCaliperResultGraphicConstants.Edges);

                    double width = result.Width;
                    double posX = result.PositionX;
                    double posY = result.PositionY;

                    // 判定：卡尺宽度是否在公差范围内
                    if (standardWidth > 0)
                    {
                        double deviation = Math.Abs(width - standardWidth);
                        if (deviation > _caliperTolerance)
                        {
                            isQualified = false;
                            graphics.Color = CogColorConstants.Red;
                            sb.AppendLine($"  ❌ 宽度={width:F2}px  偏差={deviation:F2}px (超出公差{_caliperTolerance:F1}px)");
                        }
                        else
                        {
                            graphics.Color = CogColorConstants.Green;
                            sb.AppendLine($"  ✓ 宽度={width:F2}px  偏差={deviation:F2}px (合格)");
                        }
                    }
                    else
                    {
                        sb.AppendLine($"  宽度={width:F2}px  中点=({posX:F1}, {posY:F1})");
                    }

                    _rightDisplay.StaticGraphics.Add(graphics, $"Caliper{caliperIndex}_Edge");
                }
            }
            else
            {
                isQualified = false;
                sb.AppendLine("  ❌ 未找到边缘");
            }

            // 追加到结果标签
            CaliperResultLabel.Text += sb.ToString() + "\n";
            
            return isQualified;
        }

        // 绘制统一结论标签（合格 / 不合格）
        private void DrawVerdict(bool isQualified, string reason = null)
        {
            ICogImage image = _rightDisplay.Image ?? _imageFileTool.OutputImage;
            double centerX = (image != null) ? image.Width / 2.0 : 0;
            double centerY = (image != null) ? image.Height / 2.0 : 0;
            
            CogGraphicLabel label = new CogGraphicLabel();
            label.Color = isQualified ? CogColorConstants.Green : CogColorConstants.Red;
            label.Font = new Font("微软雅黑", 24, FontStyle.Bold);
            label.X = centerX;
            label.Y = centerY - 30;
            label.Text = isQualified ? "合格" : (string.IsNullOrEmpty(reason) ? "不合格" : $"不合格（{reason}）");
            
            _rightDisplay.StaticGraphics.Add(label, "VerdictLabel");
        }

        //模板匹配工具
        private bool PMAlign()
        {
            // 检查是否已加载图像
            if (_imageFileTool.OutputImage == null)
            {
                MessageBox.Show("请先加载图像！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            if (_greyImage == null)
            {
                MessageBox.Show("图像转换为灰度图失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            try
            {
                // 设置输入图像（必须先设置，且必须是 8 位灰度图）
                _cogPMAlignTool.InputImage = _greyImage;
                
                // 设置训练图像
                _cogPMAlignTool.Pattern.TrainImage = _greyImage;
                
                // 设置训练区域（null表示全图训练）
                _cogPMAlignTool.Pattern.TrainRegion = null;
                
                // 设置搜索区域（null表示全图搜索）
                _cogPMAlignTool.SearchRegion = null;
                
                // 基本参数
                _cogPMAlignTool.RunParams.ApproximateNumberToFind = 1;
                _cogPMAlignTool.RunParams.AcceptThreshold = 0.3;  // 降低阈值提高成功率
                _cogPMAlignTool.Pattern.TrainAlgorithm = CogPMAlignTrainAlgorithmConstants.PatMaxAndPatQuick;

                // 开启缩放搜索（0.8到1.2倍）
                _cogPMAlignTool.RunParams.ZoneScale.Low = 0.8;
                _cogPMAlignTool.RunParams.ZoneScale.High = 1.2;
                _cogPMAlignTool.RunParams.ZoneScale.Configuration = CogPMAlignZoneConstants.LowHigh;

                // 开启角度搜索（-180°到180°，即 -π 到 π 弧度）
                _cogPMAlignTool.RunParams.ZoneAngle.Low = -Math.PI;
                _cogPMAlignTool.RunParams.ZoneAngle.High = Math.PI;
                _cogPMAlignTool.RunParams.ZoneAngle.Configuration = CogPMAlignZoneConstants.LowHigh;

                // 训练模板
                _cogPMAlignTool.Pattern.Train();
                
                // 检查训练是否成功
                if (!_cogPMAlignTool.Pattern.Trained)
                {
                    MessageBox.Show("模板训练失败！\n可能原因：\n1. 训练区域特征不明显\n2. 图像对比度太低\n3. 训练区域太小", 
                        "训练失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                // 用训练好的模板搜索匹配
                _cogPMAlignTool.Run();
                
                // 检查是否有匹配结果
                if (_cogPMAlignTool.Results == null || _cogPMAlignTool.Results.Count == 0)
                {
                    MessageBox.Show("模板匹配失败！未找到匹配结果。\n请检查训练区域是否包含清晰的特征。", 
                        "匹配失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"模板训练异常：{ex.Message}\n\n详细信息：{ex.InnerException?.Message}", 
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //随动坐标系
        private void Fixture()
        {
            _cogFixtureTool.InputImage = _greyImage;
            _cogFixtureTool.RunParams.UnfixturedFromFixturedTransform = _cogPMAlignTool.Results[0].GetPose();
            _cogFixtureTool.RunParams.Action = CogFixtureActionConstants.EstablishNewFixture;
            _cogFixtureTool.RunParams.FixturedSpaceName = "fixture";
            _cogFixtureTool.Run();
            
            // 训练后获取所有卡尺的标准宽度
            GetStandardCaliperWidths();
            
            MessageBox.Show($"训练成功!!!\n已保存 {_caliperStandardWidths.Count} 个卡尺的标准宽度。");
        }
        
        // 获取所有卡尺的标准宽度
        private void GetStandardCaliperWidths()
        {
            _caliperStandardWidths.Clear();
            
            // 使用随动坐标系运行每个卡尺检测
            _cogFixtureTool.InputImage = _greyImage;
            _cogFixtureTool.RunParams.UnfixturedFromFixturedTransform = _cogPMAlignTool.Results[0].GetPose();
            _cogFixtureTool.RunParams.Action = CogFixtureActionConstants.EstablishNewFixture;
            _cogFixtureTool.RunParams.FixturedSpaceName = "fixture";
            _cogFixtureTool.Run();
            
            foreach (var region in _caliperRegions)
            {
                _cogCaliperTool.InputImage = (CogImage8Grey)_cogFixtureTool.OutputImage;
                _cogCaliperTool.Region = region;
                _cogCaliperTool.RunParams.EdgeMode = CogCaliperEdgeModeConstants.Pair;
                _cogCaliperTool.RunParams.Edge0Polarity = CogCaliperPolarityConstants.DarkToLight;
                _cogCaliperTool.RunParams.Edge1Polarity = CogCaliperPolarityConstants.LightToDark;
                _cogCaliperTool.RunParams.ContrastThreshold = 10;
                _cogCaliperTool.RunParams.Edge0Position = -600;
                _cogCaliperTool.RunParams.Edge1Position = 600;
                _cogCaliperTool.Run();
                
                if (_cogCaliperTool.Results != null && _cogCaliperTool.Results.Count > 0)
                {
                    _caliperStandardWidths.Add(_cogCaliperTool.Results[0].Width);
                }
                else
                {
                    _caliperStandardWidths.Add(0);  // 未检测到边缘
                }
            }
        }
        
        private void TrainBtn_Click(object sender, EventArgs e)
        {
            if (_imageFileTool.OutputImage == null || _greyImage == null)
            {
                MessageBox.Show("请先加载模板图像！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // PMAlign 返回 true 表示训练+匹配都成功
            bool success = PMAlign();
            if (!success)
            {
                return;
            }
            
            // 匹配成功，建立随动坐标系
            Fixture();
            
            // 锁定模板：左图固定显示模板，后续检测只在右图显示
            _templateImage = _imageFileTool.OutputImage;
            _isTrained = true;
            _hasPendingImage = false;
            _calipersChanged = false;
            _leftDisplay.Image = _templateImage;
            _leftDisplay.Fit();
            _leftDisplay.Refresh();
            
            // 清空右图，避免残留上一次的检测结果
            _rightDisplay.Image = null;
            _rightDisplay.StaticGraphics.Clear();
            BlobResultLabel.Text = "";
            CaliperResultLabel.Text = "";
            _rightDisplay.Refresh();
        }
    }
}
