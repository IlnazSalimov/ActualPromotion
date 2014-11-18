using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActualPromotion.Models
{
    public partial class Bid : IEntity
    {
        public int ID
        {
            get { return this.Id; }
        }
    }
}