using Core.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Northwind.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
     
    }
}
