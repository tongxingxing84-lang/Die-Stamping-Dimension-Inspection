using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniFile;

namespace _9._14INI
{
    public class ApplicationConfig
    {
        public static int Counts = 1;

        public static string FilePath = string.Empty;

        public static void InitConfigFile()
        {
            //动态获取当前运行环境目录 /bin/debug  /bin/release
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //智能拼接路径，结果类似于C:\MyProject\bin\Debug\Resource\config.ini
            FilePath = Path.Combine(baseDirectory, "Resource", "config.ini");
            //获取文件夹路径
            string directory = Path.GetDirectoryName(FilePath);
            //如果文件夹不存在，就创建文件夹
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            //如果文件不存在，就创建文件
            if (!File.Exists(FilePath))
            {
                using (File.Create(FilePath)){}
                    
            }
        }

        public static void WriteConfig(int count)
        {
            //读取路径对应的文件，将文件转换为Ini对象
            Ini iniFile = new Ini(FilePath);

            //判断节点是否存在
            Section section = iniFile["RunParams"];
            if (section == null)
            {
                section = new Section("RunParams");//创建并且添加节点
                iniFile.Add(section);
            }

            section["Counts"] = count;
            iniFile.SaveTo(FilePath);
        }
        public static void ReadConfig()
        {
            //将文件转为对象
            Ini inifile = new Ini(FilePath);
            int count = int.Parse(inifile["RunParams"]["Counts"]);

            Counts = count;
        }
    }
}
