using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// OtherTool 的摘要说明
/// </summary>
public class OtherTool
{
    public static string GuidTo16String()
    {
        long i = 1;
        foreach (byte b in Guid.NewGuid().ToByteArray())
            i *= ((int)b + 1);
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }
    /// <summary>  /// 根据GUID获取19位的唯一数字序列  
    /// </summary>  /// <returns></returns>  
    public static long GuidToLongID()
    {
        byte[] buffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(buffer, 0);
    }
}