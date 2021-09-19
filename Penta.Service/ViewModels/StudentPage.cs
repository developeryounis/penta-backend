using Penta.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penta.Service.ViewModels
{
    public class StudentPage
    {
        public int Total { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<StudentViewModel> Students { get; set; }

        public StudentPage(IEnumerable<StudentViewModel> students, int currentPage, int pageSize, int total)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            Total = total;
            Students = students;
        }
    }
}
