using Microsoft.EntityFrameworkCore;
using RapidPayAPI.Entities;

namespace RapidPayAPI.Data
{
    public class RapidPayContext : DbContext
    {
        public RapidPayContext(DbContextOptions<RapidPayContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Card> Card { get; set; }
    }
}