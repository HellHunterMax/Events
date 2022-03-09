using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Infrastructure.Migrations
{
    public partial class ChangeGeoLocationToLongAndLat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location_GeoCoordinate_Altitude",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Location_GeoCoordinate_Course",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Location_GeoCoordinate_HorizontalAccuracy",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Location_GeoCoordinate_Speed",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Location_GeoCoordinate_VerticalAccuracy",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Location_GeoCoordinate_Altitude",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location_GeoCoordinate_Course",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location_GeoCoordinate_HorizontalAccuracy",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location_GeoCoordinate_Speed",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Location_GeoCoordinate_VerticalAccuracy",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Location_GeoCoordinate_Longitude",
                table: "Offices",
                newName: "Location_Longitude");

            migrationBuilder.RenameColumn(
                name: "Location_GeoCoordinate_Latitude",
                table: "Offices",
                newName: "Location_Latitude");

            migrationBuilder.RenameColumn(
                name: "Location_GeoCoordinate_Longitude",
                table: "Events",
                newName: "Location_Longitude");

            migrationBuilder.RenameColumn(
                name: "Location_GeoCoordinate_Latitude",
                table: "Events",
                newName: "Location_Latitude");

            migrationBuilder.AlterColumn<float>(
                name: "Location_Longitude",
                table: "Offices",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "Location_Latitude",
                table: "Offices",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "Location_Longitude",
                table: "Events",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "Location_Latitude",
                table: "Events",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location_Longitude",
                table: "Offices",
                newName: "Location_GeoCoordinate_Longitude");

            migrationBuilder.RenameColumn(
                name: "Location_Latitude",
                table: "Offices",
                newName: "Location_GeoCoordinate_Latitude");

            migrationBuilder.RenameColumn(
                name: "Location_Longitude",
                table: "Events",
                newName: "Location_GeoCoordinate_Longitude");

            migrationBuilder.RenameColumn(
                name: "Location_Latitude",
                table: "Events",
                newName: "Location_GeoCoordinate_Latitude");

            migrationBuilder.AlterColumn<double>(
                name: "Location_GeoCoordinate_Longitude",
                table: "Offices",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Location_GeoCoordinate_Latitude",
                table: "Offices",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<double>(
                name: "Location_GeoCoordinate_Altitude",
                table: "Offices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_GeoCoordinate_Course",
                table: "Offices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_GeoCoordinate_HorizontalAccuracy",
                table: "Offices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_GeoCoordinate_Speed",
                table: "Offices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_GeoCoordinate_VerticalAccuracy",
                table: "Offices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<double>(
                name: "Location_GeoCoordinate_Longitude",
                table: "Events",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Location_GeoCoordinate_Latitude",
                table: "Events",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<double>(
                name: "Location_GeoCoordinate_Altitude",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_GeoCoordinate_Course",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_GeoCoordinate_HorizontalAccuracy",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_GeoCoordinate_Speed",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_GeoCoordinate_VerticalAccuracy",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
