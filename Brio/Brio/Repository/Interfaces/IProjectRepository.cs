using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public interface IProjectRepository
    {
        IQueryable<Project> GetAll();
        IQueryable<Project> GetCompanyProjects(int companyId);
        Project GetById(int id);
        int Insert(Project model);
        void Update(Project model);
        void Delete(Project model);
        void SaveChanges();
    }
}
