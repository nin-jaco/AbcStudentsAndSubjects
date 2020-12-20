using ABCSchool.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ABCSchool.Data
{
    public class AbcSchoolDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentsSubjects> StudentsSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentsSubjects>()
                .HasKey(x => new { x.StudentId, x.SubjectId });

            modelBuilder.Entity<StudentsSubjects>()
                .HasOne(x => x.Student)
                .WithMany(y => y.StudentsSubjects)
                .HasForeignKey(y => y.SubjectId);

            modelBuilder.Entity<StudentsSubjects>()
                .HasOne(x => x.Subject)
                .WithMany(y => y.StudentsSubjects)
                .HasForeignKey(y => y.StudentId);


            modelBuilder.Entity<Subject>()
                .HasData(
                    new Subject()
                    {
                        Id = 1,
                        Name = "English"
                    },
                    new Subject()
                    {
                        Id = 2,
                        Name = "Maths"
                    },
                    new Subject()
                    {
                        Id = 3,
                        Name = "Spanish"
                    },
                    new Subject()
                    {
                        Id = 4,
                        Name = "Biology"
                    },
                    new Subject()
                    {
                        Id = 5,
                        Name = "Science"
                    },
                    new Subject()
                    {
                        Id = 6,
                        Name = "Programming"
                    },
                    new Subject()
                    {
                        Id = 7,
                        Name = "Law"
                    },
                    new Subject()
                    {
                        Id = 8,
                        Name = "Commerce"
                    },
                    new Subject()
                    {
                        Id = 9,
                        Name = "Physical Education"
                    });




            //base.OnModelCreating(modelBuilder);
        }

        public AbcSchoolDbContext(DbContextOptions<AbcSchoolDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
