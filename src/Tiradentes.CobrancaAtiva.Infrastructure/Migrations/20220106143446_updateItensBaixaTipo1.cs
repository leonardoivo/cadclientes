using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Migrations
{
    public partial class updateItensBaixaTipo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PARCELA",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_LINHA",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_ACORDO",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MULTA",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<long>(
                name: "MATRICULA",
                table: "ITENS_BAIXAS_TIPO1",
                type: "NUMBER(11)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "JUROS",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "COD_ERRO",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SEQ",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ_EMPRESA_COBRANCA",
                table: "ITENS_BAIXAS_TIPO1",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SISTEMA",
                table: "ITENS_BAIXAS_TIPO1",
                type: "CHAR(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STA_ALU ",
                table: "ITENS_BAIXAS_TIPO1",
                type: "CHAR(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TIPO_INADIMPLENCIA",
                table: "ITENS_BAIXAS_TIPO1",
                type: "CHAR(1)",
                nullable: true);
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ_EMPRESA_COBRANCA",
                table: "ITENS_BAIXAS_TIPO1");

            migrationBuilder.DropColumn(
                name: "SISTEMA",
                table: "ITENS_BAIXAS_TIPO1");

            migrationBuilder.DropColumn(
                name: "STA_ALU ",
                table: "ITENS_BAIXAS_TIPO1");

            migrationBuilder.DropColumn(
                name: "TIPO_INADIMPLENCIA",
                table: "ITENS_BAIXAS_TIPO1");           

            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PARCELA",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_LINHA",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_ACORDO",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MULTA",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MATRICULA",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(11)");

            migrationBuilder.AlterColumn<decimal>(
                name: "JUROS",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "COD_ERRO",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SEQ",
                table: "ITENS_BAIXAS_TIPO1",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");
            
        }
    }
}
