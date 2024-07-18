namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public int Create(ProductDto productDto)
        {
            return _productRepository.Create(productDto);
        }

        public Product Get(int id)
        {
            return _productRepository.Get(id);
        }

        public List<Product> GetList(int page, int count, Func<Product, bool>? filter = null)
        {
            List<Product> products;
            if (filter != null)
            {
                products = _productRepository.GetAll().Where(filter).ToList();
            }
            else
            {
                products = _productRepository.GetAll();
            }
            return products.Skip((page - 1) * count).Take(count).ToList();
        }

        public Product UpdatePrice(int id, float price)
        {
            ProductDto productDto = Get(id).ToProductDto() with { Price = price };
            return _productRepository.Update(id, productDto);
        }
    }
}

