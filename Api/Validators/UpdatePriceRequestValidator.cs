using FluentValidation;

namespace Api.Validators
{
    public class UpdatePriceRequestValidator : AbstractValidator<UpdatePriceRequest>
    {
        public UpdatePriceRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
            RuleFor(r => r.Price).GreaterThan(0);
        }
    }
}
