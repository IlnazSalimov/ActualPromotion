using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public enum ProjectStates
    {
        Active = 1,
        Deleted = 2
    }
    public partial class Project : IEntity
    {
        public int ID
        {
            get { return this.Id; }
        }
    }
}
