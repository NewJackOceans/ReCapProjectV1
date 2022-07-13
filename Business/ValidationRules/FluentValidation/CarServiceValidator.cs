using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    class CarServiceValidator : AbstractValidator<CarService>
    {
        public CarServiceValidator()
        {
            RuleFor(cr => cr.Km).NotEmpty();
            RuleFor(cr => cr.ServiceType).NotEmpty();


        }

    }
}
