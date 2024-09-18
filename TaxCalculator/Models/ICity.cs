using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Models
{
    public interface ICity
    {
        string Name { get; init; }
        int Id { get; init; }
    }
}
