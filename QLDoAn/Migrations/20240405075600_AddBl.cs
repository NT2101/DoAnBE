using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDoAn.Migrations
{
    /// <inheritdoc />
    public partial class AddBl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAccount",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAccount", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblTeacher",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTeacher", x => x.TeacherID);
                });

            migrationBuilder.CreateTable(
                name: "tblTopicType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTopicType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblStudent",
                columns: table => new
                {
                    StudentID = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStudent", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_tblStudent_tblAccount_AccountID",
                        column: x => x.AccountID,
                        principalTable: "tblAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblTopic",
                columns: table => new
                {
                    TopicID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TopicTypeID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTopic", x => x.TopicID);
                    table.ForeignKey(
                        name: "FK_tblTopic_tblStudent_StudentID",
                        column: x => x.StudentID,
                        principalTable: "tblStudent",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblTopic_tblTeacher_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "tblTeacher",
                        principalColumn: "TeacherID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblTopic_tblTopicType_TopicTypeID",
                        column: x => x.TopicTypeID,
                        principalTable: "tblTopicType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblStudent_AccountID",
                table: "tblStudent",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_tblTopic_StudentID",
                table: "tblTopic",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_tblTopic_TeacherID",
                table: "tblTopic",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_tblTopic_TopicTypeID",
                table: "tblTopic",
                column: "TopicTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTopic");

            migrationBuilder.DropTable(
                name: "tblStudent");

            migrationBuilder.DropTable(
                name: "tblTeacher");

            migrationBuilder.DropTable(
                name: "tblTopicType");

            migrationBuilder.DropTable(
                name: "tblAccount");
        }
    }
}
