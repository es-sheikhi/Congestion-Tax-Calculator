using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Repositories
{
    public interface ICityRepository
    {
        List<City> GetAllCities();
        City GetCityById(int id);
        City GetCityByName(string name);
    }
}
