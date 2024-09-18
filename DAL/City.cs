using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class City
    {
        public string Name { get; set; }
        public List<Fee> FeeList { get; set; }
        public List<FreeDay> FreeDays { get; set; }
        public List<PublicHoliday> PublicHolidays { get; set; }
    }
}
