using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitory.Data.Migrations
{
    public partial class added_admin_seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "67c2878c-f66e-477d-b688-fa4fe3160d5d", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "67c2878c-f66e-477d-b688-fa4fe3160d5d", "b8792c4d-5d76-4404-933a-075eefae30b5" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "081bc1c4-d938-4192-ab5f-cb1b10cc480e", 0, "58063bba-8a94-46db-8fcf-3c262c84b14b", "ICBAdmin@mail.com", true, null, null, false, null, "ICBADMIN@MAIL.COM", "ICBADMIN", "AQAAAAEAACcQAAAAEKx2tHN5RiWQ+Z30jDpBf6J+4AF+SO3a2KsukUsx+Kd6S44ojqk7RrBhgHQE+5m3zg==", "+55555", true, "711b522d-ebaa-4b53-9209-c9c7b9666c4f", false, "ICBAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "081bc1c4-d938-4192-ab5f-cb1b10cc480e", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "081bc1c4-d938-4192-ab5f-cb1b10cc480e", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "081bc1c4-d938-4192-ab5f-cb1b10cc480e", "58063bba-8a94-46db-8fcf-3c262c84b14b" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "67c2878c-f66e-477d-b688-fa4fe3160d5d", 0, "b8792c4d-5d76-4404-933a-075eefae30b5", "vksn@mail.com", true, null, null, false, null, "VKS@MAIL.COM", "VKSADMIN", "AQAAAAEAACcQAAAAENY+67dayz49mL9ZnG5LgSOYqcSK8Jf+dBP6B5yPsxQMhYoxiOFpLfYfaGnntn2qHA==", "+55555", true, "108ce727-6471-4d79-86d2-c97034c611d7", false, "VksAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "67c2878c-f66e-477d-b688-fa4fe3160d5d", "1" });
        }
    }
}
