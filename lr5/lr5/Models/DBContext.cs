using Microsoft.EntityFrameworkCore;
namespace lr5.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Usser> Users { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
    }

    public class Usser
    {
        public int id { get; set; }

        public string name { get; set; }
    }
}
