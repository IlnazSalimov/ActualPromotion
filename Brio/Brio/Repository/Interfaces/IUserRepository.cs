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
    /// Определяют методы, которые предоставляют доступ к хранилищу пользователей
    /// </summary>
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        User GetById(int id);
        int Insert(User model);
        void Update(User model);
        void Delete(User model);
        User Login(string email, string password);
        User GetByEmail(string email);
        void SaveChanges();
    }
}
