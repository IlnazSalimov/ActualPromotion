using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public class ProjectStepRepository : IProjectStepRepository
    {
        private IRepository<ProjectStep> projectStepRepository;

        public ProjectStepRepository(IRepository<ProjectStep> _projectStepRepository)
        {
            this.projectStepRepository = _projectStepRepository;
        }
        public IQueryable<ProjectStep> GetAll()
        {
            return projectStepRepository.GetAll();
        }

        public IQueryable<ProjectStep> GetProjectSteps(int projectId)
        {
            return projectStepRepository.GetAll().Where(p => p.ProjectId == projectId);
        }

        public ProjectStep GetById(int id)
        {
            if (id == 0)
                return null;
            return projectStepRepository.GetById(id);
        }

        public int Insert(ProjectStep model)
        {
            if (model == null)
                throw new ArgumentNullException("ProjectStep");
            return projectStepRepository.Insert(model);
        }

        public void Update(ProjectStep model)
        {
            if (model == null)
                throw new ArgumentNullException("ProjectStep");
            projectStepRepository.Update(model);

        }

        public void Delete(ProjectStep model)
        {
            if (model == null)
                throw new ArgumentNullException("ProjectStep");
            projectStepRepository.Delete(model);
        }

        public void SaveChanges()
        {
            projectStepRepository.SaveChanges();
        }
    }
}
