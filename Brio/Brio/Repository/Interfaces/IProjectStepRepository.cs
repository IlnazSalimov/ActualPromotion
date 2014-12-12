using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public interface IProjectStepRepository
    {
        IQueryable<ProjectStep> GetAll();
        IQueryable<ProjectStep> GetProjectSteps(int projectId);
        ProjectStep GetById(int id);
        int Insert(ProjectStep model);
        void Update(ProjectStep model);
        void Delete(ProjectStep model);
        void SaveChanges();
    }
}
