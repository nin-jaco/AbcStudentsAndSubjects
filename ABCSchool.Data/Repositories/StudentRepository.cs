using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCSchool.Data.Interfaces;
using ABCSchool.Data.Repositories.Base;
using ABCSchool.Domain.Entities;
using ABCSchool.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ABCSchool.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly AbcSchoolDbContext _context;

        public StudentRepository(AbcSchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Student> Get(int id)
        {
            return await _context.Students.Include(p => p.StudentSubjects).FirstOrDefaultAsync(p => p.Id ==id);
        }

        public override async Task<List<Student>> GetAll()
        {
            try
            {
                return await _context.Set<Student>().Include(p => p.StudentSubjects).ToListAsync();
            }
            catch (Exception ex)
            {
                //todo: log exception
                throw;
            }
            
        }

        public override async Task<Student> Update(Student student)
        {
            try
            {
                /*Context.Entry<Student>(student).State = EntityState.Modified;
                    Context.Entry<Student>(student).Collection("StudentSubjects").CurrentValue = student.StudentSubjects;
                    Context.Entry<Student>(student).Collection("StudentSubjects").IsModified = true;
                    //context.Update<TEntity>(entity);
                    await Context.SaveChangesAsync();
                    return student;*/

                var existingStudent = await Context.Students.Include(p => p.StudentSubjects).FirstOrDefaultAsync(t => t.Id == student.Id);
                existingStudent.Email = student.Email;
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.Mobile = student.Mobile;

                if (existingStudent.StudentSubjects == null)
                {
                    existingStudent.StudentSubjects = new List<StudentSubject>();
                }

                var oldIds = existingStudent.StudentSubjects?.Select(p => p.SubjectId).ToList() ?? new List<int>();
                var newIds = student.StudentSubjects?.Select(p => p.SubjectId).ToList() ?? new List<int>();

                var missingIds = newIds.Where(p => oldIds.All(p2 => p2 != p));
                var deleteIds = oldIds.Where(p => newIds.All(p2 => p2 != p));


                foreach (var id in missingIds)
                {
                    await _context.StudentSubjects.AddAsync(new StudentSubject() { SubjectId = id, StudentId = student.Id });
                }

                foreach (var deleteId in deleteIds)
                {
                    var deleteItem =
                        await _context.StudentSubjects.FirstOrDefaultAsync(p => p.SubjectId == deleteId && p.StudentId == student.Id);
                    _context.StudentSubjects.Remove(deleteItem);
                }


                var result = await _context.SaveChangesAsync();
                return await Get(student.Id);
            }
            catch (Exception ex)
            {
                //todo: handle error
                throw;
            }


        }
    }
}
