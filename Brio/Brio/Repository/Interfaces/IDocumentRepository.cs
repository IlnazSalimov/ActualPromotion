
using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public interface IDocumentRepository
    {
        IQueryable<Document> GetAll();
        IQueryable<Document> GetCompanyDocuments(int currentCompany);
        IQueryable<Document> GetProductDocuments(int productId, int currentCompany);
        Document GetById(int id);
        int Insert(Document model);
        void Update(Document model);
        void Delete(Document model);
        void SaveChanges();
    }
}
