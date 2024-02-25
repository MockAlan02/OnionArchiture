using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDatetimeService _dateTime;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDatetimeService dateTime) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }
        public DbSet<Cliente> Clientes { get; set; }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = (DateTime)_dateTime.NowUtc!;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = (DateTime)_dateTime.NowUtc!;
                        break;
                    default:
                        break;

                }
                
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
