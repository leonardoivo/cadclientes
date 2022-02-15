using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class updateParcelasTitulos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<decimal>(
            //    name: "VALOR",
            //    table: "PARCELAS_TITULOS",
            //    type: "NUMBER(15,2)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18,2)");

            //migrationBuilder.AlterColumn<byte>(
            //    name: "PARCELA",
            //    table: "PARCELAS_TITULOS",
            //    type: "NUMBER(3)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18,2)",
            //    oldMaxLength: 3);

            //migrationBuilder.AlterColumn<short>(
            //    name: "PERIODO",
            //    table: "PARCELAS_TITULOS",
            //    type: "NUMBER(5)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18,2)",
            //    oldMaxLength: 5);

            migrationBuilder.AlterColumn<long>(
                name: "MATRICULA",
                table: "PARCELAS_TITULOS",
                type: "NUMBER(11)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)",
                oldMaxLength: 10);

            //migrationBuilder.AlterColumn<int>(
            //    name: "NUM_ACORDO",
            //    table: "PARCELAS_TITULOS",
            //    type: "NUMBER(10)",
            //    nullable: false,
            //    oldClrType: typeof(decimal),
            //    oldType: "DECIMAL(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ_EMPRESA_COBRANCA",
                table: "PARCELAS_TITULOS",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "PERIODO_CHEQUE_DEVOLVIDO",
                table: "PARCELAS_TITULOS",
                type: "NUMBER(5)",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "SISTEMA",
                table: "PARCELAS_TITULOS",
                type: "CHAR(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TIPO_INADIMPLENCIA",
                table: "PARCELAS_TITULOS",
                type: "CHAR(1)",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ_EMPRESA_COBRANCA",
                table: "PARCELAS_TITULOS");

            migrationBuilder.DropColumn(
                name: "PERIODO_CHEQUE_DEVOLVIDO",
                table: "PARCELAS_TITULOS");

            migrationBuilder.DropColumn(
                name: "SISTEMA",
                table: "PARCELAS_TITULOS");

            migrationBuilder.DropColumn(
                name: "TIPO_INADIMPLENCIA",
                table: "PARCELAS_TITULOS");

            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR",
                table: "PARCELAS_TITULOS",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "NUMBER(15,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PARCELA",
                table: "PARCELAS_TITULOS",
                type: "DECIMAL(18,2)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "NUMBER(3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PERIODO",
                table: "PARCELAS_TITULOS",
                type: "DECIMAL(18,2)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(short),
                oldType: "NUMBER(5)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MATRICULA",
                table: "PARCELAS_TITULOS",
                type: "DECIMAL(18,2)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(11)");

            migrationBuilder.AlterColumn<decimal>(
                name: "NUM_ACORDO",
                table: "PARCELAS_TITULOS",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

        }
    }
}
