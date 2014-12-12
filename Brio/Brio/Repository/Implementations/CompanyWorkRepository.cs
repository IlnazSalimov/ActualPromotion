using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public class CompanyWorkRepository : ICompanyWorkRepository
    {
         private IRepository<CompanyWork> companyWorkRepository;

         public CompanyWorkRepository(IRepository<CompanyWork> _companyWorkRepository)
        {
            this.companyWorkRepository = _companyWorkRepository;
        }

         public IQueryable<CompanyWork> GetAll()
        {
            return companyWorkRepository.GetAll();
        }

         public CompanyWork GetById(int id)
         {
             return companyWorkRepository.GetById(id);
         }

         public List<CompanyWork> GetCompanyWorks(int companyId)
         {
             IQueryable<CompanyWork> companyWorks = companyWorkRepository.GetAll();
             return (from c in companyWorks
                    where c.CompanyId == companyId
                    select c).ToList();
         }

         public int Insert(CompanyWork model)
        {
            if (model == null)
                throw new ArgumentNullException("CompanyWork");
            return companyWorkRepository.Insert(model);
        }

         public void Update(CompanyWork model)
        {
            if (model == null)
                throw new ArgumentNullException("CompanyWork");
            companyWorkRepository.Update(model);
        }

         public void Delete(CompanyWork model)
        {
            if (model == null)
                throw new ArgumentNullException("CompanyWork");
            companyWorkRepository.Delete(model);
        }

        public void SaveChanges()
        {
            companyWorkRepository.SaveChanges();
        }
    }
}
