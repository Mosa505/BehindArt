using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BehindArt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Biography", "BirthYear", "DeathYear", "Name" },
                values: new object[,]
                {
                    { 1, "Italian polymath of the High Renaissance.", 1452, 1519, "Leonardo da Vinci" },
                    { 2, "Dutch post-impressionist painter.", 1853, 1890, "Vincent van Gogh" },
                    { 3, "Norwegian expressionist painter.", 1863, 1944, "Edvard Munch" }
                });

            migrationBuilder.InsertData(
                table: "Eras",
                columns: new[] { "Id", "Description", "EndYear", "Name", "StartYear" },
                values: new object[,]
                {
                    { 1, "A period of cultural rebirth in Europe.", 1600, "Renaissance", 1300 },
                    { 2, "Emphasis on symbolic content and geometric form.", 1905, "Post-Impressionism", 1886 },
                    { 3, "Art that presents the world from a subjective perspective.", 1933, "Expressionism", 1905 }
                });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "ArtistId", "CreatedAt", "Description", "EraId", "ImageUrl", "Story", "Title", "Year" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 8, 19, 13, 2, 50, 716, DateTimeKind.Utc).AddTicks(1702), "Portrait of a woman with an enigmatic expression.", 1, "https://placehold.co/600x800?text=Mona+Lisa", null, "Mona Lisa", 1503 },
                    { 2, 2, new DateTime(2026, 8, 19, 13, 2, 50, 716, DateTimeKind.Utc).AddTicks(1713), "A swirling night sky over a quiet village.", 2, "https://placehold.co/600x800?text=Starry+Night", null, "The Starry Night", 1889 },
                    { 3, 3, new DateTime(2026, 8, 19, 13, 2, 50, 716, DateTimeKind.Utc).AddTicks(1716), "An agonized figure against a blood-red sky.", 3, "https://placehold.co/600x800?text=The+Scream", null, "The Scream", 1893 },
                    { 4, 1, new DateTime(2026, 8, 19, 13, 2, 50, 716, DateTimeKind.Utc).AddTicks(1718), "Melting clocks in a dreamlike landscape.", 1, "https://placehold.co/600x800?text=Persistence+of+Memory", null, "The Persistence of Memory", 1931 },
                    { 5, 2, new DateTime(2026, 8, 19, 13, 2, 50, 716, DateTimeKind.Utc).AddTicks(1720), "A young girl wearing an exotic dress and a pearl earring.", 2, "https://placehold.co/600x800?text=Girl+with+Pearl+Earring", null, "Girl with a Pearl Earring", 1665 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Paintings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Eras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Eras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Eras",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
