using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Repositories
{
    public class PublicHolidayRepository : IPublicHolidayRepository
    {
        private readonly CityTaxContext _taxContext;
        public PublicHolidayRepository(CityTaxContext taxContext)
        {
            _taxContext = taxContext;
        }
        public List<PublicHoliday> GetPublicHolidaysByCityName(string name)
        {
            List<PublicHoliday> freeDays = _taxContext.PublicHolidays
                .Where(c => c.City.Name == name)
                .Include(c => c.City)
                .ToList();
            return freeDays;
        }
        public List<PublicHoliday> GetPublicHolidaysByCityId(int id)
        {
            List<PublicHoliday> freeDays = _taxContext.PublicHolidays
                .Where(c => c.City.Id == id)
                .Include(c => c.City)
                .ToList();
            return freeDays;
        }

    }
}
