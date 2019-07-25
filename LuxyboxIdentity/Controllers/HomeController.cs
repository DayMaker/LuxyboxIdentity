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
            var cartitems = dbContext.CartItems.ToList();
            var model = new HomeModel(categories, products, cartitems);

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
            var cartitems = dbContext.CartItems.ToList();
            var model = new HomeModel(categories, products, cartitems);

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
            var sessionId = Session["sessionId"].ToString();
            var currentCart = dbContext.Carts.SingleOrDefault(q => q.SessionId == sessionId);
            if (currentCart == null)
            {
                currentCart = new Cart { CreateDate = DateTime.Now, SessionId = Session["sessionId"].ToString() };
                dbContext.Carts.Add(currentCart);
                dbContext.SaveChanges();
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                currentCart.MemberId = User.Identity.GetUserId();
            }

            CartItem item = new CartItem { CartId = currentCart.Id, CreateDate = DateTime.Now, ProductId = id, Quantity = 1 };
            currentCart.CartItems.Add(item);
            dbContext.SaveChanges();
            return RedirectToAction("Cart");
        }
        public ActionResult Cart()
        {
           
            string sessionId = Session["sessionId"].ToString();
            
            
            Cart cart = dbContext.Carts.SingleOrDefault(q => q.SessionId == sessionId);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Shopping()
        {
            ViewBag.Message = "Bizi tercih ettiğiniz için teşekkür ederiz.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CheckOrder()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOrder([Bind(Include = "Id,ShipmentAdress,InvoiceAdress,CreateDate,NameSurname,InvoiceName,SessionId")] CheckOrder checkorder)
        {
            var cartitems = dbContext.CartItems.ToList();
            
            if (ModelState.IsValid)
            {

                string sessionId = Session["sessionId"].ToString();
                checkorder.SessionId = sessionId;
                checkorder.CreateDate = DateTime.Now;
                var cart = (Cart)ViewBag.CurrentCart;
                decimal? totalPrice = 0;
                foreach (var item in cart.CartItems)
                {
                    totalPrice += item.Product.Price;
                }
                checkorder.TotalPrice = totalPrice.Value;
                dbContext.CheckOrders.Add(checkorder);

                dbContext.Carts.Remove(cart);
                dbContext.SaveChanges();
                return RedirectToAction("Shopping");
            }

            return View(checkorder);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }



    }
}