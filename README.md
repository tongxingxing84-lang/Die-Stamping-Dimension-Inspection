# 基于 VisionPro 与 PLC 的视觉检测系统

本科毕业设计：使用 WinForms + Cognex VisionPro 做视觉检测，通过 S7.Net 与西门子 S7-1200/1500 PLC 通信，模拟流水线连续检测。

## 功能

### 视觉检测

- 选择图片文件夹，按顺序加载图片
- 左侧固定显示训练模板，右侧显示待检图与检测结果
- 三种检测模式：
  - 全部检测
  - 圆孔检测（Blob 硬阈值分割 + 孔数判断）
  - 尺寸检测（Caliper 卡尺测量）
- PMAlign 模板匹配定位，配合 Fixture 建立随动坐标系
- 支持添加 / 删除多个卡尺，训练时记录每个卡尺的标准宽度
- 尺寸公差默认 5 像素，可在界面调整

### 运行与统计

- 单张检测
- 连续运行（可设间隔、运行张数）
- 流水线模拟：定时器驱动、处理进度、循环取图
- 统计 OK / NG / 总数，运行期间自动锁定相关控件

### PLC 通信

- 使用 S7.NetPlus 连接西门子 S7-1200/1500
- 界面输入 PLC IP，异步连接 / 断开
- 检测完成后写结果到 DB1：
  - `DB1.DBX0.0` 触发信号
  - `DB1.DBX0.1` 检测结果（1=OK，0=NG）
  - `DB1.DBX0.2` PLC 处理完成信号
- 读取完成信号、清除触发信号

### 配置

- IniFile.NET 读写 `Resource/config.ini`，保存运行参数

## 环境要求

- Windows 10 / 11 x64
- .NET Framework 4.8
- Cognex VisionPro 9.2（程序集版本 59.2.0.0）
- Visual Studio 2019 及以上
- NuGet 依赖已放在 `packages/`：
  - S7.NetPlus 0.20.0
  - IniFile.NET 1.6.0
  - System.Buffers / System.Memory 等

> VisionPro 是商业软件，仓库**不包含** Cognex 的 DLL，需要自行安装 VisionPro。

## 编译说明

1. 用 Visual Studio 打开 `winform_进阶联合编程.slnx`。
2. 检查 `winform_进阶联合编程.csproj` 里 VisionPro 引用的 `HintPath`：

```text
..\..\..\VisionPro\VisionPro\ReferencedAssemblies\Cognex.VisionPro.dll
```

这个相对路径是按原作者机器目录写的，如果本机 VisionPro 安装位置不同，需要改成你自己的
`ReferencedAssemblies` 目录（通常在 `C:\Program Files\Cognex\VisionPro\ReferencedAssemblies`）。
3. 生成后运行 `bin\Debug\winform_进阶联合编程.exe`。

## 目录说明

| 文件 / 目录 | 说明 |
| :--- | :--- |
| `Form1.cs` | 主逻辑：图像加载、模板训练、检测、流水线、统计 |
| `Form1.Designer.cs` | 界面布局与事件绑定 |
| `PlcClient.cs` | 西门子 PLC 通信封装（S7.Net） |
| `ApplicationConfig.cs` | INI 配置文件读写 |
| `packages/` | NuGet 依赖 DLL |
| `Properties/` | 项目属性与资源 |

## 说明

- `bin/`、`obj/` 已加入 `.gitignore`，不上传编译产物
- PLC 数据块固定为 `DB1`，实际使用请按 PLC 程序修改 `PlcClient.cs`
- 训练模板后再选择待检图；训练后修改卡尺会提示重新训练