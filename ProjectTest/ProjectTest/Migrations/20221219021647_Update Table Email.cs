using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectTest.Migrations
{
    public partial class UpdateTableEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "deleted_by",
                table: "email",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted_by",
                table: "email");
        }
    }
}
