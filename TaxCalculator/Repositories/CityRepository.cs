using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly CityTaxContext _taxContext;
        public CityRepository(CityTaxContext taxContext)
        {
            _taxContext = taxContext;
        }
        public List<City> GetAllCities()
        {
            List<City> cities = _taxContext.Cities
                .Include(c => c.Fees)
                .Include(c => c.FreeDays)
                .Include(c => c.PublicHolidays)
                .ToList();
            return cities;
        }

        public City GetCityById(int id)
        {
            City city = _taxContext.Cities
                .Where(c => c.Id == id)
                .Include(c => c.Fees)
                .Include(c => c.FreeDays)
                .Include(c => c.PublicHolidays)
                .FirstOrDefault();
            return city;
        }

        public City GetCityByName(string name)
        {
            City? city = _taxContext.Cities
                .Where(c => c.Name == name)
                .Include(c => c.Fees)
                .Include(c => c.FreeDays)
                .Include(c => c.PublicHolidays)
                .FirstOrDefault();
            return city;
        }
    }
}
