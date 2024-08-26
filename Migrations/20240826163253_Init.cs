using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Stx.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: true),
                    price = table.Column<double>(type: "double precision", nullable: true),
                    releasedate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "productpricehistory",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    validfrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_productpricehistory", x => x.id);
                    table.ForeignKey(
                        name: "fk_productpricehistory_product_productid",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_productpricehistory_productid",
                table: "productpricehistory",
                column: "productid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productpricehistory");

            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
