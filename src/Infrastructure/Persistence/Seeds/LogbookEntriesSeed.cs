using Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Seeds;

public class LogbookEntriesSeed : IDataSeed<LogbookEntry>
{
    public static IEnumerable<LogbookEntry> Seed()
{
    return new[]
    {
        new LogbookEntry
        {
            Id = 1,
            Title = "Night perimeter check",
            Body = "All clear around main gate.",
            IsoTime = DateTimeOffset.Parse("2025-05-14T21:30:00Z"),
            Lat = 60.1503,
            Lon = 25.0293
        },
        new LogbookEntry
        {
            Id = 2,
            Title = "Generator maintenance",
            Body = "Changed oil and inspected filters.",
            IsoTime = DateTimeOffset.Parse("2025-05-15T07:15:00Z"),
            Lat = 60.1561,
            Lon = 25.0249
        },
        new LogbookEntry
        {
            Id = 3,
            Title = "Weather update",
            Body = "Heavy rainfall expected after 1600 hrs.",
            IsoTime = DateTimeOffset.Parse("2025-05-15T09:40:00Z"),
            Lat = null,
            Lon = null
        },
        new LogbookEntry
        {
            Id = 4,
            Title = "Drone sighting",
            Body = "Quad-copter over Vuosaari direction, 200 m AGL.",
            IsoTime = DateTimeOffset.Parse("2025-05-15T12:03:00Z"),
            Lat = 60.1568,
            Lon = 25.0472
        },
        new LogbookEntry
        {
            Id = 5,
            Title = "Supply drop received",
            Body = "Ammo and rations accounted for.",
            IsoTime = DateTimeOffset.Parse("2025-05-15T14:55:00Z"),
            Lat = 60.1534,
            Lon = 25.0367
        },
        new LogbookEntry
        {
            Id = 6,
            Title = "Radio outage",
            Body = "Primary HF link down for 12 min — rebooted transceiver.",
            IsoTime = DateTimeOffset.Parse("2025-05-15T16:22:00Z"),
            Lat = null,
            Lon = null
        },
        new LogbookEntry
        {
            Id = 7,
            Title = "Medical check",
            Body = "One soldier treated for mild hypothermia.",
            IsoTime = DateTimeOffset.Parse("2025-05-15T18:05:00Z"),
            Lat = 60.1510,
            Lon = 25.0324
        },
        new LogbookEntry
        {
            Id = 8,
            Title = "Patrol departure",
            Body = "Alpha team left for sector Santahamina east shore.",
            IsoTime = DateTimeOffset.Parse("2025-05-15T19:40:00Z"),
            Lat = 60.1496,
            Lon = 25.0441
        },
        new LogbookEntry
        {
            Id = 9,
            Title = "Wildlife interference",
            Body = "Fox damaged comms cable near OP-2; temporary fix applied.",
            IsoTime = DateTimeOffset.Parse("2025-05-15T20:18:00Z"),
            Lat = 60.1479,
            Lon = 25.0198
        },
        new LogbookEntry
        {
            Id = 10,
            Title = "Sync to HQ",
            Body = "Logbook synced over SATCOM. All entries acknowledged.",
            IsoTime = DateTimeOffset.Parse("2025-05-16T06:45:00Z"),
            Lat = null,
            Lon = null
        }
    };
}
}