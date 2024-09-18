using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models;
using TaxCalculator.Repositories;
using TaxCalculator.Services;

namespace TaxCalculator
{
    public class CongestionTaxCalculator:ITaxCalculator
    {
        private IRuleChecker _ruleChecker;


        public CongestionTaxCalculator(IRuleChecker ruleChecker)
        {
            _ruleChecker=ruleChecker;
        }
        public decimal CalculateTotalTax(IVehicle vehicle, DateTime[] dateTimes)
        {
            if (_ruleChecker.IsFreeVehicle(vehicle) || dateTimes.Length == 0)
                return 0;

            decimal totalFee = 0;
            DateTime? lastTaxedTime = null;

            foreach (var date in dateTimes.OrderBy(d => d))
            {
                if (_ruleChecker.CanChargeTax(date, vehicle))
                {
                    decimal fee = _ruleChecker.CheckFee(date);
                    totalFee += GetApplicableFee(fee, date, lastTaxedTime);
                    lastTaxedTime = date;
                }
            }

            return Math.Min(totalFee, 60);
        }

        public decimal GetApplicableFee(decimal currentFee, DateTime currentDate, DateTime? lastTaxedTime)
        {
            if (lastTaxedTime.HasValue && _ruleChecker.CheckLastTaxedTime(currentDate, lastTaxedTime.Value))
            {
                decimal lastFee = _ruleChecker.CheckFee(lastTaxedTime.Value);
                return Math.Min(currentFee, lastFee);
            }
            return currentFee;
        }
    }


}
