﻿using DBProgramming_Class_2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgramming_Class_2.Controllers
{
    public class InvoicesController : Controller
    {
        // GET: Invoices
        public ActionResult Index(string id)
        {
            var context = new BooksEntities();

            List<int> invoiceLineItems = context.InvoiceLineItems.Where(
                x => x.ProductCode == id).Select(x => x.InvoiceID).ToList();

            //get all invoices based on product code
            List<Invoice> invoices = context.Invoices.Where(x => invoiceLineItems.Contains(x.InvoiceID)).ToList();
            return View(invoices);
        }

        public ActionResult InvoicesList()
        {
            var context = new BooksEntities();
            List<Invoice> invoices = context.Invoices.OrderBy(x => x.InvoiceID).ToList();

            return View(invoices);
        }

        //public ActionResult AddInvoice(int id)
        //{
        //    var context = new BooksEntities();

        //    InvoiceLineItem invoiceLineItem = context.InvoiceLineItems.FirstOrDefault(x => x.InvoiceID == id);

        //    if (invoiceLineItem == null)
        //    {
        //        invoiceLineItem = new InvoiceLineItem();
        //    }

        //    List<Product> products = context.Products.ToList();
        //    List<InvoiceLineItem> invoiceLineItems = context.InvoiceLineItems.ToList();
        //    List<Customer> customers = context.Customers.ToList();

        //    AllDataList allDataList = new AllDataList() {
        //        productsList = products,
        //        invoiceLineItemsList = invoiceLineItems,
        //        customersList = customers
        //    };
        //    return View(invoiceLineItems);
        //}

        public ActionResult ViewInvoiceDetails(int id)
        {
            var context = new BooksEntities();
            CombinedInvoice combinedInvoice = new CombinedInvoice();

            List<int> invoice = context.InvoiceLineItems.Where(
                x => x.InvoiceID == id).Select(x => x.InvoiceID).ToList();

            if (invoice.Count() != 0)
            {
                List<InvoiceLineItem> invoiceDisplayDetail = context.InvoiceLineItems.Where(x => invoice.Contains(x.InvoiceID)).ToList();
                List<Invoice> invoiceDisplay = context.Invoices.Where(x => invoice.Contains(x.InvoiceID)).ToList();

                var updateInvoice = invoiceDisplay.FirstOrDefault(x => (x.InvoiceID == id));
                updateInvoice.InvoiceID = id;

                decimal newProductTotal = 0;
                foreach (InvoiceLineItem item in invoiceDisplayDetail)
                {
                    newProductTotal += item.ItemTotal;
                }
                decimal salesTax = Convert.ToDecimal(0.0750);
                decimal newSalesTax = newProductTotal * salesTax;

                decimal newShippingCost = 0;
                int countItems = 0;
                foreach (InvoiceLineItem item in invoiceDisplayDetail)
                {
                    countItems += item.Quantity;
                }
                if (countItems == 1)
                {
                    newShippingCost = Convert.ToDecimal(3.7500);
                }
                if (countItems > 1)
                {
                    int count = countItems - 1;
                    decimal additionalItems = Convert.ToDecimal(1.2500) * Convert.ToDecimal(count);
                    newShippingCost = Convert.ToDecimal(3.7500) + additionalItems;
                }

                updateInvoice.ProductTotal = newProductTotal;
                updateInvoice.SalesTax = newSalesTax;
                updateInvoice.Shipping = newShippingCost;
                updateInvoice.InvoiceTotal = newProductTotal + newSalesTax + newShippingCost;

                context.Invoices.AddOrUpdate(updateInvoice);
                context.SaveChanges();

                List<Invoice> invoiceDisplayUpdated = context.Invoices.Where(x => invoice.Contains(x.InvoiceID)).ToList();

                List<Customer> customer = context.Customers.Where(x => invoice.Contains(x.CustomerID)).ToList();

                combinedInvoice.Invoices = invoiceDisplayUpdated;
                combinedInvoice.InvoiceLineItems = invoiceDisplayDetail;
                combinedInvoice.Customer = customer;
            }
            else
            {
                List<Invoice> invoices = context.Invoices.Where(x => x.InvoiceID == id).ToList();
                List<InvoiceLineItem> invoiceLineItems = context.InvoiceLineItems.Where(x => x.InvoiceID == id).ToList();
                combinedInvoice.Invoices = invoices;
                combinedInvoice.InvoiceLineItems = invoiceLineItems;

            }
            return View(combinedInvoice);
        }

        public ActionResult DeleteInvoice(int id)
        {
            var context = new BooksEntities();
            List<Invoice> invoice = context.Invoices.OrderBy(x => x.InvoiceID).ToList();

            var itemToRemove = invoice.FirstOrDefault(x => (x.InvoiceID == id));
            context.Invoices.Remove(itemToRemove);
            context.SaveChanges();

            return Redirect("/Invoices/InvoicesList");
        }

        public ActionResult EditInvoiceItem(InvoiceLineItem obj)
        {
            var context = new BooksEntities();

            decimal newPrice = Convert.ToDecimal(context.Products.Where(x => x.ProductCode == obj.ProductCode).Select(x => x.UnitPrice));

            obj.UnitPrice = newPrice;

            obj.ItemTotal = obj.Quantity * obj.UnitPrice;


            try
            {
                context.InvoiceLineItems.AddOrUpdate(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return Redirect("/Invoices/ViewInvoiceDetails/" + obj.InvoiceID);
        }


        public ActionResult DeleteInvoiceItem(LineItem obj)
        {
            int id = obj.InvoiceId;
            string code = obj.ProductCode;

            var context = new BooksEntities();
            List<InvoiceLineItem> ili = context.InvoiceLineItems.OrderBy(x => x.InvoiceID).ToList();

            var itemToRemove = ili.FirstOrDefault(x => (x.InvoiceID == id) && (x.ProductCode == code));

            context.InvoiceLineItems.Remove(itemToRemove);
            context.SaveChanges();

            return Redirect("/Invoices/ViewInvoiceDetails/" + id);
        }

        public ActionResult ItemDetails(LineItem obj)
        {
            int id = obj.InvoiceId;
            string code = obj.ProductCode;

            var context = new BooksEntities();
            InvoiceLineItem item = context.InvoiceLineItems.FirstOrDefault(x => (x.InvoiceID == id) && (x.ProductCode == code));

            return View(item);

        }

        public ActionResult CreateInvoice()
        {
            var context = new BooksEntities();

            Invoice newInvoice = new Invoice();

            return View(newInvoice);
        }

        public ActionResult EditInvoice(Invoice inv)
        {
            var context = new BooksEntities();

            try
            {
                context.Invoices.AddOrUpdate(inv);
                context.SaveChanges();
            }
            catch (Exception)
            {

            }

            return Redirect("/Invoices/ViewInvoiceDetails/" + inv.InvoiceID);
        }
    }
}