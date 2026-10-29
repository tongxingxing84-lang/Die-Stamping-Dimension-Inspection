using S7.Net;
using System;
using System.Threading.Tasks;

namespace winform_进阶联合编程
{
    /// <summary>
    /// PLC通信封装类
    /// 使用S7netplus库与西门子S7-1200/1500通信
    /// </summary>
    public class PlcClient
    {
        private Plc _plc;
        
        // 连接状态
        public bool IsConnected => _plc != null && _plc.IsConnected;
        
        // 连接事件
        public event Action<bool> ConnectionStateChanged;
        
        /// <summary>
        /// 连接PLC
        /// </summary>
        /// <param name="ip">PLC的IP地址</param>
        /// <param name="cpuType">CPU类型</param>
        /// <param name="rack">机架号</param>
        /// <param name="slot">槽号</param>
        public async Task<bool> ConnectAsync(string ip, CpuType cpuType = CpuType.S71200, short rack = 0, short slot = 1)
        {
            try
            {
                _plc = new Plc(cpuType, ip, rack, slot);
                await _plc.OpenAsync();
                ConnectionStateChanged?.Invoke(true);
                return true;
            }
            catch (Exception ex)
            {
                ConnectionStateChanged?.Invoke(false);
                throw new Exception($"PLC连接失败: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 断开PLC连接
        /// </summary>
        public void Disconnect()
        {
            _plc?.Close();
            ConnectionStateChanged?.Invoke(false);
        }
        
        /// <summary>
        /// 写入检测结果到PLC
        /// DB1.DBX0.0 = Trigger 触发信号
        /// DB1.DBX0.1 = Result 检测结果 (1=OK, 0=NG)
        /// </summary>
        /// <param name="isQualified">是否合格</param>
        public async Task WriteResultAsync(bool isQualified)
        {
            if (!IsConnected) return;

            try
            {
                byte signalByte = 0x01; // bit0: Trigger = 1
                if (isQualified)
                {
                    signalByte |= 0x02; // bit1: Result = OK
                }

                // 注意：先写 Result 再写 Trigger，一次写入保证原子性
                await _plc.WriteBytesAsync(DataType.DataBlock, 1, 0, new byte[] { signalByte });
            }
            catch (Exception ex)
            {
                throw new Exception($"写入PLC失败: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 读取PLC处理完成信号
        /// </summary>
        /// <returns>是否完成</returns>
        public async Task<bool> ReadCompletedSignalAsync()
        {
            if (!IsConnected) return false;
            
            try
            {
                var data = await _plc.ReadBytesAsync(DataType.DataBlock, 1, 0, 1);
                return (data[0] & 0x04) == 0x04; // DB1.DBX0.2 = 完成信号
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 清除触发信号
        /// </summary>
        public async Task ClearTriggerSignalAsync()
        {
            if (!IsConnected) return;
            
            try
            {
                var data = await _plc.ReadBytesAsync(DataType.DataBlock, 1, 0, 1);
                data[0] = (byte)(data[0] & 0xFC); // 清除bit0和bit1
                await _plc.WriteBytesAsync(DataType.DataBlock, 1, 0, new byte[] { data[0] });
            }
            catch (Exception ex)
            {
                throw new Exception($"清除信号失败: {ex.Message}");
            }
        }
    }
}
