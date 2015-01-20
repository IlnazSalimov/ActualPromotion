using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public class DivisionRepository : IDivisionRepository
    {
        private IRepository<Division> divisionRepository;

        public DivisionRepository(IRepository<Division> _divisionRepository)
        {
            this.divisionRepository = _divisionRepository;
        }

        public IQueryable<Division> GetAll()
        {
            return divisionRepository.GetAll().Where(d => d.State == (int)States.Active);
        }

        public IQueryable<Division> GetCompanyDivisions(int companyId)
        {
            return divisionRepository.GetAll().Where(d => d.CompanyId == companyId &&
                d.State == (int)States.Active);
        }

        public Division GetById(int id)
        {
            return divisionRepository.GetById(id);
        }

        public int Insert(Division model)
        {
            if (model == null)
                throw new ArgumentNullException("Division");
            return divisionRepository.Insert(model);
        }

        public void Update(Division model)
        {
            if (model == null)
                throw new ArgumentNullException("Division");
            divisionRepository.Update(model);
        }

        public void Delete(Division model)
        {
            if (model == null)
                throw new ArgumentNullException("Division");
            //divisionRepository.Delete(model);
            model.State = (int)States.Deleted;
            divisionRepository.Update(model);
        }

        public void SaveChanges()
        {
            divisionRepository.SaveChanges();
        }
    }
}
