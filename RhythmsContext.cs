using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    public class RhythmsContext : DbContext
    {
        //           C# Class
        //             |     DB Table Name
        //             |     |   
        //             V     V
        public DbSet<Band> Bands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=Music");
        }
    }
}