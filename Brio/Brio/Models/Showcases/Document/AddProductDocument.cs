using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public class AddProductDocument : AddDocument
    {
        public int ProductId { get; set; }
    }
}
