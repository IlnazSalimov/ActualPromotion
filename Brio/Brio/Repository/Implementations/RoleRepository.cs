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
    public class RoleRepository : IRoleRepository
    {
        private IRepository<Role> _roleRepository;

        public RoleRepository(IRepository<Role> roleRepository)
        {
            this._roleRepository = roleRepository;
        }
        public IQueryable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public Role GetById(int id)
        {
            if (id == 0)
                return null;
            return _roleRepository.GetById(id);
        }

        public void Insert(Role model)
        {
            if (model == null)
                throw new ArgumentNullException("role");
            _roleRepository.Insert(model);
        }

        public void Update(Role model)
        {
            if (model == null)
                throw new ArgumentNullException("role");
            _roleRepository.Update(model);

        }

        public void Delete(Role model)
        {
            if (model == null)
                throw new ArgumentNullException("role");
            _roleRepository.Delete(model);
        }

        public void SaveChanges()
        {
            _roleRepository.SaveChanges();
        }
    }
}