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
    public interface IRoleRepository
    {
        IQueryable<Role> GetAll();
        Role GetById(int id);
        void Insert(Role model);
        void Update(Role model);
        void Delete(Role model);
        void SaveChanges();
    }
}
