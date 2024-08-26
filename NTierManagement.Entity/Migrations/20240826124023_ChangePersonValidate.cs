using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTierManagement.Entity.Migrations
{
    /// <inheritdoc />
    public partial class ChangePersonValidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Companies_CompanyID",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyID",
                table: "People",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Companies_CompanyID",
                table: "People",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Companies_CompanyID",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyID",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Companies_CompanyID",
                table: "People",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
