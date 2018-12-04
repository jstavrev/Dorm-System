using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitory.Data.Migrations
{
    public partial class user_sensors_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSensors_AspNetUsers_UserId",
                table: "UserSensors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSensors",
                table: "UserSensors");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "c6769eda-7baf-436b-bf27-afb1d3ab803c", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "c6769eda-7baf-436b-bf27-afb1d3ab803c", "ebcfe772-640f-4c44-9bac-04830fb40a0d" });

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserSensors",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserSensors",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "UserSensors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSensors",
                table: "UserSensors",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AvatarImage", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "Story", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d55e34c1-7550-47a4-9a0d-79e17f4cd3eb", 0, null, null, null, "5bec9d43-f97d-47f0-94e4-87e6f352cc91", null, "ICBAdmin@mail.com", true, null, null, false, null, "ICBADMIN@MAIL.COM", "ICBADMIN", "AQAAAAEAACcQAAAAENtvgf4DK+dETKWisJpZ79G/B2qGcpDn0E8bw/aBZpAlKKkobmN7fN1r7vmlE2usgA==", "+55555", true, 0, "c4de9f14-695d-4eba-bed7-7f05f8c84388", null, false, "ICBAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "d55e34c1-7550-47a4-9a0d-79e17f4cd3eb", "1" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSensors_UserId",
                table: "UserSensors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSensors_AspNetUsers_UserId",
                table: "UserSensors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSensors_AspNetUsers_UserId",
                table: "UserSensors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSensors",
                table: "UserSensors");

            migrationBuilder.DropIndex(
                name: "IX_UserSensors_UserId",
                table: "UserSensors");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "d55e34c1-7550-47a4-9a0d-79e17f4cd3eb", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "d55e34c1-7550-47a4-9a0d-79e17f4cd3eb", "5bec9d43-f97d-47f0-94e4-87e6f352cc91" });

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserSensors");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "UserSensors");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserSensors",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSensors",
                table: "UserSensors",
                columns: new[] { "UserId", "SensorId" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AvatarImage", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "Story", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c6769eda-7baf-436b-bf27-afb1d3ab803c", 0, null, null, null, "ebcfe772-640f-4c44-9bac-04830fb40a0d", null, "ICBAdmin@mail.com", true, null, null, false, null, "ICBADMIN@MAIL.COM", "ICBADMIN", "AQAAAAEAACcQAAAAEMPpJrDn2C4Ik+eCxSZwp2NF9BuLyv15d4NgSXkBdyhBeh+CAmJIMLNi8nrCPGpc+A==", "+55555", true, 0, "5646e18c-52c2-4d81-8432-97e307ee8c18", null, false, "ICBAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "c6769eda-7baf-436b-bf27-afb1d3ab803c", "1" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSensors_AspNetUsers_UserId",
                table: "UserSensors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
