using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Migrations
{
    public partial class updateAcordosCObrancas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {           

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "VALOR_TOTAL",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18, 2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "TOTAL_PARCELAS",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18, 2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "SALDO_DEVEDOR",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18, 2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "MULTA",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18, 2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "MORA",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18, 2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "MATRICLA",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18, 2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18,2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "NUM_ACORDO",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18, 2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18,2)");

            //migrationBuilder.AddColumn<string>(
            //    name: "CNPJ_EMPRESA_COBRANCA",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "NVARCHAR2(2000)",
            //    nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "ACORDOS_COBRANCAS",
                type: "CHAR(11)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TIPO_INADIMPLENCIA",
                table: "ACORDOS_COBRANCAS",
                type: "CHAR(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "CHAR(1)");

            migrationBuilder.AlterColumn<string>(
                name: "SISTEMA",
                table: "ACORDOS_COBRANCAS",
                type: "CHAR(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "CHAR(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ_EMPRESA_COBRANCA",
                table: "ACORDOS_COBRANCAS");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "ACORDOS_COBRANCAS");

            migrationBuilder.DropColumn(
                name: "SISTEMA",
                table: "ACORDOS_COBRANCAS");

            migrationBuilder.DropColumn(
                name: "TIPO_INADIMPLENCIA",
                table: "ACORDOS_COBRANCAS");
           

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "VALOR_TOTAL",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18, 2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "TOTAL_PARCELAS",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18, 2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "SALDO_DEVEDOR",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18, 2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "MULTA",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18, 2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "MORA",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18, 2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "MATRICLA",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18, 2)");

            //migrationBuilder.AlterColumn<decimal>(
            //    name: "NUM_ACORDO",
            //    table: "ACORDOS_COBRANCAS",
            //    type: "DECIMAL(18,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18, 2)");
        }
    }
}
