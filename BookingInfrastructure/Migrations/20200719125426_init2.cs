using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingInfrastructure.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentGroups_AspNetUsers_UserId1",
                table: "ApartmentGroups");

            migrationBuilder.DropIndex(
                name: "IX_ApartmentGroups_UserId1",
                table: "ApartmentGroups");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ApartmentGroups");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ApartmentGroups",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentGroups_UserId",
                table: "ApartmentGroups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentGroups_AspNetUsers_UserId",
                table: "ApartmentGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentGroups_AspNetUsers_UserId",
                table: "ApartmentGroups");

            migrationBuilder.DropIndex(
                name: "IX_ApartmentGroups_UserId",
                table: "ApartmentGroups");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "ApartmentGroups",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "ApartmentGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentGroups_UserId1",
                table: "ApartmentGroups",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentGroups_AspNetUsers_UserId1",
                table: "ApartmentGroups",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
