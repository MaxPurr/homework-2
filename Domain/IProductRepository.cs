namespace Domain
{
    public interface IProductRepository
    {
        int Create(ProductDto productDto);
        Product Update(int id, ProductDto productDto);
        Product Get(int id);
        List<Product> GetAll();
    }
}
