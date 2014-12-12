using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public partial class InfoCard:IEntity
    {
        public int ID
        {
            get { return this.Id; }
        }

        public string FullName
        {
            get { return this.Surname + " " + this.Name + " " + this.Patronymic; }
        }
    }
}
