using DemoApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication1.DbContexts
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = PersonInfo.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
