using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Test.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("36488fd7-8663-4bea-b95d-8097c645f1f1"), "Medium" },
                    { new Guid("548c1654-b287-4ea1-b2dc-10e065faa6fa"), "Hard" },
                    { new Guid("dfb29660-7935-4381-8259-0972213424c5"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("0af39885-7618-4539-b334-4ccb9e1ee75b"), "HLZ", "Hamilton", "hamilton-img.jpg" },
                    { new Guid("6bc1d8c7-cee9-498e-919d-fe3ef6d07bcd"), "WLG", "Wellington", "wellington-img.jpg" },
                    { new Guid("6d65dd4d-c70e-4bc6-bf2c-939070296e87"), "AKL", "Aukland", "sample-img.jpg" },
                    { new Guid("795ec274-f743-4e3b-a835-c59ca884d7c0"), "DUD", "Dunedin", "dunedin-img.jpg" },
                    { new Guid("e2521014-b803-440c-9622-18f0b726c5bb"), "TRG", "Tauranga", "tauranga-img.jpg" },
                    { new Guid("fd548cc5-5a71-4e38-af0b-ff33315f02ac"), "CHC", "Christchurch", "christchurch-img.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("36488fd7-8663-4bea-b95d-8097c645f1f1"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("548c1654-b287-4ea1-b2dc-10e065faa6fa"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("dfb29660-7935-4381-8259-0972213424c5"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("0af39885-7618-4539-b334-4ccb9e1ee75b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6bc1d8c7-cee9-498e-919d-fe3ef6d07bcd"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6d65dd4d-c70e-4bc6-bf2c-939070296e87"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("795ec274-f743-4e3b-a835-c59ca884d7c0"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e2521014-b803-440c-9622-18f0b726c5bb"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("fd548cc5-5a71-4e38-af0b-ff33315f02ac"));
        }
    }
}
