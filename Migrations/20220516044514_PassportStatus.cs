using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EPassport.Migrations
{
    public partial class PassportStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassportStatus",
                table: "ApplicationDetail",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassportStatus",
                table: "ApplicationDetail");
        }
    }
}
