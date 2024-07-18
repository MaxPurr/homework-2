namespace Domain
{
    public record class ProductDto(string Name, float Price, float Weight, ProductType ProductType, int WarehouseId);
}
