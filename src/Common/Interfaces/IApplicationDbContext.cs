using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<LogbookEntry> LogbookEntries { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
