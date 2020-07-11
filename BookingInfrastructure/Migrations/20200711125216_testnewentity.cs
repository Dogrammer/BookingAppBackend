using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingInfrastructure.Migrations
{
    public partial class testnewentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateDeleted = table.Column<DateTimeOffset>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    LastModified = table.Column<DateTimeOffset>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ActiveFrom = table.Column<DateTimeOffset>(nullable: false),
                    ActiveTo = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
