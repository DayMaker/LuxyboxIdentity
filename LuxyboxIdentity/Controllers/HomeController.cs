using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuxyboxIdentity.Models;
using LuxyboxIdentity.Data;
using System.Net;
using Microsoft.AspNet.Identity;

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
        public ActionResult AddToCart(int id)
        {
            ViewBag.Message = "Ürün Sepete Eklendi";
            Cart cart = new Cart { CreateDate = DateTime.Now, SessionId = Session["sessionId"].ToString() };

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                cart.MemberId = User.Identity.GetUserId();
            }
            dbContext.Carts.Add(cart);
            dbContext.SaveChanges();
            CartItem item = new CartItem { CartId = cart.Id, CreateDate = DateTime.Now, ProductId = id, Quantity = 1 };
            cart.CartItems.Add(item);
            dbContext.SaveChanges();
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

        public ActionResult Cart()
        {

            return View();
        }
    }
}