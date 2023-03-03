using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversitySample.Students.Service.DataAccess.Migrations
{
    public partial class StudentDbCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "EmailAddress", "FirstName", "LastName", "Username" },
                values: new object[] { new Guid("a626c0c6-4589-40f2-b970-5d25eb501eac"), "admin.student@invalid.mail", "Admin", "Student", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
