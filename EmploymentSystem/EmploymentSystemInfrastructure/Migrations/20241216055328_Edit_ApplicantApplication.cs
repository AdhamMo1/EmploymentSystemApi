using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmploymentSystemInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Edit_ApplicantApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ApplicantsApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ApplicantsApplications");
        }
    }
}
