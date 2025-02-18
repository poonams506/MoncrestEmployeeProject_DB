using Microsoft.EntityFrameworkCore;
using MoncrestEmployeeProject.Modules.Domain;

namespace MoncrestEmployeeProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public DbSet<EmployeeNetworkIps> EmployeeNetworkIpss { get; set; }

    }
}
