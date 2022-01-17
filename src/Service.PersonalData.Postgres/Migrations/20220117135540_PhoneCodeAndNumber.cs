using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.PersonalData.Postgres.Migrations
{
    public partial class PhoneCodeAndNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneCode",
                schema: "personaldata",
                table: "personaldata",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "personaldata",
                table: "personaldata",
                type: "character varying(512)",
                maxLength: 512,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneCode",
                schema: "personaldata",
                table: "personaldata");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "personaldata",
                table: "personaldata");
        }
    }
}
