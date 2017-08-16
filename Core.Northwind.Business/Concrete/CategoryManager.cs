using Core.Northwind.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Northwind.Entities.Concrete;
using Core.Northwind.DataAccess.Abstract;

namespace Core.Northwind.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;
            public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public List<Category> GetAll()
        {
          return  _categoryDal.GetList();
        }
    }
}
