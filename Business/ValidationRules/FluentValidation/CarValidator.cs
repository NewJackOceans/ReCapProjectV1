using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(p => p.Description).MinimumLength(2).NotEmpty();
            RuleFor(p => p.DailyPrice).NotEmpty().GreaterThan(0);

        }
    }
}