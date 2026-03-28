using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseballGameTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3049e059-9ea9-467f-9c30-3c3a07eaff79", "22a7f8c4-8bfd-4d51-9371-7a615f5757bf", "Fan", "FAN" },
                    { "44513cbe-994c-402a-a163-9eaff914d035", "5762653f-6375-4181-918a-fda84f67cafb", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "30a785c6-45f3-4381-8c61-a302f6f8c65f", 0, "ececc0b8-0e17-4409-a5e2-c3f524a57dc1", "baseballtracker03@gmail.com", true, false, null, "BASEBALLTRACKER03@GMAIL.COM", "BASEBALLTRACKER03@GMAIL.COM", "AQAAAAIAAYagAAAAEHT9fezt1GzZxWCgwRyF1J83wFDG2LvAWaUEZDajemazDmHns0pSnrzs2n+0kMDfIQ==", null, false, "fcb3f34a-a303-456d-9c89-22d3bd06ae55", false, "BASEBALLTRACKER03@GMAIL.COM" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "44513cbe-994c-402a-a163-9eaff914d035", "30a785c6-45f3-4381-8c61-a302f6f8c65f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3049e059-9ea9-467f-9c30-3c3a07eaff79");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "44513cbe-994c-402a-a163-9eaff914d035", "30a785c6-45f3-4381-8c61-a302f6f8c65f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44513cbe-994c-402a-a163-9eaff914d035");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "30a785c6-45f3-4381-8c61-a302f6f8c65f");
        }
    }
}
