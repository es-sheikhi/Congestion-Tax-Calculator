using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Fee> Fees { get; set; }
        public List<FreeDay> FreeDays { get; set; }
        public List<PublicHoliday> PublicHolidays { get; set; }
    }
}
