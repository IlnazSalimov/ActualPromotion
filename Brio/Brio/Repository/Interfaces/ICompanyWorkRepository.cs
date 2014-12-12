using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public interface ICompanyWorkRepository
    {
        IQueryable<CompanyWork> GetAll();
        CompanyWork GetById(int id);
        List<CompanyWork> GetCompanyWorks(int companyId);
        int Insert(CompanyWork model);
        void Update(CompanyWork model);
        void Delete(CompanyWork model);
        void SaveChanges();
    }
}
