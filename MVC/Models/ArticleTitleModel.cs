using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCExercise.Models
{
    /// <summary>
    /// 文章标题树的节点模型
    /// </summary>
    public class ArticleTitleModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pId { get; set; }
        /// <summary>
        /// 节点是否展开
        /// </summary>
        public bool open { get; set; }
    }
}