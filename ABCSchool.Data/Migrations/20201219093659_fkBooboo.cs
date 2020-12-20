using Microsoft.EntityFrameworkCore.Migrations;

namespace ABCSchool.Data.Migrations
{
    public partial class fkBooboo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentsSubjects",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentsSubjects");
            migrationBuilder.DropPrimaryKey("StudentId", table: "StudentsSubjects");
            migrationBuilder.DropPrimaryKey("SubjectId", table: "StudentsSubjects");
        }
    }
}
