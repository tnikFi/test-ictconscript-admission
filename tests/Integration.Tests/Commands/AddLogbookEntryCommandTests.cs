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
            Latitude = lat,
            Longitude = lon
        };
        await SendAsync<AddLogbookEntryCommand, LogbookEntry>(command);
        var entries = Context.LogbookEntries.ToList();
        Assert.Single(entries);
        Assert.Equal(command.Title, entries[0].Title);
        Assert.Equal(command.Body, entries[0].Body);
        Assert.Equal(command.Latitude, entries[0].Latitude);
        Assert.Equal(command.Longitude, entries[0].Longitude);
        Assert.True(entries[0].Timestamp.IsWithin(DateTimeOffset.Now, TimeSpan.FromSeconds(5)));
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
            Latitude = latitude,
            Longitude = longitude
        };
        var act = async () => await SendAsync<AddLogbookEntryCommand, LogbookEntry>(command);
        await Assert.ThrowsAsync<ValidationException>(act);
    }
}