using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class updateItensBaixaTipo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PERIODO",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PARCELA",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_LINHA",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_ACORDO",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MATRICULA",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "COD_ERRO",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SEQ",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ_EMPRESA_COBRANCA",
                table: "ITENS_BAIXAS_TIPO2",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "PERIODO_CHEQUE_DEVOLVIDO",
                table: "ITENS_BAIXAS_TIPO2",
                type: "NUMBER(5)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SISTEMA ",
                table: "ITENS_BAIXAS_TIPO2",
                type: "CHAR(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STA_ALU ",
                table: "ITENS_BAIXAS_TIPO2",
                type: "CHAR(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TIPO_INADIMPLENCIA ",
                table: "ITENS_BAIXAS_TIPO2",
                type: "CHAR(1)",
                nullable: true);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ_EMPRESA_COBRANCA",
                table: "ITENS_BAIXAS_TIPO2");

            migrationBuilder.DropColumn(
                name: "PERIODO_CHEQUE_DEVOLVIDO",
                table: "ITENS_BAIXAS_TIPO2");

            migrationBuilder.DropColumn(
                name: "SISTEMA ",
                table: "ITENS_BAIXAS_TIPO2");

            migrationBuilder.DropColumn(
                name: "STA_ALU ",
                table: "ITENS_BAIXAS_TIPO2");

            migrationBuilder.DropColumn(
                name: "TIPO_INADIMPLENCIA ",
                table: "ITENS_BAIXAS_TIPO2");
           

            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PERIODO",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PARCELA",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_LINHA",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_ACORDO",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MATRICULA",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "COD_ERRO",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SEQ",
                table: "ITENS_BAIXAS_TIPO2",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");
           
        }
    }
}
