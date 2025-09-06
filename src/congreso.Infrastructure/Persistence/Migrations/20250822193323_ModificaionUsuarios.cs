﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace congreso.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModificaionUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagen",
                table: "Usuarios",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagen",
                table: "Usuarios");
        }
    }
}
