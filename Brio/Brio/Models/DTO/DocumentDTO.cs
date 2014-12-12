using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public class ProjectDocumentDTO
    {
        public string DocumentPath { get; set; }
        public string DocumentTitle { get; set; }
        public DateTime UploadDate { get; set; }
        public int ProjectId { get; set; }
        public int Id { get; set; }
    }
}
