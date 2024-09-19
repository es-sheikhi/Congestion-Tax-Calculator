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

            var result=_sut.IsFreeVehicle(_vehicle);

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

    }
}
