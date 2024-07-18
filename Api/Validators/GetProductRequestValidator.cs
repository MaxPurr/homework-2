using FluentValidation;

namespace Api.Validators
{
    public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
    {
        public GetProductRequestValidator() {
            RuleFor(r => r.Id).GreaterThan(0);
        }
    }
}
