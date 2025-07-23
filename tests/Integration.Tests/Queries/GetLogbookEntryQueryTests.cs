using Application.Queries.GetLogbookEntry;
using Common.Exceptions;
using Domain.Entities;

namespace Integration.Tests.Queries;

public class GetLogbookEntryQueryTests : TestBase
{
    [Fact]
    public async Task ReturnsCorrectLogbookEntry()
    {
        await Context.LogbookEntries.AddRangeAsync(new LogbookEntry
        {
            Title = "Entry 1",
            Body = "Body 1"
        }, new LogbookEntry
        {
            Title = "Entry 2",
            Body = "Body 2"
        });
        await Context.SaveChangesAsync();

        var query = new GetLogbookEntryQuery(1);
        var result = await Mediator.SendAsync<GetLogbookEntryQuery, LogbookEntry>(query);
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Entry 1", result.Title);
        Assert.Equal("Body 1", result.Body);
    }

    [Fact]
    public async Task ThrowsNotFoundExceptionWhenEntryNotFound()
    {
        var query = new GetLogbookEntryQuery(-1);
        var act = async () => await Mediator.SendAsync<GetLogbookEntryQuery, LogbookEntry>(query);
        await Assert.ThrowsAsync<NotFoundException>(act);
    }
}