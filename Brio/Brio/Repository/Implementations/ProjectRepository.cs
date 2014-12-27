using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public class ProjectRepository : IProjectRepository
    {
        private IRepository<Project> _projectRepository;

        public ProjectRepository(IRepository<Project> projectRepository)
        {
            this._projectRepository = projectRepository;
        }
        public IQueryable<Project> GetAll()
        {
            return _projectRepository.GetAll().Where(p => p.StateId == (int)ProjectStates.Active);
        }

        public IQueryable<Project> GetCompanyProjects(int companyId)
        {
            return _projectRepository.GetAll().Where(p => p.CompanyId == companyId &&
                p.StateId == (int)ProjectStates.Active);
        }

        public Project GetById(int id)
        {
            if (id == 0)
                return null;
            return _projectRepository.GetById(id);
        }

        public int Insert(Project model)
        {
            if (model == null)
                throw new ArgumentNullException("Project");
            return _projectRepository.Insert(model);
        }

        public void Update(Project model)
        {
            if (model == null)
                throw new ArgumentNullException("Project");
            _projectRepository.Update(model);

        }

        public void Delete(Project model)
        {
            if (model == null)
                throw new ArgumentNullException("Project");
            model.StateId = (int)ProjectStates.Deleted;
            _projectRepository.Update(model);
            //_projectRepository.Delete(model);
        }

        public void SaveChanges()
        {
            _projectRepository.SaveChanges();
        }
    }
}
