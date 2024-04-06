using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDoAn.Migrations
{
    /// <inheritdoc />
    public partial class AddBl1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountID",
                table: "tblTeacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblTeacher_AccountID",
                table: "tblTeacher",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblTeacher_tblAccount_AccountID",
                table: "tblTeacher",
                column: "AccountID",
                principalTable: "tblAccount",
                principalColumn: "ID"
               );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblTeacher_tblAccount_AccountID",
                table: "tblTeacher");

            migrationBuilder.DropIndex(
                name: "IX_tblTeacher_AccountID",
                table: "tblTeacher");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "tblTeacher");
        }
    }
}
