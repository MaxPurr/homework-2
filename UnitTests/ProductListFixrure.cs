using Domain;

namespace UnitTests
{
    public class ProductListFixrure
    {
        public ProductListFixrure()
        {
            ProductList = new List<Product>()
            {
                new ProductBuilder()
                .WithId(1)
                .WithProductType(ProductType.Food)
                .WithCreationDate(new DateTimeOffset(2023, 11, 20, 15, 45, 9, TimeSpan.Zero))
                .WithWarehouseId(2000)
                .Build(),
                new ProductBuilder()
                .WithId(2)
                .WithProductType(ProductType.Technological)
                .WithCreationDate(new DateTimeOffset(2024, 1, 14, 19, 34, 15, TimeSpan.Zero))
                .WithWarehouseId(4000)
                .Build(),
                new ProductBuilder()
                .WithId(3)
                .WithProductType(ProductType.Common)
                .WithCreationDate(new DateTimeOffset(2024, 3, 19, 20, 29, 11, TimeSpan.Zero))
                .WithWarehouseId(4000)
                .Build(),
                new ProductBuilder()
                .WithId(4)
                .WithProductType(ProductType.HouseholdChemicals)
                .WithCreationDate(new DateTimeOffset(2023, 8, 10, 23, 1, 40, TimeSpan.Zero))
                .WithWarehouseId(2000)
                .Build(),
                new ProductBuilder()
                .WithId(5)
                .WithProductType(ProductType.Common)
                .WithCreationDate(new DateTimeOffset(2023, 8, 22, 14, 9, 6, TimeSpan.Zero))
                .WithWarehouseId(1000)
                .Build(),
            };
        }
        public List<Product> ProductList { get; private init;}
    }
}
