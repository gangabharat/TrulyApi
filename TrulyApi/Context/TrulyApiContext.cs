using Microsoft.EntityFrameworkCore;
using TrulyApi.Entities;

namespace TrulyApi.Context
{
    public class TrulyApiContext : DbContext
    {
        public TrulyApiContext(DbContextOptions<TrulyApiContext> options)
            : base(options)
        {

        }

        public DbSet<QuoteItem> Quotes { get; set; } = default!;
        public DbSet<CardItem> Cards { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
