using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entities
{
    public class MedicineContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public MedicineContext() 
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseSqlServer(Configuration.GetConnectionString("MedicineDBEntities"));
            options.UseSqlServer("Data Source = DESKTOP-G69HVIR; Database = Pharmacy_Stock; integrated security = true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicine>().ToTable("Medicine");
            modelBuilder.Entity<Features>().ToTable("Features");
        }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Features> Features { get; set; }
    }
}
