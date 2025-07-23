using Common.Exceptions;
using Common.Interfaces;
using Cortex.Mediator.Queries;
using Domain.Entities;

namespace Application.Queries.GetLogbookEntry;

public class GetLogbookEntryQuery(int id) : IQuery<LogbookEntry>
{
    public int Id { get; set; } = id;
}

public class GetLogbookEntryQueryHandler : IQueryHandler<GetLogbookEntryQuery, LogbookEntry>
{
    private readonly IApplicationDbContext _context;

    public GetLogbookEntryQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LogbookEntry> Handle(GetLogbookEntryQuery query, CancellationToken cancellationToken)
    {
        var entry = await _context.LogbookEntries.FindAsync([query.Id], cancellationToken: cancellationToken);
        NotFoundException.ThrowIfNull(entry);
        return entry;
    }
}