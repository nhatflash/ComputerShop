using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ward = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    district = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    city = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    province = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    postal_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    profile_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    verified_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    updated_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_phone_number",
                table: "Users",
                column: "phone_number",
                unique: true,
                filter: "[phone_number] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
