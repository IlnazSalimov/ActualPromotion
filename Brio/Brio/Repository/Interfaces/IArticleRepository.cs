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
    /// Определяют методы, которые предоставляют доступ к хранилищу статей
    /// </summary>
    public interface IArticleRepository
    {
        IQueryable<Article> GetAll();
        Article GetById(int id);
        IQueryable<Article> GetByPage(PagesEnum page, int currentCompany);

        int Insert(Article model);
        void Update(Article model);
        void Delete(Article model);
        void SaveChanges();
    }
}
