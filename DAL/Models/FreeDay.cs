using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class FreeDay
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public City City { get; set; }
    }
}
