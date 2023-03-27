using CRUDWebAPI.Model;
using Microsoft.EntityFrameworkCore;


namespace CRUDWebAPI.Database
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        { 
        }
            public DbSet<Employee> Employees { get; set; }
       
    }
}
