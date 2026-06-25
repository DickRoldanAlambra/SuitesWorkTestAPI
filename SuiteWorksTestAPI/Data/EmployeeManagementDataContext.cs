using Microsoft.EntityFrameworkCore;
using SuiteWorksTestAPI.Models;

namespace SuiteWorksTestAPI.Data
{
    public class EmployeeManagementDataContext : DbContext
    {
        public EmployeeManagementDataContext(
         DbContextOptions<EmployeeManagementDataContext> options)
         : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
