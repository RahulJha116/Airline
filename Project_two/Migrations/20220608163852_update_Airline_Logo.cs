using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_two.Migrations
{
    public partial class update_Airline_Logo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "airlineLogo",
                table: "Airlines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "airlineLogo",
                table: "Airlines");
        }
    }
}
