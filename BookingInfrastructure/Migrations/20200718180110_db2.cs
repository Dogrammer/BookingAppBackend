using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingInfrastructure.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_PricingPeriods_PricingPeriodId",
                table: "Apartments");

            migrationBuilder.DropTable(
                name: "UserApartmentGroups");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_PricingPeriodId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "PricingPeriods");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "PricingPeriods");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PricingPeriods");

            migrationBuilder.DropColumn(
                name: "PricingPeriodId",
                table: "Apartments");

            migrationBuilder.AddColumn<long>(
                name: "ApartmentId",
                table: "PricingPeriods",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "ApartmentGroups",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "PricingPeriodDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    LastModified = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateFrom = table.Column<DateTimeOffset>(nullable: false),
                    DateTo = table.Column<DateTimeOffset>(nullable: false),
                    Price = table.Column<long>(nullable: false),
                    PricingPeriodId = table.Column<long>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingPeriodDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingPeriodDetails_PricingPeriods_PricingPeriodId",
                        column: x => x.PricingPeriodId,
                        principalTable: "PricingPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricingPeriods_ApartmentId",
                table: "PricingPeriods",
                column: "ApartmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PricingPeriodDetails_PricingPeriodId",
                table: "PricingPeriodDetails",
                column: "PricingPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_PricingPeriods_Apartments_ApartmentId",
                table: "PricingPeriods",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PricingPeriods_Apartments_ApartmentId",
                table: "PricingPeriods");

            migrationBuilder.DropTable(
                name: "PricingPeriodDetails");

            migrationBuilder.DropIndex(
                name: "IX_PricingPeriods_ApartmentId",
                table: "PricingPeriods");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "PricingPeriods");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ApartmentGroups");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateFrom",
                table: "PricingPeriods",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTo",
                table: "PricingPeriods",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "PricingPeriods",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "PricingPeriodId",
                table: "Apartments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "UserApartmentGroups",
                columns: table => new
                {
                    ApartmentGroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ApartmentGroupId1 = table.Column<long>(type: "bigint", nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApartmentGroups", x => new { x.ApartmentGroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserApartmentGroups_AspNetUsers_ApartmentGroupId",
                        column: x => x.ApartmentGroupId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserApartmentGroups_ApartmentGroups_ApartmentGroupId1",
                        column: x => x.ApartmentGroupId1,
                        principalTable: "ApartmentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_PricingPeriodId",
                table: "Apartments",
                column: "PricingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserApartmentGroups_ApartmentGroupId1",
                table: "UserApartmentGroups",
                column: "ApartmentGroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_PricingPeriods_PricingPeriodId",
                table: "Apartments",
                column: "PricingPeriodId",
                principalTable: "PricingPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
