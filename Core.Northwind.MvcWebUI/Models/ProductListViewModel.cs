using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Northwind.Entities.Concrete;

namespace Core.Northwind.MvcWebUI.Models
{
    public class ProductListViewModel
    {
        public int CurrentCategory { get;  set; }
        public int CurrentPage { get;  set; }
        public int PageCount { get;  set; }
        public int PageSize { get;  set; }
        public List<Product> Products { get; set; }
    }
}
