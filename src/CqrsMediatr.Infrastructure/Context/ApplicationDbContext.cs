using CqrsMediatr.Domain.Entities;
using CqrsMediatr.Infrastructure.EtityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace CqrsMediatr.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MemberConfiguration());
        }
    }
}