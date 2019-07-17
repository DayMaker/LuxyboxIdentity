using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuxyboxIdentity.Models;
using LuxyboxIdentity.Data;
using System.Net;


namespace LuxyboxIdentity.Controllers
{
    public class HomeController : BaseController
    {
        

        public ActionResult Index()
        {

            //Helper.BusinessHelper.AddCategory(new Data.Category { Name = "Men" });
            var categories = dbContext.Categories.ToList();//Helper.BusinessHelper.GetCategories();
            var products = dbContext.Products.ToList();
            var model = new HomeModel(categories, products);
            return View(model);
        }
        public ActionResult Products(int id)
        {

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var products = dbContext.Products.Where(c => c.CategoryId == id).ToList();
            if (products == null)
            {
                return HttpNotFound();
            }

            var categories = dbContext.Categories.ToList();
            var model = new HomeModel(categories, products);

            return View(model);
        }
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = dbContext.Products.SingleOrDefault(q => q.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult AddToCart()
        {
            ViewBag.Message = "Ürün Sepete Eklendi";

            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}