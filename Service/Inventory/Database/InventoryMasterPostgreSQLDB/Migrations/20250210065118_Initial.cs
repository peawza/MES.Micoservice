using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryMasterPostgreSQLDB.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tm_area",
                columns: table => new
                {
                    areaid = table.Column<int>(type: "integer", nullable: false),
                    areacode = table.Column<string>(type: "text", nullable: false),
                    areaname = table.Column<string>(type: "text", nullable: false),
                    areatype = table.Column<int>(type: "integer", nullable: false),
                    plantid = table.Column<int>(type: "integer", nullable: false),
                    warehouseid = table.Column<int>(type: "integer", nullable: false),
                    remark = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    inactivedate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    createdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    createby = table.Column<string>(type: "text", nullable: true),
                    updatedate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("idx_183395_pk_tm_area_1", x => x.areaid);
                });

            migrationBuilder.CreateTable(
                name: "tm_item",
                columns: table => new
                {
                    itemid = table.Column<int>(type: "integer", nullable: false),
                    itemcode = table.Column<string>(type: "text", nullable: false),
                    itemnamethai = table.Column<string>(type: "text", nullable: false),
                    itemnameeng = table.Column<string>(type: "text", nullable: false),
                    itemgroup = table.Column<int>(type: "integer", nullable: false),
                    partno = table.Column<string>(type: "text", nullable: false),
                    partname = table.Column<string>(type: "text", nullable: false),
                    itemtype = table.Column<int>(type: "integer", nullable: false),
                    itemsubtype = table.Column<int>(type: "integer", nullable: false),
                    stdunit = table.Column<string>(type: "text", nullable: false),
                    stdsnp = table.Column<decimal>(type: "numeric", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    inactivedate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    createdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    createby = table.Column<string>(type: "text", nullable: true),
                    updatedate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("idx_183395_pk_tm_item_1", x => x.itemid);
                });

            migrationBuilder.CreateTable(
                name: "tm_location",
                columns: table => new
                {
                    locationid = table.Column<int>(type: "integer", nullable: false),
                    locationcode = table.Column<string>(type: "text", nullable: false),
                    locationname = table.Column<string>(type: "text", nullable: false),
                    areaid = table.Column<int>(type: "integer", nullable: false),
                    rm = table.Column<bool>(type: "boolean", nullable: false),
                    fg = table.Column<bool>(type: "boolean", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    inactivedate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    createdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    createby = table.Column<string>(type: "text", nullable: true),
                    updatedate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updateby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("idx_183395_pk_tm_location_1", x => x.locationid);
                });

            migrationBuilder.CreateIndex(
                name: "areacode",
                table: "tm_area",
                column: "areacode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "itemcode",
                table: "tm_item",
                column: "itemcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "locationcode",
                table: "tm_location",
                column: "locationcode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tm_area");

            migrationBuilder.DropTable(
                name: "tm_item");

            migrationBuilder.DropTable(
                name: "tm_location");
        }
    }
}
