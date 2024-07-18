namespace Domain
{
    public static class ProductConversionExtentions
    {
        public static Product ToProduct(this ProductDto productDto, int id, DateTimeOffset creationDate)
        {
            Product product = new Product()
            {
                Id = id,
                Name = productDto.Name,
                Price = productDto.Price,
                Weight = productDto.Weight,
                ProductType = productDto.ProductType,
                CreationDate = creationDate,
                WarehouseId = productDto.WarehouseId,
            };
            return product;
        }

        public static ProductDto ToProductDto(this Product product)
        {
            ProductDto productDto = new ProductDto(product.Name, product.Price, product.Weight, product.ProductType, product.WarehouseId);
            return productDto;
        }
    }
}

