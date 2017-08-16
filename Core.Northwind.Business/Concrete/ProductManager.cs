using Core.Northwind.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Northwind.Entities.Concrete;
using Core.Northwind.DataAccess.Abstract;

namespace Core.Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(int productId)
        {
            _productDal.Delete(new Product { ProductId = productId });
        }

        public List<Product> GetAll()
        {
            return _productDal.GetList();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _productDal.GetList(p=>p.CategoryId==categoryId||categoryId==0);
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(x=>x.ProductId==productId);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
