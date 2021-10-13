using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.PersonalData.Postgres.Migrations
{
    public partial class version_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTechnical",
                schema: "personaldata",
                table: "personaldata",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTechnical",
                schema: "personaldata",
                table: "personaldata");
        }
    }
}
