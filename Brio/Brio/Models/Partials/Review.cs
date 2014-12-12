using Brio;
using Brio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brio.Models
{
    public partial class Review : IEntity
    {
        public int ID
        {
            get { return this.Id; }
        }
    }
}