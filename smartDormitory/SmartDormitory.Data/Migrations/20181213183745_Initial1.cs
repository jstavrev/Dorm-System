using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitory.Data.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "e613f2f6-c8b0-47e9-912b-e77fd1020126", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "e613f2f6-c8b0-47e9-912b-e77fd1020126", "cdc5ee45-7590-46bf-bfe1-9ab4708e2b4c" });

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "UserSensors",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AvatarImage", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6e62d7a9-d3fa-42a9-a083-5c77c0338885", 0, null, null, null, "3c91f74b-0fbc-47a5-981b-490594d9e879", null, "ICBAdmin@mail.com", true, null, null, false, null, "ICBADMIN@MAIL.COM", "ICBADMIN", "AQAAAAEAACcQAAAAECE2nw8kAhDyeFQfWMg2b37V8xWUbLTzrNOxAVRo7acJZaJCmxmfFxM6vTgT9g54Xw==", "+55555", true, null, "ee919fef-0d68-4182-b0d1-855d7c10500a", false, "ICBAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "6e62d7a9-d3fa-42a9-a083-5c77c0338885", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "6e62d7a9-d3fa-42a9-a083-5c77c0338885", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "6e62d7a9-d3fa-42a9-a083-5c77c0338885", "3c91f74b-0fbc-47a5-981b-490594d9e879" });

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "UserSensors",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AvatarImage", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e613f2f6-c8b0-47e9-912b-e77fd1020126", 0, null, null, null, "cdc5ee45-7590-46bf-bfe1-9ab4708e2b4c", null, "ICBAdmin@mail.com", true, null, null, false, null, "ICBADMIN@MAIL.COM", "ICBADMIN", "AQAAAAEAACcQAAAAED6ZU/kD5MlrALaGtSeMVMplLM2UEP/y8i4Mk6nIAMz8rS+c1xBM3urAANyfLjJ7wQ==", "+55555", true, null, "e5e8ebcb-a1a4-42a2-8c3e-8c909a3185a9", false, "ICBAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "e613f2f6-c8b0-47e9-912b-e77fd1020126", "1" });
        }
    }
}
