using Application.Queries;
using Domain.Entities;

namespace Integration.Tests.Queries;

public class GetLogbookEntryQueryTests : TestBase
{
    [Fact]
    public async Task ReturnsLogbookEntries()
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

    [Fact]
    public async Task ReturnsNewestEntriesFirst()
    {
        await Context.LogbookEntries.AddRangeAsync(new LogbookEntry
        {
            Title = "test",
            Body = "testing",
            Timestamp = DateTimeOffset.Now.AddDays(-1)
        }, new LogbookEntry
        {
            Title = "test 2",
            Body = "testing again",
            Timestamp = DateTimeOffset.Now
        }, new LogbookEntry
        {
            Title = "test 3",
            Body = "still testing",
            Timestamp = DateTimeOffset.Now.AddDays(-2)
        });
        await Context.SaveChangesAsync();
        
        var result =
            await Mediator.SendAsync<GetLogbookEntriesQuery, IEnumerable<LogbookEntry>>(new GetLogbookEntriesQuery());
        var entries = result.ToList();
        
        Assert.NotEmpty(entries);
        Assert.Equal(3, entries.Count);
        Assert.Equal("test 2", entries[0].Title);
        Assert.Equal("test", entries[1].Title);
        Assert.Equal("test 3", entries[2].Title);
    }
}