using DBProgramming_Class_2.Models;
using System;
using System.Collections.Generic;
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