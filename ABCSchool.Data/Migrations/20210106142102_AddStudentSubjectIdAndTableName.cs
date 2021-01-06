using Microsoft.EntityFrameworkCore.Migrations;

namespace ABCSchool.Data.Migrations
{
    public partial class AddStudentSubjectIdAndTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentSubjects",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentSubjects");
        }
    }
}
