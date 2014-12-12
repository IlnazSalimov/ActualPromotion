using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public interface IPriceListRepository
    {
        IQueryable<PriceList> GetAll();
        PriceList GetById(int id);
        IQueryable<PriceList> GetCompanyPriceLists(int currentCompany);
        int Insert(PriceList model);
        void Update(PriceList model);
        void Delete(PriceList model);
        void SaveChanges();
    }
}
