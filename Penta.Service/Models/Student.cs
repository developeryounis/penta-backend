using System;

namespace Penta.Service.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string EnglishName { get; set; }

        public string ArabicName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int EnteredBy { get; set; }

        public DateTime Entered { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
