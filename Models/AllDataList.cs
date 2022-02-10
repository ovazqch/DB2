using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBProgramming_Class_2.Models
{
    public class AllDataList
    {
        public Customer Customers { get; set; }
        public Product Products { get; set; }
        public InvoiceLineItem InvoiceLineItems { get; set; }
        public Invoice Invoices { get; set; }
        public OrderOption OrderOption { get; set; }
        public List<Product> productsList { get; set; }
        public List<InvoiceLineItem> invoiceLineItemsList { get; set; }
        public List <Customer> customersList { get; set; }

    }
}