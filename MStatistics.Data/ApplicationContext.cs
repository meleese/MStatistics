using Microsoft.EntityFrameworkCore;
using MStatistics.DomainModels.Entities;

namespace MStatistics.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<HashTag> HashTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Tweet>(
                    e =>
                    {
                        e.HasKey(t => t.Id);
                        e.HasMany(t => t.hashTags);
                    })
                .Entity<HashTag>(
                    e =>
                    {
                        e.HasKey(t => t.Id);
                        e.HasMany(t => t.Tweets);
                    });
        }
    }
}