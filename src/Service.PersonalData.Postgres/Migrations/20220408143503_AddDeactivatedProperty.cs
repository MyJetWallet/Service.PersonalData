using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.PersonalData.Postgres.Migrations
{
    public partial class AddDeactivatedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeactivatedPhone",
                schema: "personaldata",
                table: "personaldata",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactivated",
                schema: "personaldata",
                table: "personaldata",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeactivatedPhone",
                schema: "personaldata",
                table: "personaldata");

            migrationBuilder.DropColumn(
                name: "IsDeactivated",
                schema: "personaldata",
                table: "personaldata");
        }
    }
}
