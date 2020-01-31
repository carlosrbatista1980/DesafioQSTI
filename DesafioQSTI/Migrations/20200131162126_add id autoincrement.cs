using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioQSTI.Migrations
{
    public partial class addidautoincrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHora",
                table: "ExecucaoServico",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateTable(
                name: "ClienteViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    ExecucaoServicosId = table.Column<int>(nullable: true),
                    ServicoClientesId = table.Column<int>(nullable: true),
                    ServicosId = table.Column<int>(nullable: true),
                    IsAuthenticated = table.Column<bool>(nullable: false),
                    Versao = table.Column<string>(nullable: true),
                    DataHora = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteViewModel_ExecucaoServico_ExecucaoServicosId",
                        column: x => x.ExecucaoServicosId,
                        principalTable: "ExecucaoServico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClienteViewModel_ServicoCliente_ServicoClientesId",
                        column: x => x.ServicoClientesId,
                        principalTable: "ServicoCliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClienteViewModel_Servico_ServicosId",
                        column: x => x.ServicosId,
                        principalTable: "Servico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteViewModel_ExecucaoServicosId",
                table: "ClienteViewModel",
                column: "ExecucaoServicosId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteViewModel_ServicoClientesId",
                table: "ClienteViewModel",
                column: "ServicoClientesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteViewModel_ServicosId",
                table: "ClienteViewModel",
                column: "ServicosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteViewModel");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHora",
                table: "ExecucaoServico",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
