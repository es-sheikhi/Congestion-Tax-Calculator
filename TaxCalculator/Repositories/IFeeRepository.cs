using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Repositories
{
    public interface IFeeRepository
    {
        List<Fee> GetFeesByCityName(string name);
        List<Fee> GetFeesByCityId(int id);
    }
}
