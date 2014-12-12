using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brio
{
    public class FeedbackRepository : IFeedbackRepository
    {
         private IRepository<Feedback> _feedbackRepository;

         public FeedbackRepository(IRepository<Feedback> feedbackRepository)
        {
            this._feedbackRepository = feedbackRepository;
        }
         public IQueryable<Feedback> GetAll()
        {
            return _feedbackRepository.GetAll();
        }

         public Feedback GetById(int id)
        {
            if (id == 0)
                return null;
            return _feedbackRepository.GetById(id);
        }

         public void Insert(Feedback model)
        {
            if (model == null)
                throw new ArgumentNullException("feedback");
            _feedbackRepository.Insert(model);
        }

         public void Update(Feedback model)
        {
            if (model == null)
                throw new ArgumentNullException("feedback");
            _feedbackRepository.Update(model);

        }

         public void Delete(Feedback model)
        {
            if (model == null)
                throw new ArgumentNullException("feedback");
            _feedbackRepository.Delete(model);
        }

        public void SaveChanges()
        {
            _feedbackRepository.SaveChanges();
        }
    }
}