using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brio.Models
{
    public enum Roles
    {
        Admin = 1,
        ProjectManager = 2,
        Client = 3,
        Employee = 4
    }
    public partial class Role : IEntity
    {
        public int ID
        {
            get { return this.Id; }
        }
    }
}