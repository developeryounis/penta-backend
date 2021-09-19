using Microsoft.EntityFrameworkCore;
using Penta.Service.Models;
using Penta.Service.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penta.Service.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public async Task<Student> Add(Student student)
        {
            using (var db = new PentaEntaties())
            {
                var result =  await db.Students.AddAsync(student);

                await db.SaveChangesAsync();

                return result.Entity;
            }
        }

        public async Task Delete(int studentId)
        {
            using (var db = new PentaEntaties())
            {
                var student = await db.Students.FindAsync(studentId);

                if(student != null)
                {
                    db.Students.Remove(student);

                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            using (var db = new PentaEntaties())
            {
                return await db.Students.ToListAsync();
            }
        }

        public async Task<Tuple<IEnumerable<Student>, int>> GetAllByUser(int userId, int page, int pageSize)
        {
            Tuple<IEnumerable<Student>, int> students = new Tuple<IEnumerable<Student>, int>(new List<Student>(), 0);
            using (var db = new PentaEntaties())
            {
                var user = await db.Users
                    .Include(i => i.Children)
                    .FirstOrDefaultAsync(x => x.Id == userId);

                if (user != null)
                {
                    var children = user.Children.Select(x => x.Id).ToArray();

                    int skip = pageSize * (page - 1);

                    var result = await db.Students
                        .Include(i => i.User)
                        .Where(x => children.Contains(x.EnteredBy) || x.EnteredBy == userId)
                        .Skip(skip)
                        .Take(page)
                        .ToListAsync();

                    var total = await db.Students
                        .Where(x => children.Contains(x.EnteredBy) || x.EnteredBy == userId)
                        .SumAsync(s => s.Id);
                    students = new Tuple<IEnumerable<Student>, int>(result, total);
                }
            }
            return students;
        }

        public async Task<Tuple<IEnumerable<Student>, int>> Search(int userId, string search, int page, int pageSize)
        {
            Tuple<IEnumerable<Student>, int> students = new Tuple<IEnumerable<Student>, int>(new List<Student>(), 0);
            using (var db = new PentaEntaties())
            {
                var user = await db.Users
                    .Include(i => i.Children)
                    .FirstOrDefaultAsync(x => x.Id == userId);

                if (user != null)
                {
                    var children = user.Children.Select(x => x.Id).ToArray();

                    int skip = pageSize * (page - 1);

                    var result = await db.Students
                        .Include(c => c.User)
                        .Where(x => (x.ArabicName.Contains(search) ||
                                     x.EnglishName.Contains(search) ||
                                     x.DateOfBirth.ToString("dd-MM-yyyy") == search) &&
                                    (children.Contains(x.EnteredBy) || x.EnteredBy == userId))
                        .Skip(skip)
                        .Take(page)
                        .ToListAsync();


                    var total = await db.Students
                        .Where(x => x.ArabicName.Contains(search) ||
                                     x.EnglishName.Contains(search) ||
                                     x.DateOfBirth.ToString("dd-MM-yyyy") == search &&
                                    (children.Contains(x.EnteredBy) || x.EnteredBy == userId))
                        .SumAsync(s => s.Id);

                    students = new Tuple<IEnumerable<Student>, int>(result, total);
                }
            }

            return students;
        }

        public async Task Update(Student student)
        {
            using (var db = new PentaEntaties())
            {
                var mainStudent = await db.Students.FindAsync(student.Id);

                if (mainStudent != null)
                {
                    mainStudent.ArabicName = student.ArabicName;
                    mainStudent.EnglishName = student.EnglishName;
                    mainStudent.DateOfBirth = student.DateOfBirth;

                    db.Entry(mainStudent).Property(p => p.EnglishName).IsModified = true;

                    await db.SaveChangesAsync();
                }
            }
            
        }
    }
}
