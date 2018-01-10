using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalBoss.Migrations
{
    public partial class UpdateHeroStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bosses_heroes_HeroId1",
                table: "bosses");

            migrationBuilder.DropIndex(
                name: "IX_bosses_HeroId1",
                table: "bosses");

            migrationBuilder.DropColumn(
                name: "HeroId1",
                table: "bosses");

            migrationBuilder.AlterColumn<int>(
                name: "HeroId",
                table: "bosses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_bosses_HeroId",
                table: "bosses",
                column: "HeroId");

            migrationBuilder.AddForeignKey(
                name: "FK_bosses_heroes_HeroId",
                table: "bosses",
                column: "HeroId",
                principalTable: "heroes",
                principalColumn: "HeroId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bosses_heroes_HeroId",
                table: "bosses");

            migrationBuilder.DropIndex(
                name: "IX_bosses_HeroId",
                table: "bosses");

            migrationBuilder.AddColumn<int>(
                name: "HeroId1",
                table: "bosses",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HeroId",
                table: "bosses",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_bosses_HeroId1",
                table: "bosses",
                column: "HeroId1");

            migrationBuilder.AddForeignKey(
                name: "FK_bosses_heroes_HeroId1",
                table: "bosses",
                column: "HeroId1",
                principalTable: "heroes",
                principalColumn: "HeroId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
