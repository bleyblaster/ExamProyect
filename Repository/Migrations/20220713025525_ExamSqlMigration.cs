using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class ExamSqlMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    ProductName = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValue: new DateTime(2022, 7, 12, 22, 55, 25, 695, DateTimeKind.Local).AddTicks(9848)),
                    ModifiedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_productid", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "VARCHAR(25)", nullable: true),
                    Password = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_userid", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
