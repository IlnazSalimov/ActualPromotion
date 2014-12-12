using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brio
{
    public class CompanyRepository : ICompanyRepository
    {
        private IRepository<Company> _companyRepository;

        public CompanyRepository(IRepository<Company> companyRepository)
        {
            this._companyRepository = companyRepository;
        }
        public IQueryable<Company> GetAll()
        {
            return _companyRepository.GetAll();
        }

        public Company GetById(int id)
        {
            if (id == 0)
                return null;
            return _companyRepository.GetById(id);
        }

        public Company GetByCompany(int currentCompany)
        {
            if (currentCompany == 0)
                return null;
            return _companyRepository.GetById(currentCompany);
        }

        public void Insert(Company model)
        {
            if (model == null)
                throw new ArgumentNullException("Company");
            _companyRepository.Insert(model);
        }

        public void Update(Company model)
        {
            if (model == null)
                throw new ArgumentNullException("Company");
            _companyRepository.Update(model);

        }

        public void Delete(Company model)
        {
            if (model == null)
                throw new ArgumentNullException("Company");
            _companyRepository.Delete(model);
        }

        public void SaveChanges()
        {
            _companyRepository.SaveChanges();
        }
    }
}