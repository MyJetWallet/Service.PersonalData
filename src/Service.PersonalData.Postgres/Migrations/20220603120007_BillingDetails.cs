using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.PersonalData.Postgres.Migrations
{
    public partial class BillingDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "billing_details",
                schema: "personaldata",
                columns: table => new
                {
                    ClientId = table.Column<string>(type: "text", nullable: false),
                    BillingName = table.Column<string>(type: "text", nullable: true),
                    BillingCity = table.Column<string>(type: "text", nullable: true),
                    BillingCountry = table.Column<string>(type: "text", nullable: true),
                    BillingLine1 = table.Column<string>(type: "text", nullable: true),
                    BillingLine2 = table.Column<string>(type: "text", nullable: true),
                    BillingDistrict = table.Column<string>(type: "text", nullable: true),
                    BillingPostalCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_billing_details", x => x.ClientId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "billing_details",
                schema: "personaldata");
        }
    }
}
