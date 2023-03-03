using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UniversitySample.Students.Domain.Dto;

namespace UniversitySample.Students.Domain.Validations
{
    public class StudentValidator: AbstractValidator<StudentDetailsDto>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Bitte geben Sie eine gültige Id an");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Bitte geben Sie einen gültigen Benutzernamen an");
        }
    }
}
