using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPisher.Data.Migrations
{
    /// <inheritdoc />
    public partial class trackingID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "custom_link",
                table: "Campaign_Records",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "custom_link",
                table: "Campaign_Records");
        }
    }
}
