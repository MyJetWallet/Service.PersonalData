using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.PersonalData.Postgres.Migrations
{
    public partial class NationalPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNational",
                schema: "personaldata",
                table: "personaldata",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNational",
                schema: "personaldata",
                table: "personaldata");
        }
    }
}
