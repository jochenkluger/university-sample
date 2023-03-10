using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSample.Library.Service.Migrations
{
    public partial class DbData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Available", "ISBN", "Title" },
                values: new object[,]
                {
                    { new Guid("6eee2d39-041a-4ffd-9a27-d175c2a35794"), "Maarten van Steen, Andrew S. Tanenbaum", true, "978-1543057386", "Distributed Systems" },
                    { new Guid("cb37c704-9ac3-4436-b609-648b711d5938"), "Günther Bengel", true, "978-3834816702", "Grundkurs Verteilte Systeme" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("6eee2d39-041a-4ffd-9a27-d175c2a35794"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("cb37c704-9ac3-4436-b609-648b711d5938"));
        }
    }
}
