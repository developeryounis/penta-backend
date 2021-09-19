using AutoMapper;
using Penta.Service.Models;
using Penta.Service.Repositories.Interface;
using Penta.Service.Services.Interface;
using Penta.Service.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Penta.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        public async Task<StudentViewModel> Add(StudentViewModel student)
        {
            var mappedStudent = _mapper.Map<Student>(student);
            var result =  await _studentRepository.Add(mappedStudent);

            return _mapper.Map<StudentViewModel>(result);
        }

        public async Task Delete(int studentId)
        {
            await _studentRepository.Delete(studentId);
        }

        public async Task<IEnumerable<StudentViewModel>> GetAll()
        {
            var result = await _studentRepository.GetAll();

            return _mapper.Map<IEnumerable<StudentViewModel>>(result);
        }

        public async Task<StudentPage> GetAllByUser(int userId, int page, int pageSize)
        {
            var result = await _studentRepository.GetAllByUser(userId, page, pageSize);

            var mappedResult = _mapper.Map<IEnumerable<StudentViewModel>>(result.Item1);

            return new StudentPage(mappedResult, page, pageSize, result.Item2);
        }

        public async Task<StudentPage> Search(int userId, string search, int page, int pageSize)
        {
            var result = await _studentRepository.Search(userId, search, page, pageSize);

            var mappedResult = _mapper.Map<IEnumerable<StudentViewModel>>(result.Item1);

            return new StudentPage(mappedResult, page, pageSize, result.Item2);
        }

        public async Task Update(StudentViewModel student)
        {
            var mappedStudent = _mapper.Map<Student>(student);

            await _studentRepository.Update(mappedStudent);
        }
    }
}
