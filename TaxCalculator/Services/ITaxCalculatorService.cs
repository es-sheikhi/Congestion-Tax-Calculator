using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models;

namespace TaxCalculator.Services
{
    public interface ITaxCalculatorService
    {
        decimal CalculateTotalTax(IVehicle vehicle, DateTime[] dateTimes);
        decimal GetApplicableFee(decimal currentFee, DateTime currentDate, DateTime? lastTaxedTime);
    }
}
