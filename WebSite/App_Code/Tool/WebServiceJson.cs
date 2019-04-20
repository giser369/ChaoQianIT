using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// 调用Web Service返回json数据
/// </summary>
public class WebServiceJson
{
    //get方式
    public static string GetStringByUrl(string strUrl)
    {
        //为指定URL创建HTTP请求
        WebRequest wrt = WebRequest.Create(strUrl);
        //获取对应HTTP请求的响应
        WebResponse wrse = wrt.GetResponse();
        //获取响应流
        Stream strM = wrse.GetResponseStream();
        //对接响应流（以"GBK"字符串）
        StreamReader sr = new StreamReader(strM, Encoding.GetEncoding("UTF-8"));
        //获取响应流的全部字符串
        string strallStrm = sr.ReadToEnd();
        //关闭读取流
        sr.Close();
        return strallStrm;
    }

    //post方式
    /// <summary>
    /// 调用web service返回json数据
    /// </summary>
    /// <param name="strUrl">示例："http://api.ip138.com/query/?ip=8.8.8.8&datatype=jsonp&callback=find&token=ad56856c0d06a1d1e3a12788b6948b88"</param>
    /// <param name="strPostData">示例：""</param>
    /// <returns>返回json字符串</returns>
    public static string RequestWebService(string strUrl, string strPostData)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
        request.Method = "POST";
        request.ContentType = "application/json; charset=utf-8";
        request.ContentLength = strPostData.Length;
        StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
        writer.Write(strPostData);
        writer.Flush();
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string encoding = response.ContentEncoding;
        if (encoding == null || encoding.Length < 1)
        {
            encoding = "UTF-8"; //默认编码  
        }
        StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
        string retString = reader.ReadToEnd();

        return retString;
    }
}