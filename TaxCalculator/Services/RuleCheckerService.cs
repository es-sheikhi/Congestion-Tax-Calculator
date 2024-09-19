using DAL.Models;
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
    public class RuleCheckerService:IRuleCheckerService
    {
        private ICity _city;
        private IFeeRepository _feeRepository;
        private IFreeDayRepository _freeDaysRepository;
        private IPublicHolidayRepository _publicHolidayRepository;
        public RuleCheckerService(ICity city, IFeeRepository feeRepository, 
            IFreeDayRepository freeDayRepository, IPublicHolidayRepository publicHolidayRepository)
        {
            _city = city;
            _feeRepository = feeRepository;
            _freeDaysRepository=freeDayRepository;
            _publicHolidayRepository = publicHolidayRepository;
        }
        public bool CanChargeTax(DateTime date, IVehicle vehicle)
        {
            bool check = !IsFreeDate(date) &&
                   !IsFreeVehicle(vehicle);
            return check;
        }
        public bool CheckLastTaxedTime(DateTime date, DateTime? lastTaxedTime)
        {
            bool check = (lastTaxedTime != null && (date - lastTaxedTime.Value).TotalMinutes < 60);
            return check;
        }
        public bool IsFreeVehicle(IVehicle vehicle)
        {
            var exemptVehicles = Enum.GetNames(typeof(ExemptVehicleType)).ToHashSet();
            bool check = (vehicle != null) && exemptVehicles.Contains(vehicle.GetVehicleType());
            return check;
        }
        public bool IsFreeDate(DateTime date)
        {
            bool check = _freeDaysRepository.GetFreeDaysByCityName(_city.Name)
                .Any(c => c.DayOfWeek == date.DayOfWeek) ||
                   _publicHolidayRepository.GetPublicHolidaysByCityName(_city.Name)
                   .Any(c => c.Date.ToShortDateString() == date.ToShortDateString()) ||
                   date.Month == 7;
            return check;
        }

        public decimal CheckFee(DateTime date)
        {
            var fee = _feeRepository.GetFeesByCityName(_city.Name)
                .Where(c =>DateTime.Compare(date, c.From)>=0 &&
                 DateTime.Compare(date, c.To)<=0)
                .FirstOrDefault().Amount;
            return fee;
        }
    }
}
