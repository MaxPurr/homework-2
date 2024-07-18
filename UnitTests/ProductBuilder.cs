using Domain;

namespace UnitTests
{
    public class ProductBuilder
    {
        private int? _id;
        private string? _name;
        private float? _price;
        private float? _weight;
        private ProductType? _productType;
        private DateTimeOffset? _creationDate;
        private int? _warehouseId;

        public static int DefaultId { get; set; } = 1;
        public static string DefaultName { get; set; } = string.Empty;
        public static float DefaultPrice { get; set; } = 0;
        public static float DefaultWeight { get; set; } = 0;
        public static ProductType DefaultProductType { get; set; } = ProductType.Common;
        public static DateTimeOffset DefaultCreationDate { get; set; } = DateTimeOffset.MinValue;
        public static int DefaultWarehouseId { get; set; } = 1;

        public ProductBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ProductBuilder WithName(string name) 
        {
            _name = name;
            return this;
        }

        public ProductBuilder WithPrice(float price)
        {
            _price = price;
            return this;
        }

        public ProductBuilder WithWeight(float weight)
        {
            _weight = weight;
            return this;
        }

        public ProductBuilder WithProductType(ProductType productType)
        {
            _productType = productType;
            return this;
        }

        public ProductBuilder WithCreationDate(DateTimeOffset creationDate) 
        { 
            _creationDate = creationDate;
            return this;
        }

        public ProductBuilder WithWarehouseId(int warehouseId)
        {
            _warehouseId = warehouseId;
            return this;
        }

        public Product Build()
        {
            return new Product()
            {
                Id = _id ?? DefaultId,
                Name = _name ?? DefaultName,
                Price = _price ?? DefaultPrice,
                Weight = _weight ?? DefaultWeight,
                ProductType = _productType ?? DefaultProductType,
                CreationDate = _creationDate ?? DefaultCreationDate,
                WarehouseId = _warehouseId ?? DefaultWarehouseId,
            };
        }
    }
}
