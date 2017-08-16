using Core.Core.DataAccess;
using Core.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Northwind.DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        //özel operasyonlar yani ek metod yazabiliriz
    }

 
}
