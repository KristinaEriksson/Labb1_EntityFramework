using Labb1_EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1_EntityFramework.Data
{
    public class ForzaDbContext : DbContext
    {
        public ForzaDbContext(DbContextOptions<ForzaDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<TypeOfLeave> TypeOfLeaves { get; set; }
        public DbSet<LeaveList> LeaveLists { get; set; }
    }
}
