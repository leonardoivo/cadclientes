using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class updateItensBaixaTipo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR_PAGO",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PARCELA",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_LINHA",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_ACORDO",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<long>(
                name: "MATRICULA",
                table: "ITENS_BAIXAS_TIPO3",
                type: "NUMBER(11)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "COD_ERRO",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SEQ",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ_EMPRESA_COBRANCA",
                table: "ITENS_BAIXAS_TIPO3",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SISTEMA",
                table: "ITENS_BAIXAS_TIPO3",
                type: "CHAR(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STA_ALU ",
                table: "ITENS_BAIXAS_TIPO3",
                type: "CHAR(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TIPO_INADIMPLENCIA",
                table: "ITENS_BAIXAS_TIPO3",
                type: "CHAR(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TIPO_PGTO",
                table: "ITENS_BAIXAS_TIPO3",
                type: "CHAR(1)",
                nullable: true);
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ_EMPRESA_COBRANCA",
                table: "ITENS_BAIXAS_TIPO3");

            migrationBuilder.DropColumn(
                name: "SISTEMA",
                table: "ITENS_BAIXAS_TIPO3");

            migrationBuilder.DropColumn(
                name: "STA_ALU ",
                table: "ITENS_BAIXAS_TIPO3");

            migrationBuilder.DropColumn(
                name: "TIPO_INADIMPLENCIA",
                table: "ITENS_BAIXAS_TIPO3");

            migrationBuilder.DropColumn(
                name: "TIPO_PGTO",
                table: "ITENS_BAIXAS_TIPO3");           

            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR_PAGO",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PARCELA",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_LINHA",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_ACORDO",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MATRICULA",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(11)");

            migrationBuilder.AlterColumn<decimal>(
                name: "COD_ERRO",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SEQ",
                table: "ITENS_BAIXAS_TIPO3",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");           
        }
    }
}
