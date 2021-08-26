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
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
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
                    StatusId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Skyscrapers_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "United States" },
                    { 2, "Canada" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "standing" },
                    { 2, "demolished" },
                    { 3, "destroyed" },
                    { 4, "under construction" }
                });

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
                columns: new[] { "Id", "Built", "CityId", "NrOfFloors", "OfficialHeightInMeters", "StatusId", "Title" },
                values: new object[] { 1, 1870, 1, 8, 43, 3, "Equitable Life Building" });

            migrationBuilder.InsertData(
                table: "Skyscrapers",
                columns: new[] { "Id", "Built", "CityId", "NrOfFloors", "OfficialHeightInMeters", "StatusId", "Title" },
                values: new object[] { 3, 1890, 1, 20, 94, 2, "New York World Building" });

            migrationBuilder.InsertData(
                table: "Skyscrapers",
                columns: new[] { "Id", "Built", "CityId", "NrOfFloors", "OfficialHeightInMeters", "StatusId", "Title" },
                values: new object[] { 2, 1889, 2, 17, 82, 1, "Auditorium Building" });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Skyscrapers_CityId",
                table: "Skyscrapers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Skyscrapers_StatusId",
                table: "Skyscrapers",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skyscrapers");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
