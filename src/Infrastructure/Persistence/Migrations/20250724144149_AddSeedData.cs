using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LogbookEntries",
                columns: new[] { "Id", "Body", "IsoTime", "Lat", "Lon", "Title" },
                values: new object[,]
                {
                    { 1, "All clear around main gate.", new DateTimeOffset(new DateTime(2025, 5, 14, 21, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 60.150300000000001, 25.029299999999999, "Night perimeter check" },
                    { 2, "Changed oil and inspected filters.", new DateTimeOffset(new DateTime(2025, 5, 15, 7, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 60.156100000000002, 25.024899999999999, "Generator maintenance" },
                    { 3, "Heavy rainfall expected after 1600 hrs.", new DateTimeOffset(new DateTime(2025, 5, 15, 9, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Weather update" },
                    { 4, "Quad-copter over Vuosaari direction, 200 m AGL.", new DateTimeOffset(new DateTime(2025, 5, 15, 12, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 60.156799999999997, 25.0472, "Drone sighting" },
                    { 5, "Ammo and rations accounted for.", new DateTimeOffset(new DateTime(2025, 5, 15, 14, 55, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 60.153399999999998, 25.0367, "Supply drop received" },
                    { 6, "Primary HF link down for 12 min — rebooted transceiver.", new DateTimeOffset(new DateTime(2025, 5, 15, 16, 22, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Radio outage" },
                    { 7, "One soldier treated for mild hypothermia.", new DateTimeOffset(new DateTime(2025, 5, 15, 18, 5, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 60.151000000000003, 25.032399999999999, "Medical check" },
                    { 8, "Alpha team left for sector Santahamina east shore.", new DateTimeOffset(new DateTime(2025, 5, 15, 19, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 60.1496, 25.0441, "Patrol departure" },
                    { 9, "Fox damaged comms cable near OP-2; temporary fix applied.", new DateTimeOffset(new DateTime(2025, 5, 15, 20, 18, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 60.1479, 25.0198, "Wildlife interference" },
                    { 10, "Logbook synced over SATCOM. All entries acknowledged.", new DateTimeOffset(new DateTime(2025, 5, 16, 6, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "Sync to HQ" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LogbookEntries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LogbookEntries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LogbookEntries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LogbookEntries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LogbookEntries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LogbookEntries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LogbookEntries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LogbookEntries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LogbookEntries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LogbookEntries",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
