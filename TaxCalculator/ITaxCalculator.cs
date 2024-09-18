using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models;

namespace TaxCalculator
{
    public interface ITaxCalculator
    {
        decimal CalculateTotalTax(IVehicle vehicle, DateTime[] dateTimes);
    }
}
