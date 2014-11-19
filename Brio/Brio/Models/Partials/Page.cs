using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public enum PagesEnum
    {
        Documents = 1,
        Products = 2,
        About = 3
    }

    public partial class Page : IEntity
    {
        public int ID
        {
            get { return this.Id; }
        }
    }
}
