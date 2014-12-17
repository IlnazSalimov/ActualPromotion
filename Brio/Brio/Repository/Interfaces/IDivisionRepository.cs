using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public interface IDivisionRepository
    {
        IQueryable<Division> GetAll();
        IQueryable<Division> GetCompanyDivisions(int companyId);
        Division GetById(int id);

        int Insert(Division model);
        void Update(Division model);
        void Delete(Division model);
        void SaveChanges();
    }
}
