using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// 文章管理类
/// </summary>
public class ArticleManger
{
    /// <summary>
    /// 文章信息表
    /// </summary>
    private DataTable _articleTable;
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    private string _conStr;
    /// <summary>
    /// 网页文件名
    /// </summary>
    private string _htmFile;

    //构造函数
	public ArticleManger()
	{
		
	}
}