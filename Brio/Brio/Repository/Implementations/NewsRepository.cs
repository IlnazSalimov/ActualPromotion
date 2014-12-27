using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public class NewsRepository : INewsRepository
    {
        private IRepository<News> newsRepository;

        public NewsRepository(IRepository<News> _newsRepository)
        {
            this.newsRepository = _newsRepository;
        }

        public IQueryable<News> GetAll(int companyId)
        {
            return newsRepository.GetAll().Where(p => p.CompanyId == companyId);
        }

        public News GetById(int id)
        {
            return newsRepository.GetById(id);
        }

        public int Insert(News model)
        {
            if (model == null)
                throw new ArgumentNullException("News");
            return newsRepository.Insert(model);
        }

        public void Update(News model)
        {
            if (model == null)
                throw new ArgumentNullException("News");
            newsRepository.Update(model);
        }

        public void Delete(News model)
        {
            if (model == null)
                throw new ArgumentNullException("News");
            newsRepository.Delete(model);
        }

        public void SaveChanges()
        {
            newsRepository.SaveChanges();
        }
    }
}
