using FluentValidation;

namespace Api.Validators
{
    public class ProductFilterValidator : AbstractValidator<ProductFilter>
    {
        public ProductFilterValidator() {
            When(r => r.WarehouseId != null, () =>
            {
                RuleFor(r => r.WarehouseId).GreaterThan(0);
            });
        }
    }
}
