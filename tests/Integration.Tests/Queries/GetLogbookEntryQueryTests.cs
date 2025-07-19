using Application.Queries;
using Domain.Entities;

namespace Integration.Tests.Queries;

public class GetLogbookEntryQueryTests : TestBase
{
    [Fact]
    public async Task TestGetLogbookEntryQuery()
    {
        await Context.LogbookEntries.AddRangeAsync(new LogbookEntry
        {
            Title = "test",
            Body = "testing",
            Timestamp = DateTimeOffset.Now
        }, new LogbookEntry
        {
            Title = "test 2",
            Body = "testing again",
            Timestamp = DateTimeOffset.Now
        });
        await Context.SaveChangesAsync();

        var result =
            await Mediator.SendAsync<GetLogbookEntriesQuery, IEnumerable<LogbookEntry>>(new GetLogbookEntriesQuery());
        var entries = result.ToList();
        
        Assert.NotEmpty(entries);
        Assert.Equal(2, entries.Count);
    }
}