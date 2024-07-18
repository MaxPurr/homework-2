using FluentValidation;

namespace Api.Validators
{
    public class GetProductListRequestValidator : AbstractValidator<GetProductListRequest>
    {
        public GetProductListRequestValidator() {
            RuleFor(r => r.Page).GreaterThan(0);
            RuleFor(r => r.Count).GreaterThan(0);
            RuleFor(r => r.Filter).SetValidator(new ProductFilterValidator());
        }
    }
}
