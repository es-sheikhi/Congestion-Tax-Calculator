﻿using DAL;
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
    public class CongestionTaxCalculatorService:ITaxCalculatorService
    {
        private IRuleCheckerService _ruleChecker;


        public CongestionTaxCalculatorService(IRuleCheckerService ruleChecker)
        {
            _ruleChecker=ruleChecker;
        }
        public decimal CalculateTotalTax(IVehicle vehicle, DateTime[] dateTimes)
        {
            if (_ruleChecker.IsFreeVehicle(vehicle) || dateTimes.Length == 0)
                return 0;

            decimal totalFee = 0;
            DateTime? lastChekedTime = null;

            foreach (var date in dateTimes)
            {
                if (_ruleChecker.CanChargeTax(date, vehicle))
                {
                    decimal fee = _ruleChecker.CheckFee(date);
                    totalFee += GetApplicableFee(fee, date, lastChekedTime);
                    lastChekedTime = date;
                }
            }

            return Math.Min(totalFee, 60);
        }

        public decimal GetApplicableFee(decimal currentFee, DateTime currentDate, DateTime? lastChekedTime)
        {
            if (lastChekedTime.HasValue && _ruleChecker.CheckLastTaxedTime(currentDate, lastChekedTime.Value))
            {
                decimal lastFee = _ruleChecker.CheckFee(lastChekedTime.Value);
                return Math.Max(currentFee, lastFee);
            }
            return currentFee;
        }
    }


}
