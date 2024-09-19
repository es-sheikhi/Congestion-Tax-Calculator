using DAL.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models;
using TaxCalculator.Repositories;
using TaxCalculator.Services;

namespace TaxCalculator.Tests.Unit
{
    public class RuleCheckerServiceTests
    {
        private readonly IRuleCheckerService _sut;
        private readonly ICity _city = Substitute.For<ICity>();
        public readonly IVehicle _vehicle = Substitute.For<IVehicle>();
        private readonly IFeeRepository _feeRepository = Substitute.For<IFeeRepository>();
        private readonly IFreeDayRepository _freeDayRepository = Substitute.For<IFreeDayRepository>();
        private readonly IPublicHolidayRepository _publicHolidayRepository = Substitute.For<IPublicHolidayRepository>();
        public RuleCheckerServiceTests()
        {
            _sut = new RuleCheckerService(_city, _feeRepository,
                _freeDayRepository, _publicHolidayRepository);
        }

        [Fact]
        public void IsFreeVehicle_ShouldReturnTrue_WhenVehicleIsDefinedInFreeVehiclesList()
        {
            _vehicle.GetVehicleType().Returns("Emergency");

            var result = _sut.IsFreeVehicle(_vehicle);

            Assert.True(result);
        }

        [Fact]
        public void IsFreeVehicle_ShouldReturnFalse_WhenVehicleIsNotDefinedInFreeVehiclesList()
        {
            _vehicle.GetVehicleType().Returns("BMW");

            var result = _sut.IsFreeVehicle(_vehicle);

            Assert.False(result);
        }

        [Fact]
        public void IsFreeDate_ShouldReturnTrue_WhenTheGivenDayIsInFreeDaysList()
        {
            List<FreeDay> freeDays = new List<FreeDay>
            {
                new FreeDay
                {
                    DayOfWeek = DayOfWeek.Sunday
                }
            };
            _city.Name.Returns("Gothenburg");
            _freeDayRepository.GetFreeDaysByCityName(_city.Name).Returns(freeDays);
            _publicHolidayRepository.GetPublicHolidaysByCityName(_city.Name).Returns(new List<PublicHoliday>());
            var date = new DateTime(2024, 9, 15);

            var result = _sut.IsFreeDate(date);

            Assert.True(result);
        }

        [Fact]
        public void IsFreeDate_ShouldReturnFalse_WhenTheGivenDateIsNotInFreeDaysList()
        {
            List<FreeDay> freeDays = new List<FreeDay>
            {
                new FreeDay
                {
                    DayOfWeek = DayOfWeek.Saturday
                }
            };
            _city.Name.Returns("Gothenburg");
            _freeDayRepository.GetFreeDaysByCityName(_city.Name).Returns(freeDays);
            _publicHolidayRepository.GetPublicHolidaysByCityName(_city.Name).Returns(new List<PublicHoliday>());
            var date = new DateTime(2024, 9, 15);

            var result = _sut.IsFreeDate(date);

            Assert.False(result);
        }


        [Fact]
        public void IsFreeDate_ShouldReturnTrue_WhenTheGivenDateIsInPublicHolidays()
        {
            List<PublicHoliday> publicHolidays = new List<PublicHoliday>
            {
                new PublicHoliday
                {
                    Date = new DateTime(2024,10,12)
                }
            };
            _city.Name.Returns("Gothenburg");
            _freeDayRepository.GetFreeDaysByCityName(_city.Name).Returns(new List<FreeDay>());
            _publicHolidayRepository.GetPublicHolidaysByCityName(_city.Name).Returns(publicHolidays);
            var date = new DateTime(2024, 10, 12);

            var result = _sut.IsFreeDate(date);

            Assert.True(result);
        }

        [Fact]
        public void IsFreeDate_ShouldReturnFalse_WhenTheGivenDateIsInPublicHolidays()
        {
            List<PublicHoliday> publicHolidays = new List<PublicHoliday>
            {
                new PublicHoliday
                {
                    Date = new DateTime(2024,10,12)
                }
            };
            _city.Name.Returns("Gothenburg");
            _freeDayRepository.GetFreeDaysByCityName(_city.Name).Returns(new List<FreeDay>());
            _publicHolidayRepository.GetPublicHolidaysByCityName(_city.Name).Returns(publicHolidays);
            var date = new DateTime(2024, 11, 12);

            var result = _sut.IsFreeDate(date);

            Assert.False(result);
        }

        [Fact]
        public void CheckLastTaxedTime_ShouldReturnTrue_WhenCalculatedTaxIsInLast60Minutes()
        {
            var currentDateTime = new DateTime(2024, 01, 01, 11, 0, 0);
            var lastDateTime = new DateTime(2024, 01, 01, 10, 30, 0);

            var result = _sut.CheckLastTaxedTime(currentDateTime, lastDateTime);

            Assert.True(result);
        }

        [Fact]
        public void CheckLastTaxedTime_ShouldReturnFalse_WhenCalculatedTaxIsNotInLast60Minutes()
        {
            var currentDateTime = new DateTime(2024, 01, 01, 11, 0, 0);
            var lastDateTime = new DateTime(2024, 01, 01, 09, 30, 0);

            var result = _sut.CheckLastTaxedTime(currentDateTime, lastDateTime);

            Assert.False(result);
        }

        [Fact]
        public void CheckFee_ShouldReturnAmount_WhenGivenTimeIsInDefinedHours()
        {
            List<Fee> fees = new List<Fee>
            {
                new Fee
                {
                    From = new DateTime(2024,01,01,06,0,0),
                    To = new DateTime(2024,01,01,06,29,0),
                    Amount = 8
                },
                new Fee
                {
                    From = new DateTime(2024,01,01,00,0,0),
                    To = new DateTime(2024,01,01,05,59,0),
                    Amount = 0
                }
            };
            var date = new DateTime(2024, 01, 01, 06, 12, 0);
            _city.Name.Returns("Gothenburg");
            _feeRepository.GetFeesByCityName(_city.Name).Returns(fees);

            var result = _sut.CheckFee(date);

            Assert.Equal(8, result);
        }

        [Fact]
        public void CheckFee_ShouldReturnZero_WhenGivenTimeIsNotInDefinedHours()
        {
            List<Fee> fees = new List<Fee>
            {
                new Fee
                {
                    From = new DateTime(2024,01,01,06,0,0),
                    To = new DateTime(2024,01,01,06,29,0),
                    Amount = 8
                },
                new Fee
                {
                    From = new DateTime(2024,01,01,00,0,0),
                    To = new DateTime(2024,01,01,05,59,0),
                    Amount = 0
                }
            };
            var date = new DateTime(2024, 01, 01, 04, 12, 0);
            _city.Name.Returns("Gothenburg");
            _feeRepository.GetFeesByCityName(_city.Name).Returns(fees);

            var result = _sut.CheckFee(date);

            Assert.Equal(0, result);
        }


    }
}
