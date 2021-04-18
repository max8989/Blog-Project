using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project.Data.Repository
{
    public class DateTimeNow : IDateTimeNow
    {
        public DateTime GetDateTimeNow() => DateTime.Now;
    }
}
