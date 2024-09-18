using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Repositories
{
    public interface IFreeDayRepository
    {
        List<FreeDay> GetFreeDaysByCityName(string name);
        List<FreeDay> GetFreeDaysByCityId(int id);
    }
}
