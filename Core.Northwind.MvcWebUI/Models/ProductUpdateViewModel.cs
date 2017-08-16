using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Northwind.Entities.Concrete;

namespace Core.Northwind.MvcWebUI.Models
{
    public class ProductUpdateViewModel
    {
        public List<Category> Categories { get; internal set; }
        public Product Product { get; internal set; }
    }
}
