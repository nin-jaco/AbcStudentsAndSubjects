using Microsoft.EntityFrameworkCore.Migrations;

namespace ABCSchool.Data.Migrations
{
    public partial class pkOnIdStudentSubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey("PK_StudentsSubjects_Id", "StudentsSubjects", "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
