using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityByExamples.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1b7b2eab-edf2-4909-b128-d8d3459d0b16", "f38b90c7-5efb-418a-a97c-f9347a348f92", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d346b2b3-ccd3-4a50-b051-d5cf900cdb2d", "59a1cb91-e2fd-46af-af21-110b45da98f0", "Visitor", "VISITOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b7b2eab-edf2-4909-b128-d8d3459d0b16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d346b2b3-ccd3-4a50-b051-d5cf900cdb2d");
        }
    }
}
