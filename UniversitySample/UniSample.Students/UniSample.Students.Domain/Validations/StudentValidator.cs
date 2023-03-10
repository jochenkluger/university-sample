using FluentValidation;
using UniSample.Students.Domain.Dto;

namespace UniSample.Library.Domain.Validations
{
    public class StudentValidator : AbstractValidator<StudentDto>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Bitte geben Sie eine gültige Id an");
            //Natürlich keine umfängliche Prüfung der Email - wäre hier aber sinnvoll
            RuleFor(x => x.Email).NotEmpty().WithMessage("Bitte geben Sie eine Email an"); 
            RuleFor(x => x.Lastname).NotEmpty().WithMessage("Bitte geben Sie einen Nachnamen an");
            RuleFor(x => x.Number).NotEmpty().WithMessage("Bitte geben Sie eine Matrikelnummer an");
        }

    }
}
