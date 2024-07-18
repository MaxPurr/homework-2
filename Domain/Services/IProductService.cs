namespace Domain.Services
{
    public interface IProductService
    {
        int Create(ProductDto productDto);
        Product Get(int id);
        List<Product> GetList(int page, int count, Func<Product, bool>? filter = null);
        Product UpdatePrice(int id, float price);
    }
}
