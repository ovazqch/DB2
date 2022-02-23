using DBProgramming_Class_2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgramming_Class_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new BooksEntities();

            List<Product> products = context.Products.OrderByDescending(x => x.InvoiceLineItems.Count()).ToList();

            return View(products);
        }

        public ActionResult AddProduct(Product prod)
        {
            var context = new BooksEntities();
            try
            {
                context.Products.AddOrUpdate(prod);
                context.SaveChanges();

            }
            catch (Exception ex)
            {

               
            }

            return Redirect("/Home/");
        }

        public ActionResult CreateProduct()
        {
            var context = new BooksEntities();
            Product newProduct = new Product();

            return PartialView(newProduct);
        }

        public ActionResult EditProduct(string id)
        {
            var context = new BooksEntities();

            Product item = context.Products.FirstOrDefault(x => x.ProductCode == id);
            
            return PartialView(item);
        }

        public ActionResult DeleteProduct(ProdDelete prd)
        {
            string code = prd.ProductCode;
            var context = new BooksEntities();
            Product item = context.Products.FirstOrDefault(x => (x.ProductCode == code));

            context.Products.Remove(item);
            context.SaveChanges();

            return Redirect("/Home/");
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