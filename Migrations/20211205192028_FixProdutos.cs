using Microsoft.EntityFrameworkCore.Migrations;

namespace TpFinalJoaquinGaido.Migrations
{
    public partial class FixProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_ProveedoresId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_ProveedoresId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "ProveedoresId",
                table: "Productos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProveedoresId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProveedoresId",
                table: "Productos",
                column: "ProveedoresId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_ProveedoresId",
                table: "Productos",
                column: "ProveedoresId",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
