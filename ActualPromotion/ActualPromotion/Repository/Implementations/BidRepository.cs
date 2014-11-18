using ActualPromotion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActualPromotion
{
    public class BidRepository : IBidRepository
    {
        private IRepository<Bid> bidRepository;

        public BidRepository(IRepository<Bid> bidRepository)
        {
            this.bidRepository = bidRepository;
        }
         public IQueryable<Bid> GetAll()
        {
            return bidRepository.GetAll();
        }

         public Bid GetById(int id)
        {
            if (id == 0)
                return null;
            return bidRepository.GetById(id);
        }

         public void Insert(Bid model)
        {
            if (model == null)
                throw new ArgumentNullException("Bid");
            bidRepository.Insert(model);
        }

        public void SaveChanges()
        {
            bidRepository.SaveChanges();
        }
    }
}