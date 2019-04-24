using Microsoft.EntityFrameworkCore;


//!!CHANGE THIS
namespace DentalEstrada.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        // "users" table is represented by this DbSet "Users"
        public DbSet<User> User { get; set; }
        public DbSet<Inquiry> Inquiry { get; set; }
        public DbSet<Appointment> Appointment { get; set; }

    }
}
