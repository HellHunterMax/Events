using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Location_StreetName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location_HouseNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location_City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location_PostalCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location_GeoCoordinate_Latitude = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_Longitude = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_Altitude = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_HorizontalAccuracy = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_VerticalAccuracy = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_Speed = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_Course = table.Column<double>(type: "float", nullable: false),
                    Email_Address = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    PhoneNumber_Number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration_Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration_End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location_StreetName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location_HouseNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location_City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location_PostalCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location_GeoCoordinate_Latitude = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_Longitude = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_Altitude = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_HorizontalAccuracy = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_VerticalAccuracy = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_Speed = table.Column<double>(type: "float", nullable: false),
                    Location_GeoCoordinate_Course = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events_Attendees",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events_Attendees", x => new { x.EventId, x.Id });
                    table.ForeignKey(
                        name: "FK_Events_Attendees_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EventId",
                table: "Categories",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EventId",
                table: "Comments",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OfficeId",
                table: "Events",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_EventId",
                table: "Files",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Events_Attendees");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
