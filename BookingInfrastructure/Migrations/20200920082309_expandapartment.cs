using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingInfrastructure.Migrations
{
    public partial class expandapartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BbqTools",
                table: "Apartments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ClimateControl",
                table: "Apartments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "ClosestBeachDistance",
                table: "Apartments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ClosestMarketDistance",
                table: "Apartments",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "KitchenTool",
                table: "Apartments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBedrooms",
                table: "Apartments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Wifi",
                table: "Apartments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WorkSpace",
                table: "Apartments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BbqTools",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ClimateControl",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ClosestBeachDistance",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ClosestMarketDistance",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "KitchenTool",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "NumberOfBedrooms",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Wifi",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "WorkSpace",
                table: "Apartments");
        }
    }
}
