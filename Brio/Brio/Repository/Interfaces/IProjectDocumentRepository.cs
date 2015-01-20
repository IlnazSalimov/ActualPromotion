using Brio;
using Brio.Models;
using System;
using System.Linq;
namespace Brio
{
    public interface IProjectDocumentRepository
    {
        void Delete(ProjectDocument model);
        IQueryable<ProjectDocument> GetAll();
        IQueryable<ProjectDocument> GetCompanyDocuments(int companyId);
        ProjectDocument GetById(int id);
        IQueryable<ProjectDocument> GetProjectDocuments(int projectId);
        int Insert(ProjectDocument model);
        void SaveChanges();
        void Update(ProjectDocument model);
    }
}
