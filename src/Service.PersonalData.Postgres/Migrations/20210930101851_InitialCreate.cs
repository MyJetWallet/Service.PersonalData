using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.PersonalData.Postgres.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "personaldata");

            migrationBuilder.CreateTable(
                name: "personaldata",
                schema: "personaldata",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    City = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Phone = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    CountryOfCitizenship = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    CountryOfResidence = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    EmailHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Sex = table.Column<int>(type: "integer", nullable: true),
                    KYC = table.Column<int>(type: "integer", nullable: true),
                    Confirm = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ConfirmPhone = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Address = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    USCitizen = table.Column<bool>(type: "boolean", nullable: true),
                    IpOfRegistration = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    CountryOfRegistration = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    IsInternal = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EmailGroupId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    BrandId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    PlatformType = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personaldata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "traderdocuments",
                schema: "personaldata",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TraderId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DocumentType = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Mime = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    FileName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_traderdocuments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_personaldata_CreatedAt",
                schema: "personaldata",
                table: "personaldata",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_personaldata_Email",
                schema: "personaldata",
                table: "personaldata",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personaldata_Id",
                schema: "personaldata",
                table: "personaldata",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personaldata_IsInternal",
                schema: "personaldata",
                table: "personaldata",
                column: "IsInternal");

            migrationBuilder.CreateIndex(
                name: "IX_traderdocuments_Id",
                schema: "personaldata",
                table: "traderdocuments",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "personaldata",
                schema: "personaldata");

            migrationBuilder.DropTable(
                name: "traderdocuments",
                schema: "personaldata");
        }
    }
}
