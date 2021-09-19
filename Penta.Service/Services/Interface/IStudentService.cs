using Penta.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penta.Service.Services.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentViewModel>> GetAll();

        Task<StudentPage> GetAllByUser(int userId, int page, int pageSize);

        Task<StudentPage> Search(int userId, string search, int page, int pageSize);

        Task<StudentViewModel> Add(StudentViewModel student);

        Task Update(StudentViewModel student);

        Task Delete(int studentId);
    }
}
