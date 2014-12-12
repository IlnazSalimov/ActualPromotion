using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public interface IFeedbackRepository
    {
        IQueryable<Feedback> GetAll();
        Feedback GetById(int id);
        void Insert(Feedback model);
        void Update(Feedback model);
        void Delete(Feedback model);
        void SaveChanges();
    }
}
