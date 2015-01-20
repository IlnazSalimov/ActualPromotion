using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public enum States
    {
        Active = 5,
        Deleted = -99
    }
    public partial class Division : IEntity
    {
        public int ID
        {
            get { return this.Id; }
        }
    }
}
