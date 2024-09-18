using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PublicHoliday
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public City City { get; set; }
    }
}
