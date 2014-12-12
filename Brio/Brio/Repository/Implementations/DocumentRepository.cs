
using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brio
{
    public class DocumentRepository : IDocumentRepository
    {
        private IRepository<Document> documentRepository;

        public DocumentRepository(IRepository<Document> _documentRepository)
        {
            this.documentRepository = _documentRepository;
        }

        public IQueryable<Document> GetAll()
        {
            return documentRepository.GetAll();
        }

        public int Insert(Document model)
        {
            if (model == null)
                throw new ArgumentNullException("Document");
            return documentRepository.Insert(model);
        }

        public void Update(Document model)
        {
            if (model == null)
                throw new ArgumentNullException("Document");
            documentRepository.Update(model);
        }

        public void Delete(Document model)
        {
            if (model == null)
                throw new ArgumentNullException("Document");
            documentRepository.Delete(model);
        }

        public void SaveChanges()
        {
            documentRepository.SaveChanges();
        }

        public Document GetById(int id)
        {
            return documentRepository.GetById(id);
        }

        public IQueryable<Document> GetCompanyDocuments(int currentCompany)
        {
            return documentRepository.GetAll().Where(doc => doc.CompanyId == currentCompany && doc.PageId == (int)PagesEnum.Documents);
        }

        public IQueryable<Document> GetProductDocuments(int productId, int currentCompany)
        {
            return documentRepository.GetAll().Where(doc => doc.CompanyId == currentCompany &&
                                                     doc.PageId == (int)PagesEnum.Products &&
                                                     doc.ProductId == productId);
        }
    }
}