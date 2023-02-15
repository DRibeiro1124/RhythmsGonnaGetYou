using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    public class RhythmsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=Music");
        }
    }
}