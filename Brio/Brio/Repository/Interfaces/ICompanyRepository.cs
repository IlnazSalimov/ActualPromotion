using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public interface ICompanyRepository
    {
        IQueryable<Company> GetAll();
        Company GetById(int id);
        Company GetByCompany(int currentCompany);
        void Insert(Company model);
        void Update(Company model);
        void Delete(Company model);
        void SaveChanges();
    }
}
