using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.PersonalData.Postgres.Migrations
{
    public partial class PhoneIso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneIso",
                schema: "personaldata",
                table: "personaldata",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneIso",
                schema: "personaldata",
                table: "personaldata");
        }
    }
}
