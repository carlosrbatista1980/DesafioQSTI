using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioQSTI.Migrations
{
    public partial class asdasda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicoCliente_Cliente_ClienteId",
                table: "ServicoCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicoCliente_Servico_ServicoId",
                table: "ServicoCliente");

            migrationBuilder.AlterColumn<int>(
                name: "ServicoId",
                table: "ServicoCliente",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "ServicoCliente",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoCliente_Cliente_ClienteId",
                table: "ServicoCliente",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoCliente_Servico_ServicoId",
                table: "ServicoCliente",
                column: "ServicoId",
                principalTable: "Servico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicoCliente_Cliente_ClienteId",
                table: "ServicoCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicoCliente_Servico_ServicoId",
                table: "ServicoCliente");

            migrationBuilder.AlterColumn<int>(
                name: "ServicoId",
                table: "ServicoCliente",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "ServicoCliente",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoCliente_Cliente_ClienteId",
                table: "ServicoCliente",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoCliente_Servico_ServicoId",
                table: "ServicoCliente",
                column: "ServicoId",
                principalTable: "Servico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
