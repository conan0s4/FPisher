using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPisher.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campaign_Records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Campaign_Emails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign_Records", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Login_Records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login_Records", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Page_Visits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Page_emails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page_Visits", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campaign_Records");

            migrationBuilder.DropTable(
                name: "Login_Records");

            migrationBuilder.DropTable(
                name: "Page_Visits");
        }
    }
}
