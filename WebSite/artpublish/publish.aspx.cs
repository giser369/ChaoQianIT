using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExerciseCKEditor_Default : System.Web.UI.Page
{
    /// <summary>
    /// 选中的文章ID
    /// </summary>
    private Guid _selectArtID;
    private FtpManager _ftpManager = new FtpManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.SelectCommand = string.Format("SELECT * FROM [T_ARTICLE] where ArtType={0}", int.Parse(ddlArtType.SelectedValue));
        SqlDataSource1.DataBind();
        if (ViewState["ArtID"] != null)
        {
            _selectArtID = Guid.Parse(ViewState["ArtID"].ToString());
        }
    }

    //改变文章类型
    protected void ddlArtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataSource1.SelectCommand = "SELECT * FROM [T_ARTICLE] where ArtType=9"; //先将ArtType的值设置为不存在的值，以保证ArtType的值一定会发生变化，实现GridView的刷新
        SqlDataSource1.SelectCommand = string.Format("SELECT * FROM [T_ARTICLE] where ArtType={0}", int.Parse(ddlArtType.SelectedValue));
        SqlDataSource1.DataBind();
    }
    //发布文章
    protected void btnPublish_Click(object sender, EventArgs e)
    {
        string artTitle = txtArtTitle.Text; //文章标题
        int artType = int.Parse(ddlArtType.SelectedValue); //文章类别
        DateTime artPubTime = DateTime.Now; //文章发布时间
        //string artContent = CKEditorControl1.Text; //文章内容
        string artContent = Request.Params["content"]; //文章内容

        int maxOrder = 0;
        string sqlQuery = string.Format("select max(ArtOrder) from T_ARTICLE where ArtType={0}", artType);
        DataTable dtQuery = SqlServerHooker.GetDataTable(sqlQuery);
        if (dtQuery == null || dtQuery.Rows[0][0].ToString() == "")
            maxOrder = 1;
        else
            maxOrder = (int)dtQuery.Rows[0][0] + 1;

        bool isSuc = true;
        if (radioList.SelectedIndex == 0) //新增文章
        {
            string sqlInsert = string.Format("insert into T_ARTICLE(ID,ArtType,ArtTitle,ArtDate,ArtContent,ArtOrder) values('{0}',{1},'{2}','{3}','{4}',{5})", Guid.NewGuid(), artType,
                artTitle, artPubTime.ToString("yyyy-MM-dd HH:mm:ss"), artContent, maxOrder);
            isSuc = SqlServerHooker.InsertDataToTable(sqlInsert);
        }
        else //更新文章
        {
            string sqlUpdate = string.Format("update T_ARTICLE set ArtType={0},ArtTitle='{1}',ArtContent='{2}' where ID='{3}'", artType,
                artTitle, artContent, _selectArtID);
            isSuc = SqlServerHooker.InsertDataToTable(sqlUpdate);
        }

        //生成静态html网页
        string url = string.Format("http://127.0.0.1:8080/article/detail?type={0}&order={1}", artType, maxOrder);
        string id = OtherTool.GuidTo16String();
        string path = Server.MapPath(string.Format("~/article/{0}.html", id));
        CreateStaticHtml.ReturnStaticHtml(url, path);

        //上传html文件
        bool isUpdateSuc=_ftpManager.UploadFile(path, "article/");
        File.Delete(path);

        //上传图片
        string ftpPath = "ueditor/net/upload/image/";
        string imageDir = Server.MapPath("~/artpublish/ueditor/net/upload/image/");
        string[] filePaths=Directory.GetFiles(imageDir);
        string imageNames = string.Empty;
        foreach (string filePath in filePaths)
        {
            //压缩图片
            string newFilePath = Path.GetDirectoryName(filePath)+"\\" + Path.GetFileNameWithoutExtension(filePath)+"new"+Path.GetExtension(filePath);
            ResizeImage.GetPicThumbnail(filePath,newFilePath,222,650,100);
            FileInfo fileInfo = new FileInfo(newFilePath);
            fileInfo.MoveTo(filePath);

            imageNames += Path.GetFileName(filePath) + ";";
            _ftpManager.UploadFile(filePath, ftpPath);

            File.Delete(filePath);
        }
        if(imageNames.Length>0)
        imageNames=imageNames.Remove(imageNames.Length - 1, 1);

        string sqlUpdateUrl = string.Format("update T_ARTICLE set ArtUrl='{0}',ArtContent='',ArtImages='{3}' where ArtType={1} and ArtOrder={2}", id, artType, maxOrder, imageNames);
        isSuc = SqlServerHooker.UpdateDataToTable(sqlUpdateUrl);

        if (isSuc)
        {
            div_text.InnerText = isUpdateSuc.ToString()+"发布成功！";
            gvArtManager.DataBind();
        }
        else
            div_text.InnerText = "发布失败！";
    }
    //选择文章管理表的一篇文章
    protected void gvArtManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        _selectArtID = (Guid)gvArtManager.DataKeys[gvArtManager.SelectedIndex].Value;
        ViewState["ArtID"] = _selectArtID;
        string sqlSelect = string.Format("select * from T_ARTICLE where ID='{0}'", _selectArtID);
        DataTable dt = SqlServerHooker.GetDataTable(sqlSelect);
        if (dt != null && dt.Rows.Count > 0)
        {
            txtArtTitle.Text = dt.Rows[0]["ArtTitle"].ToString();
            //CKEditorControl1.Text = dt.Rows[0]["ArtContent"].ToString();
            ddlArtType.SelectedIndex = int.Parse(dt.Rows[0]["ArtType"].ToString());
        }
    }
    //删除选中的文章
    protected void btnDeleteArt_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gvArtManager.Rows.Count; i++)
        {
            //获取GridView中每一行的CheckBox引用
            CheckBox chk = (CheckBox)gvArtManager.Rows[i].FindControl("chkCheck");
            if (chk.Checked)
            {
                Guid artID = (Guid)gvArtManager.DataKeys[i].Value;

                string sqlQuery = string.Format("select * from T_ARTICLE where ID='{0}'", artID);
                DataTable dtQuery = SqlServerHooker.GetDataTable(sqlQuery);

                //删除文章中的图片
                string imageNames = dtQuery.Rows[0][7].ToString();
                string[] imageArr = imageNames.Split(';');
                foreach (string imageName in imageArr)
                {
                    string imgFtpPath = "ueditor/net/upload/image/" + imageName;
                    _ftpManager.DeleteFile(imgFtpPath);
                }

                //删除ftp里面对应的html文件
                string htmlName = dtQuery.Rows[0][6].ToString();
                bool isSuc = _ftpManager.DeleteFile(string.Format("article/{0}.html", htmlName));
                //if (isSuc)
                //{
                    //删除数据库信息
                    string sqlDelete = string.Format("delete from T_ARTICLE where ID='{0}'", artID);
                    SqlServerHooker.DeleteDataFromTable(sqlDelete);
                //}
            }
        }
        //刷新GridView
        gvArtManager.DataBind();
    }
    //全选或者全不选文章
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvArtManager.Rows.Count; i++)
        {
            //获取GridView中每一行的CheckBox引用
            CheckBox chk = (CheckBox)gvArtManager.Rows[i].FindControl("chkCheck");
            if (chkSelectAll.Checked == true)
                chk.Checked = true;
            else
                chk.Checked = false;
        }
    }
}