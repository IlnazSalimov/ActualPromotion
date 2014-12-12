using Brio;
using Brio.Models;
using System;
using System.Linq;
namespace Brio
{
    public interface IProjectDocumentRepository
    {
        void Delete(ProjectDocument model);
        System.Linq.IQueryable<ProjectDocument> GetAll();
        ProjectDocument GetById(int id);
        IQueryable<ProjectDocument> GetProjectDocuments(int projectId);
        int Insert(ProjectDocument model);
        void SaveChanges();
        void Update(ProjectDocument model);
    }
}
