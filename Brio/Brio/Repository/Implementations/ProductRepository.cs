
using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brio
{
    public class ProductRepository : IProductRepository
    {
        private IRepository<Product> productRepository;

        public ProductRepository(IRepository<Product> _productRepository)
        {
            this.productRepository = _productRepository;
        }

        public IQueryable<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public int Insert(Product model)
        {
            if (model == null)
                throw new ArgumentNullException("Product");
            return productRepository.Insert(model);
        }

        public void Update(Product model)
        {
            if (model == null)
                throw new ArgumentNullException("Product");
            productRepository.Update(model);
        }

        public void Delete(Product model)
        {
            if (model == null)
                throw new ArgumentNullException("Product");
            productRepository.Delete(model);
        }

        public void SaveChanges()
        {
            productRepository.SaveChanges();
        }

        public Product GetById(int id)
        {
            return productRepository.GetById(id);
        }

        public IQueryable<Product> GetCompanyProducts(int currentCompany)
        {
            return productRepository.GetAll().Where(prod => prod.CompanyId == currentCompany);
        }
    }
}