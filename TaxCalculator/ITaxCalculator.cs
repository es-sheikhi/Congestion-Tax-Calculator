using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator
{
    public interface ITaxCalculator
    {
        decimal CalculateTotalTax();
        bool CanChargeTax();
        bool IsFreeVehicle();
        bool IsFreeDate();
        decimal GetFee();

    }
}
