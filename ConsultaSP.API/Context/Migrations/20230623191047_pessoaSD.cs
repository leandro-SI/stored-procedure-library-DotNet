using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultaSP.API.Context.Migrations
{
    public partial class pessoaSD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Idade", "Nome" },
                values: new object[] { 1L, 0, "Leandro Cesar" });

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Idade", "Nome" },
                values: new object[] { 2L, 0, "Luciana dos Santos" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
