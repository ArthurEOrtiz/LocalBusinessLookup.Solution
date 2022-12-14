using Microsoft.EntityFrameworkCore;

namespace LocalBusinessLookup.Models
{
  public class LocalBusinessLookupContext : DbContext
  {
    public LocalBusinessLookupContext(DbContextOptions<LocalBusinessLookupContext> options)
      : base(options)
      {   
      }

      public DbSet<LocalBusiness> LocalBusinesses { get; set; }
  }
}