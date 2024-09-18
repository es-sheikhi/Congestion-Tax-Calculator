using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Repositories
{
    public class FreeDaysRepository : IFreeDayRepository
    {
        private readonly CityTaxContext _taxContext;
        public FreeDaysRepository(CityTaxContext taxContext)
        {
            _taxContext = taxContext;
        }
        public List<FreeDay> GetFreeDaysByCityName(string name)
        {
            List<FreeDay> freeDays = _taxContext.FreeDays
                .Where(c => c.City.Name == name)
                .Include(c => c.City)
                .ToList();
            return freeDays;
        }
        public List<FreeDay> GetFreeDaysByCityId(int id)
        {
            List<FreeDay> freeDays = _taxContext.FreeDays
                .Where(c => c.City.Id == id)
                .Include(c => c.City)
                .ToList();
            return freeDays;
        }

    }
}
