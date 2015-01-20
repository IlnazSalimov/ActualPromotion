using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public class ProjectDocumentRepository : IProjectDocumentRepository
    {
        private IRepository<ProjectDocument> projectDocumentRepository;

        public ProjectDocumentRepository(IRepository<ProjectDocument> _projectDocumentRepository)
        {
            this.projectDocumentRepository = _projectDocumentRepository;
        }

        public IQueryable<ProjectDocument> GetAll()
        {
            return projectDocumentRepository.GetAll();
        }

        public IQueryable<ProjectDocument> GetCompanyDocuments(int companyId)
        {
            return projectDocumentRepository.GetAll().Where(d => d.Project.CompanyId == companyId);
        }

        public int Insert(ProjectDocument model)
        {
            if (model == null)
                throw new ArgumentNullException("ProjectDocument");
            return projectDocumentRepository.Insert(model);
        }

        public void Update(ProjectDocument model)
        {
            if (model == null)
                throw new ArgumentNullException("ProjectDocument");
            projectDocumentRepository.Update(model);
        }

        public void Delete(ProjectDocument model)
        {
            if (model == null)
                throw new ArgumentNullException("ProjectDocument");
            projectDocumentRepository.Delete(model);
        }

        public void SaveChanges()
        {
            projectDocumentRepository.SaveChanges();
        }

        public ProjectDocument GetById(int id)
        {
            return projectDocumentRepository.GetById(id);
        }

        public IQueryable<ProjectDocument> GetProjectDocuments(int projectId)
        {
            return projectDocumentRepository.GetAll().Where(doc => doc.ProjectId == projectId);
        }
    }
}
