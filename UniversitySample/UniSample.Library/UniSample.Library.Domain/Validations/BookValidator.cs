using FluentValidation;
using UniSample.Library.Domain.Dto;

namespace UniSample.Library.Domain.Validations
{
    public class BookValidator : AbstractValidator<BookDto>
    {
        public BookValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Bitte geben Sie eine gültige Id an");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Bitte geben Sie einen Autor an"); 
            RuleFor(x => x.Isbn).NotEmpty().WithMessage("Bitte geben Sie eine ISBN an");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bitte geben Sie einen Titel an");
        }
    }
}
