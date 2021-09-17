using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RPGApi.Migrations
{
    public partial class DeletePageNumberFromMessageWtf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "messagesAmount",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "totalPages",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "pageNumber",
                table: "MessagesForum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "messagesAmount",
                table: "Topics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "totalPages",
                table: "Topics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pageNumber",
                table: "MessagesForum",
                nullable: false,
                defaultValue: 0);
        }
    }
}
