using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBProgramming_Class_2.Models
{
    public class CombinedInvoice
    {
        public List<InvoiceLineItem> InvoiceLineItems { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Customer> Customer {get; set;}

        public InvoiceLineItem InvoiceLineItem { get; set; }
}
}