using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameLatitudeAndLongitude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "LogbookEntries",
                newName: "Lon");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "LogbookEntries",
                newName: "Lat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lon",
                table: "LogbookEntries",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "Lat",
                table: "LogbookEntries",
                newName: "Latitude");
        }
    }
}
