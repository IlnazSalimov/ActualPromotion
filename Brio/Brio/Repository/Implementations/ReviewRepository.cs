
using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brio
{
    /// <summary>
    /// Предоставляет методы, которые предоставляют доступ к хранилищу ролей
    /// </summary>
    public class ReviewRepository : IReviewRepository
    {
        private IRepository<Review> _reviewRepository;

        public ReviewRepository(IRepository<Review> reviewRepository)
        {
            this._reviewRepository = reviewRepository;
        }
        public IQueryable<Review> GetAll()
        {
            return _reviewRepository.GetAll();
        }

        public IQueryable<Review> GetCompanyReviews(int cuurrentCompany)
        {
            if (cuurrentCompany == 0)
                return null;
            return _reviewRepository.GetAll().Where(r => r.CompanyId == cuurrentCompany);
        }

        public Review GetById(int id)
        {
            if (id == 0)
                return null;
            return _reviewRepository.GetById(id);
        }

        public void Insert(Review model)
        {
            if (model == null)
                throw new ArgumentNullException("Review");
            _reviewRepository.Insert(model);
        }

        public void Update(Review model)
        {
            if (model == null)
                throw new ArgumentNullException("Review");
            _reviewRepository.Update(model);

        }

        public void Delete(Review model)
        {
            if (model == null)
                throw new ArgumentNullException("Review");
            _reviewRepository.Delete(model);
        }

        public void SaveChanges()
        {
            _reviewRepository.SaveChanges();
        }
    }
}