using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Migrations
{
    public partial class updateParcelasAcordo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR_PAGO",
                table: "PARCELAS_ACORDO",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR",
                table: "PARCELAS_ACORDO",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PARCELA",
                table: "PARCELAS_ACORDO",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_ACORDO",
                table: "PARCELAS_ACORDO",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ_EMPRESA_COBRANCA",
                table: "PARCELAS_ACORDO",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SISTEMA",
                table: "PARCELAS_ACORDO",
                type: "CHAR(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TIPO_INADIMPLENCIA",
                table: "PARCELAS_ACORDO",
                type: "CHAR(1)",
                nullable: true);
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ_EMPRESA_COBRANCA",
                table: "PARCELAS_ACORDO");

            migrationBuilder.DropColumn(
                name: "SISTEMA",
                table: "PARCELAS_ACORDO");

            migrationBuilder.DropColumn(
                name: "TIPO_INADIMPLENCIA",
                table: "PARCELAS_ACORDO");

            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR_PAGO",
                table: "PARCELAS_ACORDO",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR",
                table: "PARCELAS_ACORDO",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PARCELA",
                table: "PARCELAS_ACORDO",
                type: "DECIMAL(18,2)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_ACORDO",
                table: "PARCELAS_ACORDO",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");
            
        }
    }
}
