using FluentValidation;
using System;

namespace Penta.Service.ViewModels.Validation
{
    public class StudentValidator : AbstractValidator<StudentViewModel>
    {
        public StudentValidator()
        {
            RuleFor(x => x.ArabicName)
                .NotNull()
                .Matches(@"(^[\u0621-\u064A]{3,16})([ ]{0,1})([\u0621-\u064A]{3,16})?([ ]{0,1})?([\u0621-\u064A]{3,16})?([ ]{0,1})?([\u0621-\u064A]{3,16})");

            RuleFor(x => x.EnglishName)
                .NotNull()
                .Matches(@"(^(^[A-Za-z]{3,16})([ ]{0,1})([A-Za-z]{3,16})?([ ]{0,1})?([A-Za-z]{3,16})?([ ]{0,1})?([A-Za-z]{3,16})");

            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .LessThan(DateTime.Now.AddYears(-3));
        }
    }
}
