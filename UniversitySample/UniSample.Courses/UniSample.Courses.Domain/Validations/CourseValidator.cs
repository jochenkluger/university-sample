using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSample.Courses.Domain.Dto;

namespace UniSample.Courses.Domain.Validations
{
    public class CourseValidator : AbstractValidator<CourseDto>
    {
        public CourseValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Bitte geben Sie eine gültige Id an");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bitte geben Sie einen Kursnamen an"); ;
            RuleFor(x => x.ProfName).NotEmpty().WithMessage("Bitte geben Sie einen Professor für den Kurs an");
        }
    }
}
