using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Repositories
{
    public class FeeRepository : IFeeRepository
    {
        private readonly CityTaxContext _taxContext;
        public FeeRepository(CityTaxContext taxContext)
        {
            _taxContext = taxContext;
        }
        public List<Fee> GetFeesByCityName(string name)
        {
            List<Fee> fees = _taxContext.Fees
                .Where(c => c.City.Name == name)
                .Include(c => c.City)
                .ToList();
            return fees;
        }
        public List<Fee> GetFeesByCityId(int id)
        {
            List<Fee> fees = _taxContext.Fees
                .Where(c => c.City.Id == id)
                .Include(c => c.City)
                .ToList();
            return fees;
        }
    }
}
