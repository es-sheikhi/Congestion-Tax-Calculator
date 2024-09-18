using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Repositories
{
    public interface IPublicHolidayRepository
    {
        List<PublicHoliday> GetPublicHolidaysByCityName(string name);
        List<PublicHoliday> GetPublicHolidaysByCityId(int id);
    }
}
