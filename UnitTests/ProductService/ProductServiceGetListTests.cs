using Domain;
using Domain.Services;
using Moq;

namespace UnitTests
{
    public class ProductServiceGetListTests : IClassFixture<ProductListFixrure>
    {
        private readonly IProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        public ProductServiceGetListTests(ProductListFixrure productListFixrure)
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productRepositoryMock
                .Setup(f => f.GetAll())
                .Returns(productListFixrure.ProductList);
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Fact]
        public void GetList_PageExistsWithoutFilter_ReturnsPageOfProductList()
        {
            // Arrange
            int page = 1;
            int count = 3;
            var expectedProducts = new List<Product>()
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
            };

            // Act
            var actualProducts = _productService.GetList(page, count);

            // Assert
            Assert.NotNull(actualProducts);
            Assert.Equal(actualProducts, expectedProducts);
            _productRepositoryMock.Verify(f => f.GetAll(), Times.Once);
        }

        [Fact]
        public void GetList_PageNotExistsWithoutFilter_ReturnsEmptyProductList()
        {
            // Arrange
            int page = 2;
            int count = 5;
            int expectedCount = 0;

            // Act
            var actualProducts = _productService.GetList(page, count);

            // Assert
            Assert.NotNull(actualProducts);
            Assert.Equal(actualProducts.Count, expectedCount);
            _productRepositoryMock.Verify(f => f.GetAll(), Times.Once);
        }

        [Fact]
        public void GetList_FilterByProductType_ReturnsFilteredProductList()
        {
            // Arrange
            int page = 1;
            int count = 5;

            ProductType productType = ProductType.Common;
            var filter = new ProductFilterBuilder()
                .FilterByProductType(productType)
                .GetFilter();

            var expectedProducts = new List<Product>()
            {
                new ProductBuilder()
                .WithId(3)
                .WithProductType(ProductType.Common)
                .WithCreationDate(new DateTimeOffset(2024, 3, 19, 20, 29, 11, TimeSpan.Zero))
                .WithWarehouseId(4000)
                .Build(),
                new ProductBuilder()
                .WithId(5)
                .WithProductType(ProductType.Common)
                .WithCreationDate(new DateTimeOffset(2023, 8, 22, 14, 9, 6, TimeSpan.Zero))
                .WithWarehouseId(1000)
                .Build(),
            };

            // Act
            var actualProducts = _productService.GetList(page, count, filter);

            // Assert
            Assert.NotNull(actualProducts);
            Assert.Equal(actualProducts, expectedProducts);
            _productRepositoryMock.Verify(f => f.GetAll(), Times.Once);
        }

        [Fact]
        public void GetList_FilterByCreationDate_ReturnsFilteredProductList()
        {
            // Arrange
            int page = 1;
            int count = 5;
            DateTimeOffset from = new DateTimeOffset(2023, 8, 21, 14, 5, 6, TimeSpan.Zero);
            DateTimeOffset to = new DateTimeOffset(2024, 1, 14, 19, 34, 15, TimeSpan.Zero);

            var filter = new ProductFilterBuilder()
                .FilterFromDate(from)
                .FilterToDate(to)
                .GetFilter();

            var expectedProducts = new List<Product>()
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
                .WithId(5)
                .WithProductType(ProductType.Common)
                .WithCreationDate(new DateTimeOffset(2023, 8, 22, 14, 9, 6, TimeSpan.Zero))
                .WithWarehouseId(1000)
                .Build(),
            };

            // Act
            var actualProducts = _productService.GetList(page, count, filter);

            // Assert
            Assert.NotNull(actualProducts);
            Assert.Equal(actualProducts, expectedProducts);
            _productRepositoryMock.Verify(f => f.GetAll(), Times.Once);
        }

        [Fact]
        public void GetList_FilterByWarehouseId_ReturnsFilteredProductList()
        {
            // Arrange
            int page = 1;
            int count = 5;
            int warehouseId = 2000;

            var filter = new ProductFilterBuilder()
                .FilterByWarehouseId(warehouseId)
                .GetFilter();

            var expectedProducts = new List<Product>()
            {
                new ProductBuilder()
                .WithId(1)
                .WithProductType(ProductType.Food)
                .WithCreationDate(new DateTimeOffset(2023, 11, 20, 15, 45, 9, TimeSpan.Zero))
                .WithWarehouseId(2000)
                .Build(),
                new ProductBuilder()
                .WithId(4)
                .WithProductType(ProductType.HouseholdChemicals)
                .WithCreationDate(new DateTimeOffset(2023, 8, 10, 23, 1, 40, TimeSpan.Zero))
                .WithWarehouseId(2000)
                .Build(),
            };

            // Act
            var actualProducts = _productService.GetList(page, count, filter);

            // Assert
            Assert.NotNull(actualProducts);
            Assert.Equal(actualProducts, expectedProducts);
            _productRepositoryMock.Verify(f => f.GetAll(), Times.Once);
        }

        [Fact]
        public void GetList_NoProductMatchesFilter_ReturnsEmptyProductList()
        {
            int page = 1;
            int count = 5;
            ProductType productType = ProductType.Food;
            int warehouseId = 4000;

            var filter = new ProductFilterBuilder()
                .FilterByProductType(productType)
                .FilterByWarehouseId(warehouseId)
                .GetFilter();

            int expectedCount = 0;

            // Act
            var actualProducts = _productService.GetList(page, count, filter);

            // Assert
            Assert.NotNull(actualProducts);
            Assert.Equal(actualProducts.Count, expectedCount);
            _productRepositoryMock.Verify(f => f.GetAll(), Times.Once);
        }
    }
}
