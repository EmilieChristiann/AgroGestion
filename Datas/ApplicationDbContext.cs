using AgroGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace AgroGestion.Datas
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}

