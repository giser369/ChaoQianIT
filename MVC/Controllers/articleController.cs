using System.Web.Mvc;

namespace MVCExercise.Controllers
{
    public class articleController : Controller
    {
        public ActionResult list(int type,int page)
        {
            ViewBag.type = type;
            ViewBag.page = page;
            switch (type)
            {
                case 0:
                    ViewBag.Title = ".NET文章";
                    break;
                case 1:
                    ViewBag.Title = "Java文章";
                    break;
                case 2:
                    ViewBag.Title = "Web开发";
                    break;
                case 3:
                    ViewBag.Title = "数据库";
                    break;
                case 4:
                    ViewBag.Title = "系统综合";
                    break;
                case 5:
                    ViewBag.Title = "在线工具";
                    break;
            }
            return View();
        }
        public ActionResult detail(int type,int order)
        {
            ViewBag.type = type;
            ViewBag.order = order;
            return View();
        }
    }
}
