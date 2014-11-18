using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    /// <summary>
    /// Определяют методы, которые предоставляют доступ к хранилищу пользователей
    /// </summary>
    public interface IInfoCardRepository
    {
        IQueryable<InfoCard> GetAll();
        InfoCard GetById(int id);
        void Insert(InfoCard model);
        void Update(InfoCard model);
        void Delete(InfoCard model);
        void SaveChanges();
    }
}
