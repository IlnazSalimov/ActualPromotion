using ActualPromotion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualPromotion
{
    public interface IBidRepository
    {
        IQueryable<Bid> GetAll();
        Bid GetById(int id);
        void Insert(Bid model);
        void SaveChanges();
    }
}
