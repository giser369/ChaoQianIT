using MVCExercise.App_Code.CommonTool;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MVCExercise.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var jsonFormatter = new JsonMediaTypeFormatter();
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}