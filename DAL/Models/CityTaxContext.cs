using DAL.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CityTaxContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<FreeDay> FreeDays { get; set; }
        public DbSet<PublicHoliday> PublicHolidays { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DBConnection.ConnectionString);
        }
    }
}
