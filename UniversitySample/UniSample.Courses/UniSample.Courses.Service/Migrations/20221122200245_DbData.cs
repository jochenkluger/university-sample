using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSample.Courses.Service.Migrations
{
    public partial class DbData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name", "ProfName", "StudentsCount" },
                values: new object[,]
                {
                    { new Guid("100ee899-cd2f-4120-a7c0-d6df9341908e"), "Cloud Native", "Jochen Kluger", 0 },
                    { new Guid("856ee11c-7a17-4247-9864-46a3d24a9d23"), "Verteilte Systeme", "Jochen Kluger", 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("100ee899-cd2f-4120-a7c0-d6df9341908e"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("856ee11c-7a17-4247-9864-46a3d24a9d23"));
        }
    }
}
