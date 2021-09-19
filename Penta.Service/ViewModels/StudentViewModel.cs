using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Penta.Service.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        public string EnglishName { get; set; }

        public string ArabicName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int EnteredBy { get; set; }

        public DateTime Entered { get; set; }

        public UserViewModel User { get; set; }
    }

    public class UserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
