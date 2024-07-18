using Domain;
using FluentValidation;

namespace Api.Validators
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(r => r.Name).NotNull().MinimumLength(3);
            RuleFor(r => r.Price).GreaterThan(0);
            RuleFor(r => r.Weight).GreaterThan(0);
            RuleFor(r => r.ProductType).NotEqual(ProductType.Unspecified);
            RuleFor(r => r.WarehouseId).GreaterThan(0);
        }
    }
}