using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brio.Models
{
    public enum PagesEnum
    {
        [Description("Документы")]
        Documents = 1,
        [Description("Продукты")]
        Products = 2,
        [Description("О компании")]
        About = 3,
        [Description("Экспертиза")]
        ExaminingDivision = 4,
        [Description("Проектирование")]
        Engineering = 5,
        [Description("Обследование")]
        Examination = 6
    }

    public partial class Page : IEntity
    {
        public int ID
        {
            get { return this.Id; }
        }
    }
}
