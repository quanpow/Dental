using Microsoft.EntityFrameworkCore;


//!!CHANGE THIS
namespace DentalEstrada.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        // "users" table is represented by this DbSet "Users"
        // public DbSet<Muscle> Muscle { get; set; }
    }
}
