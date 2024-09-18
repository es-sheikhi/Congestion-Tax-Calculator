using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models;

namespace TaxCalculator
{
    public interface IRuleChecker
    {
        bool CanChargeTax(DateTime date, IVehicle vehicle, DateTime? lastTaxedTime);
        bool IsFreeVehicle(IVehicle vehicle);
        bool IsFreeDate(DateTime date);
    }
}
