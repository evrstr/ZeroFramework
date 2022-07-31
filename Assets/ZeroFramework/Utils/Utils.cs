using System;
using System.Collections.Generic;

namespace ZeroFramework
{
    /// <summary>
    /// 实用函数集工具类。
    /// </summary>
    public static partial class Utils
    {
        public static string ByteConversionGBMBKB(long KSize)
        {
            if (KSize / (1024 * 1024 * 1024) >= 1)//如果当前Byte的值大于等于1GB
            {
                return Math.Round(KSize / (double)(1024 * 1024 * 1024), 2).ToString() + " GB"; //将其转换成GB
            }
            else if (KSize / (1024 * 1024) >= 1)//如果当前Byte的值大于等于1MB
            {
                return Math.Round(KSize / (double)(1024 * 1024), 2).ToString() + " MB"; //将其转换成MB
            }
            else if (KSize / 1024 >= 1)//如果当前Byte的值大于等于1KB
            {
                return Math.Round(KSize / (double)1024, 2).ToString() + " KB"; //将其转换成KB
            }
            else
            {
                return KSize.ToString() + " Byte";  //显示Byte值
            }
        }
    }
}