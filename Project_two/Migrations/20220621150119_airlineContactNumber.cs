using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_two.Migrations
{
    public partial class airlineContactNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "airlineContactNumber",
                table: "Airlines",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "airlineContactNumber",
                table: "Airlines",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
