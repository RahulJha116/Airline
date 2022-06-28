using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_two.Migrations
{
    public partial class AddAdminTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    adminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    adminName = table.Column<string>(nullable: true),
                    adminEmailId = table.Column<string>(nullable: true),
                    adminPasskey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.adminId);
                });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "adminId", "adminEmailId", "adminName", "adminPasskey" },
                values: new object[] { 1, "Admin1", "Admin1", "Admin1" });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "adminId", "adminEmailId", "adminName", "adminPasskey" },
                values: new object[] { 2, "Admin2", "Admin2", "Admin2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");
        }
    }
}
