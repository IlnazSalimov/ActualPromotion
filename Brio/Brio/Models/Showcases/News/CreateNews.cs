using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public class CreateNews
    {
        public int CompanyId { get; set; }
        public int DivisionId { get; set; }
        public int AuthorUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
    }
}
