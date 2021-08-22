using Microsoft.EntityFrameworkCore.Migrations;

namespace Skyscrapers.Data.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skyscrapers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Built = table.Column<int>(type: "int", nullable: false),
                    OfficialHeightInMeters = table.Column<int>(type: "int", nullable: false),
                    NrOfFloors = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skyscrapers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skyscrapers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "United States" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Canada" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { 1, 1, "New York City" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { 2, 1, "Chicago" });

            migrationBuilder.InsertData(
                table: "Skyscrapers",
                columns: new[] { "Id", "Built", "CityId", "NrOfFloors", "OfficialHeightInMeters", "Status", "Title" },
                values: new object[] { 1, 1870, 1, 8, 43, "destroyed", "Equitable Life Building" });

            migrationBuilder.InsertData(
                table: "Skyscrapers",
                columns: new[] { "Id", "Built", "CityId", "NrOfFloors", "OfficialHeightInMeters", "Status", "Title" },
                values: new object[] { 3, 1890, 1, 20, 94, "demolished", "New York World Building" });

            migrationBuilder.InsertData(
                table: "Skyscrapers",
                columns: new[] { "Id", "Built", "CityId", "NrOfFloors", "OfficialHeightInMeters", "Status", "Title" },
                values: new object[] { 2, 1889, 2, 17, 82, "standing", "Auditorium Building" });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Skyscrapers_CityId",
                table: "Skyscrapers",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skyscrapers");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
