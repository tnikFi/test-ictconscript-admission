using Common.Interfaces;
using Cortex.Mediator.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries;

public class GetLogbookEntriesQuery : IQuery<IEnumerable<LogbookEntry>>;

public class GetLogbookEntriesQueryHandler : IQueryHandler<GetLogbookEntriesQuery, IEnumerable<LogbookEntry>>
{
    private readonly IApplicationDbContext _context;

    public GetLogbookEntriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LogbookEntry>> Handle(GetLogbookEntriesQuery query, CancellationToken cancellationToken)
    {
        var entries = await _context.LogbookEntries
            .ToArrayAsync(cancellationToken);
        return entries.OrderByDescending(x => x.Timestamp);
    }
}
