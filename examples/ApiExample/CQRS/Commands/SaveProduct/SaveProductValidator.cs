using FluentValidation;

namespace ApiExample.CQRS.Commands.SaveProduct
{
    public class SaveProductValidator : AbstractValidator<SaveProductCommand>
    {
        public SaveProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
