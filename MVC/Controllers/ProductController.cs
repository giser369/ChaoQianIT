using MVCExercise.App_Code.DBTool;
using MVCExercise.Models.WebAPI;
using System.Collections.Generic;
using System.Web.Http;

namespace MVCExercise.Controllers
{
    public class ProductController : ApiController
    {
        IEnumerable<Product> products = new Product[]
            {
                new Product { Id=1,Name="Tomato Soup",Category="Groceries",Price=1},
                new Product { Id=2,Name="Yo-yo",Category="Toys",Price=375},
                new Product { Id=3,Name="Hammer",Category="Hardware",Price=16}
            };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }
        //public string GetAllProducts()
        //{
        //    return JsonHelper.SerializeObject(products);
        //}
    }
}
