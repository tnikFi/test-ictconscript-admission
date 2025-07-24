using Domain.Common;

namespace Domain.Entities;

public class LogbookEntry : BaseEntity
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public double? Lat { get; set; }
    public double? Lon { get; set; }
}