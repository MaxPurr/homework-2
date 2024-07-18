using Domain;
using Domain.Exceptions;
using System.Collections.Concurrent;

namespace DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private ConcurrentDictionary<int, Product> _store;
        private int _sequence;
        public ProductRepository()
        {
            _store = new ConcurrentDictionary<int, Product>();
            _sequence = 0;
        }

        public int Create(ProductDto productDto)
        {
            int id = Interlocked.Increment(ref _sequence);
            var creationDate = DateTimeOffset.Now;
            Product product = productDto.ToProduct(id, creationDate);
            if (_store.TryAdd(id, product))
            {
                return id;
            }
            throw new AbortedException($"Something went wrong while trying to create new product with id = {id}");
        }

        public Product Update(int id, ProductDto productDto)
        {
            Product oldProduct = Get(id);
            Product newProduct = productDto.ToProduct(id, oldProduct.CreationDate);
            if (_store.TryUpdate(id, newProduct, oldProduct))
            {
                return newProduct;
            }
            throw new AbortedException($"Something went wrong while trying to update product with id = {id}");
        }

        public Product Get(int id)
        {
            if (_store.TryGetValue(id, out Product? product))
            {
                return product;
            }
            throw new NotFoundException($"Product with id = {id} not found.");
        }

        public List<Product> GetAll()
        {
            return _store.Values.ToList();
        }
    }
}

