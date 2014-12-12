
using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAll();
        IQueryable<Product> GetCompanyProducts(int currentCompany);
        Product GetById(int id);
        int Insert(Product model);
        void Update(Product model);
        void Delete(Product model);
        void SaveChanges();
    }
}
