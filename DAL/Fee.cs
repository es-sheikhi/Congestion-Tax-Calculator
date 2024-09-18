using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Fee
    {
        public TimeOnly From { get; set; }
        public TimeOnly To { get; set; }
        public decimal Amount { get; set; }
        public City City { get; set; }
    }
}
