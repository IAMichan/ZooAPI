using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Gender = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Species = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BirthPlace = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ParentMaleName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ParentMaleId = table.Column<int>(type: "int", nullable: true),
                    ParentFemaleName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ParentFemaleId = table.Column<int>(type: "int", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enclosure = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Headkeeper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headkeeper", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedingData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: true),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Food = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedingData_AnimalRecords_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "AnimalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeedingInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: true),
                    HeadkeeperId = table.Column<int>(type: "int", nullable: true),
                    FeedingDateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedingInformations_AnimalRecords_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "AnimalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VeterinaryRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: true),
                    VetId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeterinaryRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeterinaryRecords_AnimalRecords_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "AnimalRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedingData_AnimalId",
                table: "FeedingData",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingInformations_AnimalId",
                table: "FeedingInformations",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinaryRecords_AnimalId",
                table: "VeterinaryRecords",
                column: "AnimalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedingData");

            migrationBuilder.DropTable(
                name: "FeedingInformations");

            migrationBuilder.DropTable(
                name: "Headkeeper");

            migrationBuilder.DropTable(
                name: "VeterinaryRecords");

            migrationBuilder.DropTable(
                name: "Vets");

            migrationBuilder.DropTable(
                name: "AnimalRecords");
        }
    }
}
