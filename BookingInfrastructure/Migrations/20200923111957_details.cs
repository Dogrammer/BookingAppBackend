using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingInfrastructure.Migrations
{
    public partial class details : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PricingPeriodDetails_PricingPeriods_PricingPeriodId",
                table: "PricingPeriodDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PricingPeriods_Apartments_ApartmentId",
                table: "PricingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_PricingPeriods_ApartmentId",
                table: "PricingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_PricingPeriodDetails_PricingPeriodId",
                table: "PricingPeriodDetails");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "PricingPeriods");

            migrationBuilder.DropColumn(
                name: "PricingPeriodId",
                table: "PricingPeriodDetails");

            migrationBuilder.AddColumn<long>(
                name: "ApartmentId",
                table: "PricingPeriodDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PricingPeriodDetails_ApartmentId",
                table: "PricingPeriodDetails",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PricingPeriodDetails_Apartments_ApartmentId",
                table: "PricingPeriodDetails",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PricingPeriodDetails_Apartments_ApartmentId",
                table: "PricingPeriodDetails");

            migrationBuilder.DropIndex(
                name: "IX_PricingPeriodDetails_ApartmentId",
                table: "PricingPeriodDetails");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "PricingPeriodDetails");

            migrationBuilder.AddColumn<long>(
                name: "ApartmentId",
                table: "PricingPeriods",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PricingPeriodId",
                table: "PricingPeriodDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PricingPeriods_ApartmentId",
                table: "PricingPeriods",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PricingPeriodDetails_PricingPeriodId",
                table: "PricingPeriodDetails",
                column: "PricingPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_PricingPeriodDetails_PricingPeriods_PricingPeriodId",
                table: "PricingPeriodDetails",
                column: "PricingPeriodId",
                principalTable: "PricingPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PricingPeriods_Apartments_ApartmentId",
                table: "PricingPeriods",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
