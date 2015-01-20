using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public interface INewsRepository
    {
        IQueryable<News> GetAll(int companyId);
        News GetById(int id);
        int Insert(News model);
        void Update(News model);
        void Delete(News model);
        void SaveChanges();
    }
}
