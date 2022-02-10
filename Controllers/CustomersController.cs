using DBProgramming_Class_2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgramming_Class_2.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index(int id)
        {
            var context = new BooksEntities();

            if(id == null || id == 0)
            {
               return Redirect("/Customers/AddCustomer");
            }
            Customer customer = context.Customers.FirstOrDefault(x => x.CustomerID == id);
            
            return View(customer);
        }

        public ActionResult AddCustomer(int id)
        {
            var context = new BooksEntities();

            Customer customer = context.Customers.FirstOrDefault(x => x.CustomerID == id);

            if(customer == null)
            {
                customer = new Customer();
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            var context = new BooksEntities();

            try
            {
                context.Customers.AddOrUpdate(customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
               
            }

            return View(customer);
        }

        public ActionResult Delete(int id)
        {
            var context = new BooksEntities();
            List<Customer> customers = context.Customers.OrderBy(x => x.CustomerID).ToList();

            var customerToRemove = customers.FirstOrDefault(c => c.CustomerID == id);

            context.Customers.Remove(customerToRemove);
            context.SaveChanges();

            return Redirect("/Customers/CustomerList");
        }

        public ActionResult CustomerList(string searchTerm)
        {
            var context = new BooksEntities();

            List<Customer> customers = context.Customers.OrderBy(x => x.CustomerID).ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                customers = customers
                    .Where(x =>
                    x.Name.ToLower().Contains(searchTerm.ToLower())
                    ).ToList();
            }
            return View(customers);
        }
    }
}