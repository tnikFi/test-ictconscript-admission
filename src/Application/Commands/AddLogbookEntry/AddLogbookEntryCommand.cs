using Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.AddLogbookEntry;

public class AddLogbookEntryCommand : IValueCommand<LogbookEntry>
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public double? Lat { get; set; }
    public double? Lon { get; set; }
}

public class AddLogbookEntryCommandHandler : IValueCommandHandler<AddLogbookEntryCommand, LogbookEntry>
{
    private readonly IApplicationDbContext _context;

    public AddLogbookEntryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LogbookEntry> Handle(AddLogbookEntryCommand command, CancellationToken cancellationToken)
    {
        var entry = await _context.LogbookEntries.AddAsync(new LogbookEntry
        {
            Title = command.Title,
            Body = command.Body,
            Lat = command.Lat,
            Lon = command.Lon,
            IsoTime = DateTimeOffset.Now
        }, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }
}