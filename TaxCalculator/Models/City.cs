using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    public class City : ICity
    {
        public string Name { get; init;}
        public int Id { get; init; }
    }
}
