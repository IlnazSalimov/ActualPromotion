using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio
{
    public class CompanyWorkDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string ImagesUrl { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
