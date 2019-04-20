using MVCExercise.App_Code.DBTool;
using MVCExercise.Models;
using MVCExercise.Models.WebAPI;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using Tool.Class;

namespace MVCExercise.Controllers
{
    /// <summary>
    /// 获取到文章标题树的控制器
    /// </summary>
    public class ArticleTileController : ApiController
    {
        //IEnumerable<ArticleTitleModel> products = new ArticleTitleModel[]
        //    {
        //        new ArticleTitleModel { id="1",name="Java",pId="0"},
        //        new ArticleTitleModel { id="2",name="Java反射",pId="1"},
        //        new ArticleTitleModel { id="3",name="Java注解",pId="1"}
        //    };

        public List<ArticleTitleModel> GetAllProducts()
        {
            List<ArticleTitleModel> listArticle = new List<ArticleTitleModel>();

            string sqlQuery = "select * from T_ARTICLE_TILENODE_INFOR t";
            System.Data.DataTable dtResult=SqlServerOperate.GetDataTable(sqlQuery);
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                foreach (DataRow item in dtResult.Rows)
                {
                    ArticleTitleModel model = new ArticleTitleModel();
                    model.id = item["nodeid"].ToString();
                    model.name = item["nodename"].ToString();
                    model.pId = item["parentid"].ToString();
                    model.open = true;
                    listArticle.Add(model);
                }
            }

            //List<ArticleTitleModel> listArticle = new List<ArticleTitleModel>()
            //{ new ArticleTitleModel { id="1",name="Java",pId="0",open=true},
            //    new ArticleTitleModel { id="2",name="Java反射",pId="1",open=true},
            //    new ArticleTitleModel { id="3",name="Java注解",pId="1",open=false},
            //    new ArticleTitleModel { id="4",name="Java注解1",pId="3",open=true}
            //};
            return listArticle;
        }
    }
}
