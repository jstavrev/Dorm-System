using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitory.Data.Migrations
{
    public partial class Changed_Sensor_Value_From_Num_To_String : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrentValue",
                table: "Sensors",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CurrentValue",
                table: "Sensors",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
