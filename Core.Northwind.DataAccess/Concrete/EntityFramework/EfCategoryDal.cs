using Core.Core.DataAccess.EntityFramework;
using Core.Northwind.DataAccess.Abstract;
using Core.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal 
    {
    }
}
