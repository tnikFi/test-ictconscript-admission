using Domain.Common;

namespace Domain.Entities;

public class LogbookEntry : BaseEntity
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}