using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalBoss.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "heroes",
                columns: table => new
                {
                    HeroId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(nullable: true),
                    Weapon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_heroes", x => x.HeroId);
                });

            migrationBuilder.CreateTable(
                name: "bosses",
                columns: table => new
                {
                    BossId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    HeroId = table.Column<string>(nullable: true),
                    HeroId1 = table.Column<int>(nullable: true),
                    ImmediateThreat = table.Column<bool>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Species = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bosses", x => x.BossId);
                    table.ForeignKey(
                        name: "FK_bosses_heroes_HeroId1",
                        column: x => x.HeroId1,
                        principalTable: "heroes",
                        principalColumn: "HeroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bosses_HeroId1",
                table: "bosses",
                column: "HeroId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bosses");

            migrationBuilder.DropTable(
                name: "heroes");
        }
    }
}
