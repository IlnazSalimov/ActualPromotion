using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brio
{
    public class ArticleRepository : IArticleRepository
    {
        private IRepository<Article> articleRepository;

        public ArticleRepository(IRepository<Article> _articleRepository)
        {
            this.articleRepository = _articleRepository;
        }

        public IQueryable<Article> GetAll()
        {
            return articleRepository.GetAll();
        }

        public Article GetById(int id)
        {
            return articleRepository.GetById(id);
        }

        public IQueryable<Article> GetByPage(PagesEnum page, int currentCompany)
        {
            IQueryable<Article> articles = articleRepository.GetAll();
            return from a in articles
                   where a.PageId == (int)page &&
                   a.CompanyId == currentCompany
                   select a;

        }

        public int Insert(Article model)
        {
            if (model == null)
                throw new ArgumentNullException("article");
            return articleRepository.Insert(model);
        }

        public void Update(Article model)
        {
            if (model == null)
                throw new ArgumentNullException("article");
            articleRepository.Update(model);
        }

        public void Delete(Article model)
        {
            if (model == null)
                throw new ArgumentNullException("article");
            articleRepository.Delete(model);
        }

        public void SaveChanges()
        {
            articleRepository.SaveChanges();
        }
    }
}