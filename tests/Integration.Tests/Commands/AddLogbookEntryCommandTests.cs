using Application.Commands.AddLogbookEntry;
using Domain.Entities;
using FluentValidation;

namespace Integration.Tests.Commands;

public class AddLogbookEntryCommandTests : TestBase
{
    [Fact]
    public async Task AssignsUniqueIdsToEntries()
    {
        var command1 = new AddLogbookEntryCommand
        {
            Title = "Title 1",
            Body = "Body 1"
        };
        var command2 = new AddLogbookEntryCommand
        {
            Title = "Title 2",
            Body = "Body 2"
        };
        await SendAsync<AddLogbookEntryCommand, LogbookEntry>(command1);
        await SendAsync<AddLogbookEntryCommand, LogbookEntry>(command2);

        var entries = Context.LogbookEntries.ToList();
        Assert.Equal(2, entries.Count);
        Assert.NotEqual(entries[0].Id, entries[1].Id);
    }

    [Theory]
    [InlineData("Title", "Body")]
    [InlineData("Title", "")]
    [InlineData("Title", "", 0d, 0d)]
    [InlineData("Title", "", 90d, 180d)]
    [InlineData("Title", "", -90d, -180d)]
    public async Task AddsValidEntryToDatabase(string title, string body, double? lat = null, double? lon = null)
    {
        var command = new AddLogbookEntryCommand
        {
            Title = title,
            Body = body,
            Lat = lat,
            Lon = lon
        };
        await SendAsync<AddLogbookEntryCommand, LogbookEntry>(command);
        var entries = Context.LogbookEntries.ToList();
        Assert.Single(entries);
        Assert.Equal(command.Title, entries[0].Title);
        Assert.Equal(command.Body, entries[0].Body);
        Assert.Equal(command.Lat, entries[0].Lat);
        Assert.Equal(command.Lon, entries[0].Lon);
        Assert.True(entries[0].IsoTime.IsWithin(DateTimeOffset.Now, TimeSpan.FromSeconds(5)));
    }

    [Theory]
    [InlineData(null, "body")]
    [InlineData("", "body")]
    [InlineData(" ", "body")]
    [InlineData("title", null)]
    public async Task ThrowsValidationExceptionForInvalidTitleOrBody(string? title, string? body)
    {
        var command = new AddLogbookEntryCommand
        {
            Title = title!,
            Body = body!
        };
        var act = async () => await SendAsync<AddLogbookEntryCommand, LogbookEntry>(command);
        await Assert.ThrowsAsync<ValidationException>(act);
    }

    [Theory]
    [InlineData(0d, null)]
    [InlineData(null, 0d)]
    [InlineData(0d, 181d)]
    [InlineData(0d, -181d)]
    [InlineData(91d, 0d)]
    [InlineData(-91d, 0d)]
    public async Task ThrowsValidationExceptionForInvalidCoordinates(double? latitude, double? longitude)
    {
        var command = new AddLogbookEntryCommand
        {
            Title = "Title",
            Body = "Body",
            Lat = latitude,
            Lon = longitude
        };
        var act = async () => await SendAsync<AddLogbookEntryCommand, LogbookEntry>(command);
        await Assert.ThrowsAsync<ValidationException>(act);
    }
}