using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Models;
using TaxCalculator.Services;

namespace TaxCalculator.Tests.Unit
{
    public class CongestionTaxCalculatorServiceTests
    {
        private readonly ITaxCalculatorService _sut;
        public readonly IVehicle _vehicle = Substitute.For<IVehicle>();
        private readonly IRuleCheckerService _ruleCheckerService=Substitute.For<IRuleCheckerService>();

        public CongestionTaxCalculatorServiceTests()
        {
            _sut=new CongestionTaxCalculatorService(_ruleCheckerService);
        } 

        [Fact]
        public void GetApplicableFee_ShouldReturnHigherValue_WhenAVehiclePassesSeveralGatesWithin60Minutes()
        {
            _ruleCheckerService.CheckFee(Arg.Any<DateTime>()).Returns(10);
            DateTime currentDate = DateTime.Now;
            DateTime LastDate = DateTime.Now.AddMinutes(-30);
            decimal currentFee = 13;

            var result = _sut.GetApplicableFee(currentFee, currentDate, LastDate);

            Assert.Equal(13, result);
        }

        [Fact]
        public void CalculateTotalTax_ShouldReturnTaxAmount_WhenVehicleIsNotDefinedInFreeVehiclesList()
        {
            _vehicle.GetVehicleType().Returns("BMW");
            _ruleCheckerService.CheckFee(Arg.Any<DateTime>()).Returns(5);
            _ruleCheckerService.IsFreeVehicle(_vehicle).Returns(false);
            _ruleCheckerService.CanChargeTax(Arg.Any<DateTime>(), _vehicle).Returns(true);
            var dates = new DateTime[]
            {
                new DateTime(2024,01,01,12,0,0),
                new DateTime(2024,01,01,13,0,0)
            };

            var result = _sut.CalculateTotalTax(_vehicle, dates);

            Assert.Equal(10, result);
        }
        
        [Fact]
        public void CalculateTotalTax_ShouldReturnZero_WhenVehicleIsDefinedInFreeVehiclesList()
        {
            _vehicle.GetVehicleType().Returns("Emergency");
            _ruleCheckerService.CheckFee(Arg.Any<DateTime>()).Returns(5);
            _ruleCheckerService.IsFreeVehicle(_vehicle).Returns(true);
            _ruleCheckerService.CanChargeTax(Arg.Any<DateTime>(), _vehicle).Returns(true);
            var dates = new DateTime[]
            {
                new DateTime(2024,01,01,12,0,0),
                new DateTime(2024,01,01,13,0,0)
            };

            var result = _sut.CalculateTotalTax(_vehicle, dates);

            Assert.Equal(0, result);
        }
        
        [Fact]
        public void CalculateTotalTax_ShouldReturnTaxAmount_WhenCanChargeVehicleForTax()
        {
            _vehicle.GetVehicleType().Returns("BMW");
            _ruleCheckerService.CheckFee(Arg.Any<DateTime>()).Returns(5);
            _ruleCheckerService.IsFreeVehicle(_vehicle).Returns(false);
            _ruleCheckerService.CanChargeTax(Arg.Any<DateTime>(), _vehicle).Returns(true);
            var dates = new DateTime[]
            {
                new DateTime(2024,01,01,12,0,0),
                new DateTime(2024,01,01,13,0,0)
            };

            var result = _sut.CalculateTotalTax(_vehicle, dates);

            Assert.Equal(10, result);
        }
        
        [Fact]
        public void CalculateTotalTax_ShouldReturnZero_WhenCanNotChargeVehicleForTax()
        {
            _vehicle.GetVehicleType().Returns("BMW");
            _ruleCheckerService.CheckFee(Arg.Any<DateTime>()).Returns(5);
            _ruleCheckerService.IsFreeVehicle(_vehicle).Returns(false);
            _ruleCheckerService.CanChargeTax(Arg.Any<DateTime>(), _vehicle).Returns(false);
            var dates = new DateTime[]
            {
                new DateTime(2024,01,01,12,0,0),
                new DateTime(2024,01,01,13,0,0)
            };

            var result = _sut.CalculateTotalTax(_vehicle, dates);

            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateTotalTax_ShouldReturn60_WhenCalcilatedTaxIsGreaterThan60()
        {
            _vehicle.GetVehicleType().Returns("BMW");
            _ruleCheckerService.CheckFee(Arg.Any<DateTime>()).Returns(20);
            _ruleCheckerService.IsFreeVehicle(_vehicle).Returns(false);
            _ruleCheckerService.CanChargeTax(Arg.Any<DateTime>(), _vehicle).Returns(true);
            var dates = new DateTime[]
            {
                new DateTime(2024,01,01,12,0,0),
                new DateTime(2024,01,01,13,0,0),
                new DateTime(2024,01,01,14,0,0),
                new DateTime(2024,01,01,15,0,0)
            };

            var result = _sut.CalculateTotalTax(_vehicle, dates);

            Assert.Equal(60, result);
        }

    }
}
