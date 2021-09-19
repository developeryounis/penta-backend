using Penta.Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penta.Service.Repositories.Interface
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAll();

        Task<Tuple<IEnumerable<Student>, int>> GetAllByUser(int userId, int page, int pageSize);

        Task<Tuple<IEnumerable<Student>, int>> Search(int userId, string search, int page, int pageSize);

        Task<Student> Add(Student student);

        Task Update(Student student);

        Task Delete(int studentId);

    }
}
