
using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    /// <summary>
    /// Определяют методы, которые предоставляют доступ к хранилищу регионов
    /// </summary>
    public interface IReviewRepository
    {
        IQueryable<Review> GetAll();
        IQueryable<Review> GetCompanyReviews(int currentCompany);
        Review GetById(int id);
        void Insert(Review model);
        void Update(Review model);
        void Delete(Review model);
        void SaveChanges();
    }
}
