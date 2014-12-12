using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brio
{
    public class PriceListRepository : IPriceListRepository
    {
        private IRepository<PriceList> priceListRepository;

        public PriceListRepository(IRepository<PriceList> _priceListRepository)
        {
            this.priceListRepository = _priceListRepository;
        }

        public IQueryable<PriceList> GetAll()
        {
            return priceListRepository.GetAll();
        }

        public IQueryable<PriceList> GetCompanyPriceLists(int currentCompany)
        {
            return priceListRepository.GetAll().Where(pl => pl.CompanyId == currentCompany);
        }

        public int Insert(PriceList model)
        {
            if (model == null)
                throw new ArgumentNullException("PriceList");
            return priceListRepository.Insert(model);
        }

        public void Update(PriceList model)
        {
            if (model == null)
                throw new ArgumentNullException("PriceList");
            priceListRepository.Update(model);
        }

        public void Delete(PriceList model)
        {
            if (model == null)
                throw new ArgumentNullException("PriceList");
            priceListRepository.Delete(model);
        }

        public void SaveChanges()
        {
            priceListRepository.SaveChanges();
        }


        public PriceList GetById(int id)
        {
            return priceListRepository.GetById(id);
        }

    }
}