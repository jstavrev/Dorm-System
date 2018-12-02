using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitory.Data.Migrations
{
    public partial class user_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "081bc1c4-d938-4192-ab5f-cb1b10cc480e", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "081bc1c4-d938-4192-ab5f-cb1b10cc480e", "58063bba-8a94-46db-8fcf-3c262c84b14b" });

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "AvatarImage",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Story",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AvatarImage", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "Story", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c6769eda-7baf-436b-bf27-afb1d3ab803c", 0, null, null, null, "ebcfe772-640f-4c44-9bac-04830fb40a0d", null, "ICBAdmin@mail.com", true, null, null, false, null, "ICBADMIN@MAIL.COM", "ICBADMIN", "AQAAAAEAACcQAAAAEMPpJrDn2C4Ik+eCxSZwp2NF9BuLyv15d4NgSXkBdyhBeh+CAmJIMLNi8nrCPGpc+A==", "+55555", true, 0, "5646e18c-52c2-4d81-8432-97e307ee8c18", null, false, "ICBAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "c6769eda-7baf-436b-bf27-afb1d3ab803c", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "c6769eda-7baf-436b-bf27-afb1d3ab803c", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "c6769eda-7baf-436b-bf27-afb1d3ab803c", "ebcfe772-640f-4c44-9bac-04830fb40a0d" });

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvatarImage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Story",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "081bc1c4-d938-4192-ab5f-cb1b10cc480e", 0, "58063bba-8a94-46db-8fcf-3c262c84b14b", "ICBAdmin@mail.com", true, null, null, false, null, "ICBADMIN@MAIL.COM", "ICBADMIN", "AQAAAAEAACcQAAAAEKx2tHN5RiWQ+Z30jDpBf6J+4AF+SO3a2KsukUsx+Kd6S44ojqk7RrBhgHQE+5m3zg==", "+55555", true, "711b522d-ebaa-4b53-9209-c9c7b9666c4f", false, "ICBAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "081bc1c4-d938-4192-ab5f-cb1b10cc480e", "1" });
        }
    }
}
