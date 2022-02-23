using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBProgramming_Class_2.Models
{
    public class LineItemPlusProdList
    {
        public InvoiceLineItem InvoiceLineItem {get;set;}
        public List<Product> Products { get; set; }
    }
}