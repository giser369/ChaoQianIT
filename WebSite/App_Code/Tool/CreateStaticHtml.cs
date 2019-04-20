using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// CreateStaticHtml 的摘要说明
/// </summary>
public class CreateStaticHtml
{
    public static void ReturnStaticHtml(string url,string path)
    {
        WebRequest request = WebRequest.Create(url);
        try
        {
            Stream stream = request.GetResponse().GetResponseStream();
            StreamReader sr = new StreamReader(stream, Encoding.UTF8);
            string htmlcontent = sr.ReadToEnd();
            stream.Close();
            sr.Close();
            SaveFile(path, htmlcontent);
        }
        catch (Exception ex)
        {
        }
    }

    private static void SaveFile(string path, string content)//保存静态页
    {
        try
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(content);
            sw.Close();
            fs.Close();
        }
        catch (Exception ex)
        {
        }
    }
}